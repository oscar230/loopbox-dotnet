using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.Library.RekordboxXML
{
    [Serializable]
    [XmlRoot("COLLECTION")]
    class Collection : ICollection
    {
        [XmlAttribute("Entries")]
        public int entries;
        [XmlElement("TRACK")]
        public List<ITrack> tracks;

        public int Entries { get => entries; set => throw new NotImplementedException(); }
        public List<ITrack> Tracks { get => tracks; set => throw new NotImplementedException(); }
    }
}
