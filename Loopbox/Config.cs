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
        // Structs to replicate RekordBox XML config
        //
        private struct Dj_Playlists
        {
            string version;
            Product product;
            Collection collection;
            Playlists playlists;
        }
        private struct Product
        {
            string name;
            string version;
            string company;
        }
        private struct Collection
        {
            int entries;
            List<Track> tracks;
        }
        private struct Track
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
        }
        private struct Tempo
        {
            float inizio; // Beginning (Italian)
            float bpm;
            string metro; // Meter (Italian)
            int battito; // Beat (Italian)
        }
        // Cues
        private struct Position_Mark
        {
            string name;
            int type;
            float start;
            float end; // For loops
            int num; // -1 for cue else hot cue
            int red; // Display color for interfaces
            int green; // Display color for interfaces
            int blue; // Display color for interfaces
        }
        private struct Playlists
        {
            List<Node> nodes; // Top of node tree is name=ROOT
        }
        // Nodes in playlists
        private struct Node
        {
            int type; // 0 directory, 1 playlist
            string name;
            int count; // Amount of nodes if type is 0
            int entries; // Amount of tracks if type is 1
            List<Node> nodes;
            List<Track> tracks; //Tracks by TrackId
        }

        //
        // Config parameters
        //
        private XmlDocument document; // The config in xml format
        private Dj_Playlists dj; // The config

        // Constructor
        Config(string filepath)
        {
            Debug.WriteLine("Config: " + filepath);
            try
            {
                document.Load(filepath);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }
    }
}
