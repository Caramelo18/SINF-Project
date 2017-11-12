using System;
using System.Linq;
using System.Web.Mvc;
using FirstREST.Models;

namespace FirstREST.Areas.ProductDetails.Controllers
{
    public class HomeController : Controller
    {

        DbEntities db = new DbEntities();

        //
        // GET: /ProductDetails/Home/

        public ActionResult Index()
        {
            //TODO: change this to only be called once

            //Saft.SaftIntegration.ParseArtigos();
            //Saft.SaftIntegration.addProductsFromPrimaveraToDb();

            var artigos = from m in db.Artigo
                          select m;

            ViewBag.artigos = artigos;

            return View();
        }

    }
}
