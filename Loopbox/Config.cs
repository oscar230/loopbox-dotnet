﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;

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
            private int trackId; // RekordBox index
            [XmlAttribute("Key")]
            private int key = int.MinValue; // Same as trackId, to represent track in playlists
            [XmlAttribute("Name")]
            private string name;
            [XmlAttribute("Artist")]
            private string artist;
            [XmlAttribute("Composer")]
            private string composer;
            [XmlAttribute("Album")]
            private string album;
            [XmlAttribute("Grouping")]
            private string grouping;
            [XmlAttribute("Genre")]
            private string genre;
            [XmlAttribute("Kind")]
            private string kind;
            [XmlAttribute("Size")]
            private int size;
            [XmlAttribute("TotalTime")]
            private int totaltime;
            [XmlAttribute("DiscNumber")]
            private int discnumber;
            [XmlAttribute("TrackNumber")]
            private int tracknumber;
            [XmlAttribute("Year")]
            private int year;
            [XmlAttribute("AverageBpm")]
            private decimal averagebpm;
            [XmlAttribute("DateAdded")]
            private string dateadded;
            [XmlAttribute("BitRate")]
            private int bitrate;
            [XmlAttribute("SampleRate")]
            private int samplerate;
            [XmlAttribute("Comments")]
            private string comments;
            [XmlAttribute("PlayCount")]
            private int playcount;
            [XmlAttribute("Rating")]
            private int rating;
            [XmlAttribute("Location")]
            private string location;
            [XmlAttribute("Remixer")]
            private string remixer;
            [XmlAttribute("Tonality")]
            private string tonality;
            [XmlAttribute("Label")]
            private string label;
            [XmlAttribute("Mix")]
            private string mix;

            [XmlElement("TEMPO")]
            public List<Tempo> tempos;
            [XmlElement("POSITION_MARK")]
            public List<Position_Mark> position_Marks;

            private FileInfo file = null;

            public Track Lookup(Collection collection)
            {
                if (key == int.MinValue)
                    return this; //Is already a track in collection, nothing to do
                else
                    return collection.tracks.Find(t => t.trackId.Equals(this.key));
            }
            private string ConvertLocation(string rekordboxurl) => new System.Uri(rekordboxurl.Replace(@"file://localhost/", @"file:///").Replace(@"#", @"%23").Replace(@".", @"%2E")).LocalPath;
            private FileInfo GetFile() => file == null ? file = new FileInfo(Location): file;
            public FileInfo File => GetFile();
            public Bitmap AlbumArt => Meta.Image(Location);
            public bool AlbumArtExists => Meta.Image(Location) == null ? false : true;
            public bool MetaComplete => string.IsNullOrEmpty(name) || string.IsNullOrEmpty(artist) || string.IsNullOrEmpty(genre) || string.IsNullOrEmpty(label) ? false : true;
            public bool Exist => GetFile().Exists;
            public string Artist => artist;
            public string Name => name;
            public int TrackId => trackId;
            public string Composer => composer;
            public string Album => album;
            public string Grouping => grouping;
            public string Genre => genre;
            public string Kind => kind;
            public int Size => size;
            public int Year => year;
            public float AverageBpm => (float)averagebpm;
            public DateTime DateAdded => DateTime.Parse(dateadded);
            public int Bitrate => bitrate;
            public int Samplerate => samplerate;
            public string Comments => comments;
            public int PlayCount => playcount;
            public int Rating => rating;
            public string Remixer => remixer;
            public string Tonality => tonality;
            public string Label => label;
            public string Mix => mix;
            public string Location => ConvertLocation(location);
            public override string ToString() => "Track: " + trackId + " " + name;
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

            public bool IsDirectory() => this.type == 0;
            public bool IsPlaylist() => this.type == 1;
            public List<Node> GetPlaylists()
            {
                var playlists = new List<Node>();
                foreach (Node n in nodes)
                {
                    if (n.IsPlaylist()) playlists.Add(n);
                    else if (n.IsDirectory()) playlists.AddRange(n.GetPlaylists());
                }
                return playlists;
            }
            public List<Node> GetDirectories()
            {
                var directories = new List<Node>();
                foreach (Node n in nodes)
                {
                    if (IsDirectory())
                    {
                        directories.Add(n);
                        directories.AddRange(n.GetDirectories());
                    }
                }
                return directories;
            }
        }

        private Library library; // The config

        // Constructor
        public Config(string filepath)
        {
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Library));
                TextReader reader = new StreamReader(filepath);
                this.library = (Library)deserializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

            this.library.ToString();
        }

        public Library Get() => library;
    }
}