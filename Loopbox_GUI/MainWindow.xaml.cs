using Loopbox;
using Microsoft.Win32;
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
