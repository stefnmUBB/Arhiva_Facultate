using Helpers.CurveDetectorDataSetGenerator.Curves;
using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Parallelization;
using Licenta.Commons.Utils;
using Licenta.Imaging;
using Licenta.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Helpers.CurveDetectorDataSetGenerator
{
    internal class DataGenerator
    {
        Random Random = new Random();
        static int i = 0;

        int RandExp(int max)
        {
            double[] v = new double[max];
            for (int i = 0; i < max; i++) v[i] = 1 - Math.Exp(-0.5 * i);            

            var r = Random.NextDouble();
            for(int i=0;i<max-1;i++)
            {
                if (v[i] <= r && r < v[i + 1]) return i;                
            }
            return max - 1;
        }

        public void GenIO(out double[] image, out double[] data)
        {
            int maxBCount = 8;
            var cCount = 1 + RandExp(maxBCount);            
            var curves = GenerateCurves(cCount);

            var lCurves = new List<double[]>();

            using (var bmp = new Bitmap(64, 64, System.Drawing.Imaging.PixelFormat.Format32bppPArgb))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    foreach (var curve in curves)
                    {
                        var thickness = Random.NextDouble() * 3;
                        curve.Draw(g, Random.Next(10, 100), thickness);
                        lCurves.Add(CurveEncoder.Encode(curve, thickness));
                    }
                }

                while (lCurves.Count < maxBCount)
                    lCurves.Add(CurveEncoder.Encode(null, 1));
                //bmp.Save($@"cvtest\{i}.png");

                data = lCurves.SelectMany(_ => _).Select(_ => _ / 64.0).ToArray();

                //File.WriteAllText($@"cvtest\{i}.txt", string.Join(Environment.NewLine, curves.Select(_ => _.ToString())));
                i++;

                var edges = new ImageRGB(bmp).CannyEdgeDetectionMatrix();
                //new ImageRGB(edges).ToBitmap().Save($@"cvtest\edges{i}.png");

                image = edges.Items.ToArray();
                
            }
        }    

        private Bezier5[] GenerateCurves(int count)
        {
            List<Bezier5> curves = new List<Bezier5>();
            Bezier5 prevCurve = null;

            for(int i=0;i<count;i++)
            {
                var c = GenerateRandomCurve();
                if (prevCurve!=null && Random.NextDouble() < 0.7)
                    c.Points[0] = prevCurve.Points[5];                
                prevCurve = c;
                curves.Add(c);
            }
            return curves.ToArray();
        }

        private Bezier5 GenerateRandomCurve()
        {
            var pts = new PointF[6];
            for(int i = 0; i < 6;i++)
            {
                pts[i] = new PointF((float)RandomDouble(), (float)RandomDouble()); ;
            }
            return new Bezier5(pts);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private double RandomDouble()
        {
            return 80 * Random.NextDouble() - 8;
        }

    }
}
