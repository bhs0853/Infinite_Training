using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;
using Railway_Reservation;

namespace Testing_Proj
{
    [TestFixture]
    public class Testing
    {
        private TextReader originalConsoleIn;

        [SetUp]
        public void setUp()
        {
            originalConsoleIn = Console.In;
        }

        [TearDown]
        public void tearDown()
        {
            Console.SetIn(originalConsoleIn);
        }

        [Test]
        [TestCase("admin@org.com", "admin", 101, "admin")]
        [TestCase("testuser@org.com", "testuser", 102, "user")]
        [TestCase("testuser@org.com", "user", -1, "not_found")]
        public void TestSignIn(string email, string password, int expectedUser, string expectedRole)
        {
            StringReader sr = new StringReader(email + "\n" + password);
            Console.SetIn(sr);

            var UserAuth = new UserAuth();

            var output = UserAuth.signIn();
            ClassicAssert.AreEqual(output, (expectedUser, expectedRole));
        }
    }
}
