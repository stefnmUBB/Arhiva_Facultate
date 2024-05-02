using System;
using System.Collections.Generic;
using System.Drawing;

namespace Helpers.CurveDetectorDataSetGenerator.Curves
{
    // equation:
    // x(t)=Center.X+Scale.X*cos(t+Tilt)
    // y(t)=Center.Y+Scale.Y*sin(t)
    // where t = ArcStart..ArcEnd
    internal class Ellipse : Curve
    {
        public PointF Center { get; }
        public double ScaleX { get; }
        public double ScaleY { get; }
        public double Tilt { get; }
        public double ArcStart { get; }
        public double ArcEnd { get; }

        public Ellipse(PointF center, double scaleX, double scaleY, double tilt, double arcStart, double arcEnd)
        {
            Center = center;
            ScaleX = scaleX;
            ScaleY = scaleY;
            Tilt = tilt % (2 * Math.PI);
            ArcStart = arcStart % (2 * Math.PI);
            ArcEnd = arcEnd % (2 * Math.PI);
        }

        public void Draw(Graphics g, int steps, double thickness)
        {          
            var points = new List<PointF>();
            for (int i = 0; i <= steps; i++)
            {
                double t = ArcStart + (1.0 * i / steps) * (ArcEnd - ArcStart);
                var x = Center.X + ScaleX * Math.Cos(t + Tilt);
                var y = Center.Y + ScaleY * Math.Sin(t);
                
                points.Add(new PointF((float)x, (float)y));
            }
            points.Add(points[0]);
            CurveRenderer.DrawLines(g, points.ToArray(), thickness);
        }

        public override string ToString() => $"EL {Center} {ScaleX} {ScaleY} {Tilt} {ArcStart} {ArcEnd}";
    }
}
