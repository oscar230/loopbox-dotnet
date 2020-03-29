using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Loopbox_MetadataDownloader.DataSources
{
    class Discogs : IMetadataRetreiver
    {
        private string base_url = @"https://www.discogs.com/search/" + @"&";
        private List<string> args_url = new List<string>() { @"type=release", @"artist=", @"track=" };
        private WebScraper scraper;
        private bool found = false;
        private Metadata metadata;
        public Discogs(string artist, string track)
        {
            Debug.WriteLine("Discogs looking for artist: " + artist + " with track: " + track);
            args_url[1].Concat(artist).ToString();
            args_url[2].Concat(track).ToString();
            scraper = new WebScraper(SetupURL());
            if (found = scraper.Contains("We couldn't find anything in the Discogs database matching your search criteria."))
                Debug.WriteLine("\tDiscogs could not find that.");
            else
            {
                string link = scraper.GetHtmlDocument().DocumentNode.Descendants("a").Where(a => a.HasClass("thumbnail_link")).Select(a => a.GetAttributeValue("href", null)).FirstOrDefault();
                Debug.WriteLine("\tDiscogs found first track at: " + link);
                scraper = new WebScraper(link);
                string hash = GetHashCode().ToString();
                string filepath = Downloader._albumart_directory.Concat(hash).ToString();
                WebScraper.DownloadImage(filepath, scraper.GetHtmlDocument().DocumentNode.Descendants("img").Where(i => i.ParentNode.HasClass("thumbnail_center")).Select(i => i.GetAttributeValue("src", null)).FirstOrDefault());
                metadata.file = new FileInfo(filepath);
                Debug.WriteLine("\tFound album art, stored to: " + metadata.file.FullName);
                var profile = scraper.GetHtmlDocument().DocumentNode.Descendants("div").Where(d => d.HasClass("profile"));
                Debug.WriteLine("\tFound profile: " + profile.ToString());
            }
        }
        public bool Found { get => found; }
        private string SetupURL() => base_url.Concat(SetupArguments()).ToString();
        private string SetupArguments()
        {
            string output = string.Empty;
            foreach (string arg in args_url)
                output.Concat(arg.Replace(" ", "+").Concat("&")).ToString().Replace(",", "+").Replace("-", "+").ToString();
            return output;
        }

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
