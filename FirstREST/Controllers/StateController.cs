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

                Saft.SaftIntegration.ParseCustomers(doc, db);
                Saft.SaftIntegration.addClientsFromPrimaveraToDb(db);

                Saft.SaftIntegration.ParseProducts(doc, db);
                Saft.SaftIntegration.addProductsFromPrimaveraToDb(db);

                Saft.SaftIntegration.ParseSalesInvoice(doc, db);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }


            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
