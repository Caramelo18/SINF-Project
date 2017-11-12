using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using FirstREST.Models;

namespace FirstREST.Areas.ProductDetails.Controllers
{
    public class HomeController : Controller
    {

        DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /ProductDetails/Home/

        public ActionResult Index()
        {
            //TODO: change this to only be called once

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\Desktop\SINF\saft.xml");

            Saft.SaftIntegration.ParseProducts(doc, db);

            //Saft.SaftIntegration.ParseArtigos();
            //Saft.SaftIntegration.addProductsFromPrimaveraToDb();

            var artigos = from m in db.Product
                          select m;

            ViewBag.artigos = artigos;

            return View();
        }

    }
}
