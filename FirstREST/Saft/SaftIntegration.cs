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

        # region Cliente


        public static void ParseClientes()
        {
            DatabaseEntities db = new DatabaseEntities();

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\Desktop\saft.xml");
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


        public static void ParseArtigos()
        {

            DatabaseEntities db = new DatabaseEntities();

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\Desktop\SINF\saft.xml");

            XmlNodeList artigos = doc.GetElementsByTagName("Product");

            foreach (XmlNode node in artigos)
            {
                if (node.HasChildNodes)
                {
                    string id = node.ChildNodes[1].InnerText;
                    var product = db.Product.Find(id);

                    if (product != null)
                    {
                        product.ProductDescription = node.ChildNodes[3].InnerText;
                        product.ProductGroup = node.ChildNodes[2].InnerText;
                        product.ProductNumberCode = node.ChildNodes[4].InnerText;
                        product.ProductType = node.ChildNodes[0].InnerText;

                        try { db.SaveChanges(); }
                        catch (Exception e) { }
                    }
                    else
                    {
                        AddArtigoToDb(node, db);
                    }
                }
            }
        }

        public static void AddArtigoToDb(XmlNode node, DatabaseEntities db)
        {
            Models.Product product = new Models.Product
            {
                ProductType = node.ChildNodes[0].InnerText,
                ProductCode = node.ChildNodes[1].InnerText,
                ProductGroup = node.ChildNodes[2].InnerText,
                ProductDescription = node.ChildNodes[3].InnerText,
                ProductNumberCode = node.ChildNodes[4].InnerText
            };
            try
            {
                db.Product.Add(product);
                db.SaveChanges();
            }
            catch (Exception e) { }
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