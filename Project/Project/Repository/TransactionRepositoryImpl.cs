using Project.Dto;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Repository
{
    public class TransactionRepositoryImpl : ITransactionRepository
    {
        private BankingDBEntities db = null;
        public TransactionRepositoryImpl()
        {
            db = new BankingDBEntities();
        }
        public string AddPayee(Payee payee)
        {
            string message = "";
            try
            {
                db.Sp_AddPayee(payee.Beneficiary_Name, payee.From_Account, payee.To_Account, payee.Nickname);
                message = "Payee added successfully";
            }
            catch (SqlException e)
            {
                message = e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                message = e.InnerException?.Message ?? e.Message;
            }
            return message;
        }

        public (List<Payee> payees, string message) GetAllPayees(int accountNumber)
        {
            return (db.Payees.Where(p => p.From_Account == accountNumber).ToList(), "Payees Fetched successfully");

        }

        public (Payee payee, string message) GetPayee(int id)
        {
            Payee payee = null;
            String msg = "";
            try
            {
                var result = db.fn_GetPayee(id).FirstOrDefault();
                if (result == null)
                {
                    return (null, "Payee not found");
                }
                payee = new Payee()
                {
                    Payee_Id = result.Payee_Id.Value,
                    To_Account = result.Account_Number.Value,
                    Beneficiary_Name = result.Beneficiary_Name,
                    Nickname = result.Nickname
                };
                msg = "Fetched Payee Details Successfully";
            }
            catch (SqlException e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            return (payee, msg);
        }

        public (List<Transaction_Details> transactions, string message) GetStatement(int accountNumber, DateTime fromDate, DateTime toDate)
        {
            List<Transaction_Details> txnDetails = new List<Transaction_Details>();
            string msg = "";
            try
            {
                var result = db.fn_GetStatement(accountNumber, fromDate, toDate);
                foreach (var r in result)
                {
                    txnDetails.Add(new Transaction_Details()
                    {
                        Transaction_Id = r.Transaction_Id.Value,
                        From_Account = r.From_Account,
                        To_Account = r.To_Account,
                        Transaction_Mode = r.Transaction_Mode,
                        Transaction_Type = r.Transaction_Type,
                        Amount = r.Amount,
                        Balance = r.Balance,
                        Transaction_Date = r.Transaction_Date,
                        Remarks = r.Remarks
                    });
                }
                msg = "Statement generated successfully";
            }
            catch (SqlException e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            return (txnDetails, msg);
        }

        public (Transaction_Details txnDetails, string message) MakeTransaction(int accountNumber, TransactionDto transaction)
        {
            Transaction_Details txn = null;
            string msg = "";
            try
            {
                var ibd = db.Internet_Banking_Details.FirstOrDefault(i => i.Account_Number == accountNumber);
                if (ibd == null)
                {
                    return (null, "From Account not found");
                }

                if (!BCrypt.Net.BCrypt.Verify(transaction.Transaction_Password, ibd.transaction_password))
                {
                    return (null, "Invalid Transaction Password");
                }

                int txnNumber = db.Sp_MakeTransaction(accountNumber, transaction.To_Account, transaction.Transaction_Mode, transaction.Amount, System.DateTime.Now, transaction.Remarks, ibd.transaction_password).FirstOrDefault().Value;
                System.Diagnostics.Debug.WriteLine("tran: " + txnNumber);
                txn = db.Transaction_Details.Find(txnNumber);
                msg = "Transaction Successful";
            }
            catch (SqlException e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            return (txn, msg);
        }
    }
}