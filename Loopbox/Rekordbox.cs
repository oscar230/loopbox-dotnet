using Loopbox.RekordboxXML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox
{
    class Rekordbox
    {
        private Library library;

        public Rekordbox(string pathname) => library = Load(pathname);
        public Library Library => library;
        public static string ConvertLocation(string rekordboxurl) => new System.Uri(rekordboxurl.Replace(@"file://localhost/", @"file:///").Replace(@"#", @"%23").Replace(@".", @"%2E")).LocalPath;
        private static Library Load(string pathname)
        {
            if (Path.GetExtension(pathname).ToLower().Contains("xml"))
                return LoadXML(pathname);
            else if (Path.GetExtension(pathname).ToLower().Contains(".zip"))
                throw new NotImplementedException();
            else
                throw new FileNotFoundException();
        }
        private static Library LoadXML(string pathname)
        {
            TextReader reader = new StreamReader(pathname);
            XmlSerializer deserializer = new XmlSerializer(typeof(RekordboxXML.Library));
            Library library = (RekordboxXML.Library)deserializer.Deserialize(reader);
            reader.Close();
            return library;
        }
    }
}
