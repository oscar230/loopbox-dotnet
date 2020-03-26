using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loopbox.Library
{
    public interface ITrack
    {
        int Id { get; set; }
        string Name { get; set; }
        string Artist { get; set; }
        string Composer { get; set; }
        string Album { get; set; }
        string Grouping { get; set; }
        string Genre { get; set; }
        string Kind { get; set; }
        int Size { get; set; }
        int Totaltime { get; set; }
        int Discnumber { get; set; }
        int Tracknumber { get; set; }
        int Year { get; set; }
        decimal Averagebpm { get; set; }
        string Dateadded { get; set; }
        int Bitrate { get; set; }
        int Samplerate { get; set; }
        string Comments { get; set; }
        int Playcount { get; set; }
        int Rating { get; set; }
        string Location { get; set; }
        string Remixer { get; set; }
        string Tonality { get; set; }
        string Label { get; set; }
        string Mix { get; set; }
        bool Exists { get; }
    }
}
