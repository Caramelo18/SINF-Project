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
            return View();
        }

    }
}
