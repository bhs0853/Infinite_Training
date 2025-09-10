using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using Project.Models;
using Project.Services;
using Project.Repository;

namespace Project_Testing
{
    class OtpService_Tests
    {
        Mock<IOtpRepository> mock;
        OtpService service;

        [SetUp]
        public void Setup()
        {
            mock = new Mock<IOtpRepository>();
            service = new OtpService(mock.Object);
        }

        [Test]
        public void GenerateOtp_Returns_MinusOne_When_Email_Empty()
        {
            int result = service.GenerateOtp("");
            ClassicAssert.AreEqual(-1, result);
        }

        [Test]
        public void GenerateOtp_Calls_Repository_When_Email_Valid()
        {
            mock.Setup(m => m.GenerateOtp("testmail@bank.com")).Returns(123456);
            int result = service.GenerateOtp("testmail@bank.com");
            ClassicAssert.AreEqual(123456, result);
        }

        [Test]
        public void VerifyOtp_Should_Call_Repo()
        {
            mock.Setup(m => m.VerifyOtp("testmail@bank.com", 123456)).Returns(true);
            bool result = service.VerifyOtp("testmail@bank.com", 123456);
            ClassicAssert.IsTrue(result);
        }
    }
}
