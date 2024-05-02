using AI.Lab9.Data;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;
using static AI.Lab9.Solvers.DigitsUtils;
using static AI.Lab9.Solvers.SepiaUtils;
using System.Drawing;
using AI.Lab9.Properties;

namespace AI.Lab9.Solvers
{
    internal class DataSets
    {
        public static string[] IrisLabels = new string[] { "Iris-setosa", "Iris-versicolor", "Iris-virginica" };

        public static List<(double[] Input, double[] Output)> Iris()
        {
            var features = new string[] { "SepalLength", "SepalWidth", "PetalLength", "PetalWidth" };
            var labelToInt = new Dictionary<string, double>
            {
                { "Iris-setosa", 0 },
                { "Iris-versicolor", 1 },
                { "Iris-virginica", 2 }
            };

            var csv = new CsvData(Resources.iris, features.Append("Class").ToArray());
            return csv.ToObjects<Flower>().ToList().Shuffle()
               .Select(f =>
               (
                   Input: new double[] { f.SepalWidth, f.PetalWidth, f.SepalLength, f.PetalLength }.ToArray(),
                   Output: new double[]
                    {
                        labelToInt[f.Class] == 0 ? 1:0,
                        labelToInt[f.Class] == 1 ? 1:0,
                        labelToInt[f.Class] == 2 ? 1:0,
                    }
               )).ToList();
        }


        public static List<(double[] Input, double[] Output)> Digits(byte[] bytes)
        {
            return bytes.ReadLinesUTF8()
              .Skip(21)
              .Select((l, i) => (l, i))
              .GroupBy(_ => _.i / 33)
              .Select(_ => _.Select(li => li.l).ToList())
              .Where(_ => _.Count == 33)
              .Select(l => (
                  data: l.Take(32).JoinToString("").Where(c => c == '0' || c == '1').Select(c => c == '0' ? (byte)0 : (byte)1).ToArray(),
                  value: int.Parse(Regex.Replace(l.Last(), @"\s+", ""))))
              .Select(_ => new RawDigit(_.data, _.value))
              .ToList().Reduce().ToData().Shuffle().ToList();
        }

        public static List<(double[] Input, double[] Output)> OptDigitsOrigCv()
            => Digits(Resources.optdigits_orig_cv);


        public static List<(double[] Input, double[] Output)> Digits(string filename)
        {
            return File.ReadAllLines(filename)
              .Skip(21)
              .Select((l, i) => (l, i))
              .GroupBy(_ => _.i / 33)
              .Select(_ => _.Select(li => li.l).ToList())
              .Where(_ => _.Count == 33)
              .Select(l => (
                  data: l.Take(32).JoinToString("").Where(c => c == '0' || c == '1').Select(c => c == '0' ? (byte)0 : (byte)1).ToArray(),
                  value: int.Parse(Regex.Replace(l.Last(), @"\s+", ""))))
              .Select(_ => new RawDigit(_.data, _.value))
              .ToList().Reduce().ToData().Shuffle().ToList();
        }
        

        public static List<(double[] Input, double[] Output)> FilterImages(string filename)
        {
            return File.ReadAllLines(filename)
                .Select(_ => _.Split(' ').Select(s => int.Parse(s)).ToArray())
                .Select(_ => (Data: _.Take(3 * 1024).Select(x => (byte)x).ToArray(), IsSepia: _.Last() == 1 ? 1 : 0))
                .Select(_ => new ImageData32(_.Data, _.IsSepia))
                .ToList().Reduce().ToHSB().ToData().Shuffle().ToList();
        }
    }

    internal static class SepiaUtils
    {
        public class ImageData32
        {
            public byte[] Data { get; }
            public int Sepia { get; }

            public ImageData32(byte[] data, int sepia)
            {
                Data = data;
                Sepia = sepia;
            }

            public ImageData8 Reduce()
            {
                var data = new byte[3 * 8 * 8];

                for (int ty = 0; ty < 8; ty++)
                {
                    for (int tx = 0; tx < 8; tx++)
                    {
                        int r = 0;
                        int g = 0;
                        int b = 0;
                        for (int y = 0; y < 4; y++)
                        {
                            int row = 4 * ty + y;
                            for (int x = 0; x < 4; x++)
                            {
                                int col = 4 * tx + x;
                                int i = 3 * (32 * row + col);
                                r += Data[i];
                                g += Data[i + 1];
                                b += Data[i + 2];
                            }
                        }
                        r /= 16; g /= 16; b /= 16;
                        data[3 * (ty * 8 + tx)] = (byte)r;
                        data[3 * (ty * 8 + tx) + 1] = (byte)g;
                        data[3 * (ty * 8 + tx) + 2] = (byte)b;
                    }
                }

                return new ImageData8(data, Sepia);
            }
        }

