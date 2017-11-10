using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace FirstREST.Areas.ProductDetails.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /ProductDetails/Home/

        public ActionResult Index()
        {

            List<Models.Artigo> listaArtigos = Saft.SaftIntegration.ParseArtigos();

            ViewBag.artigos = listaArtigos;

            return View();
        }

    }
}
