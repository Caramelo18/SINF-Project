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

            var artigos = new List<Models.Product>();
            var articlesTemp = (from p in db.Product
                                select p).ToList();

            for (int i = 0; i < 5; i++)
            {
                artigos.Add(articlesTemp.ElementAt(i));
            }

            double aPayable = Math.Round(db.AccountPayable.Sum(x => x.ValorPendente), 1);

            double aReceivable = Math.Round(db.AccountReceivable.Sum(x => x.ValorPendente), 1);

            Overview ov = new Overview
            {
                TotalRevenue = 1,
                TotalSales = 2,
                accountsPayable = aPayable,
                accountsReceivable = aReceivable,
                numSales = 5,
                numPurchases = numCompras,
                articles = artigos
            };
            return ov;
        }
        
    }
}
