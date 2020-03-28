using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    static class SetupXML
    {
        private const string _dir = @"rekordboxtest";
        public static string Get4Tracks => Path.Combine(_dir, @"4tracks.xml");
        public static string GetLarge => Path.Combine(_dir, @"large.xml");
        public static string Get2Playlists => Path.Combine(_dir, @"2playlists.xml");
    }
}
