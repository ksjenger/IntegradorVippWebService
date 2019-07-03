using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegradorVippWebService
{
    [XmlRoot(ElementName = "VolumeObjeto")]
    public class VolumeObjeto
    {
        [XmlElement(ElementName = "Peso")]
        public string Peso { get; set; }
        [XmlElement(ElementName = "Cubagem")]
        public string Cubagem { get; set; }
        [XmlElement(ElementName = "Altura")]
        public string Altura { get; set; }
        [XmlElement(ElementName = "Largura")]
        public string Largura { get; set; }
        [XmlElement(ElementName = "Comprimento")]
        public string Comprimento { get; set; }
        [XmlElement(ElementName = "ContaLote")]
        public string ContaLote { get; set; }
        [XmlElement(ElementName = "ChaveRoteamento")]
        public string ChaveRoteamento { get; set; }
        [XmlElement(ElementName = "CodigoBarraVolume")]
        public string CodigoBarraVolume { get; set; }
        [XmlElement(ElementName = "CodigoBarraCliente")]
        public string CodigoBarraCliente { get; set; }
        [XmlElement(ElementName = "ObservacaoVisual")]
        public string ObservacaoVisual { get; set; }
        [XmlElement(ElementName = "ObservacaoQuatro")]
        public string ObservacaoQuatro { get; set; }
        [XmlElement(ElementName = "ObservacaoCinco")]
        public string ObservacaoCinco { get; set; }
        [XmlElement(ElementName = "PosicaoVolume")]
        public string PosicaoVolume { get; set; }
        [XmlElement(ElementName = "Conteudo")]
        public string Conteudo { get; set; }
        [XmlElement(ElementName = "ValorDeclarado")]
        public string ValorDeclarado { get; set; }
        [XmlElement(ElementName = "AdicionaisVolume")]
        public string AdicionaisVolume { get; set; }
        [XmlElement(ElementName = "VlrACobrar")]
        public string VlrACobrar { get; set; }
        [XmlElement(ElementName = "Etiqueta")]
        public string Etiqueta { get; set; }
        [XmlElement(ElementName = "ValorTarifa")]
        public string ValorTarifa { get; set; }
        [XmlElement(ElementName = "ValorAdicionais")]
        public string ValorAdicionais { get; set; }
        [XmlElement(ElementName = "ValorPostagem")]
        public string ValorPostagem { get; set; }
        [XmlElement(ElementName = "StEntregaSabado")]
        public string StEntregaSabado { get; set; }
        [XmlElement(ElementName = "StEntregaDomiciliar")]
        public string StEntregaDomiciliar { get; set; }
        [XmlElement(ElementName = "DiasUteisPrazo")]
        public string DiasUteisPrazo { get; set; }
    }
}
