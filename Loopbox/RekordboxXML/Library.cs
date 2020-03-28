using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.RekordboxXML
{
    [Serializable]
    [XmlRoot("DJ_PLAYLISTS")]
    public class Library
    {
        [XmlAttribute("Version")]
        public string version;
        [XmlElement("PRODUCT")]
        public Product product;
        [XmlElement("COLLECTION")]
        public Collection collection;
        [XmlElement("PLAYLISTS")]
        public Playlist playlists;
        public string Version { get => version; set => throw new NotImplementedException(); }
        public Product Product { get => product; set => throw new NotImplementedException(); }
        public Collection Collection { get => collection; set => throw new NotImplementedException(); }
        public Playlist Playlists { get => playlists; set => throw new NotImplementedException(); }
    }
}
