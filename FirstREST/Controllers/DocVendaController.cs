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
    public class DocVenda
    {
        public Models.Invoice invoice { get; set; }
        public List<Models.Line> lines { get; set; }

        public DocVenda(Models.Invoice invoice, List<Models.Line> lines)
        {
            this.invoice = invoice;
            this.lines = lines;
        }
    }

    public class DocVendaController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /Clientes/
        /*public IEnumerable<Lib_Primavera.Model.DocVenda> Get()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string period = allUrlKeyValues.LastOrDefault(x => x.Key == "period").Value;
            if (period == null)
            return Lib_Primavera.PriIntegration.Encomendas_List();
              else
            return Lib_Primavera.PriIntegration.Encomendas_List(period);
        }*/

        public List<DocVenda> Get()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            string from = allUrlKeyValues.LastOrDefault(x => x.Key == "from").Value;
            string to = allUrlKeyValues.LastOrDefault(x => x.Key == "to").Value;

            DateTime fromDate = Convert.ToDateTime(from);
            DateTime toDate = Convert.ToDateTime(to);

            var docs = new List<DocVenda>();

            /*if (from != null && to != null)
            {
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
            {}*/

            List<Models.Invoice> invoices = (from i in db.Invoice select i).ToList();
            foreach (Models.Invoice invoice in invoices)
            {
                List<Models.Line> lines = (from i in db.Invoice
                                           join l in db.Line
                                           on i.InvoiceNo equals l.InvoiceNo
                                           where i.InvoiceNo == invoice.InvoiceNo
                                           select l).ToList();
                docs.Add(new DocVenda(invoice, lines));

            }

            //docs = docs.OrderByDescending(x => x.Data).ToList();
            return docs;
        }

        [Route ("api/DocVenda/get?id={id*}")]
        public DocVenda Get(string id)
        {
            try
            {
                Models.Invoice invoice = (from i in db.Invoice 
                                          where i.InvoiceNo == id
                                          select i).AsQueryable().First();

                List<Models.Line> lines = (from i in db.Invoice
                                           join l in db.Line
                                           on i.InvoiceNo equals l.InvoiceNo
                                           where i.InvoiceNo == invoice.InvoiceNo
                                           select l).ToList();

                return new DocVenda(invoice, lines);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
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
