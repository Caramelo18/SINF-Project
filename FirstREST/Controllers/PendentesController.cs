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
        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Pendente> AccountsReceivable()
        {
            return Lib_Primavera.PriIntegration.Accounts_Receivable_List();
        }

        [HttpGet]
        public IEnumerable<Lib_Primavera.Model.Pendente> AccountsPayable()
        {
            return Lib_Primavera.PriIntegration.Accounts_Payable_List();
        }
    }
}
