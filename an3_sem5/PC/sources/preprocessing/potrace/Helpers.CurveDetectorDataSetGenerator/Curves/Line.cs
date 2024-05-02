using System.Drawing;
using System.Drawing.Drawing2D;

namespace Helpers.CurveDetectorDataSetGenerator.Curves
{
    public class Line : Curve, IPointDefinedCurve
    {
        public PointF StartPoint { get => P1; set => P1 = value; }
        public PointF EndPoint { get => P2; set => P2 = value; }
        public PointF P1 { get; private set; }
        public PointF P2 { get; private set; }
        public Line(PointF p1, PointF p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public Line(double x1, double y1, double x2, double y2):this(new PointF((float)x1, (float)y1), new PointF((float)x2, (float)y2))
        {

        }

        public void Draw(Graphics g, int steps, double thickness)
        {
            CurveRenderer.DrawLine(g, P1, P2, thickness);            
        }

        public override string ToString() => $"LN {P1} {P2}";
    }
}
