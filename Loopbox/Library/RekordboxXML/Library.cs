using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.Library.RekordboxXML
{
    [Serializable]
    [XmlRoot("DJ_PLAYLISTS")]
    public class Library : ILibrary
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
        public IProduct Product { get => product; set => throw new NotImplementedException(); }
        public ICollection Collection { get => collection; set => throw new NotImplementedException(); }
        public IPlaylist Playlists { get => playlists; set => throw new NotImplementedException(); }
    }
}
