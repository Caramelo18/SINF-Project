﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstREST.Areas.Clients.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Clients/Home/

        public ActionResult Index()
        {

            //Saft.SaftIntegration.ParseClientes();

            //ViewBag.clientes = clients;
            return View();
        }

    }
}
