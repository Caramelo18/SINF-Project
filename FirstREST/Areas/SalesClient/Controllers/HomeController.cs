using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using FirstREST.Models;

namespace FirstREST.Areas.SalesClient.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SalesClient/Home/



        public ActionResult Index()
        {

            DatabaseEntities db = new DatabaseEntities();
            
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath(@"~\Content\saft.xml"));

            // Gets the sales Invoice from the saf-t file
            Saft.SaftIntegration.ParseSalesInvoice(doc, db);

            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string> { "Home", "Vendas ao Cliente" };

            return View();
        }

    }
}
