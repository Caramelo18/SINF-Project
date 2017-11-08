using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.SalesOrders.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SalesOrders/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