        public class ImageData8
        {
            public byte[] Data { get; }
            public int Sepia { get; }

            public ImageData8(byte[] data, int value)
            {
                Data = data;
                Sepia = value;
            }

            public ImageData8HSB ToHSB()
            {
                var hsb = new double[3 * 64];
                for (int i = 0; i < 64; i++)
                {
                    int r = Data[3 * i];
                    int g = Data[3 * i + 1];
                    int b = Data[3 * i + 2];
                    var c = Color.FromArgb(r, g, b);

                    hsb[3 * i] = c.GetHue() * 0.01;
                    hsb[3 * i + 1] = c.GetSaturation();
                    hsb[3 * i + 2] = c.GetBrightness();

                }

                return new ImageData8HSB(hsb, Sepia);
            }
        }

        public class ImageData8HSB
        {
            public double[] Data { get; } // H:0.00-3.60, S,B:0.0-1.0
            public int Sepia { get; }

            public ImageData8HSB(double[] data, int value)
            {
                Data = data;
                Sepia = value;
            }
        }
    
        public static List<ImageData8> Reduce(this List<ImageData32> digits) => digits.Select(_ => _.Reduce()).ToList();
        public static List<ImageData8HSB> ToHSB(this List<ImageData8> digits) => digits.Select(_ => _.ToHSB()).ToList();
        public static List<(double[] x, double[] t)> ToData(this List<ImageData8> values)
            => values.Select(_ => (_.Data.Select(x => (double)x).ToArray(), new double[] { _.Sepia==0?1:0, _.Sepia==1?1:0 }))
            .ToList();

        public static List<(double[] x, double[] t)> HuesToData(this List<ImageData8HSB> values)
            => values.Select(_ => (_.Data.ToArray().Select((x,i)=>(x,i)).Where(w=>w.i%3==0).Select(w=>w.x).ToArray(), new double[] { _.Sepia == 0 ? 1 : 0, _.Sepia == 1 ? 1 : 0 }))
            //=> values.Select(_ => (_.Data.ToArray(), new double[] { _.Sepia == 0 ? 1 : 0, _.Sepia == 1 ? 1 : 0 }))
            .ToList();

        public static List<(double[] x, double[] t)> ToData(this List<ImageData8HSB> values)
            => values.Select(_ => (_.Data.ToArray(), new double[] { _.Sepia == 0 ? 1 : 0, _.Sepia == 1 ? 1 : 0 }))
            .ToList();
    }

    internal static class DigitsUtils
    {
        public class RawDigit
        {
            public byte[] Data { get; }
            public int Value { get; }

            public RawDigit(byte[] data, int value)
            {
                Data = data;
                Value = value;
            }

            public ReducedDigit Reduce()
            {
                var data = new byte[8 * 8];

                for (int ty = 0; ty < 8; ty++)
                {
                    for (int tx = 0; tx < 8; tx++)
                    {
                        byte count = 0;
                        for (int y = 0; y < 4; y++)
                        {
                            for (int x = 0; x < 4; x++)
                            {
                                if (Data[32 * (4 * ty + y) + (4 * tx + x)] != 0)
                                    count++;
                            }
                        }
                        data[ty * 8 + tx] = count;
                    }
                }

                return new ReducedDigit(data, Value);
            }
        }

        public class ReducedDigit
        {
            public byte[] Data { get; }
            public int Value { get; }

            public ReducedDigit(byte[] data, int value)
            {
                Data = data;
                Value = value;
            }
        }
        public static List<ReducedDigit> Reduce(this List<RawDigit> digits) => digits.Select(_ => _.Reduce()).ToList();
        public static List<(double[] x, double[] t)> ToData(this List<ReducedDigit> digits)
            => digits.Select(_ => (_.Data.Select(x => (double)x).ToArray(),
            (new Func<int, double[]>(i =>
            {
                var r = new double[10];
                r[i] = 1;
                return r;
            }))(_.Value)))
            .ToList();
    }
}
