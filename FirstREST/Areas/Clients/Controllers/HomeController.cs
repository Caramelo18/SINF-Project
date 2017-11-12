using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

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

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\Desktop\SINF\saft.xml");

            /*Saft.SaftIntegration.ParseCustomers(doc, db);

            var clients = from m in db.Customer
                          select m;

            ViewBag.clients = clients;*/
            
            return View(clientes);
        }

    }
}
