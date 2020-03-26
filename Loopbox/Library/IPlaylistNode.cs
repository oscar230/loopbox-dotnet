using Loopbox.Library.RekordboxXML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox.Library
{
    public interface IPlaylistNode
    {
        int Type { get; set; }
        string Name { get; set; }
        int Entries { get; set; }
        int KeyType { get; set; }
        List<IPlaylistNode> Nodes { get; set; }
        List<ITrack> Tracks { get; set; }
        bool IsDirectory { get; set; }
        bool IsPlaylist { get; set; }
        List<IPlaylistNode> Playlists { get; set; }
        List<IPlaylistNode> Directories { get; set; }
    }
}
