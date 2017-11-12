using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.PurchaseOrders.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /PurchaseOrders/Home/

        public ActionResult Index()
        {

            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string> { "Home", "Ordens de Compra" };

            return View();
        }

    }
}
