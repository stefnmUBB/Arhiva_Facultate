using AI.Lab11.Extensions;
using System.Drawing;

namespace AI.Lab11.Data
{
    public class Emoji
    {
        public byte[] Bytes { get; }
        public Bitmap Image { get; }

        public int SheetX { get; }
        public int SheetY { get; }

        public string Name { get; }
        public Emoji(Bitmap bmp, string name, int sheetX, int sheetY)
        {
            Image = bmp;
            Bytes = bmp.ToBytes();
            Name = name;
            SheetX = sheetX;
            SheetY = sheetY;
        }

    }
}
