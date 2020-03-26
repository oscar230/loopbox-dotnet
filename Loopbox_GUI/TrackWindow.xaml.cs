using Loopbox.Library;
using System.Windows;

namespace Loopbox_GUI
{
    /// <summary>
    /// Interaction logic for TrackWindow.xaml
    /// </summary>
    public partial class TrackWindow : Window
    {
        ITrack track;
        public TrackWindow(ITrack track)
        {
            this.track = track;
            InitializeComponent();
            Setup();
        }
        private void Setup()
        {
            textTrackAlbum.Text = track.Album;
            textTrackArtist.Text = track.Artist;
            textTrackAvarageBpm.Text = track.Averagebpm + " bpm";
            textTrackBitrate.Text = track.Bitrate.ToString();
            textTrackComments.Text = track.Comments;
            textTrackComposer.Text = track.Composer;
            textTrackDateadded.Text = track.Dateadded.ToString();
            textTrackExists.Text = track.Exists ? "Yes" : "No";
            textTrackGenre.Text = track.Genre;
            textTrackGrouping.Text = track.Grouping;
            textTrackKind.Text = track.Kind;
            textTrackLabel.Text = track.Label;
            textTrackMix.Text = track.Mix;
            textTrackName.Text = track.Name;
            textTrackPlaycount.Text = track.Playcount.ToString();
            textTrackRating.Text = track.Rating.ToString();
            textTrackRemixer.Text = track.Remixer;
            textTrackSamplerate.Text = track.Samplerate.ToString();
            textTrackSize.Text = track.Size.ToString();
            textTrackTonality.Text = track.Tonality;
            textTrackYear.Text = track.Year.ToString();
            //btnMetaAlbumArt.IsEnabled = !track.AlbumArtExists;
            //if (track.AlbumArtExists)
            //    imageTrack.Source = track.AlbumArt;
            //btnMeta.IsEnabled = !track.MetaComplete;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e) => Close();
        private void btnMetaAlbumArt_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnMeta_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
