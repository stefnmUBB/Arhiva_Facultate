using System.Collections.Generic;
using System.Drawing;

namespace Helpers.CurveDetectorDataSetGenerator.Curves
{
    internal class Bezier3 : Curve, IPointDefinedCurve
    {
        public PointF P1 { get; private set; }
        public PointF P2 { get; }
        public PointF P3 { get; private set; }
        public PointF StartPoint { get => P1; set => P1=value; }
        public PointF EndPoint { get => P3; set => P3=value; }

        public Bezier3(PointF p1, PointF p2, PointF p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
        }

        public Bezier3(double x1, double y1, double x2, double y2, double x3, double y3)
            : this(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), new PointF((float)x3, (float)y3))
        { }

        public void Draw(Graphics g, int steps, double thickness)
        {
            var points = new List<PointF>();
            for (int i = 0; i <= steps; i++) 
            {
                double t = 1.0 * i / steps;
                var f1 = (1 - t) * (1 - t);
                var f2 = 2 * (1 - t) * t;
                var f3 = t * t;
                var x = f1 * P1.X + f2 * P2.X + f3 * P3.X;
                var y = f1 * P1.Y + f2 * P2.Y + f3 * P3.Y;
                points.Add(new PointF((float)x, (float)y));
            }
            CurveRenderer.DrawLines(g, points.ToArray(), thickness);
        }

        public override string ToString() => $"B3 {P1} {P2} {P3}";
    }
}
