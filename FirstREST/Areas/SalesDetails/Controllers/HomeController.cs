using FirstREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace FirstREST.Areas.SalesDetails.Controllers
{
    public class HomeController : Controller
    {
        DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /SalesDetails/Home/

        public ActionResult Index()
        {
            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string> { "Home", "Detalhes de Vendas" };

            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath(@"~\Content\saft.xml"));

            Saft.SaftIntegration.ParseSalesInvoices(doc, db);

            return View();
        }

    }
}
