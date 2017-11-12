using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using System.Web.Mvc;
using FirstREST.Models;

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


        public static void ParseClientes(XmlDocument doc, DbEntities db)
        {
            XmlNodeList clientsList = doc.GetElementsByTagName("Customer");

            foreach (XmlNode xml in clientsList)
            {
                if (xml.HasChildNodes)
                {
                    Models.Cliente client = (Models.Cliente)ParseRecursive(xml.ChildNodes, "FirstREST.Models.Cliente");
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

            DbEntities db = new DbEntities();

            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\Desktop\SINF\saft.xml");

            XmlNodeList artigos = doc.GetElementsByTagName("Product");

            foreach (XmlNode node in artigos)
            {
                if (node.HasChildNodes)
                {
                    var artigo = db.Artigo.Find(node.ChildNodes[1].InnerText);

                    if (artigo == null)
                    {
                        AddArtigoToDb(node, db);
                    }
                    else
                    {
                        artigo.Descricao = node.ChildNodes[3].InnerText;
                        artigo.Grupo = node.ChildNodes[2].InnerText;
                        artigo.NumberCode = node.ChildNodes[4].InnerText;
                        artigo.Tipo = node.ChildNodes[0].InnerText;

                        try { db.SaveChanges(); }
                        catch (Exception e) { }
                    }
                }
            }
        }

        public static void AddArtigoToDb(XmlNode node, DbEntities db)
        {
            Models.Artigo artigo = new Models.Artigo
            {
                Tipo = node.ChildNodes[0].InnerText,
                Code = node.ChildNodes[1].InnerText,
                Grupo = node.ChildNodes[2].InnerText,
                Descricao = node.ChildNodes[3].InnerText,
                NumberCode = node.ChildNodes[4].InnerText
            };
            try
            {
                db.Artigo.Add(artigo);
                db.SaveChanges();
            }
            catch (Exception e) { }
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