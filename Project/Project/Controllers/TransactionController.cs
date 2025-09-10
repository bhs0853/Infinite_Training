using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project.Dto;
using Project.Models;
using Project.Repository;
using Project.Services;

namespace Project.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionService transactionService;

        public TransactionController()
        {
            transactionService = new TransactionService(new TransactionRepositoryImpl());
        }

        public ActionResult Transfer(int? toAccount)
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin", "InternetBanking");
            }

            var model = new TransactionDto();

            if (toAccount.HasValue)
            {
                model.To_Account = toAccount.Value;
            }

            ViewBag.Account = Session["Account"];
            return View(model);

        }

        [HttpPost]
        public ActionResult Transfer(TransactionDto txn)
        {
            var (transaction, message) = transactionService.MakeTransaction((int)Session["AccountNumber"], txn);
            if (transaction == null)
            {
                ViewBag.Message = message;
                return View();
            }
            ViewBag.Account = Session["Account"];
            ViewBag.Message = message;
            return View("transferresult", transaction);
        }
        public ActionResult Statement()
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin", "InternetBanking");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Statement(DateTime? from, DateTime? to)
        {
            int accountNumber = (int)Session["AccountNumber"];
            (List<Transaction_Details> transactions, string message) = transactionService.GetStatement(accountNumber, (from ?? Convert.ToDateTime("2000-01-01")), (to ?? Convert.ToDateTime(System.DateTime.Now)));

            ViewBag.Message = message;
            System.Diagnostics.Debug.WriteLine(transactions.Count);
            return View(transactions.OrderByDescending(t => t.Transaction_Date).ToList());
        }



        public ActionResult AddPayee()
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin", "InternetBanking");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddPayee(Payee payee)
        {
            payee.From_Account = (int)Session["AccountNumber"];
            var message = transactionService.AddPayee(payee);
            if (!message.StartsWith("Payee added successfully"))
            {
                ViewBag.Message = message;
                return View();
            }
            ViewBag.Message = message;
            ViewBag.Account = Session["Account"];
            return RedirectToAction("GetAllPayees", "Transaction");
        }

        public ActionResult GetPayee(int id)
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin", "InternetBanking");
            }
            var (payee, message) = transactionService.GetPayee(id);
            ViewBag.Account = Session["Account"];
            return View(payee);
        }


        public ActionResult DeletePayee(int toAccount)
        {
            int fromAccount = (int)Session["AccountNumber"];

            transactionService.DeletePayee(fromAccount, toAccount);

            return RedirectToAction("GetAllPayees", "Transaction");
        }


        public ActionResult GetAllPayees()
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin", "InternetBanking");
            }
            var (payees, message) = transactionService.GetAllPayees((int)Session["AccountNumber"]);
            ViewBag.Account = Session["Account"];
            return View(payees);
        }
    }
}