using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Loopbox_Metadata.DataSources
{
    class Spotify : IMetadataRetreiver
    {
        public FileInfo GetAlbumArt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GetTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GetArtist { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime GetReleaseDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public decimal GetBpm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GetGenre { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GetLabel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Found => throw new NotImplementedException();
        public DataSource GetSource => new DataSource { name = "Discogs", owner = "Zink Media", url = "www.discogs.com" };
    }
}
