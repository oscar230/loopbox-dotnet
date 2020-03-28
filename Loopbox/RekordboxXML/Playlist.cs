using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.RekordboxXML
{
    [Serializable]
    [XmlRoot("PLAYLISTS")]
    public class Playlist
    {
        [XmlElement("NODE")]
        public List<PlaylistNode> playlistNodes; // Top of node tree is name=ROOT
        public List<PlaylistNode> PlaylistNodes { get => new List<PlaylistNode>(playlistNodes); set => throw new NotImplementedException(); }
    }
}
