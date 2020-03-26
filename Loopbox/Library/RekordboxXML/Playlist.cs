using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.Library.RekordboxXML
{
    [Serializable]
    [XmlRoot("PLAYLISTS")]
    public class Playlist : IPlaylist
    {
        [XmlElement("NODE")]
        public List<PlaylistNode> playlistNodes; // Top of node tree is name=ROOT
        public List<IPlaylistNode> PlaylistNodes { get => new List<IPlaylistNode>(playlistNodes); set => throw new NotImplementedException(); }
    }
}
