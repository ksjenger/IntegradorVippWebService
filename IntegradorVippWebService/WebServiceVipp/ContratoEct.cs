using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegradorVippWebService
{
    [XmlRoot(ElementName = "ContratoEct")]
    public class ContratoEct
    {
        [XmlElement(ElementName = "NrContrato")]
        public string NrContrato { get; set; }
        [XmlElement(ElementName = "CodigoAdministrativo")]
        public string CodigoAdministrativo { get; set; }
        [XmlElement(ElementName = "NrCartao")]
        public string NrCartao { get; set; }
    }
}
