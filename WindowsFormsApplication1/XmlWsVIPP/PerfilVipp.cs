using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace IntegradorWebService.XmlWsVIPP
{

    [XmlRoot(ElementName = "PerfilVipp", Namespace = "http://www.visualset.inf.br/")]
    public class PerfilVipp
    {
        [XmlElement(ElementName = "Usuario", Namespace = "http://www.visualset.inf.br/")]
        public string Usuario { get; set; }
        [XmlElement(ElementName = "Token", Namespace = "http://www.visualset.inf.br/")]
        public string Token { get; set; }
        [XmlElement(ElementName = "IdPerfil", Namespace = "http://www.visualset.inf.br/")]
        public string IdPerfil { get; set; }
    }
}
