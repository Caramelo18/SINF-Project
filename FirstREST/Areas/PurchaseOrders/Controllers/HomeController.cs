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
            return View();
        }

    }
}
