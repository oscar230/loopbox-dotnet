using Loopbox.AlbumArt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Loopbox.RekordboxXML
{
    [Serializable]
    [XmlRoot("TRACK")]
    public class Track
    {
        [XmlAttribute("TrackID")]
        public int trackId; // RekordBox index
        [XmlAttribute("Key")]
        public int key = int.MinValue; // Same as trackId, to represent track in playlists
        [XmlAttribute("Name")]
        public string name;
        [XmlAttribute("Artist")]
        public string artist;
        [XmlAttribute("Composer")]
        public string composer;
        [XmlAttribute("Album")]
        public string album;
        [XmlAttribute("Grouping")]
        public string grouping;
        [XmlAttribute("Genre")]
        public string genre;
        [XmlAttribute("Kind")]
        public string kind;
        [XmlAttribute("Size")]
        public int size;
        [XmlAttribute("TotalTime")]
        public int totaltime;
        [XmlAttribute("DiscNumber")]
        public int discnumber;
        [XmlAttribute("TrackNumber")]
        public int tracknumber;
        [XmlAttribute("Year")]
        public int year;
        [XmlAttribute("AverageBpm")]
        public decimal averagebpm;
        [XmlAttribute("DateAdded")]
        public string dateadded;
        [XmlAttribute("BitRate")]
        public int bitrate;
        [XmlAttribute("SampleRate")]
        public int samplerate;
        [XmlAttribute("Comments")]
        public string comments;
        [XmlAttribute("PlayCount")]
        public int playcount;
        [XmlAttribute("Rating")]
        public int rating;
        [XmlAttribute("Location")]
        public string location;
        [XmlAttribute("Remixer")]
        public string remixer;
        [XmlAttribute("Tonality")]
        public string tonality;
        [XmlAttribute("Label")]
        public string label;
        [XmlAttribute("Mix")]
        public string mix;
        [XmlElement("TEMPO")]
        public List<Tempo> tempos;
        [XmlElement("POSITION_MARK")]
        public List<PositionMark> position_Marks;
        public int Id { get => trackId > 0 ? trackId : key; set => throw new NotImplementedException(); }
        public string Name { get => name; set => throw new NotImplementedException(); }
        public string Artist { get => artist; set => throw new NotImplementedException(); }
        public string Composer { get => composer; set => throw new NotImplementedException(); }
        public string Album { get => album; set => throw new NotImplementedException(); }
        public string Grouping { get => grouping; set => throw new NotImplementedException(); }
        public string Genre { get => genre; set => throw new NotImplementedException(); }
        public string Kind { get => kind; set => throw new NotImplementedException(); }
        public int Size { get => size; set => throw new NotImplementedException(); }
        public int Totaltime { get => totaltime; set => throw new NotImplementedException(); }
        public int Discnumber { get => discnumber; set => throw new NotImplementedException(); }
        public int Tracknumber { get => tracknumber; set => throw new NotImplementedException(); }
        public int Year { get => year; set => throw new NotImplementedException(); }
        public decimal Averagebpm { get => averagebpm; set => throw new NotImplementedException(); }
        public string Dateadded { get => dateadded; set => throw new NotImplementedException(); }
        public int Bitrate { get => bitrate; set => throw new NotImplementedException(); }
        public int Samplerate { get => samplerate; set => throw new NotImplementedException(); }
        public string Comments { get => comments; set => throw new NotImplementedException(); }
        public int Playcount { get => playcount; set => throw new NotImplementedException(); }
        public int Rating { get => rating; set => throw new NotImplementedException(); }
        public string Location { get => Rekordbox.ConvertLocation(location); set => throw new NotImplementedException(); }
        public string Remixer { get => remixer; set => throw new NotImplementedException(); }
        public string Tonality { get => tonality; set => throw new NotImplementedException(); }
        public string Label { get => label; set => throw new NotImplementedException(); }
        public string Mix { get => mix; set => throw new NotImplementedException(); }
        public bool Exists => new FileInfo(Location).Exists;
        private FileInfo GetFile => Exists ? new FileInfo(Location) : null;
        public BitmapImage Art => GetFile.Extension.Contains("mp3") ? mp3.Get(GetFile) : null;
        public bool ArtExists => GetFile.Extension.Contains("mp3") ? mp3.Exists(GetFile) : false;
        public bool MetaComplete => string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(Artist) && string.IsNullOrEmpty(Genre) && string.IsNullOrEmpty(Label) ? false : true;
    }
}
