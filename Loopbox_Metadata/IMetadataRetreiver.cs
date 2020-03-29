using System;
using System.Drawing;
using System.IO;

namespace Loopbox_Metadata
{
    public struct Metadata
    {
        public FileInfo artfile;
        public string title;
        public string artist;
        public DateTime releasedate;
        public decimal bpm;
        public string genre;
        public string label;
    }
    public struct DataSource
    {
        public string name;
        public string owner;
        public string url;
    }
    public interface IMetadataRetreiver
    {
        DataSource GetSource { get; }
        FileInfo GetAlbumArt { get; set; }
        string GetTitle { get; set; }
        string GetArtist { get; set; }
        DateTime GetReleaseDate { get; set; }
        decimal GetBpm { get; set; }
        string GetGenre { get; set; }
        string GetLabel { get; set; }
        bool Found { get;  }
    }
}
