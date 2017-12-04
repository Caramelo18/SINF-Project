using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using FirstREST.Models;

namespace FirstREST.Areas.Clients.Controllers
{
    public class CustomerViewModel
    {
        public Models.Customer Client { get; set; }
        public string Address { get; set; }
    }

    public class HomeController : Controller
    {

        DatabaseEntities db = new DatabaseEntities();
        //
        // GET: /Clients/Home/

        public ActionResult Index()
        {
            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string> { "Home", "Clientes" };
           
            var clients = (from c in db.Customer
                          join a in db.BillingAddress
                          on c.BillingAddressID equals a.ID
                          select new CustomerViewModel{Client = c, Address = a.AddressDetail}).ToList();

            ViewBag.clients = clients;

            return View();
        }

    }
}
