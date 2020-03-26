using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox.Library
{
    public interface IPlaylist
    {
        List<IPlaylistNode> PlaylistNodes { get; set; }
    }
}
