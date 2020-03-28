using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Loopbox;

namespace Loopbox_GUI
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        LoopboxLib loopbox;
        public StatisticsWindow(LoopboxLib loopbox)
        {
            this.loopbox = loopbox;
            InitializeComponent();
            Setup();
        }

        private void Setup()
        {
            textTrackCount.Text = "Tracks in collection: " + loopbox.GetTracksCount();
            textPlaylistCount.Text = "Playlists: " + loopbox.GetAllPlaylistsCount();
            textMissingCount.Text = "Tracks missing: " + loopbox.GetTracksNotExistsCount();
            textLowQualityCount.Text = "Tracks of low quality: " + loopbox.GetTracksLowBitrateCount();
            textInPlaylistCount.Text = "Tracks in any playlist: " + loopbox.GetTracksInAnyPlaylistCount();
            textNotInPlaylistCount.Text = "Tracks not in any playlist: " + loopbox.GetTracksNotInAnyPlaylistCount();
            textPlaylistDuplicateCount.Text = "Playlists with duplicate tracks: " + loopbox.GetPlaylistsWithDuplicateTracksCount();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) => Close();
        private void btnViewAllTracks_Click(object sender, RoutedEventArgs e) => new TracklistWindow(loopbox.GetTracks(), "All tracks in collection.").Show();
        private void btnMissing_Click(object sender, RoutedEventArgs e) => new TracklistWindow(loopbox.GetTracksNotExists(), "Tracks missing from filesystem.").Show();
        private void btnInPlaylist_Click(object sender, RoutedEventArgs e) => new TracklistWindow(loopbox.GetTracksInAnyPlaylist(), "Tracks in any playlist.").Show();
        private void btnInNoPlaylist_Click(object sender, RoutedEventArgs e) => new TracklistWindow(loopbox.GetTracksNotInAnyPlaylist(), "Tracks not in any playlist.").Show();
        private void btnLowQuality_Click(object sender, RoutedEventArgs e) => new TracklistWindow(loopbox.GetTracksLowBitrate(), "Tracks of low quality.").Show();
        private void btnPlaylistsDuplicate_Click(object sender, RoutedEventArgs e) => new PlaylistWindow(loopbox.GetPlaylistsWithDuplicateTracks(), "Playlists with duplicate tracks.").Show();
    }
}
