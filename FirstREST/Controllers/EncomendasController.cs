using FirstREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{

    public class EncomendaProduto
    {
        public Models.DocVenda doc { get; set; }
        public List<Models.LinhaDocVenda> lines { get; set; }

        public EncomendaProduto(Models.DocVenda doc, List<Models.LinhaDocVenda> lines)
        {
            this.doc = doc;
            this.lines = lines;
        }
    }

    public class EncomendasController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        public List<Models.DocVenda> Get()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string from = allUrlKeyValues.LastOrDefault(x => x.Key == "from").Value;
            string to = allUrlKeyValues.LastOrDefault(x => x.Key == "to").Value;

            DateTime fromDate = Convert.ToDateTime(from);
            DateTime toDate = Convert.ToDateTime(to);

            var docs = new List<Models.DocVenda>();

            if (from != null && to != null)
            {
                docs = (from p in db.DocVenda
                        where p.Data >= fromDate && p.Data <= toDate
                        select p).ToList();
            }
            else if (from != null)
            {
                docs = (from p in db.DocVenda
                        where p.Data >= fromDate
                        select p).ToList();
            }
            else if (to != null)
            {
                docs = (from p in db.DocVenda
                        where p.Data <= toDate
                        select p).ToList();
            }
            else
            {
                docs = (from p in db.DocVenda
                        select p).ToList();
            }

            docs = docs.OrderByDescending(x => x.Data).ToList();
            return docs;
        }

        public EncomendaProduto Get(string id)
        {
            try
            {
                Models.DocVenda doc = (from p in db.DocVenda
                                        where p.Id == id
                                        select p).AsQueryable().First();

                List<Models.LinhaDocVenda> linhas = (from i in db.LinhaDocVenda
                                                     where i.IdCabecDoc == id
                                                     select i).ToList();

                EncomendaProduto encomenda = new EncomendaProduto(doc, linhas);
                
                return encomenda;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        [HttpGet]
        public List<EncomendaProduto> GetByProduct(string id)
        {
            try
            {
                List<EncomendaProduto> listaDocVenda = new List<EncomendaProduto>();

                List<Models.DocVenda> docs = (from docVenda in db.DocVenda
                                             join linha in db.LinhaDocVenda
                                             on docVenda.Id equals linha.IdCabecDoc
                                             where linha.CodArtigo == id
                                             select docVenda).ToList();

                foreach (var doc in docs)
                {
                    List<Models.LinhaDocVenda> linhas = (from i in db.LinhaDocVenda
                                                         where i.CodArtigo == id
                                                         select i).ToList();

                    EncomendaProduto encomenda = new EncomendaProduto(doc, linhas);
                    listaDocVenda.Add(encomenda);
                }
                
                return listaDocVenda;      
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        [HttpGet]
        public int GetNoInventoryOrders()
        {
            try
            {
                int total = 0;
                List<string> produtos = new List<string>();

                List<Models.DocVenda> docs = (from docVenda in db.DocVenda
                                              select docVenda).ToList();

                List<Models.Product> products = (from i in db.Product
                                                 select i).ToList();

                foreach (var doc in docs)
                {
                    List<Models.LinhaDocVenda> linhas = (from i in db.LinhaDocVenda
                                                         where i.IdCabecDoc == doc.Id
                                                         select i).ToList();
                    foreach (var linha in linhas)
                    {
                        var product = products.Find(p => p.ProductCode == linha.CodArtigo);

                        if (product != null && product.ProductStock < linha.Quantidade && linha.Quantidade != 0)
                        {
                            total++;
                            break;
                        }
                    }

                }

                return total;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return 0;
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
                string uri = Url.Link("DefaultApi", new { DocId = dv.id });
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
