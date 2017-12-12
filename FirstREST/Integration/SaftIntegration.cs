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

namespace FirstREST.Integration
{
    public class SaftIntegration
    {
        static Stack<Tuple<string, string>> keys = new Stack<Tuple<string, string>>();
        static int billingId = 1;

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

        public static object ParseRecursive(XmlNodeList list, string className, DatabaseEntities db)
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
                        object subClass = ParseRecursive(node.ChildNodes, newClassName, db);

                        removeKey(node.Name);
                        bool belongsToClass = subClassProperty != null;

                        if (belongsToClass && !subClassProperty.GetGetMethod().IsVirtual)
                        {
                            subClassProperty.SetValue(classModel, subClass);
                        }
                        else
                        {
                            //adicionar fk
                            subClass = addForeignKey(subClass, node.Name);
                            //db.Entry(subClass).State = System.Data.Entity.EntityState.Modified;
                            db.Set(subClass.GetType()).Add(subClass);
                            try
                            {
                                //db.SaveChanges();
                                if (node.Name == "BillingAddress")
                                {
                                    PropertyInfo billId = subClass.GetType().GetProperty("ID");
                                    var value = billId.GetValue(subClass);
                                    PropertyInfo customerBillId = classModel.GetType().GetProperty("BillingAddressID");
                                    customerBillId.SetValue(classModel, value);
                                }
                            }
                            catch (DbEntityValidationException e)
                            { }
                        }
                    }
                    else
                    {
                        PropertyInfo propertyInfo = classModel.GetType().GetProperty(node.Name);
                        propertyInfo.SetValue(classModel, Convert.ChangeType(node.InnerText, propertyInfo.PropertyType), null);
                        saveKey(className, node.Name, node.InnerText);
                    }
                }
            }

            return classModel;
        }

        # region utils
        private static bool saveKey(string className, string property, string value)
        {

            if ((className == "FirstREST.Models.Customer" && property == "CustomerId") ||
                (className == "FirstREST.Models.Invoice" && property == "InvoiceNo") ||
                (className == "FirstREST.Models.Line" && property == "LineNumber"))
            {
                keys.Push(new Tuple<string, string>(property, value));
                return true;
            }
            return false;
        }

        private static void removeKey(string className)
        {
            if (className == "Customer" || className == "Invoice" || className == "Line")
            {
                keys.Pop();
            }
        }

        private static object addForeignKey(object parsedClass, string className)
        {
            PropertyInfo classProperty;
            switch (className)
            {
                case "DocumentTotals":
                case "Line":
                    classProperty = parsedClass.GetType().GetProperty(keys.Peek().Item1);
                    classProperty.SetValue(parsedClass, keys.Peek().Item2);

                    break;
                case "Tax":
                    Tuple<string, string> temp = keys.Peek();
                    keys.Pop();
                    classProperty = parsedClass.GetType().GetProperty(keys.Peek().Item1);
                    classProperty.SetValue(parsedClass, keys.Peek().Item2);
                    keys.Push(temp);
                    classProperty = parsedClass.GetType().GetProperty(keys.Peek().Item1);
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
                        Models.Customer newClient = (Models.Customer)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Customer", db);
                        db.Customer.Add(newClient);
                    }

                    saveToDb(db);
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
                        Models.Product newProduct = (Models.Product)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Product", db);

                        db.Product.Add(newProduct);
                    }

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    { }
                }
            }
        }

        #endregion Artigo

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
                        Models.SalesInvoices newInvoice = (Models.SalesInvoices)ParseRecursive(xml.ChildNodes, "FirstREST.Models.SalesInvoices", db);
                        db.SalesInvoices.Add(newInvoice);
                    }

                    saveToDb(db);
                }
            }
        }

        #endregion DocsVenda
    }
}