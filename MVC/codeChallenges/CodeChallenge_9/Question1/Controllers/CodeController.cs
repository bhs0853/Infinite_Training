using Question1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeChallenge9.Controllers
{
    public class CodeController : Controller
    {
        practiceEntities db = new practiceEntities();
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomersInGermany()
        {
            var customerList = (from c in db.Customers
                                where c.Country == "Germany"
                                select c).ToList();
            return View(customerList);
        }

        public ActionResult CustomerByOrderId()
        {
            var customer = (from c in db.Customers
                            where c.CustomerID == (from o in db.Orders
                                                   where o.OrderID == 10248
                                                   select o.CustomerID).FirstOrDefault()
                            select c);
            return View(customer);
        }
    }
}