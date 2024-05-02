using Licenta.Imaging;
using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using Licenta.Commons.Utils;
using System.Runtime.InteropServices.WindowsRuntime;
using Licenta.Commons.Parallelization;

namespace Licenta.Utils
{
    public static class Bitmaps
    {
        public static Color ToColor(this ColorRGB c)
            => Color.FromArgb((byte)((double)c.R * 255), (byte)((double)c.G * 255), (byte)((double)c.B * 255));

        public static Bitmap ToBitmap(this ImageRGB image)
        {
            var colors = image.Items.SelectAsync(_ => ToColor(_.Clamp()).ToArgb()).ToArray();
            var bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppRgb);
            var r = new Rectangle(Point.Empty, bitmap.Size);
            var data = bitmap.LockBits(r, ImageLockMode.WriteOnly, bitmap.PixelFormat);
            Marshal.Copy(colors, 0, data.Scan0, colors.Length);
            bitmap.UnlockBits(data);
            return bitmap;
        }

        public static ColorRGB[] GetColorsFromBitmap(this Bitmap bitmap)
        {
            var r = new Rectangle(Point.Empty, bitmap.Size);
            if (bitmap.PixelFormat == PixelFormat.Format32bppRgb || bitmap.PixelFormat == PixelFormat.Format32bppArgb || bitmap.PixelFormat == PixelFormat.Format32bppPArgb)
            {                
                var data = bitmap.LockBits(r, ImageLockMode.ReadOnly, bitmap.PixelFormat);
                var ints = new int[data.Height * data.Width];
                Marshal.Copy(data.Scan0, ints, 0, ints.Length);
                bitmap.UnlockBits(data);
                return ints.Select(ColorRGB.FromRGB).ToArray();
            }
            if(bitmap.PixelFormat == PixelFormat.Format24bppRgb)
            {                
                var data = bitmap.LockBits(r, ImageLockMode.ReadOnly, bitmap.PixelFormat);

                var bytes = new byte[data.Height * data.Stride];
                Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
                bitmap.UnlockBits(data);

                var colors = new ColorRGB[bitmap.Width * bitmap.Height];
                for (int y = 0; y < bitmap.Height; y++)
                    for (int x = 0; x < bitmap.Width; x++) 
                    {
                        var datai = y * data.Stride + 3 * x;
                        colors[y * bitmap.Width + x] = new ColorRGB(bytes[datai], bytes[datai + 1], bytes[datai + 2]);
                    }
                return colors;                
            }
            throw new InvalidOperationException($"Only 24bpp and 32bpp (A)RGB pixel formats are supported. Current Format = {bitmap.PixelFormat}");
        }
    }
}
