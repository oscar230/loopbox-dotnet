using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.RekordboxXML
{
    [Serializable]
    [XmlRoot("COLLECTION")]
    public class Collection
    {
        [XmlAttribute("Entries")]
        public int entries;
        [XmlElement("TRACK")]
        public List<Track> tracks;

        public int Entries { get => entries; set => throw new NotImplementedException(); }
        public List<Track> Tracks { get => new List<Track>(tracks); set => throw new NotImplementedException(); }
    }
}
