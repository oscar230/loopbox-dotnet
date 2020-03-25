using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Loopbox.Config;

namespace Loopbox
{
    public class LoopboxLib
    {
        private Config config;
        private bool loaded = false;
        public LoopboxLib()
        {

        }

        public bool Load(string filepath)
        {
            try
            {
                config = new Config(filepath);
                Debug.WriteLine("Succeeded loading config from: " + filepath);
                loaded = true;
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.WriteLine("Error loading config from " + filepath);
                config = null;
                return false;
            }
        }
        public bool IsLoaded() => loaded;
        public Library GetLibrary() => config.Get();
        public Collection GetCollection() => GetLibrary().collection;
        public List<Track> GetTracks() => GetCollection().tracks;
        public int GetTracksCount() => GetCollection().entries;
        public Track GetTrack(int trackId) => GetTracks().FindAll(t => t.TrackId == trackId).FirstOrDefault<Track>();
        public Node GetPlaylistsRoot() => config.Get().playlists.playlistNodes.FirstOrDefault();
        public List<Node> GetAllPlaylists() => GetPlaylistsRoot().GetPlaylists();
        public int GetAllPlaylistsCount() => GetAllPlaylists().Count();
        public List<Node> GetAllDirectories() => GetPlaylistsRoot().GetDirectories();
        public List<Node> GetPlaylistByName(string name) => GetAllPlaylists().FindAll(p => p.name.Equals(name));
        public Node GetSinglePlaylistByName(string name) => GetPlaylistByName(name).FirstOrDefault();
        public List<Track> GetTracksInPlaylist(Node node)
        {
            var tracks = new List<Track>();
            foreach (Track track in node.tracks)
                tracks.Add(track.Lookup(GetCollection()));
            return tracks;
        }
        public List<Track> GetTracksInPlaylistByName(string name) => GetSinglePlaylistByName(name).tracks;
        public bool GetTrackExists(int trackId) => GetTrack(trackId).Exist;
        public List<Track> GetTracksNotExists()
        {
            var tracks = new List<Track>();
            foreach (Track t in GetTracks())
                if (!t.Exist)
                    tracks.Add(t);
            return tracks;
        }
        public int GetTracksNotExistsCount() => GetTracksNotExists().Count();
        public List<Track> GetTracksInAnyPlaylist()
        {
            var tracks = new List<Track>();
            foreach (Node p in GetAllPlaylists())
                tracks.AddRange(GetTracksInPlaylist(p));
            return tracks.Distinct().ToList();
        }
        public int GetTracksInAnyPlaylistCount() => GetTracksInAnyPlaylist().Count();
        public List<Track> GetTracksNotInAnyPlaylist()
        {
            var tracks = GetTracks();
            foreach (Track track in GetTracksInAnyPlaylist())
                tracks.Remove(track);
            return tracks;
        }
        public int GetTracksNotInAnyPlaylistCount() => GetTracksNotInAnyPlaylist().Count();
        public List<string> GetFileTypes() => GetTracks().Select(t => t.Kind).Distinct().ToList();
        // TODO, least played/most played, track in bpm ranges, check quality by sample rate, get cue point with color in hex.
    }
}
