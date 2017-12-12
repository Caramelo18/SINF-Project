using FirstREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class Overview
    {
        public double TotalRevenue
        {
            get;
            set;
        }

        public double TotalSales
        {
            get;
            set;
        }

        public double accountsPayable
        {
            get;
            set;
        }

        public double accountsReceivable
        {
            get;
            set;
        }

        public int numSales
        {
            get;
            set;
        }

        public double numPurchases
        {
            get;
            set;
        }

        public List<Models.Product> articles
        {
            get;
            set;
        }
    }
    public class OverviewController : ApiController
    {

        DatabaseEntities db = new DatabaseEntities();

        public Overview Get()
        {
            double totalRevenue = 0;
            double totalSales = 0;
            double aPayable = 0;
            double aReceivable = 0;
            List<Models.Product> artigos = new List<Models.Product>();

            var aux = (from p in db.Product select p);

            if (aux.Count() != 0)
                artigos = aux.Take(5).ToList();

            int auxInt = db.AccountPayable.Count();
            if (auxInt != 0)
                aPayable = Math.Round(db.AccountPayable.Sum(x => x.ValorPendente), 1);

            auxInt = db.AccountReceivable.Count();
            if (auxInt != 0)
                aReceivable = Math.Round(db.AccountReceivable.Sum(x => x.ValorPendente), 1);

            var auxSales = (from s in db.SalesInvoices
                       select s.TotalCredit);
            if (auxSales.Count() != 0)
                totalSales = auxSales.AsQueryable().First();


            int numCompras = (from x in db.DocCompra
                          select x).Count();

            int numVendas = (from s in db.DocVenda
                                 select s).Count();
            
            Overview ov = new Overview
            {
                TotalRevenue = totalRevenue,
                TotalSales = totalSales,
                accountsPayable = aPayable,
                accountsReceivable = aReceivable,
                numSales = numVendas,
                numPurchases = numCompras,
                articles = artigos
            };
            return ov;
        }

    }
}
