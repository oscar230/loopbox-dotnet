using Loopbox;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Loopbox_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoopboxLib loopbox;
        public MainWindow()
        {
            loopbox = new LoopboxLib();
            InitializeComponent();
            CheckButtonsEnabled();
#if DEBUG
            loopbox.Load(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\test.xml");
            CheckButtonsEnabled();
#endif
        }

        private void CheckButtonsEnabled()
        {
            btnAlbum.IsEnabled = loopbox.IsLoaded();
            btnExport.IsEnabled = loopbox.IsLoaded();
            btnLoad.IsEnabled = !loopbox.IsLoaded();
            btnStatistics.IsEnabled = loopbox.IsLoaded();
            btnMeta.IsEnabled = loopbox.IsLoaded();
            btnViewAllTracks.IsEnabled = loopbox.IsLoaded();
        }
        private void LoadConfig()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                if ((new FileInfo(openFileDialog.FileName)).Extension.Contains("xml"))
                {
                    if (loopbox.Load(openFileDialog.FileName) == false)
                    {
                        MessageBox.Show("Collection not regognized, please export again and retry.", "Collection failed.");
                    }
                }
                else
                {
                    MessageBox.Show("You need to load a XML file, get this from rekordbox by navigating to file -> export collection in xml format.", "File is not XML.");
                }
            }
            else
            {
                MessageBox.Show("Error loading collection. No action taken. Please try again.", "Unkown error.");
            }
            CheckButtonsEnabled();
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e) => LoadConfig();
        private void btnStatistics_Click(object sender, RoutedEventArgs e) => new StatisticsWindow(loopbox).Show();
        private void btnMeta_Click(object sender, RoutedEventArgs e) => Close();
        private void btnAlbum_Click(object sender, RoutedEventArgs e) => Close();
        private void btnExport_Click(object sender, RoutedEventArgs e) => Close();
        private void btnExit_Click(object sender, RoutedEventArgs e) => Close();
        private void hyperlinkCredits_Click(object sender, RoutedEventArgs e) => HyperLinkRedirect(hyperlinkCredits.NavigateUri.ToString());
        private void hyperlinkRekordbox_Click(object sender, RoutedEventArgs e) => HyperLinkRedirect(hyperlinkRekordbox.NavigateUri.ToString());
        private void HyperLinkRedirect(Hyperlink hyperlink) => HyperLinkRedirect(hyperlink.NavigateUri.ToString());
        private void HyperLinkRedirect(string url) => System.Diagnostics.Process.Start(url);
        private void btnViewAllTracks_Click(object sender, RoutedEventArgs e) => new TracklistWindow(loopbox.GetTracks(), "All tracks in library.").Show();
    }
}
