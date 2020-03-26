using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox.Library
{
    public interface ITempo
    {
        decimal Inizio { get; set; }
        decimal Bpm { get; set; }
        string Metro { get; set; }
        int Battito { get; set; }
    }
}
