using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using System.Xml;

namespace FirstREST.Saft
{
    public class SaftIntegration
    {

        # region Cliente

        public static List<Models.Artigo> ParseArtigos()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\user\Desktop\SINF\saft.xml");

            List<Models.Artigo> listaArtigos = new List<Models.Artigo>();

            XmlNodeList artigos = doc.GetElementsByTagName("Product");

            for (int i = 0; i < artigos.Count; i++)
            {
                XmlNode node = artigos[i];

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
        }

        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        #region Artigo



        #endregion Artigo



        #region DocCompra


        #endregion DocCompra


        #region DocsVenda

        #endregion DocsVenda

        #region Fornecedor


        #endregion
    }
}