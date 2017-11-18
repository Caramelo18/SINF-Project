using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirstREST.Lib_Primavera.Model
{
    public class Pendente
    {
        public string TipoEntidade
        {
            get;
            set;
        }
        public string Entidade
        {
            get;
            set;
        }
        public DateTime DataDoc
        {
            get;
            set;
        }
        public DateTime DataVenc
        {
            get;
            set;
        }
        public double ValorTotal
        {
            get;
            set;
        }
        public double ValorPendente
        {
            get;
            set;
        }
        public string ModoPag
        {
            get;
            set;
        }
    }
}
