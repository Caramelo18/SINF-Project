using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.ProductDetails.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /ProductDetails/Home/

        public ActionResult Index()
        {
            //Artigos
            List<Lib_Primavera.Model.Artigo> artigos = Lib_Primavera.PriIntegration.ListaArtigos();
            ViewBag.artigos = artigos;

            return View();
        }

    }
}
