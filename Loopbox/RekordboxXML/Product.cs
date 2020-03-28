using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.RekordboxXML
{
    [Serializable]
    [XmlRoot("PRODUCT")]
    public class Product
    {
        [XmlAttribute("Name")]
        public string name;
        [XmlAttribute("Version")]
        public string version;
        [XmlAttribute("Company")]
        public string company;

        public string Name { get => name; set => throw new NotImplementedException(); }
        public string Version { get => version; set => throw new NotImplementedException(); }
        public string Company { get => company; set => throw new NotImplementedException(); }
    }
}
