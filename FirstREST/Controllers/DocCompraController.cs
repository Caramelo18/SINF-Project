using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using FirstREST.Models;

namespace FirstREST.Controllers
{
    public class DocCompraGeral
    {
        public Models.DocCompra doc { get; set; }
        public int totalQuantity { get; set; }
    }
    public class DocCompraController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        /**
         * Fatura - 
         * */

        public List<Models.DocCompra> Get()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string from = allUrlKeyValues.LastOrDefault(x => x.Key == "from").Value;
            string to = allUrlKeyValues.LastOrDefault(x => x.Key == "to").Value;

            DateTime fromDate = Convert.ToDateTime(from);
            DateTime toDate = Convert.ToDateTime(to);

            var docs = new List<Models.DocCompra>();

            if (from != null && to != null){
                docs = (from p in db.DocCompra
                            where p.Data >= fromDate && p.Data <= toDate
                            select p).ToList();
            }
            else if (from != null)
            {
                docs = (from p in db.DocCompra
                            where p.Data >= fromDate
                            select p).ToList();
            }
            else if (to != null)
            {
                docs = (from p in db.DocCompra
                            where p.Data <= toDate
                            select p).ToList();
            }
            else
            {
                docs = (from p in db.DocCompra
                        select p).ToList();
            }

            docs = docs.OrderByDescending(x => x.Data).ToList();
            return docs;
        }
        
        // GET api/DocCompra/id   
        public Models.DocCompra Get(string id)
        {
            try
            {
                Models.DocCompra doc = (from p in db.DocCompra
                                          where p.Id == id
                                          select p).AsQueryable().First();
                return doc;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.DocCompra> ProductPurchases()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string period = allUrlKeyValues.LastOrDefault(x => x.Key == "period").Value;
            string product = allUrlKeyValues.LastOrDefault(x => x.Key == "product").Value;

            return Lib_Primavera.PriIntegration.Compras_Produto_List(product, period);
        }

        public HttpResponseMessage Post(Lib_Primavera.Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.VGR_New(dc);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, dc.id);
                string uri = Url.Link("DefaultApi", new { DocId = dc.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }

    }
}
