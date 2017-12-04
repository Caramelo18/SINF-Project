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
    public class SaftIntegration
    {

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
                        subClassProperty.SetValue(classModel, subClass);
                    }
                    else
                    {
                        PropertyInfo propertyInfo = classModel.GetType().GetProperty(node.Name);
                        propertyInfo.SetValue(classModel, Convert.ChangeType(node.InnerText, propertyInfo.PropertyType), null);
                    }
                }
            }
            return classModel;
        }

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

        #endregion Artigo



        #region DocCompra

        #endregion DocCompra


        #region DocsVenda

        #endregion DocsVenda

        #region Fornecedor


        #endregion
    }
}