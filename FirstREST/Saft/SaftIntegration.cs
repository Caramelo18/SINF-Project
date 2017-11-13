using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Web.Mvc;
using FirstREST.Models;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace FirstREST.Saft
{
    public class SaftIntegration
    {
        static Stack<Tuple<string, string>> keys = new Stack<Tuple<string,string>>();

        static DatabaseEntities db = new DatabaseEntities();

        public static object GetInstance(string strFullyQualifiedName)
        {
            Type type = Type.GetType(strFullyQualifiedName);
            if (type != null)
                return Activator.CreateInstance(type);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var asm in assemblies)
            {
                type = asm.GetType(strFullyQualifiedName);
                if (type != null)
                    return Activator.CreateInstance(type);
            }
            return null;
        }

        public static object ParseRecursive(XmlNodeList list, string className)
        {
            object classModel = GetInstance(className);
            if (classModel == null) return null;
            foreach (XmlNode node in list)
            {
                if (node.HasChildNodes)
                {
                    bool isSubClass = (node.ChildNodes.Count > 1);
                    if (isSubClass)
                    {
                        string newClassName = "FirstREST.Models." + node.Name;
                        PropertyInfo subClassProperty = classModel.GetType().GetProperty(node.Name);
                        object subClass = ParseRecursive(node.ChildNodes, newClassName);

                        removeKey(node.Name);
                        saveToDb(db);
                        bool belongsToClass = subClassProperty != null;

                        if (belongsToClass && !subClassProperty.GetGetMethod().IsVirtual)
                        {
                            PropertyAttributes attr = subClassProperty.Attributes;
                            subClassProperty.SetValue(classModel, subClass);
                        }
                        else
                        {
                            
                            Type classType = GetInstance(newClassName).GetType();
                            //adicionar fk
                            object newObject = addForeignKey(subClass, node.Name);
                            db.Set(classType).Add(newObject);
                            
                            //ver ids das connected tables
                        }
                    }
                    else
                    {
                        saveKey(className, node.Name, node.InnerText);

                        PropertyInfo propertyInfo = classModel.GetType().GetProperty(node.Name);
                        propertyInfo.SetValue(classModel, Convert.ChangeType(node.InnerText, propertyInfo.PropertyType), null);
                    }
                }
            }
            return classModel;
        }

        # region shitty stuff cause c# is shitty
        private static void saveKey(string className, string property, string value)
        {
               
            if((className ==  "FirstREST.Models.Customer" && property == "CustomerId") ||
                (className == "FirstREST.Models.Invoice" && property == "InvoiceNo") ||
                (className == "FirstREST.Models.Line" && property == "LineNumber"))
            {
                keys.Push(new Tuple<string, string>(property, value));
            }
        }

        private static void removeKey(string className)
        {
            if(className ==  "Customer" || className ==  "Invoice" || className == "Line")
            {
                keys.Pop();
            }
        }

        private static object addForeignKey(object parsedClass, string className)
        {
            switch (className)
            {
                case "DocumentTotals":
                case "Line":
                case "Tax":
                    PropertyInfo classProperty = parsedClass.GetType().GetProperty(keys.Peek().Item1);
                    classProperty.SetValue(parsedClass, keys.Peek().Item2);
                    break;
            }
            return parsedClass;
        }

        # endregion end of shitty stuff

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


        public static void ParseCustomers(XmlDocument doc, DatabaseEntities db)
        {
            XmlNodeList clientsList = doc.GetElementsByTagName("Customer");

            foreach (XmlNode xml in clientsList)
            {
                if (xml.HasChildNodes)
                {

                    string id = xml.ChildNodes[0].InnerText;
                    Models.Customer client = db.Customer.Find(id);

                    if (client != null)
                    {
                        System.Diagnostics.Debug.WriteLine("vou dar update");
                        //TO DO: update
                        //client = (Models.Customer)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Customer");

                    }
                    else
                    {
                        Models.Customer newClient = (Models.Customer)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Customer");
                        db.Customer.Add(newClient);
                    }

                    saveToDb(db);
                }
            }
        }

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

        public static void ParseProducts(XmlDocument doc, DatabaseEntities db)
        {
            XmlNodeList productsList = doc.GetElementsByTagName("Product");

            foreach (XmlNode xml in productsList)
            {
                if (xml.HasChildNodes)
                {

                    string id = xml.ChildNodes[1].InnerText;
                    Models.Product product = db.Product.Find(id);

                    if (product != null)
                    {
                        //TO DO: update
                        //product = (Models.Product)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Product");

                    }
                    else
                    {
                        Models.Product newProduct = (Models.Product)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Product");

                        db.Product.Add(newProduct);
                    }

                    saveToDb(db);
                }
            }
        }

        public static void addProductsFromPrimaveraToDb(DatabaseEntities db)
        {
            List<Lib_Primavera.Model.Artigo> listaArtigos = Lib_Primavera.PriIntegration.ListaArtigos();

            foreach (var item in listaArtigos)
            {
                var product = db.Product.Find(item.CodArtigo);

                if (product != null)
                {
                    product.ProductStock = Convert.ToInt32(item.STKAtual);
                }
                else
                {
                    Models.Product newProduct = new Models.Product
                    {
                        ProductCode = item.CodArtigo,
                        ProductDescription = item.DescArtigo,
                        ProductStock = Convert.ToInt32(item.STKAtual)
                    };
                    db.Product.Add(newProduct);
                }

                saveToDb(db);

            }
        }

        #endregion Artigo



        #region DocCompra

        public static void ParseSalesInvoice(XmlDocument doc, DatabaseEntities db)
        {
            XmlNodeList salesList = doc.GetElementsByTagName("SalesInvoice");

            foreach (XmlNode xml in salesList)
            {
                if (xml.HasChildNodes)
                {

                    string id = xml.ChildNodes[0].InnerText;
                    Models.Customer client = db.Customer.Find(id);

                    if (client != null)
                    {
                        System.Diagnostics.Debug.WriteLine("vou dar update");
                        //TO DO: update
                        //client = (Models.Customer)ParseRecursive(xml.ChildNodes, "FirstREST.Models.SalesInvoice");

                    }
                    else
                    {
                        Models.Customer newClient = (Models.Customer)ParseRecursive(xml.ChildNodes, "FirstREST.Models.SalesInvoice");
                        db.Customer.Add(newClient);
                    }

                    saveToDb(db);
                }
            }
        }

        #endregion DocCompra


        #region DocsVenda

        public static void ParseSalesInvoices(XmlDocument doc, DatabaseEntities db)
        {
            XmlNodeList salesInvoicesList = doc.GetElementsByTagName("SalesInvoices");

            foreach (XmlNode xml in salesInvoicesList)
            {
                if (xml.HasChildNodes)
                {

                    string invoiceNo = xml.ChildNodes[0].InnerText;
                    Models.Invoice client = db.Invoice.Find(invoiceNo);

                    if (client != null)
                    {
                        System.Diagnostics.Debug.WriteLine("vou dar update");
                        //TO DO: update
                        //client = (Models.Customer)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Customer");

                    }
                    else
                    {
                        Models.SalesInvoices newInvoice = (Models.SalesInvoices)ParseRecursive(xml.ChildNodes, "FirstREST.Models.SalesInvoices");
                        db.SalesInvoices.Add(newInvoice);
                    }

                    saveToDb(db);
                }
            }
        }

        #endregion DocsVenda

        #region Fornecedor


        #endregion
    }
}