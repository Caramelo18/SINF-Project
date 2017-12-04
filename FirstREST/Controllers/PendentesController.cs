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
        public IEnumerable<Lib_Primavera.Model.Pendente> AccountsReceivable()
        {
            return Lib_Primavera.PriIntegration.Accounts_Receivable_List();
        }
        /*public List<Models.AccountsReceivable> AccountsReceivable()
        {
            var accounts = (from p in db.AccountsReceivable
                            select p).ToList();
            return accounts;

        }*/

        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Pendente> AccountsPayable()
        {
            return Lib_Primavera.PriIntegration.Accounts_Payable_List();
        }
    }
}
