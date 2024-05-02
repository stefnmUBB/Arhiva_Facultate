using Helpers.CurveDetectorDataSetGenerator.Curves;
using System;
using System.Drawing;
using System.Linq;

namespace Helpers.CurveDetectorDataSetGenerator
{
    internal static class CurveEncoder
    {
        public static double[] Encode(Bezier5 curve, double thickness)
        {
            var r = new double[13];
            if (curve == null) return r;
            r[0] = thickness;
            Array.Copy(curve.Points.Select(_ => new double[] { _.X, _.Y }).SelectMany(_ => _).ToArray(), 0, r, 1, 12);
            return r;
        }

        public static void Decode(double[] data, out Bezier5 curve, out double thickness)
        {
            if (data.Length != 13)
                throw new ArgumentException("Invalid data");            
            thickness = data[0];
            var points = data.Skip(1).Select((x, i) => (x, i: i / 2)).GroupBy(_ => _.i)
                .Select(g => new PointF((float)g.First().x, (float)g.Last().x)).ToArray();
            curve = new Bezier5(points);
        }
    }
}
