using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AI.Lab11.Extensions
{
    internal static class BitmapExtensions
    {
        public static byte[] ToBytes(this Bitmap bmp)
        {
            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
            var length = bmp.Height * data.Stride;
            var bytes = new byte[length];
            Marshal.Copy(data.Scan0, bytes, 0, length);
            bmp.UnlockBits(data);
            return bytes;
        }

        public static Bitmap ToGrayscale(this Bitmap bmp)
        {
            var res = new Bitmap(bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), PixelFormat.Format24bppRgb));

            for(int y=0;y<res.Height;y++)
            {
                for (int x = 0; x < res.Width; x++)
                {
                    var b = (int)(res.GetPixel(x, y).GetBrightness() * 255);
                    res.SetPixel(x, y, Color.FromArgb(b, b, b));
                }
            }
            return res;
        }

        public static Bitmap Crop(this Bitmap bmp, int x, int y, int w, int h)
        {
            return new Bitmap(bmp.Clone(new Rectangle(x, y, w, h), PixelFormat.Format24bppRgb));
        }
    }
}
