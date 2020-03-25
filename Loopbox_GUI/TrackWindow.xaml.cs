using System.Windows;
using static Loopbox.Config;

namespace Loopbox_GUI
{
    /// <summary>
    /// Interaction logic for TrackWindow.xaml
    /// </summary>
    public partial class TrackWindow : Window
    {
        Track track;
        public TrackWindow(Track track)
        {
            this.track = track;
            InitializeComponent();
            Setup();
        }
        private void Setup()
        {
            textTrackAlbum.Text = track.Album;
            textTrackArtist.Text = track.Artist;
            textTrackAvarageBpm.Text = track.AverageBpm.ToString();
            textTrackBitrate.Text = track.Bitrate.ToString();
            textTrackComments.Text = track.Comments;
            textTrackComposer.Text = track.Composer;
            textTrackDateadded.Text = track.DateAdded;
            textTrackExists.Text = track.Exist ? "Yes" : "No";
            textTrackGenre.Text = track.Genre;
            textTrackGrouping.Text = track.Grouping;
            textTrackKind.Text = track.Kind;
            textTrackLabel.Text = track.Label;
            textTrackMix.Text = track.Mix;
            textTrackName.Text = track.Name;
            textTrackPlaycount.Text = track.PlayCount.ToString();
            textTrackRating.Text = track.Rating.ToString();
            textTrackRemixer.Text = track.Remixer;
            textTrackSamplerate.Text = track.Samplerate.ToString();
            textTrackSize.Text = track.Size.ToString();
            textTrackTonality.Text = track.Tonality;
            textTrackYear.Text = track.Year.ToString();
            btnMetaAlbumArt.IsEnabled = track.AlbumArtExists;
            if (track.AlbumArtExists)
                imageTrack.Source = Media.ImageSourceFromBitmap(track.AlbumArt);
            btnMeta.IsEnabled = !track.MetaComplete;
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
