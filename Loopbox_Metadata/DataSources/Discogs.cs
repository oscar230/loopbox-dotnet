using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Loopbox_Metadata.DataSources
{
    class Discogs : IMetadataRetreiver
    {
        private const string base_url = @"https://www.discogs.com/";
        private const string search_url = base_url + @"search/" + @"?";
        private string[] args_url = { @"type=release", @"&artist=", @"&track=" };
        private WebScraper scraper;
        private bool found = false;
        private Metadata metadata;
        public Discogs(string artist, string track)
        {
            Debug.WriteLine("Discogs looking for artist: " + artist + " with track: " + track);
            args_url[1] += artist;
            args_url[2] += track;
            scraper = new WebScraper(SetupURL);
            if (found = scraper.Contains("We couldn't find anything in the Discogs database matching your search criteria."))
                Debug.WriteLine("\tDiscogs could not find that.");
            else
            {
                string link = base_url + scraper.GetHtmlDocument().DocumentNode.Descendants("a").Where(a => a.HasClass("thumbnail_link")).Select(a => a.GetAttributeValue("href", null)).FirstOrDefault();
                Debug.WriteLine("\tDiscogs found first track at: " + link);
                scraper = new WebScraper(link);

                string hash = GetHashCode().ToString();
                string inital_filepath = Downloader._albumart_directory + "/" + hash.ToString();
                string image_url = new Uri(scraper.GetHtmlDocument().DocumentNode.Descendants("img").Where(i => i.ParentNode.HasClass("thumbnail_center")).Select(e => e.GetAttributeValue("src", null)).Where(s => !String.IsNullOrEmpty(s)).FirstOrDefault()).ToString();
                metadata.artfile = WebScraper.DownloadImage(inital_filepath, image_url);
                Debug.WriteLine("\tFound album art, stored to: " + metadata.artfile.FullName);

                var profile = scraper.GetHtmlDocument().DocumentNode.Descendants("div").Where(d => d.HasClass("profile"));
                Debug.WriteLine("\tFound profile: " + profile.ToString());
            }
        }
        public bool Found { get => found; }
        private string SetupURL => search_url + SetupArguments();

        private string SetupArguments()
        {
            string output = string.Empty;
            foreach (string arg in args_url)
                output += arg.Replace(" ", "+").Replace(",", "+").Replace("-", "+").Replace("(", string.Empty).Replace(")", string.Empty).Replace("[", string.Empty).Replace("]", string.Empty); ;
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
        public DataSource GetSource => new DataSource { name="Discogs", owner= "Zink Media", url= "www.discogs.com" };
    }
}
