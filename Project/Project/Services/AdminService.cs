using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Data.SqlClient;
using Project.Models;
using Project.Repository;


namespace Project.Services
{
    public class AdminService
    {
        private BankingDBEntities db;
        private IAccountRepository accountRepository;

        public AdminService()
        {
            db = new BankingDBEntities();
            accountRepository = new AccountRepositoryImpl();
        }
        public (int admin_id, string message) AdminLogin(string email, string password)
        {

            int admin_id = -1;
            string message = "";
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                message = "please enter your username and password";
            }

            try
            {
                var result = db.fn_AdminLogin(email, password).FirstOrDefault();

                if (result != null && result.id.HasValue && result.id.Value != -1)
                {
                    admin_id = result.id.Value;
                    message = "Admin login successful";
                }


                else
                {
                    message = "!!! Invalid email or password";
                }
            }
            catch (SqlException e)
            {
                message = e.Message;
            }
            return (admin_id, message);

        }
        public (List<RegisterAccount> registerAccounts, string message) PendingApprovals()
        {
            List<RegisterAccount> registerAccounts = new List<RegisterAccount>();
            string message = "";
            try
            {
                registerAccounts = db.RegisterAccounts.ToList();
                message = "Accounts fetched successfully";
            }
            catch (SqlException e)
            {
                message = e.Message;
            }
            return (registerAccounts, message);
        }

        public RegisterAccount ViewDetails(int id)
        {
            RegisterAccount account = db.RegisterAccounts.Find(id);
            return account;
        }

        public string Approve(int service_reference_number, int admin_id)
        {
            string message = "";
            try
            {
                var user = db.RegisterAccounts.Find(service_reference_number);
        
                string remarks = accountRepository.CreateAccount(service_reference_number, admin_id);
                if (user.Opt_Net_Banking.Value)
                {
                    remarks += $"\nYour Internet Banking Credentials \n Username: {user.Email_Id} \n Password: {user.Date_Of_Birth.ToString().Substring(0,10)} \n Debit card generated successfully"; ;
                }
                else if (user.Opt_Debit_Card.Value)
                {
                    remarks += "\nDebit card generated successfully";
                }
                SendApprovalEmail(user.Email_Id, user.First_Name, remarks);
                message = service_reference_number + " Approved successfully";
            }
            catch (SqlException e)
            {
                message = e.Message;
            }
            return message;
        }
        private void SendApprovalEmail(string email, string username, string remarks)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bank Admin", "admin@yourbank.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Account Approved";

            message.Body = new TextPart("plain")
            {
                Text = $"Dear {username},\n\nYour account has been approved.\n{remarks}\n\nPlease log in and reset your password.\n\nRegards,\nBank Admin"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("tarakakillada@gmail.com", "baev kroz pvpf trck");
                smtp.Send(message);
                smtp.Disconnect(true);
            }
        }

        public string RejectAccount(int serviceReferenceNumber, int id, string remarks)
        {
            string message = "";
            try
            {
                var user = db.RegisterAccounts.Find(serviceReferenceNumber);
                if (user == null)
                {
                    return "User not found.";
                }

                db.Sp_RejectAccount(serviceReferenceNumber, id, remarks);

                SendRejectionEmail(user.Email_Id, user.First_Name, remarks);

                message = serviceReferenceNumber + " Rejected successfully";
            }
            catch (SqlException e)
            {
                message = e.Message;
            }
            return message;
        }
        private void SendRejectionEmail(string email, string username, string remarks)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bank Admin", "admin@yourbank.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Account Rejected";

            message.Body = new TextPart("plain")
            {
                Text = $"Dear {username},\n\nWe regret to inform you that your account registration has been rejected.\nReason: {remarks}\n\nIf you believe this was a mistake, please contact support.\n\nRegards,\nBank Admin"
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("infinitebankingprj@gmail.com", "csbz cuof eheu tenv");
                smtp.Send(message);
                smtp.Disconnect(true);
            }
        }


        public (int supportId, string message) RaiseSupportMessage(SupportMessage supportMessage)
        {
            string message = "";
            int supportId = 0;

            try
            {
                    db.Database.ExecuteSqlCommand(
                    "INSERT INTO SupportMessages (UserEmail, Subject, Message, SentAt, Status) VALUES (@UserEmail, @Subject, @Message, GETDATE(), 'Pending')",
                    new SqlParameter("@UserEmail", supportMessage.UserEmail),
                    new SqlParameter("@Subject", supportMessage.Subject),
                    new SqlParameter("@Message", supportMessage.Message)
                );

                message = "Support message submitted successfully.";
            }
            catch (SqlException ex)
            {
                message = "Error: " + ex.Message;
            }

            return (supportId, message);
        }
        public List<Transaction_Details> GetFilteredTransactions(string filter, DateTime? fromDate, DateTime? toDate)
        {
            DateTime today = DateTime.Now;
            DateTime startDate = today;

            switch (filter)
            {
                case "Today":
                    startDate = DateTime.Today;
                    today = DateTime.Now;
                    break;

                case "Week":
                    startDate = today.AddDays(-7);
                    break;
                case "Month":
                    startDate = today.AddMonths(-1);
                    break;
                case "Custom":
                    if (fromDate.HasValue && toDate.HasValue)
                    {
                        startDate = fromDate.Value;
                        today = toDate.Value.Date.AddDays(1).AddTicks(-1);
                    }
                    break;
            }

            return db.Transaction_Details
                     .Where(t => t.Transaction_Date >= startDate && t.Transaction_Date <= today)
                     .ToList();
        }
    }
}