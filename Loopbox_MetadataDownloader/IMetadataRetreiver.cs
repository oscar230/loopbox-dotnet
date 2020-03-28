using System;
using System.Drawing;
using System.IO;

namespace Loopbox_MetadataDownloader
{
    public interface IMetadataRetreiver
    {
        FileInfo GetAlbumArt { get; set; }
        string GetTitle { get; set; }
        string GetArtist { get; set; }
        DateTime GetReleaseDate { get; set; }
        decimal GetBpm { get; set; }
        string GetGenre { get; set; }
        string GetLabel { get; set; }
    }
}
