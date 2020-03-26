using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.Library.RekordboxXML
{
    [Serializable]
    [XmlRoot("POSITION_MARK")]
    class PositionMark : IPositionMark
    {
        [XmlAttribute("Name")]
        public string name;
        [XmlAttribute("Type")]
        public int type;
        [XmlAttribute("Start")]
        public decimal start;
        [XmlAttribute("End")]
        public decimal end; // For loops
        [XmlAttribute("Num")]
        public int num; // -1 for cue else hot cue
        [XmlAttribute("Red")]
        public int red; // Display color for interfaces
        [XmlAttribute("Green")]
        public int green; // Display color for interfaces
        [XmlAttribute("Blue")]
        public int blue; // Display color for interfaces

        public string Name { get => name; set => throw new NotImplementedException(); }
        public int Type { get => type; set => throw new NotImplementedException(); }
        public decimal Start { get => start; set => throw new NotImplementedException(); }
        public decimal End { get => end; set => throw new NotImplementedException(); }
        public int Num { get => num; set => throw new NotImplementedException(); }
        public string Color { get {
                System.Drawing.Color c = System.Drawing.Color.FromArgb(red, green, blue);
                return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            } set => throw new NotImplementedException(); }
    }
}
