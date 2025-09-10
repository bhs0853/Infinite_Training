using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Repository
{
    public class AccountRepositoryImpl : IAccountRepository
    {
        private BankingDBEntities db = null;
        public AccountRepositoryImpl()
        {
            db = new BankingDBEntities();
        }

        public string CreateAccount(int service_reference_number, int id)
        {
            string message = "";
            try
            {
                var user = db.RegisterAccounts.Find(service_reference_number);
                db.Sp_CreateAccount(service_reference_number, id);
                var cust = db.Customers.Where(c => c.Aadhar == user.Aadhar).FirstOrDefault();
                var acc = db.Accounts.Where(a => a.Customer_Id == cust.Customer_Id).FirstOrDefault();
                if (user.Opt_Net_Banking.Value)
                {
                    db.Sp_CreateInternetBanking(acc.Account_Number, BCrypt.Net.BCrypt.HashPassword(cust.Date_Of_Birth.ToString().Substring(0,10)), BCrypt.Net.BCrypt.HashPassword(cust.Date_Of_Birth.ToString()).Substring(0,10));
                }
                else if (user.Opt_Debit_Card.Value)
                {
                    db.Sp_CreateDebitCard(acc.Account_Number);
                }
                message = "Account created: Approval Successful \n Your account Number: " + acc.Account_Number;
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

        public (Account acc, string message) GetAccountDetails(int accountNo)
        {
            Account account = null;
            String msg = "";
            try
            {
                account = db.Accounts.Find(accountNo);
                msg = "Fetched Details Successfully";
            }
            catch (SqlException e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            return (account, msg);
        }

        public string RegisterAccount(RegisterAccount account)
        {
            string result = "";
            try
            {
                int? serviceReferenceNumber = db.Sp_Register_Account(account.Title, account.First_Name, account.Middle_Name, account.Last_Name, account.Father_Name, account.Mobile_Number, account.Email_Id, account.Aadhar, account.Gender, account.Date_Of_Birth, account.Residential_Address, account.Permanent_Address, account.Occupation_Type, account.Source_Of_Income, account.Gross_Annual_Income, account.Opt_Debit_Card, account.Opt_Net_Banking).FirstOrDefault();
                result = "Account creation request raised with Service Reference Number: " + serviceReferenceNumber;
            }
            catch (SqlException e)
            {
                result = e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                result = e.InnerException?.Message ?? e.Message;
            }
            return result;
        }
    }
}