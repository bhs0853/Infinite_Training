using Project.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project.Repository
{
    public class InternetBankingRepositoryImpl : IInternetBankingRepository
    {
        private BankingDBEntities db = null;
        public InternetBankingRepositoryImpl()
        {
            db = new BankingDBEntities();
        }
        public string ChangeLoginPassword(int accountNumber, string oldPassword, string newPassword)
        {
            try
            {
                var ibd = db.Internet_Banking_Details.FirstOrDefault(i => i.Account_Number == accountNumber);
                if (ibd == null)
                {
                    return "Account not found";
                }
                if (!BCrypt.Net.BCrypt.Verify(oldPassword, ibd.login_password))
                {
                    return "Invalid old password";
                }
                string hashedNew = BCrypt.Net.BCrypt.HashPassword(newPassword);
                db.Sp_ChangeLoginPassword(accountNumber, ibd.login_password, hashedNew);
                return "Login Password changed successfully";
            }
            catch (SqlException e)
            {
                return e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                return e.InnerException?.Message ?? e.Message;
            }
        }

        public string ChangeTransactionPassword(int accountNumber, string oldPassword, string newPassword)
        {
            try
            {
                var ibd = db.Internet_Banking_Details.FirstOrDefault(i => i.Account_Number == accountNumber);
                if (ibd == null)
                {
                    return "Account not found";
                }
                if (!BCrypt.Net.BCrypt.Verify(oldPassword, ibd.transaction_password))
                {
                    return "Invalid old transaction password";
                }
                string hashedNew = BCrypt.Net.BCrypt.HashPassword(newPassword);
                db.Sp_ChangeTransactionPassword(accountNumber, ibd.transaction_password, hashedNew);
                return "Transaction Password changed successfully";
            }
            catch (SqlException e)
            {
                return e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                return e.InnerException?.Message ?? e.Message;
            }
        }

        public string CreateDebitCard(int accountNumber)
        {
            string message = "";
            try
            {
                db.Sp_CreateDebitCard(accountNumber);
                message = "Debit Card Generated successfully";
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


        public string CreateInternetBanking(int accountNumber, string loginPassword, string transactionPassword)
        {
            string message = "";
            try
            {
                string hashedLoginPassword = BCrypt.Net.BCrypt.HashPassword(loginPassword);
                string hashedTransactionPassword = BCrypt.Net.BCrypt.HashPassword(transactionPassword);

                db.Sp_CreateInternetBanking(accountNumber, hashedLoginPassword, hashedTransactionPassword);
                message = "Created Internet Banking successfully";
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

        public (int accountNumber, string message, string lastLogin) CustomerLogin(string email, string password)
        {
            int accNo = -1;
            string msg = "";
            string lastLogin = "";
            try
            {
                var ibd = db.Internet_Banking_Details.FirstOrDefault(i => i.Email_Id == email);
                if (ibd == null)
                {
                    return (-1, "User not found", "");
                }
                if (ibd.is_locked == true)
                {
                    if (ibd.locked_time != null && (DateTime.Now - ibd.locked_time.Value).TotalHours >= 3)
                    {
                        ibd.is_locked = false;
                        ibd.failed_attempts = 0;
                        db.SaveChanges();
                        return (-1, "Account is unlocked. You can Login again.", "");
                    }
                    return (-1, "Account is locked. Wait 3 hours to unlock.", "");
                }
                bool isValid = BCrypt.Net.BCrypt.Verify(password, ibd.login_password);
                if (isValid)
                {
                    ibd.failed_attempts = 0;
                    db.SaveChanges();
                    accNo = ibd.Account_Number.Value;
                    msg = "Customer Login Successful";
                    lastLogin = ibd.last_login.Value.ToString();
                    db.Database.ExecuteSqlCommand("update Internet_Banking_Details set last_login = getdate() where Account_Number = @AccNo", new SqlParameter("@AccNo", ibd.Account_Number));
                }
                else
                {
                    ibd.failed_attempts += 1;
                    if (ibd.failed_attempts >= 3)
                    {
                        ibd.is_locked = true;
                        ibd.locked_time = DateTime.Now;
                    }
                    db.SaveChanges();
                    msg = "Invalid credentials";
                }
            }
            catch (SqlException e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            return (accNo, msg, lastLogin);
        }
        public (int admin_id, string message) AdminLogin(string email, string password)
        {
            int admin_id = -1;
            string msg = "";
            try
            {
                admin_id = db.fn_AdminLogin(email, password).FirstOrDefault().id.Value;
                msg = "Admin Login Successful";
            }
            catch (SqlException e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            catch (Exception e)
            {
                msg = e.InnerException?.Message ?? e.Message;
            }
            return (admin_id, msg);
        }
        public string ResetLoginPassword(string email, string newPassword)
        {
            using (var db = new BankingDBEntities())
            {
                var user = db.Internet_Banking_Details.FirstOrDefault(u => u.Email_Id == email);
                if (user != null)
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                    user.login_password = hashedPassword;
                    db.SaveChanges();
                    return "Password reset successful!";
                }
                return "Email not found.";
            }
        }
    }
}