using System;
using System.Linq;
using System.Net.Mail;
using Project.Models;

namespace Project.Repository
{
    public class OtpRepositoryImpl : IOtpRepository
    {
        private BankingDBEntities db = null;

        public OtpRepositoryImpl()
        {
            db = new BankingDBEntities();
        }

        public int GenerateOtp(string email)
        {
            int otp = new Random().Next(100000, 999999);
            db.OtpRequests.Add(new OtpRequest
            {
                Email_Id = email,
                Otp = otp,
                created_time = DateTime.Now,
                is_used = false
            });
            db.SaveChanges();
            SendEmail(email, "Bank Account Password Reset", $"To reset your password, please use the following OTP: {otp}");
            return otp;
        }

        public bool VerifyOtp(string email, int otp)
        {
            var req = db.OtpRequests.Where(o => o.Email_Id == email && o.Otp == otp && o.is_used == false).OrderByDescending(o => o.created_time).FirstOrDefault();
            if (req != null)
            {
                req.is_used = true;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public void SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage("infinitebankingprj@gmail.com", to);
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.Credentials = new System.Net.NetworkCredential("infinitebankingprj@gmail.com", "csbz cuof eheu tenv");
            client.EnableSsl = true;
            mail.Subject = subject;
            mail.Body = body;
            client.Send(mail);
        }
    }
}