using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.SalesProduct.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SalesProduct/Home/

        public ActionResult Index()
        {
            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string> { "Home", "Produtos" };

            return View();
        }

    }
}
