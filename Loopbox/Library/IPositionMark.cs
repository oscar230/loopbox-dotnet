using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox.Library
{
    public interface IPositionMark
    {
        string Name { get; set; }
        int Type { get; set; }
        decimal Start { get; set; }
        decimal End { get; set; }
        int Num { get; set; }
        string Color { get; set; }
    }
}
