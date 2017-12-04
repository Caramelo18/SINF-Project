using FirstREST.Models;
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
        DatabaseEntities db = new DatabaseEntities();

        public List<Models.Supplier> Get()
        {
            var suppliers = db.Supplier.SqlQuery("SELECT * FROM Supplier").ToList();
            return suppliers;
        }

        public Models.Supplier Get(string id)
        {
            try
            {
                Models.Supplier supplier = (from p in db.Supplier
                                          where p.CodFornecedor == id
                                          select p).AsQueryable().First();
                return supplier;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }
    }
}
