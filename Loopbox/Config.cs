using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Loopbox
{
    public class Config
    {
        //
        // Data structures to replicate RekordBox XML config
        //
        public class Library // Called Dj_Playlists in XML
        {
            string version;
            Product product;
            Collection collection;
            Playlists playlists;

            public Library(XmlNode node)
            {
                ElementDebug(node);

                version = node.Attributes["Version"].Value;
                product = new Product(node.SelectSingleNode("PRODUCT"));
                collection = new Collection(node.SelectSingleNode("COLLECTION"));
                playlists = new Playlists(node.SelectSingleNode("PLAYLISTS"));
            }

            public List<Track> GetTracks() => collection.tracks;
            public override string ToString() => "Library version " + version;
        }
        public class Product
        {
            string name;
            string version;
            string company;

            public Product(XmlNode node)
            {
                ElementDebug(node);

                name = node.Attributes["Name"].Value;
                version = node.Attributes["Version"].Value;
                company = node.Attributes["Company"].Value;
            }

            public override string ToString() => "Product " + name + " version " + version + " by " + company;
        }
        public class Collection
        {
            int entries;
            public List<Track> tracks;

            public Collection(XmlNode node)
            {
                tracks = new List<Track>();

                ElementDebug(node);

                entries = int.Parse(node.Attributes["Entries"].Value);
                foreach (XmlNode n in node.SelectNodes("TRACK"))
                    tracks.Add(new Track(n));
            }

            public override string ToString() => "Collection containing " + entries + " entries.";
        }
        public class Track
        {
            int trackId; // RekordBox index
            string name;
            string artist;
            string composer;
            string album;
            string grouping;
            string genre;
            string kind;
            int size;
            int totaltime;
            int discnumber;
            int tracknumber;
            int year;
            string averagebpm;
            DateTime dateadded;
            int bitrate;
            int samplerate;
            string comments;
            int playcount;
            int rating;
            FileInfo fileInfo;
            string remixer;
            string tonality;
            string label;
            string mix;
            List<Tempo> tempos;
            List<Position_Mark> position_Marks;

            public Track(XmlNode node)
            {
                tempos = new List<Tempo>();
                position_Marks = new List<Position_Mark>();

                ElementDebug(node);

                trackId = int.Parse(node.Attributes["TrackID"].Value);
                name = node.Attributes["Name"].Value;
                artist = node.Attributes["Artist"].Value;
                composer = node.Attributes["Composer"].Value;
                album = node.Attributes["Album"].Value;
                grouping = node.Attributes["Grouping"].Value;
                genre = node.Attributes["Genre"].Value;
                kind = node.Attributes["Kind"].Value;
                size = int.Parse(node.Attributes["Size"].Value);
                totaltime = int.Parse(node.Attributes["TotalTime"].Value);
                discnumber = int.Parse(node.Attributes["DiscNumber"].Value);
                tracknumber = int.Parse(node.Attributes["TrackNumber"].Value);
                year = int.Parse(node.Attributes["Year"].Value);
                averagebpm = node.Attributes["AverageBpm"].Value;
                dateadded = DateTime.Parse(node.Attributes["DateAdded"].Value);
                bitrate = int.Parse(node.Attributes["BitRate"].Value);
                samplerate = int.Parse(node.Attributes["SampleRate"].Value);
                comments = node.Attributes["Comments"].Value;
                samplerate = int.Parse(node.Attributes["SampleRate"].Value);
                fileInfo = new FileInfo(PioneerFilepathParser(node.Attributes["Location"].Value));
                comments = node.Attributes["Remixer"].Value;
                comments = node.Attributes["Tonality"].Value;
                comments = node.Attributes["Label"].Value;
                comments = node.Attributes["Mix"].Value;

                foreach (XmlNode n in node.SelectNodes("TEMPO"))
                    tempos.Add(new Tempo(n));

                foreach (XmlNode n in node.SelectNodes("POSITION_MARK"))
                    position_Marks.Add(new Position_Mark(n));
            }

            public override string ToString() => "Track #" + trackId + " - " + name;
        }
        public class Tempo
        {
            string inizio; // Beginning (Italian)
            string bpm;
            string metro; // Meter (Italian)
            int battito; // Beat (Italian)

            public Tempo(XmlNode node)
            {
                ElementDebug(node);

                inizio = node.Attributes["Inizio"].Value;
                bpm = node.Attributes["Bpm"].Value;
                metro = node.Attributes["Metro"].Value;
                battito = int.Parse(node.Attributes["Battito"].Value);
            }

            public override string ToString() => "Tempo starts at " + inizio + " with a bpm of " + bpm;
        }
        // Cues
        public class Position_Mark
        {
            string name;
            int type;
            string start;
            int num; // -1 for cue else hot cue

            string end = null; // For loops
            int red = 0; // Display color for interfaces
            int green = 0; // Display color for interfaces
            int blue = 0; // Display color for interfaces

            public Position_Mark(XmlNode node)
            {
                ElementDebug(node);

                name = node.Attributes["Name"].Value;
                type = int.Parse(node.Attributes["Type"].Value);
                start = node.Attributes["Start"].Value;
                num = int.Parse(node.Attributes["Num"].Value);

                if (node.Attributes["End"] != null) end = node.Attributes["End"].Value;
                if (node.Attributes["Red"] != null) red = int.Parse(node.Attributes["Red"].Value);
                if (node.Attributes["Green"] != null) green = int.Parse(node.Attributes["Green"].Value);
                if (node.Attributes["Blue"] != null) blue = int.Parse(node.Attributes["Blue"].Value);
            }

            public override string ToString() => "Position mark at " + start;
        }
        public class Playlists
        {
            List<PlaylistNode> playlistNodes; // Top of node tree is name=ROOT

            public Playlists(XmlNode node)
            {
                playlistNodes = new List<PlaylistNode>();

                ElementDebug(node);

                foreach (XmlNode n in node.SelectNodes("NDOE"))
                    playlistNodes.Add(new PlaylistNode(n));
            }

            public override string ToString() => "Playlists containing " + playlistNodes.Count + " playlist nodes.";
        }
        // Nodes in playlists
        public class PlaylistNode
        {
            int type; // 0 directory, 1 playlist
            string name;
            int count; // Amount of nodes if type is 0
            int entries; // Amount of tracks if type is 1
            int keyType;
            List<PlaylistNode> nodes;
            List<Track> tracks; //Tracks by TrackId

            public PlaylistNode(XmlNode node)
            {
                nodes = new List<PlaylistNode>();
                tracks = new List<Track>();

                ElementDebug(node);

                type = int.Parse(node.Attributes["Type"].Value);
                name = node.Attributes["Name"].Value;
                count = int.Parse(node.Attributes["Count"].Value);
                entries = int.Parse(node.Attributes["Entries"].Value);
                keyType = int.Parse(node.Attributes["KeyType"].Value);

                foreach (XmlNode n in node.SelectNodes("NDOE"))
                    nodes.Add(new PlaylistNode(n));

                foreach (XmlNode n in node.SelectNodes("TRACK"))
                    tracks.Add(new Track(n));
            }

            public override string ToString() => type == 0 ? "This is a directory for playlists." : "This is playlist.";
        }

        private Library library; // The config

        // Constructor
        public Config(string filepath)
        {
            XmlDocument document;
            try
            {
                document = new XmlDocument();
                document.Load(filepath);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
            this.library = new Library(document.DocumentElement);
        }

        public Library Get() => library;

        private static void ElementDebug(XmlNode node)
        {
            Debug.WriteLine("XML at " + node.Name + " parent of " + node.ChildNodes.Count + " nodes.");
            //foreach (XmlNode child in node.ChildNodes)
            //    Debug.WriteLine("\t-> " + child.Name);
        }

        //Source: https://stackoverflow.com/questions/11465191/how-to-match-two-paths-pointing-to-the-same-file/11465376#11465376
        private static string PioneerFilepathParser(string filepath)
        {
            return filepath.Replace("file://localhost/", "").Replace("%20", " ").Replace("/", @"\").Replace(@"\\", @"\");
        }
    }
}