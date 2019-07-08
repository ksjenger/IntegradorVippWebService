using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntegradorWebService
{
    public class FormatacaoPlanilha
    {
        #region Atributos
        public string NomeAtributo { get; set; }
        public int Coluna { get; set; }
        #endregion

        #region Construtores
        public FormatacaoPlanilha()
        {
            NomeAtributo = string.Empty;
            Coluna = int.MinValue;
        }
        #endregion

        #region Validacao
        #endregion
    }
}