using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

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
        public static List<FormatacaoPlanilha> ListarFormatacao()
        {
            List<FormatacaoPlanilha> lista = new List<FormatacaoPlanilha>();

            string layout = IntegradorWebService.Properties.Settings.Default.Layout;
            layout = layout.Replace('-', '+');
            layout = layout.Replace('"', ' ');
            layout = layout.Replace('\\', ' ');
            byte[] data = Convert.FromBase64String(layout.Trim());
            string decodedString = Encoding.UTF8.GetString(data);
            XElement xml = XElement.Parse(decodedString);
            
            foreach (XElement x in xml.Elements())
            {
                FormatacaoPlanilha formatacaoPlanilha = new FormatacaoPlanilha()
                {
                    NomeAtributo = x.Attribute("NomeAtributo").Value,
                    Coluna = int.Parse(x.Attribute("Coluna").Value)
                };

                lista.Add(formatacaoPlanilha);
            }
            return lista;
        }



        #endregion
    }
}