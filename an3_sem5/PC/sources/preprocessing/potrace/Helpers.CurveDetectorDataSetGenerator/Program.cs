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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
