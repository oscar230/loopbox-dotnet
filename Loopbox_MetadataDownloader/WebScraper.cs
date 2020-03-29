using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace Loopbox_MetadataDownloader
{
    public class WebScraper
    {
        private string url;
        public string Url { set => url = value; }
        private HtmlDocument document = null;
        public WebScraper(string url)
        {
            this.url = url;
            Load();
        }
        private void Load() => document = new HtmlWeb().Load(url);
        public bool Contains(string text) => document.DocumentNode.Descendants().ToString().Contains("text");
        public HtmlDocument GetHtmlDocument() => document;
        public static void DownloadImage(string filename, string url) => new WebClient().DownloadFile(url, filename);
    }
}
