using IntegradorWebService.Visualset.IntegradorWebService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace IntegradorWebService.Visualset.IntegradorWebService.Services.ExcelServices
{
    class CarregaFormatacao
    {
        #region Validacao
        public static List<FormatacaoPlanilha> ListarFormatacao()
        {

            try
            {
                List<FormatacaoPlanilha> lista = new List<FormatacaoPlanilha>();                
                string layout = Properties.Settings.Default.Layout;
                char[] charsToTrim = { '"', ' ', '\\' };
                layout = layout.Trim(charsToTrim);
                byte[] data = Convert.FromBase64String(layout.Trim());
                string decodedString = Encoding.UTF8.GetString(data);
                XElement xmlElement = XElement.Parse(decodedString);
                var xml = xmlElement;
                
                var q = from e in xml.Descendants("FormatacaoPlanilha")
                        select new FormatacaoPlanilha()
                        {
                            NomeAtributo = e.Element("NomeAtributo").Value,
                            Coluna = int.Parse(e.Element("Coluna").Value),
                            Inicio = int.Parse(e.Element("Inicio").Value),
                            Tamanho = int.Parse(e.Element("Tamanho").Value),
                            Delimitador = e.Element("Delimitador").Value
                        };


                foreach (var k in q)
                {
                    lista.Add(k);
                }

                return lista;
            }
            catch (FormatException e)
            {
                throw e; 
            }

        }
        #endregion
    }
}
