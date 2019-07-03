using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegradorVippWebService
{
    [XmlRoot(ElementName = "Servico")]
    public class Servico
    {
        [XmlElement(ElementName = "ServicoECT")]
        public string ServicoECT { get; set; }
    }
}
