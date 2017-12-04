using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.AccountsReceivable.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /AccountsReceivable/Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
