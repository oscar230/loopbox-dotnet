using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Loopbox.Library.RekordboxXML
{
    // Nodes in playlists
    [Serializable]
    [XmlRoot("NODE")]
    class PlaylistNode : IPlaylistNode
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
        public List<IPlaylistNode> nodes;
        [XmlElement("TRACK")]
        public List<ITrack> tracks; //Tracks by TrackId
        private List<ITrack> _internal_tracks;
        private List<ITrack> InternalTracks => _internal_tracks == null ? _internal_tracks = SetInternalTracks() : _internal_tracks;
        private List<ITrack> SetInternalTracks()
        {
            List<ITrack> tracks = new List<ITrack>();
            foreach (ITrack track in tracks)
                tracks.Add(track);
            return tracks;
        }
        public List<ITrack> Tracks { get => InternalTracks; set => throw new NotImplementedException(); }
        public int Type { get => type; set => throw new NotImplementedException(); }
        public string Name { get => name; set => throw new NotImplementedException(); }
        public int Entries { get => count > 0 ? count : entries; set => throw new NotImplementedException(); }
        public int KeyType { get => keyType; set => throw new NotImplementedException(); }
        public List<IPlaylistNode> Nodes { get => nodes; set => throw new NotImplementedException(); }
        public bool IsDirectory { get => type == 0; set => throw new NotImplementedException(); }
        public bool IsPlaylist { get => type == 1; set => throw new NotImplementedException(); }

        List<IPlaylistNode> Playlists { get {
                var playlists = new List<IPlaylistNode>();
                foreach (PlaylistNode n in nodes)
                {
                    if (n.IsPlaylist) playlists.Add(n);
                    else if (n.IsDirectory) playlists.AddRange(n.Playlists);
                }
                return playlists;
            } set => throw new NotImplementedException(); }

        List<IPlaylistNode> IPlaylistNode.Playlists { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        List<IPlaylistNode> Directories { get {
                var directories = new List<IPlaylistNode>();
                foreach (IPlaylistNode n in nodes)
                {
                    if (n.IsDirectory)
                    {
                        directories.Add(n);
                        directories.AddRange(n.Directories);
                    }
                }
                return directories;
            } set => throw new NotImplementedException(); }

        List<IPlaylistNode> IPlaylistNode.Directories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
