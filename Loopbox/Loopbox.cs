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
        private string ConvertLocation(string rekordboxurl) => new System.Uri(rekordboxurl.Replace(@"file://localhost/", @"file:///").Replace(@"#", @"%23").Replace(@".", @"%2E")).LocalPath;
        public bool IsLoaded() => loaded;
        public List<Track> GetTracks() => config.Get().collection.tracks;
        public int GetTracksCount() => config.Get().collection.entries;
        public Track GetTrack(int trackId) => GetTracks().FindAll(t => t.trackId == trackId).FirstOrDefault<Track>();
        public Node GetPlaylistsRoot() => config.Get().playlists.playlistNodes.FirstOrDefault();
        public List<Node> GetAllPlaylists() => GetPlaylistsRoot().GetPlaylists();
        public int GetAllPlaylistsCount() => GetAllPlaylists().Count();
        public List<Node> GetAllDirectories() => GetPlaylistsRoot().GetDirectories();
        public List<Node> GetPlaylistByName(string name) => GetAllPlaylists().FindAll(p => p.name.Equals(name));
        public Node GetSinglePlaylistByName(string name) => GetPlaylistByName(name).FirstOrDefault();
        public List<Track> GetTracksInPlaylist(Node node) => node.tracks;
        public List<Track> GetTracksInPlaylistByName(string name) => GetSinglePlaylistByName(name).tracks;
        public bool GetTrackExists(int trackId) => new FileInfo(GetTrack(trackId).location).Exists;
        public List<Track> GetTracksNotExists()
        {
            var tracks = new List<Track>();
            foreach (Track t in GetTracks())
                if (!new FileInfo(ConvertLocation(t.location)).Exists)
                {
                    tracks.Add(t);
                    Debug.WriteLine("Did not find file at: " + new FileInfo(ConvertLocation(t.location)).FullName);
                }
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
        public List<Track> GetTracksNotInAnyPlaylist() => GetTracks().FindAll(t1 => GetTracks().FindAll(t2 => t2.trackId == t1.trackId).Count == 0); // TODO make more effecient
        public int GetTracksNotInAnyPlaylistCount() => GetTracksNotInAnyPlaylist().Count();
        public List<string> GetFileTypes() => GetTracks().Select(t => t.kind).Distinct().ToList();
        // TODO, least played/most played, track in bpm ranges, check quality by sample rate, get cue point with color in hex.
    }
}
