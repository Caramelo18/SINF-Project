using FirstREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace FirstREST.Controllers
{
    public class StateController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        [HttpPut]
        public HttpResponseMessage Update()
        {
            // PUT
            // api/state/update
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(System.Web.Hosting.HostingEnvironment.MapPath(@"~\Content\saft.xml"));

                Integration.SaftIntegration.ParseCustomers(doc, db);
                Integration.DbIntegration.addClientsFromPrimaveraToDb(db);

                Integration.SaftIntegration.ParseProducts(doc, db);
                Integration.DbIntegration.addProductsFromPrimaveraToDb(db);

                //TODO: fix this
                //Integration.SaftIntegration.ParseSalesInvoices(doc, db);

                Integration.DbIntegration.addDocCompraToDb(db);
                Integration.DbIntegration.addDocVendaToDb(db);

                Integration.DbIntegration.addSupplierToDb(db);
                Integration.DbIntegration.addAccountsReceivableToDb(db);
                Integration.DbIntegration.addAccountsPayableToDb(db);

                System.Diagnostics.Debug.WriteLine("acabei");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }


            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
