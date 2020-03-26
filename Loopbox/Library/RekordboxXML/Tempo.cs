using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.Library.RekordboxXML
{
    [Serializable]
    [XmlRoot("TEMPO")]
    class Tempo : ITempo
    {
        [XmlAttribute("Inizio")]
        public decimal inizio; // Beginning (Italian)
        [XmlAttribute("Bpm")]
        public decimal bpm;
        [XmlAttribute("Metro")]
        public string metro; // Meter (Italian)
        [XmlAttribute("Battito")]
        public int battito; // Beat (Italian)

        public decimal Inizio { get => inizio; set => throw new NotImplementedException(); }
        public decimal Bpm { get => bpm; set => throw new NotImplementedException(); }
        public string Metro { get => metro; set => throw new NotImplementedException(); }
        public int Battito { get => battito; set => throw new NotImplementedException(); }
    }
}
