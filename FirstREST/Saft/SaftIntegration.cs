using System;
using System.Linq;
using System.Xml;
using System.Web.Mvc;
using FirstREST.Models;

namespace FirstREST.Saft
{
    public class SaftIntegration
    {

        # region Cliente

        /*
        public static List<Models.Artigo> ParseArtigos()
        {
            //Loads xml file
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\Desktop\SINF\saft.xml");

            List<Models.Artigo> listaArtigos = new List<Models.Artigo>();

            XmlNodeList artigos = doc.GetElementsByTagName("Product");

            foreach (XmlNode node in artigos)
            {

                if (node.HasChildNodes)
                {
                    Models.Artigo artigo = new Models.Artigo
                    {
                        TipoArtigo = node.ChildNodes[0].InnerText,
                        CodArtigo = node.ChildNodes[1].InnerText,
                        GrupoArtigo = node.ChildNodes[2].InnerText,
                        DescArtigo = node.ChildNodes[3].InnerText
                    };

                    listaArtigos.Add(artigo);
                }
            }

            return listaArtigos;
        }*/

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