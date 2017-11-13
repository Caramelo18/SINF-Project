using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // The last element of the breadcrumbs list is the current page
            ViewBag.breadcrumbs = new List<string>{"Home"};


            var accountsPayable = Lib_Primavera.PriIntegration.Accounts_Payable_List();
            accountsPayable = accountsPayable.ToList();

            var accountsReceivable = Lib_Primavera.PriIntegration.Accounts_Receivable_List();
            accountsReceivable = accountsReceivable.ToList();

            var articlesTemp = Lib_Primavera.PriIntegration.ListaArtigos();
            var articles = new List<Lib_Primavera.Model.Artigo>();

            for (int i = 0; i < 5; i++)
            {
                articles.Add(articlesTemp.ElementAt(i));
            }

            ViewBag.accountsPayable = (int) accountsPayable.ElementAt(0).TotalMerc;
            ViewBag.accountsReceivable = (int) accountsReceivable.ElementAt(0).TotalMerc;
            ViewBag.numPurchases = Lib_Primavera.PriIntegration.NumCompras();
            ViewBag.numSales = Lib_Primavera.PriIntegration.NumVendas();
            ViewBag.totalSales = (int) Lib_Primavera.PriIntegration.TotalVendas();
            ViewBag.articles = articles;

            return View();
        }
    }
}
