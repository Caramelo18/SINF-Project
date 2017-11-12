using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class DocCompraController : ApiController
    {

        public IEnumerable<Lib_Primavera.Model.DocCompra> Get()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string period = allUrlKeyValues.LastOrDefault(x => x.Key == "period").Value;
            if (period == null)
                return Lib_Primavera.PriIntegration.VGR_List();
            else
                return Lib_Primavera.PriIntegration.VGR_List(period);
        }

        
        // GET api/DocCompra/id   
        public Lib_Primavera.Model.DocCompra Get(string id)
        {
            Lib_Primavera.Model.DocCompra doccompra = Lib_Primavera.PriIntegration.DocCompra(id);
            if (doccompra == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return doccompra;
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

        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.DocCompra> AccountsPayable()
        {
            return Lib_Primavera.PriIntegration.Accounts_Payable_List();
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
