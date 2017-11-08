using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.Suppliers.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Suppliers/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
