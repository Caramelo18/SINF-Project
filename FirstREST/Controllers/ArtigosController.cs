using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using FirstREST.Models;


namespace FirstREST.Controllers
{
    public class ArtigosController : ApiController
    {
        DatabaseEntities db = new DatabaseEntities();

        //
        // GET: /Artigos/

        public IQueryable<Models.Product> Get(){
            var products = (from p in db.Product
                           select p);
            return products;
        }

        // GET api/artigo/5    
        public Models.Product Get(string id)
        {
            try
            {
                Models.Product product = (from p in db.Product
                                          where p.ProductCode == id
                                          select p).AsQueryable().First();
                return product;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return null;
            }
        }
    }
}

