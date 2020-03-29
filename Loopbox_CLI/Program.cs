using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Loopbox;

namespace Loopbox_CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            LoopboxLib loopbox = new LoopboxLib();
            if (args.Length > 0 && new FileInfo(args[0]).Exists)
                loopbox.Load(args[0]);
            else
                loopbox.Load("C:/rekordbox.xml");
            Menu.Print(loopbox.ToString());
            Menu.PrintAttribute("Tracks in library", loopbox.GetTracksCount().ToString());
            Menu.PrintAttribute("Playlists in library", loopbox.GetAllPlaylistsCount().ToString());
            //loopbox.DownloadMetadata(loopbox.GetTracks()[0]);
            //loopbox.DownloadMetadata(loopbox.GetTracks()[123]);
            //loopbox.DownloadMetadata(loopbox.GetTracks()[333]);
            //loopbox.DownloadMetadata(loopbox.GetTracks()[1000]);
            //loopbox.DownloadMetadata(loopbox.GetTracks()[1010]);
            loopbox.DownloadMetadata(loopbox.GetTracks()[2000]);
            Thread.Sleep(5 * 1000);
        }
    }

    static class Menu
    {
        public static void Print(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }
        public static void PrintAttribute(string title, string value)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(title + " : ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(value);
        }
    }
}
