using System;
using System.Drawing;
using System.IO;

namespace Loopbox_MetadataDownloader
{
    public struct Metadata
    {
        public FileInfo file;
        public string title;
        public string artist;
        public DateTime releasedate;
        public decimal bpm;
        public string genre;
        public string label;
    }
    public interface IMetadataRetreiver
    {
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
