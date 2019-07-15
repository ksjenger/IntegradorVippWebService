using IntegradorWebService.XmlWsVIPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IntegradorWebService.XmlWsVIPP
{

    [XmlRoot(ElementName = "Volumes")]
    public class Volumes
    {
        [XmlElement(ElementName = "VolumeObjeto")]
        public VolumeObjeto[] VolumeObjeto { get; set; }

        public Volumes(VolumeObjeto[] volumeObjeto)
        {
            VolumeObjeto = volumeObjeto;
        }

        public Volumes()
        {
        }
    }
}
