using FirstREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FirstREST.Controllers
{
    public class PendentesController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        [HttpGet]
        public List<Models.AccountReceivable> AccountsReceivable()
        {
            var accounts = (from p in db.AccountReceivable
                            select p).ToList();
            return accounts;

        }

        [HttpGet]
        public List<Models.AccountPayable> AccountsPayable()
        {
            var accounts = (from p in db.AccountPayable
                            select p).ToList();
            return accounts;
        }
    }
}
