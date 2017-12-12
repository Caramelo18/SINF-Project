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

            System.Diagnostics.Debug.WriteLine("Primavera Customers");
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
            }
            saveToDb(db);
        }

        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Artigo

        public static void addProductsFromPrimaveraToDb(DatabaseEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Primavera Products");
            List<Lib_Primavera.Model.Artigo> listaArtigos = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (var item in listaArtigos)
            {
                var product = db.Product.Find(item.CodArtigo);

                if (product != null)
                {
                    product.ProductStock = Convert.ToInt32(item.STKAtual);
                    product.Armazem = item.Localizacao;
                }
            }
            saveToDb(db);
        }

        #endregion Artigo



        #region DocCompra

        public static void addLinhaDocCompraToDb(DatabaseEntities db, FirstREST.Lib_Primavera.Model.DocCompra item)
        {
            foreach (var linha in item.LinhasDoc)
            {

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
            }
            saveToDb(db);
        }

        public static void addDocCompraToDb(DatabaseEntities db)
        {

            System.Diagnostics.Debug.WriteLine("Primavera Docs Compra");
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
            }
            saveToDb(db);
        }

        #endregion DocCompra


        #region DocsVenda

        public static void addLinhaDocVendaToDb(DatabaseEntities db, FirstREST.Lib_Primavera.Model.DocVenda item)
        {
            foreach (var linha in item.LinhasDoc)
            {

                Models.LinhaDocVenda newLinha = new Models.LinhaDocVenda
                {
                    CodArtigo = linha.CodArtigo,
                    DescArtigo = linha.DescArtigo,
                    Desconto = linha.Desconto,
                    IdCabecDoc = linha.IdCabecDoc,
                    PrecoUnitario = linha.PrecoUnitario,
                    Quantidade = linha.Quantidade,
                    TotalILiquido = linha.TotalILiquido,
                    TotalLiquido = linha.TotalLiquido,
                    Unidade = linha.Unidade
                };
                db.LinhaDocVenda.Add(newLinha);
            }
            saveToDb(db);
        }

        public static void addDocVendaToDb(DatabaseEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Primavera Docs Venda");
            List<Lib_Primavera.Model.DocVenda> docList = Lib_Primavera.PriIntegration.Encomendas_List();

            foreach (var item in docList)
            {
                var docVenda = db.DocVenda.Find(item.id);

                if (docVenda == null)
                {
                    Models.DocVenda newDoc = new Models.DocVenda
                    {
                        Id = item.id,
                        Data = item.Data,
                        Entidade = item.Entidade,
                        NumDoc = item.NumDoc,
                        Serie = item.Serie,
                        TotalMerc = item.TotalMerc
                    };
                    db.DocVenda.Add(newDoc);

                    addLinhaDocVendaToDb(db, item);
                }
            }
            saveToDb(db);
        }

        #endregion DocsVenda

        #region Fornecedor

        public static void addSupplierToDb(DatabaseEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Primavera Suppliers");
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
            }
            saveToDb(db);
        }

        #endregion

        #region AccountsReceivable

        public static void addAccountsReceivableToDb(DatabaseEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Primavera Accounts Receivable");
            List<Lib_Primavera.Model.Pendente> accountsList = Lib_Primavera.PriIntegration.Accounts_Receivable_List();

            foreach (var item in accountsList)
            {

                var account = db.AccountReceivable.Find(item.Entidade, item.DataDoc, item.DataVenc, item.ValorTotal, item.ValorPendente);
                
                if (account == null)
                {
                    Models.AccountReceivable newAccount = new Models.AccountReceivable
                    {
                        TipoEntidade = item.TipoEntidade,
                        Entidade = item.Entidade,
                        DataDoc = item.DataDoc,
                        DataVenc = item.DataVenc,
                        ValorTotal = item.ValorTotal,
                        ValorPendente = item.ValorPendente,
                        ModoPag = item.ModoPag
                    };
                    db.AccountReceivable.Add(newAccount);
                }
            }
            saveToDb(db);
        }

        public static void addAccountsPayableToDb(DatabaseEntities db)
        {
            System.Diagnostics.Debug.WriteLine("Primavera Accounts Payable");
            List<Lib_Primavera.Model.Pendente> accountsList = Lib_Primavera.PriIntegration.Accounts_Payable_List();

            foreach (var item in accountsList)
            {

                var account = db.AccountPayable.Find(item.Entidade, item.DataDoc, item.DataVenc, item.ValorTotal, item.ValorPendente);

                if (account == null)
                {
                    Models.AccountPayable newAccount = new Models.AccountPayable
                    {
                        TipoEntidade = item.TipoEntidade,
                        Entidade = item.Entidade,
                        DataDoc = item.DataDoc,
                        DataVenc = item.DataVenc,
                        ValorTotal = item.ValorTotal,
                        ValorPendente = item.ValorPendente,
                        ModoPag = item.ModoPag
                    };
                    db.AccountPayable.Add(newAccount);
                }
            }
            saveToDb(db);
        }

        #endregion
    }
}