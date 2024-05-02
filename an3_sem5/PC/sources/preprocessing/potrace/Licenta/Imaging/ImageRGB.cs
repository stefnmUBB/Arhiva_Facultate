using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using Licenta.Utils;
using System;
using System.Drawing;

namespace Licenta.Imaging
{
    public class ImageRGB : Matrix<ColorRGB>
    {
        public int Width => ColumnsCount;
        public int Height => RowsCount;

        public ImageRGB(int width, int height) : base(height, width) { }
        public ImageRGB(Bitmap bitmap) : base(bitmap.Height, bitmap.Width, bitmap.GetColorsFromBitmap()) { }                
        public ImageRGB(IReadMatrix<ColorRGB> m) : base(m) { }
        public ImageRGB(IReadMatrix<DoubleNumber> m) : base(m.Select(v => new ColorRGB(v, v, v))) { }
        public ImageRGB(IReadMatrix<double> m) : base(m.Select(v => new ColorRGB(v, v, v))) { }
        public ImageRGB(IReadMatrix<byte> m) : base(m.Select(v => new ColorRGB(v, v, v))) { }

        public ImageRGB Convolve(Matrix<DoubleNumber> c) 
            => new ImageRGB(Matrices.Convolve<ColorRGB, DoubleNumber, ColorRGB>(this, c));     
        
        public static ImageRGB FromFile(string path)
        {
            using (var bmp = new Bitmap(path))
            using(var bmpformatted = new Bitmap(bmp.Width, bmp.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
            {
                Graphics.FromImage(bmpformatted).DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);                
                return new ImageRGB(bmpformatted);
            }                
        }

        public static ImageRGB FromFile(string path, float percentage)
        {
            using (var bmp = new Bitmap(path))
            {
                var newWidth = (int)Math.Max(1, bmp.Width * percentage / 100);
                var newHeight = (int)Math.Max(1, bmp.Height * percentage / 100);
                using (var bmpformatted = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
                {
                    using (var g = Graphics.FromImage(bmpformatted))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                        g.DrawImage(bmp, 0, 0, newWidth, newHeight);
                    }
                    return new ImageRGB(bmpformatted);
                }
            }
        }

        public static ImageRGB FromFile(string path, int newWidth, int newHeight)
        {
            using (var bmp = new Bitmap(path))
            using (var bmpformatted = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
            {
                Graphics.FromImage(bmpformatted).DrawImage(bmp, 0, 0, newWidth, newHeight);
                return new ImageRGB(bmpformatted);
            }
        }

    }
}
