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
    public class DocVendaController : ApiController
    {
        //
        // GET: /docvendas/
        public IEnumerable<Lib_Primavera.Model.DocVenda> Get()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string period = allUrlKeyValues.LastOrDefault(x => x.Key == "period").Value;
            if (period == null)
            return Lib_Primavera.PriIntegration.Encomendas_List();
              else
            return Lib_Primavera.PriIntegration.Encomendas_List(period);
        }


        // GET api/docvendas/5    
        public Lib_Primavera.Model.DocVenda Get(string id)
        {
            Lib_Primavera.Model.DocVenda docvenda = Lib_Primavera.PriIntegration.Encomenda_Get(id);
            if (docvenda == null)
            {
                throw new HttpResponseException(
                        Request.CreateResponse(HttpStatusCode.NotFound));

            }
            else
            {
                return docvenda;
            }
        }

        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.DocVenda> ProductSales()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string period = allUrlKeyValues.LastOrDefault(x => x.Key == "period").Value;
            string product = allUrlKeyValues.LastOrDefault(x => x.Key == "product").Value;

            return Lib_Primavera.PriIntegration.Encomendas_Produto_List(product, period);
        }


        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.DocVenda> ClientSales()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string client = allUrlKeyValues.LastOrDefault(x => x.Key == "client").Value;

            return Lib_Primavera.PriIntegration.Encomendas_Cliente_List(client);
        }

        public HttpResponseMessage Post(Lib_Primavera.Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.Encomendas_New(dv);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, dv.id);
                string uri = Url.Link("DefaultApi", new {DocId = dv.id });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }


        public HttpResponseMessage Put(int id, Lib_Primavera.Model.Cliente cliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

            try
            {
                erro = Lib_Primavera.PriIntegration.UpdCliente(cliente);
                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }
            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);
            }
        }



        public HttpResponseMessage Delete(string id)
        {


            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();

            try
            {

                erro = Lib_Primavera.PriIntegration.DelCliente(id);

                if (erro.Erro == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, erro.Descricao);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, erro.Descricao);
                }

            }

            catch (Exception exc)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, erro.Descricao);

            }

        }
    }
}
