using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegradorVippWebService
{
    [XmlRoot(ElementName = "NotasFiscais")]
    public class NotasFiscais
    {
        [XmlElement(ElementName = "NotaFiscal")]
        public NotaFiscal NotaFiscal { get; set; }
    }
}
