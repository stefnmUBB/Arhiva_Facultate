using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AI.Lab9.Utils
{
    internal static class SepiaDatasetGen
    {
        public static Bitmap MakeSepia(Bitmap bmp)
        {
            for(int y=0;y<bmp.Height; y++)
            {
                for(int x=0;x<bmp.Width;x++)
                {
                    var px = bmp.GetPixel(x, y);
                    int tr = (393 * px.R + 769 * px.G + 189 * px.B) / 1000;
                    int tg = (349 * px.R + 686 * px.G + 168 * px.B) / 1000;
                    int tb = (272 * px.R + 534 * px.G + 131 * px.B) / 1000;
                    tr = Math.Min(tr, 255);
                    tg = Math.Min(tg, 255);
                    tb = Math.Min(tb, 255);
                    bmp.SetPixel(x, y, Color.FromArgb(tr, tg, tb));
                }
            }
            return bmp;
        }

        public static byte[] GetBytes(Bitmap bmp)
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            var px = bmp.GetPixel(x, y);                            
                            bw.Write(px.R);
                            bw.Write(px.G);
                            bw.Write(px.B);
                        }
                    }                    
                }
                return ms.ToArray();
            }

        }

        public static void Generate(string srcFolder, string outFile)
        {
            List<(byte[] data, bool Sepia)> Data = new List<(byte[] data, bool Sepia)>();

            var files = Directory.GetFiles(srcFolder).Where(_ => _.ToLower().EndsWith(".png")).ToList();

            var outDir = Path.Combine(srcFolder, "sepia");
            if (Directory.Exists(outDir))
                Directory.Delete(outDir, true);
            Directory.CreateDirectory(outDir);

            foreach(var file in files)
            {
                Console.WriteLine(file);
                var fdata = new Bitmap(file);
                var bmp = new Bitmap(fdata);
                fdata.Dispose();

                bool sepia = Rand.NextDouble() < 0.5;

                if(sepia)                                
                    bmp = MakeSepia(bmp);

                bmp = new Bitmap(bmp, 32, 32);
                bmp.Save(Path.Combine(outDir, Path.GetFileName(file)));

                Data.Add((GetBytes(bmp), sepia));                
            }
            File.WriteAllLines(outFile, Data.Select(d => d.data.JoinToString(" ") + (d.Sepia ? 1 : 0)));
        }
    }
}
