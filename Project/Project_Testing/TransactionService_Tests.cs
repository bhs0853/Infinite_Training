using System;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Moq;
using Project.Models;
using Project.Services;
using Project.Repository;
using Project.Dto;

namespace Project_Testing
{
    class TransactionService_Tests
    {
        Mock<ITransactionRepository> mock;
        TransactionService service;

        [SetUp]
        public void Setup()
        {
            mock = new Mock<ITransactionRepository>();
            service = new TransactionService(mock.Object);
        }

        [Test]
        public void AddPayee_Returns_Error_When_Self_Payee()
        {
            Payee payee = new Payee { From_Account = 10000, To_Account = 10000 };
            string result = service.AddPayee(payee);
            ClassicAssert.AreEqual("Cannot add self as payee", result);
        }

        [Test]
        public void AddPayee_Calls_Repository_When_Valid()
        {
            Payee payee = new Payee { From_Account = 10000, To_Account = 10001 };
            mock.Setup(m => m.AddPayee(payee)).Returns("Added");
            string result = service.AddPayee(payee);
            ClassicAssert.AreEqual("Added", result);
        }

        [Test]
        public void MakeTransaction_Returns_Error_When_Invalid_Amount()
        {
            TransactionDto txn = new TransactionDto { Amount = 0 };
            var (txnDetails, msg) = service.MakeTransaction(10001, txn);
            ClassicAssert.AreEqual("Invalid amount", msg);
        }

        [Test]
        public void GetStatement_Returns_List()
        {
            var list = new List<Transaction_Details> { new Transaction_Details { Transaction_Id = 1, Amount = 1000, From_Account = 10000, To_Account = 10001 } };
            mock.Setup(m => m.GetStatement(It.IsAny<int>(), It.IsAny <DateTime>(), It.IsAny <DateTime>())).Returns((list, "OK"));
            var (txns, msg) = service.GetStatement(1, DateTime.MinValue, DateTime.MaxValue);
            ClassicAssert.IsNotNull(txns);
            ClassicAssert.AreEqual(1, txns.Count);
            ClassicAssert.AreEqual("OK", msg);
        }
    }
}
