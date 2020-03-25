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
            SetupText();
        }
        private void SetupText()
        {
            textTrackName.Text = "Title: " + track.name;
            textTrackArtist.Text = "Artist: " + track.artist;
            textTrackArtist.Text = "File exists: " + track.artist;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
