using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox.Library
{
    public interface ILibrary
    {
        string Version { get; set; }
        IProduct Product { get; set; }
        ICollection Collection { get; set; }
        IPlaylist Playlists { get; set; }
    }
}
