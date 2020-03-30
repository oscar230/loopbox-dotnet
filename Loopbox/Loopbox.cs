using Loopbox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loopbox.RekordboxXML;
using Loopbox_Metadata;
using Loopbox_VirtualDevice;

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
        private List<Device> devices = new List<Device>();

        //
        // TRACK
        //
        public List<Track> GetTracks() => Tracks;
        private List<Track> GetTracks(List<int> ids) => Tracks.Where(t => ids.Contains(t.Id)).ToList();
        public int GetTracksCount() => Tracks.Count;
        public Track GetTrack(int id) => Tracks.FindAll(t => t.Id == id).FirstOrDefault();
        public List<Track> GetTracksInPlaylist(PlaylistNode playlist) => GetTracks(playlist.Tracks);
        public List<Track> GetTracksInPlaylist(string name) => GetTracksInPlaylist(GetPlaylist(name));
        public bool GetTrackExists(int trackId) => GetTrack(trackId).Exists;
        public List<Track> GetTracksNotExists() => Tracks.Where(t => !t.Exists).ToList<Track>();
        public int GetTracksNotExistsCount() => GetTracksNotExists().Count;
        public List<Track> GetTracksExists() => Tracks.Where(t => t.Exists).ToList<Track>();
        public int GetTracksExistsCount() => GetTracksExists().Count;
        public List<Track> GetTracksInAnyPlaylist()
        {
            var tracks = new List<Track>();
            foreach (PlaylistNode playlist in GetAllPlaylists())
                foreach (Track track in GetTracksInPlaylist(playlist))
                    tracks.Add(track);
            return tracks.Distinct().ToList();
        }
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
        public bool GetPlaylistHasDuplicates(PlaylistNode playlist) => playlist.tracks.GroupBy(t => t.Id).Where(gt => gt.Count() > 1).Select(gt2 => gt2.Key).Count() > 0;
        public List<PlaylistNode> GetPlaylistsWithDuplicateTracks() => GetAllPlaylists().Where(t => GetPlaylistHasDuplicates(t)).ToList();
        public int GetPlaylistsWithDuplicateTracksCount() => GetPlaylistsWithDuplicateTracks().Count();
        //
        // METADATA DOWNLOADER
        //
        public bool DownloadMetadata(Track track) => new Downloader(track.Artist, track.Name).Found();
        //
        // VIRTUAL DEVICE
        //
        public void CreateVirtualDevice(string devicename) => devices.Add(new Device(devicename));
        public void RemoveVirtualDevice(string devicename) => devices.FindAll(d => d.DeviceName.Equals(devicename)).FirstOrDefault().Destroy();
    }
}
