using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.SalesDetails.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SalesDetails/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
