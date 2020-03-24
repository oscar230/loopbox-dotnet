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
        private class Library // Called Dj_Playlists in XML
        {
            string version;
            Product product;
            Collection collection;
            Playlists playlists;

            public Library(XmlElement element)
            {
                ElementDebug(element);
                version = element.GetAttribute("Version");
            }
        }
        private class Product
        {
            string name;
            string version;
            string company;

            public Product(XmlElement element)
            {
                ElementDebug(element);
            }
        }
        private class Collection
        {
            int entries;
            List<Track> tracks;

            public Collection(XmlElement element)
            {
                ElementDebug(element);
            }
        }
        private class Track
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
            float averagebpm;
            DateTime dateadded;
            int bitrate;
            int samplerate;
            string comments;
            int playcount;
            int rating;
            FileInfo location;
            string remixer;
            string tonality;
            string label;
            string mix;
            List<Tempo> tempos;
            List<Position_Mark> position_Marks;

            public Track(XmlElement element)
            {
                ElementDebug(element);
            }
        }
        private class Tempo
        {
            float inizio; // Beginning (Italian)
            float bpm;
            string metro; // Meter (Italian)
            int battito; // Beat (Italian)

            public Tempo(XmlElement element)
            {
                ElementDebug(element);
            }
        }
        // Cues
        private class Position_Mark
        {
            string name;
            int type;
            float start;
            float end; // For loops
            int num; // -1 for cue else hot cue
            int red; // Display color for interfaces
            int green; // Display color for interfaces
            int blue; // Display color for interfaces

            public Position_Mark(XmlElement element)
            {
                ElementDebug(element);
            }
        }
        private class Playlists
        {
            List<Node> nodes; // Top of node tree is name=ROOT

            public Playlists(XmlElement element)
            {
                ElementDebug(element);
            }
        }
        // Nodes in playlists
        private class Node
        {
            int type; // 0 directory, 1 playlist
            string name;
            int count; // Amount of nodes if type is 0
            int entries; // Amount of tracks if type is 1
            List<Node> nodes;
            List<Track> tracks; //Tracks by TrackId

            public Node(XmlElement element)
            {
                ElementDebug(element);
            }
        }

        //
        // Config parameters
        //
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
            ElementDebug(document.DocumentElement);
            //this.library = new Library(document.DocumentElement);
        }

        public static void ElementDebug(XmlElement element)
        {
            Debug.WriteLine("XML at " + element.Name + " parent of " + element.ChildNodes.Count() + " nodes.");
        }
    }
}
