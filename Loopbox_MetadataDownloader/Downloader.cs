using System;
using System.Collections.Generic;
using System.IO;
using Loopbox_MetadataDownloader.DataSources;

namespace Loopbox_MetadataDownloader
{
    public class Downloader
    {
        public const string _albumart_directory = "albumart";
        List<IMetadataRetreiver> sources;
        public Downloader(string artist, string track)
        {
            Directory.CreateDirectory(_albumart_directory);
            SetupSources(artist, track);
        }
        private void SetupSources(string artist, string track)
        {
            sources.Add(new Discogs(artist, track)); // Discogs seems sufficient add more if metadata seems a bit off
            //sources.Add(new Spotify());
            //sources.Add(new Beatport());
            //sources.Add(new Deezer());
        }
    }
}
