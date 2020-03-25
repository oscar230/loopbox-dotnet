using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib;

namespace Loopbox
{
    static class Meta
    {
        private static TagLib.File GetFile(string pathname) => TagLib.File.Create(pathname);
        public static Bitmap Image(string pathname)
        {
            var mStream = new MemoryStream();
            var firstPicture = GetFile(pathname).Tag.Pictures.FirstOrDefault();
            if (firstPicture != null)
            {
                byte[] pData = firstPicture.Data.Data;
                mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
                var bitmap = new Bitmap(mStream, false);
                mStream.Dispose();
                return bitmap;
            }
            else
                return null;
        }
    }
}
