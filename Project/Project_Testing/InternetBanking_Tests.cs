using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using Project.Models;
using Project.Services;
using Project.Repository;

namespace Project_Testing
{
    class InternetBanking_Tests
    {
        Mock<IInternetBankingRepository> mock;
        InternetBankingService service;

        [SetUp]
        public void Setup()
        {
            mock = new Mock<IInternetBankingRepository>();
            service = new InternetBankingService(mock.Object);
        }
        [Test]
        public void ChangeLoginPassword_Returns_Error_When_Passwords_Same()
        {
            string result = service.ChangeLoginPassword(10000, "login", "login");
            ClassicAssert.AreEqual("New password cannot be same as old password", result);
        }

        [Test]
        public void ChangeLoginPassword_Calls_Repo_When_Valid()
        {
            mock.Setup(m => m.ChangeLoginPassword(10000, "old", "new")).Returns("Changed");
            string result = service.ChangeLoginPassword(10000, "old", "new");
            ClassicAssert.AreEqual("Changed", result);
        }
        [Test]
        public void ChangeTransactionPassword_Returns_Error_When_Passwords_Same()
        {
            string result = service.ChangeTransactionPassword(10000, "transaction", "transaction");
            ClassicAssert.AreEqual("New password cannot be same as old password", result);
        }

        [Test]
        public void ChangeTransactionPassword_Calls_Repo_When_Valid()
        {
            mock.Setup(m => m.ChangeTransactionPassword(10000, "old", "new")).Returns("Changed");
            string result = service.ChangeTransactionPassword(10000, "old", "new");
            ClassicAssert.AreEqual("Changed", result);
        }

        [Test]
        public void CustomerLogin_Calls_Repository()
        {
            mock.Setup(r => r.CustomerLogin("testmail@bank.com", "login")).Returns((10000, "OK", "2025-09-09"));
            var (accountNumber, message, lastLogin) = service.CustomerLogin("testmail@bank.com", "login");
            ClassicAssert.AreEqual(10000, accountNumber);
            ClassicAssert.AreEqual("OK", message);
        }

        [Test]
        public void CreateInternetBanking_Calls_Repository()
        {
            mock.Setup(m => m.CreateInternetBanking(10000, "login", "transaction")).Returns("Done");
            string result = service.CreateInternetBanking(10000, "login", "transaction");
            ClassicAssert.AreEqual("Done", result);
        }
    }
}
