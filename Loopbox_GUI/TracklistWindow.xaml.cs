﻿using Loopbox.RekordboxXML;
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
    /// Interaction logic for TracklistWindow.xaml
    /// </summary>
    public partial class TracklistWindow : Window
    {
        List<Track> tracks;
        public TracklistWindow(List<Track> tracks, string title)
        {
            this.tracks = tracks;
            InitializeComponent();
            SearchClear();
            textTitle.Text = title;
            textTrackCount.Text = "Tracks shown: " + tracks.Count;
            btnTrackOpen.IsEnabled = false;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e) => Close();
        private void listBoxTracklist_SelectionChanged(object sender, SelectionChangedEventArgs e) => btnTrackOpen.IsEnabled = true;
        private void btnSearch_Click(object sender, RoutedEventArgs e) => listBoxTracklist.ItemsSource = Search(search.Text);
        private void btnSearchClear_Click(object sender, RoutedEventArgs e) => SearchClear();
        private void SearchClear() => listBoxTracklist.ItemsSource = Search(string.Empty);
        private List<Track> Search(string searchterm) => Loopbox.LoopboxLib.GetTracksSearch(tracks, searchterm);
        private void btnTrackOpen_Click(object sender, RoutedEventArgs e) => OpenTrack();
        private void listBoxTracklist_MouseDoubleClick(object sender, MouseButtonEventArgs e) => OpenTrack();
        private void OpenTrack()
        {
            if (listBoxTracklist.SelectedItem != null)
                new TrackWindow(tracks.Find(t => t.Id.Equals((listBoxTracklist.SelectedItem as Track).Id))).Show();
            else
                Debug.WriteLine("No track selected.");
        }
        private void listBoxTracklist_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            btnTrackOpen.IsEnabled = false;
            listBoxTracklist.SelectedItem = null;
        }
    }
}
