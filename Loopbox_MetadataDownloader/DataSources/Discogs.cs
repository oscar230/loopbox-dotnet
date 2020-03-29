using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Loopbox_MetadataDownloader.DataSources
{
    class Discogs : IMetadataRetreiver, WebScraper
    {
        // TODD implement discogs API https://www.discogs.com/developers/
        public FileInfo GetAlbumArt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GetTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GetArtist { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime GetReleaseDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal GetBpm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GetGenre { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GetLabel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
