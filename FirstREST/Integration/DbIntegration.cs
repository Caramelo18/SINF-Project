using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Web.Mvc;
using FirstREST.Models;
using System.Data.Entity.Validation;

namespace FirstREST.Integration
{
    public class DbIntegration
    {
        private static void saveToDb(DatabaseEntities db)
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var errorMessages = e.EntityValidationErrors
                .SelectMany(x => x.ValidationErrors)
                .Select(x => x.ErrorMessage);

                var fullError = string.Join("; ", errorMessages);

                var exception = string.Concat(e.Message, "Errors: ", fullError);

                throw new DbEntityValidationException(exception, e.EntityValidationErrors);
            }
        }

        # region Cliente

        public static void addClientsFromPrimaveraToDb(DatabaseEntities db)
        {
            List<Lib_Primavera.Model.Cliente> clientsList = Lib_Primavera.PriIntegration.ListaClientes();

            foreach (var item in clientsList)
            {
                var client = db.Customer.Find(item.CodCliente);

                if (client != null)
                {
                    client.CustomerName = item.NomeCliente;
                    client.CustomerEmail = item.Email;
                    client.Currency = item.Moeda;
                    client.CustomerTaxID = item.NumContribuinte;
                }
                else
                {
                    Models.Customer newClient = new Models.Customer
                    {
                        CustomerID = item.CodCliente,
                        CustomerName = item.NomeCliente,
                        Currency = item.Moeda,
                        CustomerEmail = item.Email,
                        CustomerTaxID = item.NumContribuinte
                    };
                    db.Customer.Add(newClient);

                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var errorMessages = e.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                    var fullError = string.Join("; ", errorMessages);

                    var exception = string.Concat(e.Message, "Errors: ", fullError);

                    throw new DbEntityValidationException(exception, e.EntityValidationErrors);
                }

            }
        }

        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Artigo

        public static void addProductsFromPrimaveraToDb(DatabaseEntities db)
        {
            List<Lib_Primavera.Model.Artigo> listaArtigos = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (var item in listaArtigos)
            {
                var product = db.Product.Find(item.CodArtigo);

                if (product != null)
                {
                    product.ProductStock = Convert.ToInt32(item.STKAtual);
                    product.Armazem = item.Localizacao;
                }
                else
                {
                    Models.Product newProduct = new Models.Product
                    {
                        ProductCode = item.CodArtigo,
                        ProductDescription = item.DescArtigo,
                        ProductStock = Convert.ToInt32(item.STKAtual),
                        Armazem = item.Localizacao
                    };
                    db.Product.Add(newProduct);
                }
                saveToDb(db);
            }
        }

        #endregion Artigo



        #region DocCompra

        public static void addLinhaDocCompraToDb(XmlDocument doc, DatabaseEntities db, FirstREST.Lib_Primavera.Model.DocCompra item)
        {
            foreach (var linha in item.LinhasDoc)
            {

                System.Diagnostics.Debug.WriteLine(linha.IdCabecDoc);

                Models.LinhaDocCompra newLinha = new Models.LinhaDocCompra
                {
                    Armazem = linha.Armazem,
                    CodArtigo = linha.CodArtigo,
                    DescArtigo = linha.DescArtigo,
                    Desconto = linha.Desconto,
                    IdCabecDoc = linha.IdCabecDoc,
                    Lote = linha.Lote,
                    NumLinha = linha.NumLinha,
                    PrecoUnitario = linha.PrecoUnitario,
                    Quantidade = linha.Quantidade,
                    TotalILiquido = linha.TotalILiquido,
                    TotalLiquido = linha.TotalLiquido,
                    Unidade = linha.Unidade
                };
                db.LinhaDocCompra.Add(newLinha);

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var errorMessages = e.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                    var fullError = string.Join("; ", errorMessages);

                    var exception = string.Concat(e.Message, "Errors: ", fullError);

                    throw new DbEntityValidationException(exception, e.EntityValidationErrors);
                }

            }
        }

        public static void addDocCompraToDb(XmlDocument doc, DatabaseEntities db)
        {
            List<Lib_Primavera.Model.DocCompra> docList = Lib_Primavera.PriIntegration.VGR_List();

            foreach (var item in docList)
            {
                var docCompra = db.DocCompra.Find(item.id);

                if (docCompra == null)
                {
                    Models.DocCompra newDoc = new Models.DocCompra
                    {
                        Id = item.id,
                        Data = item.Data,
                        Entidade = item.Entidade,
                        NumDoc = item.NumDoc,
                        NumDocExterno = item.NumDocExterno,
                        Serie = item.Serie,
                        TotalMerc = item.TotalMerc
                    };
                    db.DocCompra.Add(newDoc);

                    addLinhaDocCompraToDb(doc, db, item);
                }
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var errorMessages = e.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                    var fullError = string.Join("; ", errorMessages);

                    var exception = string.Concat(e.Message, "Errors: ", fullError);

                    throw new DbEntityValidationException(exception, e.EntityValidationErrors);
                }
            }
        }

        #endregion DocCompra


        #region DocsVenda

        #endregion DocsVenda

        #region Fornecedor


        #endregion
    }
}