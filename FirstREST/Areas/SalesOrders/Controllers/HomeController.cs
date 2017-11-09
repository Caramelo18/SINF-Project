using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.SalesOrders.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /SalesOrders/Home/

        public ActionResult Index()
        {
            List<Lib_Primavera.Model.DocVenda> vendas = Lib_Primavera.PriIntegration.Encomendas_List();
            return View(vendas);
        }

        public ActionResult Get(string id)
        {
            Lib_Primavera.Model.DocVenda docVenda = Lib_Primavera.PriIntegration.Encomenda_Get(id);

            return View(docVenda);
        }

    }
}
