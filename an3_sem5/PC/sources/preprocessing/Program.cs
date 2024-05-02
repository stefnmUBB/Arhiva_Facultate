using Licenta.Imaging;
using Licenta.Utils;
using Licenta.Commons.AI;
using Licenta.Commons.AI.Perceptrons;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Schema;
using System.Threading;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Configuration;
using System.Data.SqlTypes;
using Licenta.Commons.Utils;

namespace Helpers.CurveDetectorDataSetGenerator
{
    internal static class Program
    {
        static void CreateDataSet()
        {
            var n = 10000;
            using (var ms = new MemoryStream()) 
            {
                var items = new double[n][];
                int k = 0;
                object l = new object();

                Task f(int s, int e)
                {
                    return Task.Run(() =>
                    {
                        var g = new DataGenerator();
                        for (int i=s;i<e;i++)
                        {
                            if (i % 10 == 0) Console.WriteLine($"T{i}");
                            g.GenIO(out double[] im, out double[] d);
                            items[i] = im.Concat(d).ToArray();                            
                        }
                    });
                }

                var t1 = f(0, n / 3);                
                var t2 = f(n / 3, 2*n/3);
                var t3 = f(2 * n / 3, n);
                t1.Wait();
                t2.Wait();                
                t3.Wait();                

                Console.WriteLine("Saving");

                using (var bw = new BinaryWriter(ms)) 
                {
                    bw.Write(n);                                       

                    for (int x = 0; x < n; x++)
                    {
                        for (int i = 0; i < items[x].Length; i++)
                            bw.Write(items[x][i]);                        
                    }
                }
                File.WriteAllBytes("curves_dataset.dat", ms.ToArray());
                Console.WriteLine("Done.");
            }
        }

        static IEnumerable<(double[] i, double[] o)> LoadDataSet()
        {            
            using(var ms=new MemoryStream(File.ReadAllBytes("curves_dataset.dat")))
            using (var br = new BinaryReader(ms))
            {
                var n = br.ReadInt32();
                for (int i = 0; i < n; i++)
                {
                    var input = new double[64 * 64];
                    var output = new double[13 * 8];

                    for (int k = 0; k < input.Length; k++) input[k] = br.ReadDouble();
                    for (int k = 0; k < output.Length; k++) output[k] = br.ReadDouble() / 64;
                    yield return (input, output);
                }
            }
        }

        static void ProcessDataSet()
        {
            var dataset = LoadDataSet().ToArray();
            var inputs = dataset.Select(_ => _.i).ToArray();
            var outputs = dataset.Select(_ => _.o).ToArray();

            using (var f = File.CreateText("bezier.csv"))
            {
                foreach (var d in dataset)
                    f.WriteLine($"{string.Join(",", d.i)},{string.Join(",", d.o)}");
            }
            return;
            Console.WriteLine(string.Join(" ", inputs.First().Take(20)));
            Console.WriteLine(string.Join(" ", outputs.First().Take(20)));

            File.WriteAllLines("ins.txt", inputs.Select(_ => string.Join(" ", _)));
            File.WriteAllLines("outs.txt", outputs.Select(_ => string.Join(" ", _)));

            int imgx = 0;

            double[] GetImage(double[] outp)
            {
                //Console.WriteLine("-----------------");
                var data = new double[13];
                using (var bmp = new Bitmap(64, 64, PixelFormat.Format32bppPArgb))
                {
                    using (var g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        for (int k = 0; k < 16; k++)
                        {
                            for (int p = 0; p < 13; p++)
                                data[p] = outp[13 * k + p] * 64;
                            //Console.WriteLine(String.Join(" ", data));
                            CurveEncoder.Decode(data, out var bez, out var thick);
                            bez.Draw(g, 16, thick);
                        }
                    }                    
                    //bmp.Save($@"dbg\img{imgx++}.png");
                    return new ImageRGB(bmp).Items.Select(_ => _.R.Value).ToArray();
                }
            }

            var archBuilder = new AnnArchBuilder();
            archBuilder.LearningRate = 0.0001;
            archBuilder.Loss = new Func<double[], double[], double>((r, p) =>
            {
                if (p.Any(double.IsNaN))
                    return 1000;                

                var l0 = LossFunctions.MeanError(r, p);
                return l0;
                if (l0 > 1)
                {
                    var m = p.Max();
                    //Console.WriteLine($"Too big data :{p.Max()}");
                    return m;
                }

                //Console.WriteLine($"Out: {string.Join(" ", p.Take(10))}");
                double[] rCanvas = null, pCanvas = null;
                rCanvas = GetImage(r);
                pCanvas = GetImage(p);

                //Parallel.Invoke(() => rCanvas = GetImage(r), () => pCanvas = GetImage(p));
                var l = LossFunctions.MeanError(rCanvas, pCanvas);
                //Console.WriteLine($"iLoss = {l}");

                return l;                
                //return rCanvas.Zip(pCanvas, (x, y) => Math.Abs(x - y) * Math.Abs(x - y)).Sum();

                //Console.WriteLine($"item Loss = {loss}");

            });

            var lastLoss = double.PositiveInfinity;

            int i = 0;
            archBuilder.Optimize(inputs, outputs, (snn, s, loss) =>
            {
                imgx = 0;
                var dt = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss");
                snn.Save($@"bezann\ckp_{dt}_{s}_{i++}_{loss}.dat");
            }, attemptsCount: 1, layers: new List<AnnLayerModel>
            {
                new AnnLayerModel(typeof(Relu), 2000),
                new AnnLayerModel(typeof(Sigmoid), 800),
                new AnnLayerModel(typeof(Self), 500),
            }
            );
        }

