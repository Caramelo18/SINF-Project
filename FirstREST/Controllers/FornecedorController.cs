using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class FornecedorController : ApiController
    {
        public IEnumerable<Lib_Primavera.Model.Fornecedor> Get()
        {
            return Lib_Primavera.PriIntegration.SupplierList();
        }

        public Lib_Primavera.Model.Fornecedor Get(string id)
        {
            Lib_Primavera.Model.Fornecedor fornecedor = Lib_Primavera.PriIntegration.GetSupplier(id);

            if(fornecedor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            else
            {
                return fornecedor;
            }
        }
    }
}
