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
            var file = GetFile(pathname);
            Debug.WriteLine("Extract album art from file\n\tname: " + file.Name + "\n\tmime: "+ file.MimeType + "\n\tpicture size: "+ file.Tag.Pictures.Length);
            var mStream = new MemoryStream();
            var firstPicture = file.Tag.Pictures.FirstOrDefault();
            if (firstPicture != null)
            {
                byte[] pData = firstPicture.Data.Data;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                var bitmap = new Bitmap(mStream, false);
                mStream.Dispose();
                return ImageSourceFromBitmap(bitmap);
            }
            else
                return null;
        }
        public static bool ImageExist(string pathname) => GetFile(pathname).Tag.Pictures.Length > 0;

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);
        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }
    }
}
