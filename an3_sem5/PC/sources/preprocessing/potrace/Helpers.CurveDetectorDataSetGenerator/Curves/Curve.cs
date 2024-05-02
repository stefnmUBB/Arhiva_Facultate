using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Helpers.CurveDetectorDataSetGenerator.Curves
{
    internal interface Curve
    {
        void Draw(Graphics g, int steps, double thickness);
    }

    public static class CurveRenderer
    {
        public static void DrawLine(Graphics g, PointF p1, PointF p2, double thickness)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var pen = new Pen(Color.Black, (float)thickness);
            g.DrawLine(pen, p1, p2);
        }

        public static void DrawLines(Graphics g, PointF[] pts, double thickness)
        {
            if (pts.Any(p => double.IsNaN(p.X) || double.IsNaN(p.Y) || p.X < -1000 || p.X > 1000 || p.Y < -1000 || p.Y > 1000))
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(Color.Black, (float)thickness))
                g.DrawLines(pen, pts);
            g.Flush(System.Drawing.Drawing2D.FlushIntention.Sync);
        }

        public static void DrawLines(Graphics g, PointF[] pts, double thickness, Color color)
        {
            if (pts.Any(p => double.IsNaN(p.X) || double.IsNaN(p.Y) || p.X < -1000 || p.X > 1000 || p.Y < -1000 || p.Y > 1000))
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(color, (float)thickness))
                g.DrawLines(pen, pts);
            g.Flush(System.Drawing.Drawing2D.FlushIntention.Sync);
        }
    }
}
