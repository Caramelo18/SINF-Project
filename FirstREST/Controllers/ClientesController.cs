using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using FirstREST.Models;

namespace FirstREST.Controllers
{
    public class TopClients
    {
        public string customer {get; set;}
        public double sum { get; set; }

        public TopClients(string customer, double sum)
        {
            this.customer = customer;
            this.sum = sum;
        }

        public void AddValue(double sum)
        {
            this.sum += sum;
        }
    }

    public class ClientesController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /Clientes/

        public List<Models.Customer> Get()
        {
            var clients = db.Customer.SqlQuery("SELECT * FROM Customer").ToList();
            //var clients = db.Customer.Select(u => u.Fax).ToList();

            return clients;
        }

        // GET api/cliente/5    
        public Models.Customer Get(string id)
        {
            try
            {
                Models.Customer client = (from p in db.Customer
                                          where p.CustomerID == id
                                          select p).AsQueryable().First();
                return client;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        [HttpGet]
        public void test()
        {
            System.Diagnostics.Debug.WriteLine("fds");
        }

        [HttpGet]
        public List<TopClients> GetBestByProduct(string product)
        {
            try
            {
                List<TopClients> listClients = new List<TopClients>();

                List<Models.DocVenda> docs = (from docVenda in db.DocVenda
                                              join linha in db.LinhaDocVenda
                                              on docVenda.Id equals linha.IdCabecDoc
                                              where linha.CodArtigo == product
                                              select docVenda).ToList();

                foreach (var doc in docs)
                {
                    if (!listClients.Any(client => client.customer == doc.Entidade))
                    {
                        listClients.Add(new TopClients(doc.Entidade, 0));
                    }

                    int index = listClients.FindIndex(client => client.customer == doc.Entidade);

                    double? aux = db.LinhaDocVenda
                        .Where(c => c.CodArtigo == product)
                        .Sum(c => c.Quantidade);

                    if (aux.HasValue)
                    {
                        double sum = aux.Value;
                        listClients[index].AddValue(sum);
                    }
                }

                listClients.OrderByDescending(client => client.sum);

                var test = listClients.Take(5).ToList();

                return listClients.Take(5).ToList();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }

        public HttpResponseMessage Post(Lib_Primavera.Model.Cliente cliente)
        {
            Lib_Primavera.Model.RespostaErro erro = new Lib_Primavera.Model.RespostaErro();
            erro = Lib_Primavera.PriIntegration.InsereClienteObj(cliente);

            if (erro.Erro == 0)
            {
                var response = Request.CreateResponse(
                   HttpStatusCode.Created, cliente);
                string uri = Url.Link("DefaultApi", new { CodCliente = cliente.CodCliente });
                response.Headers.Location = new Uri(uri);
                return response;
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

        }


        public HttpResponseMessage Put(string id, Lib_Primavera.Model.Cliente cliente)
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
