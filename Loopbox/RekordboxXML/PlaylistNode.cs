using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.RekordboxXML
{
    // Nodes in playlists
    [Serializable]
    [XmlRoot("NODE")]
    public class PlaylistNode
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
        public List<PlaylistNode> nodes;
        [XmlElement("TRACK")]
        public List<Track> tracks; //Tracks by TrackId
        private List<Track> _internal_tracks;
        private List<Track> InternalTracks => _internal_tracks == null ? _internal_tracks = SetInternalTracks() : _internal_tracks;
        private List<Track> SetInternalTracks()
        {
            List<Track> tracks = new List<Track>();
            foreach (Track track in tracks)
                tracks.Add(track);
            return tracks;
        }
        public List<Track> Tracks { get => InternalTracks; set => throw new NotImplementedException(); }
        public int Type { get => type; set => throw new NotImplementedException(); }
        public string Name { get => name; set => throw new NotImplementedException(); }
        public int Entries { get => count > 0 ? count : entries; set => throw new NotImplementedException(); }
        public int KeyType { get => keyType; set => throw new NotImplementedException(); }
        public List<PlaylistNode> Nodes { get => new List<PlaylistNode>(nodes); set => throw new NotImplementedException(); }
        public bool IsDirectory { get => type == 0; set => throw new NotImplementedException(); }
        public bool IsPlaylist { get => type == 1; set => throw new NotImplementedException(); }

        public List<PlaylistNode> Playlists { get {
                var playlists = new List<PlaylistNode>();
                foreach (PlaylistNode n in nodes)
                {
                    if (n.IsPlaylist) playlists.Add(n);
                    else if (n.IsDirectory) playlists.AddRange(n.Playlists);
                }
                return playlists;
            } set => throw new NotImplementedException(); }

        public List<PlaylistNode> Directories { get {
                var directories = new List<PlaylistNode>();
                foreach (PlaylistNode n in nodes)
                {
                    if (n.IsDirectory)
                    {
                        directories.Add(n);
                        directories.AddRange(n.Directories);
                    }
                }
                return directories;
            } set => throw new NotImplementedException(); }
    }
}
