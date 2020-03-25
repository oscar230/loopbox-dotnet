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
        public StatisticsWindow(LoopboxLib loopbox)
        {
            InitializeComponent();
            textTrackCount.Text = "Total number of tracks: " + loopbox.GetTracksCount();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e) => Close();

        private void btnMissing_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQuality_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNoPlaylist_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
