using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox.Library
{
    public interface ICollection
    {
        int Entries { get; set; }
        List<ITrack> Tracks { get; set; }
    }
}
