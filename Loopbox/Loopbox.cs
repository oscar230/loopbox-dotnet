using Loopbox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loopbox.RekordboxXML;

namespace Loopbox
{
    public class LoopboxLib
    {
        private const int _bitrate_threshold_low = 320;
        //
        // Internal functionality
        //
        private Rekordbox rekordbox = null;
        private Library Library => IsLoaded() ? rekordbox.Library : null;
        private Collection Collection => Library.Collection;
        private List<Track> Tracks => Collection.Tracks;
        private PlaylistNode PlaylistRoot => Library.Playlists.PlaylistNodes.FirstOrDefault().Name.Equals("ROOT") ? Library.Playlists.PlaylistNodes.FirstOrDefault() : throw new Exception("Did not find ROOT playlist.");
        private List<PlaylistNode> Playlists => PlaylistRoot.Playlists;
        public override string ToString() => IsLoaded() ? "Library loaded from " + Library.Product.Name + " veriosn " + Library.Product.Version : "No library loaded.";
        public bool Load(string filepath) => (this.rekordbox = new Rekordbox(filepath)) != null;
        public bool IsLoaded() => rekordbox != null;

        //
        // TRACK
        //
        public List<Track> GetTracks() => Tracks;
        public int GetTracksCount() => Tracks.Count;
        public Track GetTrack(int id) => Tracks.FindAll(t => t.Id == id).FirstOrDefault();
        public List<Track> GetTracksByPlaylist(PlaylistNode playlist) => playlist.Tracks;
        public List<Track> GetTracksInPlaylist(string name) => GetPlaylist(name).Tracks;
        public bool GetTrackExists(int trackId) => GetTrack(trackId).Exists;
        public List<Track> GetTracksNotExists() => Tracks.Where(t => !t.Exists).ToList<Track>();
        public int GetTracksNotExistsCount() => GetTracksNotExists().Count;
        public List<Track> GetTracksExists() => Tracks.Where(t => t.Exists).ToList<Track>();
        public int GetTracksExistsCount() => GetTracksExists().Count;
        public List<Track> GetTracksInAnyPlaylist() => Playlists.SelectMany(pln => pln.Tracks).Distinct().ToList();
        public int GetTracksInAnyPlaylistCount() => GetTracksInAnyPlaylist().Count();
        public List<Track> GetTracksNotInAnyPlaylist() => Tracks.Except(GetTracksInAnyPlaylist()).ToList();
        public int GetTracksNotInAnyPlaylistCount() => GetTracksNotInAnyPlaylist().Count();
        public List<string> GetTracksFileTypes() => Tracks.Select(t => t.Kind).Distinct().ToList();
        private static bool SearchTermsMatch(string a, string b) => a.ToLower().Replace(@" ", string.Empty).Equals(b.ToLower().Replace(@" ", string.Empty));
        private static bool SearchTerms(string a, List<string> terms)
        {
            foreach (string s in terms)
                if (SearchTermsMatch(a, s))
                    return true;
            return false;
        }
        public List<Track> GetTracksLowBitrate() => Tracks.FindAll(t => t.Bitrate < _bitrate_threshold_low);
        public int GetTracksLowBitrateCount() => GetTracksLowBitrate().Count();
        public static List<Track> GetTracksSearch(List<Track> tracks, string searchquery) => string.IsNullOrEmpty(searchquery) ? tracks : tracks.FindAll(track => SearchTerms(searchquery, new List<string>() { track.Name, track.Album, track.Artist, track.Genre, track.Label })).ToList();
        //
        // PLAYLIST & DIRECTORIES
        //
        public List<PlaylistNode> GetAllPlaylists() => Playlists;
        public int GetAllPlaylistsCount() => Playlists.Count;
        public List<PlaylistNode> GetAllPlaylist(string name) => Playlists.FindAll(p => p.Name.Equals(name));
        public PlaylistNode GetPlaylist(string name) => Playlists.FindAll(p => p.Name.Equals(name)).FirstOrDefault();
        public List<PlaylistNode> GetAllDirectories() => PlaylistRoot.Directories;
    }
}
