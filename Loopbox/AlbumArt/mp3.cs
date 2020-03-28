using Loopbox.RekordboxXML;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TagLib;

namespace Loopbox.AlbumArt
{
    static class mp3
    {
        static public BitmapImage Get(FileInfo file) => GetFromFile(file);
        static public bool Exists(FileInfo file)
        {
            var tagfile = TagLib.File.Create(file.FullName);
            if (tagfile.Tag.Pictures.Length >= 1)
                return true;
            return false;
        }
        static private BitmapImage GetFromFile(FileInfo file)
        {
            var tagfile = TagLib.File.Create(file.FullName);
            if (tagfile.Tag.Pictures.Length >= 1)
            {
                var bin = (byte[])(tagfile.Tag.Pictures[0].Data.Data);
                var memstream = new MemoryStream(bin);
                memstream.Seek(0, SeekOrigin.Begin);
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = memstream;
                bmp.EndInit();
                bmp.CacheOption = BitmapCacheOption.None;
                bmp.Freeze();
                return bmp;
            }
            else
                return null;
        }
    }
}
