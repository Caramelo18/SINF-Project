using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Models
{
    public class Cliente {
        //
        // GET: /Cliente/

        public string CustomerID {
            get;
            set;
        }

        public string AccountID {
            get;
            set;
        }

        public string CustomerTaxID
        {
            get;
            set;
        }

        public string CompanyName
        {
            get;
            set;
        }

        public string Telephone
        {
            get;
            set;
        }
        public string Fax
        {
            get;
            set;
        }
        public string Website
        {
            get;
            set;
        }
        public string SelfBillingIndicator
        {
            get;
            set;
        }

        public BillingAddress billingaddress
        {
            get;
            set;
        }

        public class BillingAddress
        {
            public string AddressDetail
            {
                get;
                set;
            }
            public string City
            {
                get;
                set;
            }
            public string PostalCode
            {
                get;
                set;
            }
            public string Country
            {
                get;
                set;
            }
        }
    }
}
