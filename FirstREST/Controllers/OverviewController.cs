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
    }
    public class OverviewController : ApiController
    {
        public Overview Get()
        {
            Overview ov = new Overview
            {
                TotalRevenue = 1,
                TotalSales = 2,
                accountsPayable = 3,
                accountsReceivable = 4,
                numSales = 5,
                numPurchases = 6
            };
            return ov;
        }
        
    }
}
