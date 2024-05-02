using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab11.Data
{
    internal class Pic2
    {
        public byte[] Pixels { get; }
        public int EmotionId { get; }

        public Bitmap Bitmap { get; }

        public Pic2(byte[] pixels, int emotionId)
        {
            Pixels = pixels;
            EmotionId = emotionId;

            Bitmap = new Bitmap(48, 48);
            for (int y = 0; y < 48; y++)
            {
                for (int x = 0; x < 48; x++)
                {
                    var c = Pixels[48 * y + x];
                    Bitmap.SetPixel(x, y, Color.FromArgb(c, c, c));
                }
            }            
        }        
    }
}
