using AI.Lab11.Data;
using AI.Lab11.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AI.Lab11
{
    public static partial class DataSets
    {
        private class EmojisCollection
        {
            static Bitmap EmojiBitmap = Resources.emoji32_img;

            static Dictionary<(int Y, int X), string> EmojiNames = Resources.emoji32_txt
                .Split('\n')
                .Select(_ => _.Split(' '))
                .Where(_ => !_[0].Contains("Flag"))
                .Where(_ => !_[0].Contains("Squared"))
                .Where(_ => !_[0].Contains("KEYCAP"))
                .Select(_ => (y: int.Parse(_[2]), x: int.Parse(_[1]), name: _[0]))
                .Where(_ => (_.x >= 14 && _.x <= 28) || (_.x >= 31 && _.x <= 36) || (_.x >= 40 && _.x <= 45))
                .ToDictionary(_ => (_.y, _.x), _ => _.name);

            static Emoji FromBitmap(int x, int y)
            {
                var bmp = new Bitmap(32, 32);
                var g = Graphics.FromImage(bmp);
                g.DrawImageUnscaled(EmojiBitmap, -34 * x - 1, -34 * y - 1);
                return new Emoji(bmp, EmojiNames[(y, x)], x, y);
            }

            static int EmojisPerRow = EmojiBitmap.Width / 34;
            static int EmojisCount = EmojisPerRow * EmojiBitmap.Height / 34;
            public static List<Emoji> _Emojis() => Enumerable.Range(0, EmojisCount)
                .Select(i => (y: i / EmojisPerRow, x: i % EmojisPerRow))
                .Where(EmojiNames.ContainsKey)
                .Select(_ => FromBitmap(_.x, _.y))
                .ToList();
        }

        public static List<Emoji> Emojis() => EmojisCollection._Emojis();


    }
}
