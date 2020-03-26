using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox.Library
{
    public interface IProduct
    {
        string Name { get; set; }
        string Version { get; set; }
        string Company { get; set; }
    }
}
