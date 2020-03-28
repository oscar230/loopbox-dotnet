using Loopbox.RekordboxXML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Loopbox_GUI
{
    /// <summary>
    /// Interaction logic for PlaylistWindow.xaml
    /// </summary>
    public partial class PlaylistWindow : Window
    {
        List<PlaylistNode> playlists;
        public PlaylistWindow(List<PlaylistNode> playlists, string title)
        {
            this.playlists = playlists;
            InitializeComponent();
            textTitle.Text = title;
            textPlaylistCount.Text = "Playlists shown: " + playlists.Count;
            listBoxPlaylist.ItemsSource = playlists;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e) => Close();
    }
}
