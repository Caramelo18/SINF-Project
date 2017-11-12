using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using FirstREST.Models;

namespace FirstREST.Areas.Clients.Controllers
{
    public class HomeController : Controller
    {

        DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /Clients/Home/

        public ActionResult Index()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\Desktop\SINF\saft.xml");

            /*Saft.SaftIntegration.ParseCustomers(doc, db);

            var clients = from m in db.Customer
                          select m;

            ViewBag.clients = clients;*/
            
            return View();
        }

    }
}
