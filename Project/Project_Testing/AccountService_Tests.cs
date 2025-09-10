using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using Project.Models;
using Project.Services;
using Project.Repository;

namespace Project_Testing
{
    [TestFixture]
    public class AccountService_Tests
    {
       Mock<IAccountRepository> mock_repo;
       AccountService service;

        [SetUp]
        public void Setup()
        {
            mock_repo = new Mock<IAccountRepository>();
            service = new AccountService(mock_repo.Object);
        }
        [Test]
        public void RegisterAccount_Return_Error_When_Aadhar_IsEmpty()
        {
            string result = service.RegisterAccount(new RegisterAccount { Aadhar = "" });
            ClassicAssert.AreEqual("Aadhar is required", result);
        }

        [Test]
        public void RegisterAccount_Calls_Repo_When_Aadhar_IsValid()
        {
            mock_repo.Setup(m => m.RegisterAccount(It.IsAny<RegisterAccount>())).Returns("Success");
            RegisterAccount account = new RegisterAccount { Aadhar = "123456789012" };
            string result = service.RegisterAccount(account);
            ClassicAssert.AreEqual("Success", result);
        }
        [Test]
        public void CreateAccount_Calls_Repo()
        {
            mock_repo.Setup(m => m.CreateAccount(10001, 1)).Returns("Account Created");
            var result = service.CreateAccount(10001, 1);
            ClassicAssert.AreEqual("Account Created", result);
        }

        [Test]
        public void GetAccountDetails_Returns_Account()
        {
            var acc = new Account { Account_Number = 10001 };
            mock_repo.Setup(m => m.GetAccountDetails(10001)).Returns((acc, "Fetched"));
            var (account, message) = service.GetAccountDetails(10001);
            ClassicAssert.AreEqual(acc, account);
            ClassicAssert.AreEqual("Fetched", message);
        }
    }
}
