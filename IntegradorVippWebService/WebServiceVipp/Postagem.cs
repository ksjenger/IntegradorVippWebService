using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegradorVippWebService
{
    [XmlRoot(ElementName = "Postagem")]
    public class Postagem
    {
        [XmlElement(ElementName = "ContratoEct")]
        public ContratoEct ContratoEct { get; set; }
        [XmlElement(ElementName = "Destinatario")]
        public Destinatario Destinatario { get; set; }
        [XmlElement(ElementName = "Servico")]
        public Servico Servico { get; set; }
        [XmlElement(ElementName = "NotasFiscais")]
        public NotasFiscais NotasFiscais { get; set; }
        [XmlElement(ElementName = "Volumes")]
        public Volumes Volumes { get; set; }
        [XmlElement(ElementName = "ListaErros")]
        public ListaErros ListaErros { get; set; }
        [XmlElement(ElementName = "StatusPostagem")]
        public string StatusPostagem { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
