using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Project.Services;
using Project.Models;
using Project.Repository;

namespace Banking_Project.Controllers
{
    public class SupportController : Controller
    {
        private AdminService adminService;

        private BankingDBEntities db;
        public SupportController()
        {
            adminService = new AdminService();
            db = new BankingDBEntities();
        }


        [HttpGet]
        public ActionResult ViewMessages()
        {
            if (Session["AdminId"] == null)
            {
      
                return RedirectToAction("AccessDenied", "AdminApproval");
            }
            var messages = db.SupportMessages.Where(m => m.Status == "Pending").ToList();
            return View(messages);

        }


        public ActionResult ResolvedMessages()
        {
            if (Session["AdminId"] == null)
            {
                
                return RedirectToAction("AccessDenied", "AdminApproval");
            }
            var messages = db.SupportMessages.Where(m => m.Status != "Pending").ToList();
            return View("ViewMessages", messages); 
        }

        [HttpGet]
        public ActionResult ReplyToMessage(int id)
        {
            if (Session["AdminId"] == null)
            {
                
                return RedirectToAction("AccessDenied", "AdminApproval");
            }
            var message = db.SupportMessages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }

            return View(message);
        }

        [HttpPost]
        public ActionResult ReplyToMessage(int id, string reply)
        {
            if (Session["AdminId"] == null)
            {
                
                return RedirectToAction("AccessDenied", "AdminApproval");
            }
            var message = db.SupportMessages.Find(id);
            if (message != null)
            {
                message.AdminReply = reply;
                message.RepliedAt = DateTime.Now;
                message.Status = "Solved";
                db.SaveChanges();

               
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("admin@gmail.com"));
                email.To.Add(MailboxAddress.Parse(message.UserEmail));
                email.Subject = "Reply to your support request";
                email.Body = new TextPart("plain") { Text = reply };

                using (var smtp = new SmtpClient())
                {
                    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                    smtp.Authenticate("infinitebankingprj@gmail.com", "csbz cuof eheu tenv");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
            }

            return RedirectToAction("ViewMessages");
        }
    }
}
    
