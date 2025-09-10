using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project.Dto;
using Project.Models;
using Project.Repository;

namespace Project.Services
{
    public class TransactionService
    {
        private ITransactionRepository _transactionRepo;
        private BankingDBEntities db = new BankingDBEntities();

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepo = transactionRepository;
            
        }

        public string AddPayee(Payee payee)
        {
            if (payee.From_Account == payee.To_Account)
                return "Cannot add self as payee";

            return _transactionRepo.AddPayee(payee);
        }

        public (Payee payee, string message) GetPayee(int id)
        {
            return _transactionRepo.GetPayee(id);
        }

        public void DeletePayee(int from_account, int to_account)
        {
            Payee payee = db.Payees.Where(p => p.From_Account == from_account && p.To_Account == to_account).FirstOrDefault();
            System.Diagnostics.Debug.WriteLine(from_account + " t: " + to_account+" "+ payee);
            if (payee != null)
            {
                db.Payees.Remove(payee);
                db.SaveChanges();
            }
        }
        public (List<Payee> payees, string message) GetAllPayees(int accountNumber)
        {
            return _transactionRepo.GetAllPayees(accountNumber);
        }

        public (List<Transaction_Details> transactions, string message) GetStatement(int accountNumber, DateTime fromDate, DateTime toDate)
        {
            return _transactionRepo.GetStatement(accountNumber, fromDate, toDate);
        }

        public (Transaction_Details txnDetails, string message) MakeTransaction(int accountNumber, TransactionDto transaction)
        {
            if (transaction.Amount <= 0)
                return (null, "Invalid amount");

            return _transactionRepo.MakeTransaction(accountNumber, transaction);
        }
    }
}