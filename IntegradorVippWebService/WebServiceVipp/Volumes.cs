using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegradorVippWebService
{
    [XmlRoot(ElementName = "Volumes")]
    public class Volumes
    {
        [XmlElement(ElementName = "VolumeObjeto")]
        public VolumeObjeto VolumeObjeto { get; set; }
    }
}
