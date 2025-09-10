using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Dto;
using Project.Models;
using Project.Repository;
using Project.Services;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        private AccountService accountService;

        public AccountController()
        {
            accountService = new AccountService(new AccountRepositoryImpl());
        }

        public ActionResult Register()
        {
            return View(new RegisterAccountDto());
        }

        [HttpPost]
        public ActionResult Register(RegisterAccountDto account)
        {
            if (!ModelState.IsValid)
            {
                return View(account);
            }

            string result = accountService.RegisterAccount(RegisterAccount.FromDto(account));

            if (result.StartsWith("Account creation"))
            {
                TempData["Message"] = result;
                return View("Success");
            }

            ViewBag.Message = result;
            return View(account);
        }

        public ActionResult Details(int accountNo)
        {
            if (Session["AccountNumber"] == null)
            {
                return RedirectToAction("CustomerLogin", "InternetBanking");
            }

            var (acc, message) = accountService.GetAccountDetails(accountNo);
            if (acc == null)
                ViewBag.Message = message;

            return View(acc);
        }
    }
}