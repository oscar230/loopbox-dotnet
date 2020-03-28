using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            Console.WriteLine(loopbox.IsLoaded());
            Console.WriteLine("Tracks: " + loopbox.GetTracksCount());
        }
    }
}
