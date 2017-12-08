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
            int numCompras = (from x in db.DocCompra
                              select x).Count();

            List<Models.Product> artigos = (from p in db.Product
                                            select p).Take(5).ToList();

            double aPayable = Math.Round(db.AccountPayable.Sum(x => x.ValorPendente), 1);

            double aReceivable = Math.Round(db.AccountReceivable.Sum(x => x.ValorPendente), 1);

            double totalSales = (from s in db.SalesInvoices
                                 select s.TotalCredit).AsQueryable().First();

            int numVendas = (from s in db.Invoice
                                 select s).Count();



            Overview ov = new Overview
            {
                TotalRevenue = 1,
                TotalSales = totalSales,
                accountsPayable = 0,
                accountsReceivable = aReceivable,
                numSales = numVendas,
                numPurchases = numCompras,
                articles = artigos
            };
            return ov;
        }

    }
}
