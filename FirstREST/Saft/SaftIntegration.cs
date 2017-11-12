using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Web.Mvc;
using FirstREST.Models;
using System.Collections.Generic;

namespace FirstREST.Saft
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
                        string newClassName = className + "+" + node.Name;
                        PropertyInfo subClassProperty = classModel.GetType().GetProperty(node.Name.ToLower());
                        object subClass = ParseRecursive(node.ChildNodes, newClassName);
                        subClassProperty.SetValue(classModel, subClass);
                    }
                    else
                    {
                        PropertyInfo propertyInfo = classModel.GetType().GetProperty(node.Name);
                        propertyInfo.SetValue(classModel, node.InnerText);
                    }
                }
            }
            return classModel;
        }

        # region Cliente


        public static void ParseCustomers(XmlDocument doc, DatabaseEntities db)
        {
            XmlNodeList clientsList = doc.GetElementsByTagName("Customer");

            foreach (XmlNode xml in clientsList)
            {
                if (xml.HasChildNodes)
                {
                    Models.Customer client = (Models.Customer)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Customer");
                    try
                    {
                        db.Customer.Add(client);
                        db.SaveChanges();

                    }
                    catch (Exception e)
                    {

                    }
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

                    try { db.SaveChanges(); }
                    catch (Exception e) { }
                }
            }
        }

        public static void addProductsFromPrimaveraToDb()
        {
            DatabaseEntities db = new DatabaseEntities();
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
                try
                {
                    db.SaveChanges();
                }
                catch (Exception e) { }

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