using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Loopbox
{
    static class Meta
    {
        private static TagLib.File GetFile(string pathname) => TagLib.File.Create(pathname);
        public static ImageSource GetAlbumArt(string pathname)
        {
            var firstPicture = GetFile(pathname).Tag.Pictures.FirstOrDefault();
            if (firstPicture != null)
            {
                byte[] pData = firstPicture.Data.Data;
                var mStream = new MemoryStream();
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                var bitmap = new Bitmap(mStream, false);
                return ImageSourceFromBitmap(bitmap);
            }
            else
                return null;
        }
        public static bool ImageExist(string pathname) => GetFile(pathname).Tag.Pictures.FirstOrDefault() == null ? false : true;
        public static ImageSource ImageSourceFromBitmap(Bitmap bmp) => Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
    }
}
