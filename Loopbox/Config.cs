using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Loopbox
{
    public class Config
    {
        [Serializable]
        [XmlRoot("DJ_PLAYLISTS")]
        public class Library // Called Dj_Playlists in XML
        {
            [XmlAttribute("Version")]
            public string version;
            [XmlElement("PRODUCT")]
            public Product product;
            [XmlElement("COLLECTION")]
            public Collection collection;
            [XmlElement("PLAYLISTS")]
            public Playlists playlists;
        }

        [Serializable]
        [XmlRoot("PRODUCT")]
        public class Product
        {
            [XmlAttribute("Name")]
            public string name;
            [XmlAttribute("Version")]
            public string version;
            [XmlAttribute("Company")]
            public string company;
        }

        [Serializable]
        [XmlRoot("COLLECTION")]
        public class Collection
        {
            [XmlAttribute("Entries")]
            public int entries;
            [XmlElement("TRACK")]
            public List<Track> tracks;
        }

        [Serializable]
        [XmlRoot("TRACK")]
        public class Track
        {
            [XmlAttribute("TrackID")]
            public int trackId; // RekordBox index
            [XmlAttribute("Name")]
            public string name;
            [XmlAttribute("Artist")]
            public string artist;
            [XmlAttribute("Composer")]
            public string composer;
            [XmlAttribute("Album")]
            public string album;
            [XmlAttribute("Grouping")]
            public string grouping;
            [XmlAttribute("Genre")]
            public string genre;
            [XmlAttribute("Kind")]
            public string kind;
            [XmlAttribute("Size")]
            public int size;
            [XmlAttribute("TotalTime")]
            public int totaltime;
            [XmlAttribute("DiscNumber")]
            public int discnumber;
            [XmlAttribute("TrackNumber")]
            public int tracknumber;
            [XmlAttribute("Year")]
            public int year;
            [XmlAttribute("AverageBpm")]
            public decimal averagebpm;
            [XmlAttribute("DateAdded")]
            public TimeSpan dateadded;
            [XmlAttribute("BitRate")]
            public int bitrate;
            [XmlAttribute("SampleRate")]
            public int samplerate;
            [XmlAttribute("Comments")]
            public string comments;
            [XmlAttribute("PlayCount")]
            public int playcount;
            [XmlAttribute("Rating")]
            public int rating;
            [XmlAttribute("Location")]
            public string location;
            [XmlAttribute("Remixer")]
            public string remixer;
            [XmlAttribute("Tonality")]
            public string tonality;
            [XmlAttribute("Label")]
            public string label;
            [XmlAttribute("Mix")]
            public string mix;

            [XmlElement("TEMPO")]
            public List<Tempo> tempos;
            [XmlElement("POSITION_MARK")]
            public List<Position_Mark> position_Marks;
        }

        [Serializable]
        [XmlRoot("TEMPO")]
        public class Tempo
        {
            [XmlAttribute("Inizio")]
            public decimal inizio; // Beginning (Italian)
            [XmlAttribute("Bpm")]
            public decimal bpm;
            [XmlAttribute("Metro")]
            public string metro; // Meter (Italian)
            [XmlAttribute("Battito")]
            public int battito; // Beat (Italian)
        }

        [Serializable]
        [XmlRoot("POSITION_MARK")]
        public class Position_Mark
        {
            [XmlAttribute("Name")]
            public string name;
            [XmlAttribute("Type")]
            public int type;
            [XmlAttribute("Start")]
            public decimal start;
            [XmlAttribute("End")]
            public decimal end; // For loops
            [XmlAttribute("Num")]
            public int num; // -1 for cue else hot cue
            [XmlAttribute("Red")]
            public int red; // Display color for interfaces
            [XmlAttribute("Green")]
            public int green; // Display color for interfaces
            [XmlAttribute("Blue")]
            public int blue; // Display color for interfaces
        }

        [Serializable]
        [XmlRoot("PLAYLISTS")]
        public class Playlists
        {
            [XmlElement("NODE")]
            public List<Node> playlistNodes; // Top of node tree is name=ROOT
        }

        // Nodes in playlists
        [Serializable]
        [XmlRoot("NODE")]
        public class Node
        {
            [XmlAttribute("Type")]
            public int type; // 0 directory, 1 playlist
            [XmlAttribute("Name")]
            public string name;
            [XmlAttribute("Count")]
            public int count; // Amount of nodes if type is 0
            [XmlAttribute("Entries")]
            public int entries; // Amount of tracks if type is 1
            [XmlAttribute("KeyType")]
            public int keyType;
            [XmlElement("NODE")]
            public List<Node> nodes;
            [XmlElement("TRACK")]
            public List<Track> tracks; //Tracks by TrackId
        }

        private Library library; // The config

        // Constructor
        public Config(string filepath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Library));
                using (TextReader reader = new FileInfo(filepath).OpenText())
                {
                    this.library = (Library)serializer.Deserialize(reader);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        public Library Get() => library;
    }
}