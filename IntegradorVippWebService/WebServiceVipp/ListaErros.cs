using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegradorVippWebService
{
    [XmlRoot(ElementName = "ListaErros")]
    public class ListaErros
    {
        [XmlElement(ElementName = "Erro")]
        public List<Erro> Erro { get; set; }
    }
}
