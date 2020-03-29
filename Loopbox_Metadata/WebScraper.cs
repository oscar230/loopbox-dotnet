using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using System.Management.Automation;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Loopbox_Metadata
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
        private void Load()
        {
            Debug.WriteLine("WebScraper loading: " + url);
            if (url != null)
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                    document = new HtmlWeb().Load(url);
                else
                    Debug.WriteLine("URI bad format: " + url);
        }

        public bool Contains(string text)
        {
            var b = false;
            foreach (HtmlNode node in document.DocumentNode.Descendants())
                if (node.InnerHtml.Contains(text))
                    b = true;
            Debug.WriteLine("WebScraper Contains: " + text + " = " + b);
            return b;
        }

        public HtmlDocument GetHtmlDocument() => document;
        public static FileInfo DownloadImage(string filename, string url)
        {
            var strs = url.Split('.');
            filename = filename + "." + strs[strs.Length - 1];
            Debug.WriteLine("WebScraper download image from: " + url + " save to: " + filename);
            string program = "powershell.exe";
            string command = "wget -v -o \'" + filename + "\' \'" + url + "\'";
            Debug.WriteLine("Starting " + program + " with command:\n\t" + command);

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = program;
            psi.Arguments = command;
            Process.Start(psi);

            var file = new FileInfo(filename);
            return file;
        }
    }
}