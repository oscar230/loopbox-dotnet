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

        public string Name { get => Name; set => throw new NotImplementedException(); }
        public string Version { get => Version; set => throw new NotImplementedException(); }
        public string Company { get => Company; set => throw new NotImplementedException(); }
    }
}
