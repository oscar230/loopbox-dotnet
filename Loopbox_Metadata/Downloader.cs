using System;
using System.Collections.Generic;
using System.IO;
using Loopbox_Metadata.DataSources;

namespace Loopbox_Metadata
{
    public class Downloader
    {
        public const string _albumart_directory = "albumart";
        List<IMetadataRetreiver> sources;
        public Downloader(string artist, string track)
        {
            this.sources = new List<IMetadataRetreiver>()
            {
                new Discogs(artist, track)
            };
        Directory.CreateDirectory(_albumart_directory);
        }

        public bool Found()
        {
            foreach (IMetadataRetreiver source in sources)
                if (source.Found)
                    return true;
            return false;
        }
    }
}
