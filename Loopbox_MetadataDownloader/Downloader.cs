using System;
using System.Collections.Generic;
using Loopbox_MetadataDownloader.DataSources;

namespace Loopbox_MetadataDownloader
{
    public class Downloader
    {
        List<IMetadataRetreiver> sources;
        public Downloader(string artist )
        {
            SetupSources();
        }
        private void SetupSources()
        {
            sources.Add(new Discogs());
            sources.Add(new Spotify());
            sources.Add(new Beatport());
            sources.Add(new Deezer());
        }
    }
}
