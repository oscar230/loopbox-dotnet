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
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
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
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void hyperlinkCredits_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(hyperlinkCredits.NavigateUri.ToString());
        }

        private void hyperlinkRekordbox_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(hyperlinkRekordbox.NavigateUri.ToString());
        }
    }
}
