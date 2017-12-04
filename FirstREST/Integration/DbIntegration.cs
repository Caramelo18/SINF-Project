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
                    client.NumberPurchases = item.NumCompras;
                }
                /*else
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

                }*/
                saveToDb(db);

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
                }/*
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
                }*/
                saveToDb(db);
            }
        }

        #endregion Artigo



        #region DocCompra

        public static void addLinhaDocCompraToDb(DatabaseEntities db, FirstREST.Lib_Primavera.Model.DocCompra item)
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

                saveToDb(db);

            }
        }

        public static void addDocCompraToDb(DatabaseEntities db)
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

                    addLinhaDocCompraToDb(db, item);
                }
                saveToDb(db);
            }
        }

        #endregion DocCompra


        #region DocsVenda

        #endregion DocsVenda

        #region Fornecedor

        public static void addSupplierToDb(DatabaseEntities db)
        {
            List<Lib_Primavera.Model.Fornecedor> supplierList = Lib_Primavera.PriIntegration.SupplierList();

            foreach (var item in supplierList)
            {
                var supplier = db.Supplier.Find(item.CodFornecedor);

                if (supplier == null)
                {
                    Models.Supplier newSupplier = new Models.Supplier
                    {
                        CodFornecedor = item.CodFornecedor,
                        Contribuinte = item.Contribuinte,
                        Nome = item.Nome,
                        NomeFiscal = item.NomeFiscal,
                        Telefone = item.Telefone
                    };
                    db.Supplier.Add(newSupplier);
                }
                saveToDb(db);
            }
        }

        #endregion

        #region AccountsReceivable

        public static void addAccountsReceivableToDb(DatabaseEntities db)
        {
            List<Lib_Primavera.Model.Pendente> accountsList = Lib_Primavera.PriIntegration.Accounts_Receivable_List();

            foreach (var item in accountsList)
            {

                var account = db.AccountsReceivable.Find(item.Entidade, item.DataDoc, item.DataVenc, item.ValorTotal, item.ValorPendente);

                System.Diagnostics.Debug.WriteLine("account: " + account);
                
                if (account == null)
                {
                    System.Diagnostics.Debug.WriteLine("VOU CRIAR");
                    System.Diagnostics.Debug.WriteLine(item.DataDoc + " - " + item.DataVenc);
                    System.Diagnostics.Debug.WriteLine(item.Entidade + " - " + item.TipoEntidade);
                    System.Diagnostics.Debug.WriteLine(item.ValorPendente + " - " + item.ValorTotal + " - " + item.ModoPag);
                    Models.AccountsReceivable newAccount = new Models.AccountsReceivable
                    {
                        TipoEntidade = item.TipoEntidade,
                        Entidade = item.Entidade,
                        DataDoc = item.DataDoc,
                        DataVenc = item.DataVenc,
                        ValorTotal = item.ValorTotal,
                        ValorPendente = item.ValorPendente,
                        ModoPag = item.ModoPag
                    };
                    db.AccountsReceivable.Add(newAccount);
                    saveToDb(db);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(" JA EXISTE");
                    System.Diagnostics.Debug.WriteLine(item.DataDoc + " - " + item.DataVenc);
                    System.Diagnostics.Debug.WriteLine(item.Entidade + " - " + item.TipoEntidade);
                    System.Diagnostics.Debug.WriteLine(item.ValorPendente + " - " + item.ValorTotal + " - " + item.ModoPag);
                }
            }
        }

        #endregion
    }
}