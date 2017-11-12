using System;
using System.Linq;
using System.Web.Mvc;
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

            Saft.SaftIntegration.ParseArtigos();
            Saft.SaftIntegration.addProductsFromPrimaveraToDb();

            var artigos = from m in db.Product
                          select m;

            ViewBag.artigos = artigos;

            return View();
        }

    }
}
