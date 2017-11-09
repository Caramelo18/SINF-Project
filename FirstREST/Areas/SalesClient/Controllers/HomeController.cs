using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.SalesClient.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SalesClient/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
