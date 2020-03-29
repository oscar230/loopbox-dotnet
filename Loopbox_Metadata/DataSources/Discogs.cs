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

                // TODO fix these scrapers, crash all the time if there is not info present on the web site
                //metadata.label = scraper.GetHtmlDocument().DocumentNode.Descendants("a").Where(a => a.ParentNode.ParentNode.InnerHtml.Contains("Label:") ).FirstOrDefault().InnerHtml;
                //Debug.WriteLine("\tFound label: " + metadata.label);

                //metadata.artist = scraper.GetHtmlDocument().DocumentNode.Descendants("a").Where(a => a.ParentNode.ParentNode.ParentNode.Id.Equals("profile_title")).FirstOrDefault().InnerHtml.Split('(')[0];
                //Debug.WriteLine("\tFound artist: " + metadata.artist);

                //string genre = scraper.GetHtmlDocument().DocumentNode.Descendants("a").Where(a => a.GetAttributes("href").ToList().Where(attr => attr.Value.Contains("/genre/")).Count() > 0).FirstOrDefault().InnerHtml;
                //string style = scraper.GetHtmlDocument().DocumentNode.Descendants("a").Where(a => a.GetAttributes("href").ToList().Where(attr => attr.Value.Contains("/style/")).Count() > 0).FirstOrDefault().InnerHtml;
                //metadata.genre = genre + " - " + style;
                //Debug.WriteLine("\tFound genre: " + metadata.genre);

                //try
                //{
                //    metadata.releasedate = DateTime.Parse(scraper.GetHtmlDocument().DocumentNode.Descendants("a").Where(a => a.GetAttributeValue("href", null).Contains("/search/?decade=")).FirstOrDefault().GetAttributeValue("href", null).Split('=').LastOrDefault().ToString());
                //    Debug.WriteLine("\tFound releasedate: " + metadata.releasedate.ToString());
                //}
                //catch (Exception)
                //{
                //    try
                //    {
                //        metadata.releasedate = DateTime.Parse(scraper.GetHtmlDocument().DocumentNode.Descendants("a").Where(a => a.GetAttributeValue("href", null).Contains("/search/?decade=")).FirstOrDefault().InnerHtml);
                //        Debug.WriteLine("\tFound year: " + metadata.releasedate.ToString());
                //    }
                //    catch (Exception)
                //    {
                //        Debug.WriteLine("\tDid not find any release date or year!");
                //    }
                //}

                //List<string> titles = new List<string>(scraper.GetHtmlDocument().DocumentNode.Descendants("span").Where(a => a.HasClass("tracklist_track_title") && a.InnerHtml.Length > 0).Select(s => s.InnerHtml));
                //Debug.WriteLine("\tFound " + titles.Count + "titles.");
                //foreach (string t in titles)
                //    Debug.WriteLine("\t\t" + t);
                //metadata.title = titles.Max<object>(t => StringsSimilarity(track, t.ToString())).ToString();
            }
        }

        // Credits Marty Neal at Stackoverflow
        // Source: https://stackoverflow.com/questions/6944056/c-sharp-compare-string-similarity
        private int StringsSimilarity(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
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
