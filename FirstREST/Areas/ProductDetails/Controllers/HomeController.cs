using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using FirstREST.Models;
using System.Collections.Generic;

namespace FirstREST.Areas.ProductDetails.Controllers
{
    public class HomeController : Controller
    {

        DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /ProductDetails/Home/

        public ActionResult Index()
        {

            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string> { "Home", "Inventory" };

            //TODO: change this to only be called once

            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath(@"~\Content\saft.xml"));

            Saft.SaftIntegration.ParseProducts(doc, db);
            Saft.SaftIntegration.addProductsFromPrimaveraToDb(db);

            var artigos = from m in db.Product
                          select m;

            ViewBag.artigos = artigos;

            return View();
        }

        public ActionResult GetProduct(string id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string> { "Home", "Inventory", id};

            var artigo = from m in db.Product
                         where m.ProductCode == id
                         select m;

            ViewBag.artigo = artigo.ToList();

            var encomendas = Lib_Primavera.PriIntegration.Encomendas_Produto_List(id, null);
            ViewBag.encomendas = encomendas;

                 
            //Lib_Primavera.PriIntegration.Encomendas_Produto_List(id, null);

            return View();
        }
    }
}
