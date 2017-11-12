using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.Clients.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Clients/Home/

        public ActionResult Index()
        {
            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string> { "Home", "Clientes" };

            var clientes = Lib_Primavera.PriIntegration.ListaClientes();
            return View(clientes);
        }

    }
}
