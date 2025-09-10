using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Models;
using Project.Services;
using Project.Repository;

namespace Project.Controllers
{
    public class ReportsController : Controller
    {
        private readonly BankingDBEntities db = new BankingDBEntities();
        private AdminService adminService;

        public ReportsController()
        {
            adminService = new AdminService();
        }
        public ActionResult Index(string filter, DateTime? fromDate, DateTime? toDate)
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("AccessDenied", "AdminApproval");
            }
            var transactions = adminService.GetFilteredTransactions(filter, fromDate, toDate);
            return View(transactions);
        }
    }
}