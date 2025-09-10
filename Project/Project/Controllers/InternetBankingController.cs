using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.Repository;
using Project.Services;

namespace Project.Controllers
{
    public class InternetBankingController : Controller
    {
        private readonly InternetBankingService internetBankingService;
        private readonly AccountService accountService;
        private readonly AdminService adminService;
        private readonly TransactionService transactionService;
        private readonly BankingDBEntities db;
        private OtpService otpService;
        private static string tempEmail;

        public InternetBankingController()
        {
            internetBankingService = new InternetBankingService(new InternetBankingRepositoryImpl());
            accountService = new AccountService(new AccountRepositoryImpl());
            adminService = new AdminService();
            transactionService = new TransactionService(new TransactionRepositoryImpl());
            otpService = new OtpService(new OtpRepositoryImpl());
            db = new BankingDBEntities();
        }
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            ViewBag.Step = "Email";
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Message = "Email is required.";
                ViewBag.Step = "Email";
                return View();
            }
            if (db.Customers.Where(c => c.Email_Id == email).Count() <= 0)
            {
                ViewBag.Message = "Email not Found.";
                ViewBag.Step = "Email";
                return View();
            }

            tempEmail = email;
            otpService.GenerateOtp(email);

            ViewBag.Message = $"OTP sent to {email}.";
            ViewBag.Step = "OTP";
            return View();
        }

        [HttpPost]
        public ActionResult VerifyOtp(string otp)
        {
            if (string.IsNullOrEmpty(tempEmail))
            {
                ViewBag.Message = "Session expired. Please start again.";
                ViewBag.Step = "Email";
                return View("ForgotPassword");
            }

            if (otpService.VerifyOtp(tempEmail, Convert.ToInt32(otp)))
            {
                ViewBag.Step = "Reset";
                return View("ForgotPassword");
            }

            ViewBag.Message = "Invalid OTP. Please try again.";
            ViewBag.Step = "OTP";
            return View("ForgotPassword");
        }

        [HttpPost]
        public ActionResult ResetPassword(string newPassword)
        {
            if (string.IsNullOrEmpty(tempEmail))
            {
                ViewBag.Message = "Session expired. Please start again.";
                ViewBag.Step = "Email";
                return View("ForgotPassword");
            }

            string msg = internetBankingService.ResetLoginPassword(tempEmail, newPassword);
            ViewBag.Message = msg;
            ViewBag.Step = "Email";
            return View("ForgotPassword");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(int accountNo, string loginPassword, string transactionPassword)
        {
            if (string.IsNullOrEmpty(loginPassword) || string.IsNullOrEmpty(transactionPassword))
            {
                ViewBag.Message = "Passwords Cannot be empty";
            }
            string msg = internetBankingService.CreateInternetBanking(accountNo, loginPassword, transactionPassword);
            ViewBag.Message = msg;
            return View();
        }

        public ActionResult ChangeLoginPassword()
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin", "InternetBanking");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangeLoginPassword(string oldPassword, string newPassword)
        {
            string msg = internetBankingService.ChangeLoginPassword((int) Session["AccountNumber"], oldPassword, newPassword);
            ViewBag.Message = msg;
            ViewBag.Account = Session["Account"];
            return View();
        }

        public ActionResult ChangeTransactionPassword()
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin", "InternetBanking");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangeTransactionPassword(string oldPassword, string newPassword)
        {
            string msg = internetBankingService.ChangeTransactionPassword((int)Session["AccountNumber"], oldPassword, newPassword);
            ViewBag.Account = Session["Account"];
            ViewBag.Message = msg;
            return View();
        }

        public ActionResult CreateDebitCard(int accountNo)
        {
            string msg = internetBankingService.CreateDebitCard(accountNo);
            ViewBag.Message = msg;
            ViewBag.Account = Session["Account"];
            return View();
        }

        public ActionResult CustomerLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CustomerLogin(string email, string password)
        {
            var (accountNumber, message, lastLogin) = internetBankingService.CustomerLogin(email, password);

            if (accountNumber > 0)
            {
                Session["AccountNumber"] = accountNumber;
                Session["LastLogin"] = lastLogin;
                ViewBag.Message = "";
                return RedirectToAction("Dashboard", "InternetBanking");
            }

            ViewBag.Message = message;
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Dashboard()
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin");
            }
            (Account acc, string message) = accountService.GetAccountDetails((int)Session["AccountNumber"]);
            (List<Transaction_Details> transactions, string message1) = transactionService.GetStatement(acc.Account_Number, Convert.ToDateTime("2000-01-01"), Convert.ToDateTime(System.DateTime.Now));
            ViewBag.AccountNumber = acc.Account_Number;
            ViewBag.AccountMessage = message;
            ViewBag.LastLogin = Session["LastLogin"];
            Session["Account"] = acc;
            ViewBag.TransactionMessage = message1;
            return View((acc, transactions.OrderByDescending(t => t.Transaction_Date).ToList()));
        }

        [HttpGet]
        public ActionResult SendSupportMessage()
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult SendSupportMessage(SupportMessage supportMessage)
        {
            if (ModelState.IsValid)
            {
                supportMessage.UserEmail = db.Accounts.Find((int)Session["AccountNumber"]).Customer.Email_Id;
                var result = adminService.RaiseSupportMessage(supportMessage);

                ViewBag.Status = result.message;

                return View("SendSupportMessage", supportMessage);
            }

            ViewBag.Status = "Invalid input. Please check your details.";
            return View("SendSupportMessage", supportMessage);

        }

        public ActionResult CustomerLogout()
        {
            Session.Clear();
            return RedirectToAction("CustomerLogin");
        }
    }
}