        static void Deflate()
        {
            var path = "D:\\Users\\Stefan\\Datasets\\IAM_Words\\iam_words\\words\\a02\\a02-004\\a02-004-03-02.png";
            var ibmp = new Bitmap(path);
            ibmp = new Bitmap(ibmp, ibmp.Width * 3, ibmp.Height * 3);
            var img = new ImageRGB(ibmp);
            var o = img.ApplyPotrace();

            var bmp = new Bitmap(o.Bitmap);
            var g = Graphics.FromImage(bmp);
            

            var segments = new List<(PointF p1, PointF p2)>();

            foreach(var curve in o.Curves)
            {
                for(int i=0;i<curve.Length;i++)
                {
                    var p1 = new PointF((float)curve[i].A.X, (float)curve[i].A.Y);
                    var p2 = new PointF((float)curve[(i + 1) % curve.Length].A.X, (float)curve[(i + 1) % curve.Length].A.Y);
                    segments.Add((p1, p2));
                    g.DrawLine(Pens.Red, p1, p2);
                }
            }

            segments.ForEach(_=>Console.WriteLine(_));

            int rad = 10;

            double distp(int x, int y, PointF p)
            {
                return Math.Sqrt((x - p.X) * (x - p.X) + (y - p.Y) * (y - p.Y));
            }

            double dists(int x, int y, (PointF p1, PointF p2) s)
            {
                var a = s.p1.Y - s.p2.Y;
                var b = s.p2.X - s.p1.X;
                var c = s.p1.X * s.p2.Y - s.p2.X * s.p1.Y;
                return (a * x + b * y + c) / Math.Sqrt(a * a + b * b);
            }

            double dist(int x, int y, (PointF p1, PointF p2) s) => Math.Abs(dists(x, y, s));            

            bool Intersects(int x, int y, (PointF p1, PointF p2) s)
            {
                if (!(((double)y).IsBetween(s.p1.Y, s.p2.Y) || ((double)y).IsBetween(s.p2.Y, s.p1.Y))) 
                    return false;
                return dists(x, y, s) * dists(0, y, s) <= 0;
            }
            int countIntersections(int x, int y)
            {
                return segments.Select(s => Intersects(x, y, s)).Where(_ => _).Count();
            }

            IEnumerable<(PointF p1, PointF p2)> GetSegmentsInRadius(int x, int y, int r)
            {
                foreach(var s in segments)
                {
                    var d = dist(x, y, s);
                    var d1 = Math.Sqrt((x - s.p1.X) * (x - s.p1.X) + (y - s.p1.Y) * (y - s.p1.Y));
                    var d2 = Math.Sqrt((x - s.p2.X) * (x - s.p2.X) + (y - s.p2.Y) * (y - s.p2.Y));
                    var l = Math.Sqrt((s.p1.X - s.p2.X) * (s.p1.X - s.p2.X) + (s.p1.Y - s.p2.Y) * (s.p1.Y - s.p2.Y));

                    if (d < r && (d1 + d2 < r + l)) 
                        yield return s;
                }
            }

            Dictionary<(int X, int Y), bool> interior = new Dictionary<(int X, int Y), bool>();

            for(int y=0;y<bmp.Height;y++)
            {
                for(int x=0;x<bmp.Width;x++)
                {
                    if (countIntersections(x, y) % 2 == 1)
                        interior[(x, y)] = true;

                }
            }

            Dictionary<(int X, int Y), double> recs = new Dictionary<(int X, int Y), double>();

            for(int y=0;y<bmp.Height;y++)
            {
                for(int x=0;x<bmp.Width;x++)
                {
                    if (!interior.ContainsKey((x, y))) continue;
                    var segs = GetSegmentsInRadius(x, y, rad).ToArray();
                    if (segs.Length >= 2) 
                    {
                        if(segs.Length==2)
                        {
                            PointF p1=new PointF();
                            PointF p2=new PointF();
                            bool found = true;

                            if (segs[0].p1 == segs[1].p1) { p1 = segs[0].p1; p2 = segs[1].p1; found = true; }
                            if (segs[0].p1 == segs[1].p2) { p1 = segs[0].p1; p2 = segs[1].p2; found = true; }
                            if (segs[0].p2 == segs[1].p1) { p1 = segs[0].p2; p2 = segs[1].p1; found = true; }
                            if (segs[0].p2 == segs[1].p2) { p1 = segs[0].p2; p2 = segs[1].p2; found = true; }

                            if (found && distp(x, y, p1) < rad)
                                continue;                            
                        }

                        var avg = segs.Select(s => dist(x, y, s)).Average();
                        recs[(x, y)] = segs.Select(s => Math.Abs(dist(x, y, s) - avg)).Select(_ => _).Average();
                    }
                    
                }
            }

            foreach(var i in interior)
            {
                //g.FillRectangle(new SolidBrush(Color.FromArgb(32, 255, 0, 0)), i.Key.X, i.Key.Y, 1, 1);
            }

            var M = recs.Values.Max();
            foreach(var r in recs)
            {
                int v = (int)(r.Value / M * 255);
                if (v < 32) v = 0; else v = 255;
                if (v == 0)
                {
                    var c = Color.FromArgb(255, 0, 255 - v, 0);
                    g.FillRectangle(new SolidBrush(c), r.Key.X, r.Key.Y, 1, 1);
                }
            }


            Display.Show(new ImageRGB(bmp));

            bmp.Save("out.png");
            //var 

        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Deflate();
            //IAMXMLTranslate.Run();            
            //Application.Run(new Form1());
            Console.WriteLine("Done.");
            Console.ReadLine();
            return;

            //CreateDataSet();
            //ProcessDataSet();            
            //return;
            /*var bmp = new Bitmap(@"C:\Users\Stefan\Desktop\Untitled.png");
            //var bmp = new Bitmap(@"D:\Users\Stefan\Datasets\reteta.png");
            var nbmp = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format24bppRgb);
            Graphics.FromImage(nbmp).DrawImageUnscaled(bmp, 0, 0);
            bmp = nbmp;

            nbmp.Save("Saved.png");

            var img = new ImageRGB(bmp);
            img = Images.CannyEdgeDetection(img);
            img.ToBitmap().Save("result.png");*/

            var r = new Random();

            Func<double, double, double, double> f = (x, y, z) => 2 * x + 3 * y - z + x * y * 0.0025;

            var archBuilder = new AnnArchBuilder();

            var inputs = Enumerable.Range(0, 100).Select(_ => new double[3]
                { r.Next(-20,20) * r.NextDouble()/20, r.Next(-20, 20) * r.NextDouble()/20, r.Next(-20, 20) * r.NextDouble()/20 })
                .ToArray();            
            var outputs = inputs.Select(_ => new double[1] { f(_[0], _[1], _[2]) }).ToArray();

            var m = new AnnModel { InputLength = 3, OutputLength = 1 };
            m.HiddenLayers.Add(new AnnLayerModel(typeof(Self), 20));
            m.HiddenLayers.Add(new AnnLayerModel(typeof(Self), 30));
            m.HiddenLayers.Add(new AnnLayerModel(typeof(Self), 10));
            var ann = m.Compile();
            ann.Train(inputs, outputs, epochsCount: 50000, lossFunction: LossFunctions.MeanError, learningRate: 0.0001);

            var q = ann.PredictSingle(new double[] { 1.0/20, 2.0 / 20, 3.0 / 20 });
            Console.WriteLine($"{q[0]} {f(1.0 / 20, 2.0 / 20, 3.0 / 20)}");

            Console.ReadLine();

            return;

            var lastLoss = double.PositiveInfinity;
            int i = 0;
            archBuilder.Optimize(inputs, outputs, (snn, s, loss) =>
            {
                if (loss < lastLoss)
                {
                    lastLoss = loss;
                    snn.Save($@"annstestf\ckp_{DateTime.Now}_{i++}_{loss}.dat");
                }
            });


            //var outputs = inputs.Select(_ => new double[1] { f(_[0], _[1]) }).ToArray();

            //var runtimeAnn = RuntimeAnn.Load("ann.dat");
            /*var runtimeAnn = ann.Compile();

            var h = runtimeAnn.Train(inputs, outputs, epochsCount: 500);
            Console.WriteLine("History:");
            h.Loss.ForEach(Console.WriteLine);

            var outs = runtimeAnn.ComputeOutput(new double[] { 1, 2 });

            Console.WriteLine("Result = " + string.Join(", ", outs));

            runtimeAnn.Save("ann.dat");*/


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
