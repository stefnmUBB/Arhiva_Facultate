using System.Collections.Generic;
using System.Drawing;

namespace Helpers.CurveDetectorDataSetGenerator.Curves
{
    public class Bezier4 : Curve, IPointDefinedCurve
    {
        public PointF StartPoint { get => P1; set => P1 = value; }
        public PointF EndPoint { get => P4; set => P4 = value; }

        public PointF P1 { get; private set; }
        public PointF P2 { get; }
        public PointF P3 { get; }
        public PointF P4 { get; private set; }

        public Bezier4(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
        }

        public Bezier4(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
            : this(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2), new PointF((float)x3, (float)y3), new PointF((float)x4, (float)y4))
        { }

        public void Draw(Graphics g, int steps, double thickness)
        {
            var points = new List<PointF>();
            for (int i = 0; i <= steps; i++)
            {
                double t = 1.0 * i / steps;
                var f1 = (1 - t) * (1 - t) * (1 - t);
                var f2 = 3 * (1 - t) * (1 - t) * t;
                var f3 = 3 * (1 - t) * t * t;
                var f4 = t * t * t;

                var x = f1 * P1.X + f2 * P2.X + f3 * P3.X + f4 * P4.X;
                var y = f1 * P1.Y + f2 * P2.Y + f3 * P3.Y + f4 * P4.Y;
                points.Add(new PointF((float)x, (float)y));
            }
            CurveRenderer.DrawLines(g, points.ToArray(), thickness);
        }

        public override string ToString() => $"B4 {P1} {P2} {P3} {P4}";
    }
}
