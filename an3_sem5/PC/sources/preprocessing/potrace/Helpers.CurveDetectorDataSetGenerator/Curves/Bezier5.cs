using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.CurveDetectorDataSetGenerator.Curves
{
    internal class Bezier5
    {
        public PointF[] Points = new PointF[6];

        public Bezier5(PointF[] points)
        {
            Points = points;
        }

        public void Draw(Graphics g, int steps, double thickness)
        {
            var points = new PointF[steps + 1];
            for (int i = 0; i <= steps; i++)
            {                
                double t = 1.0 * i / steps;
                var c = 1 - t;
                double t2 = t * t, t3 = t2 * t, t4 = t3 * t, t5 = t4 * t;
                double c2 = c * c, c3 = c2 * c, c4 = c3 * c, c5 = c4 * c;

                var f = new double[]
                {
                    c5, 5*c4*t, 10*c3*t2, 10*c2*t3, 5*c*t4, t5
                };
                double x = 0, y = 0;
                for (int j = 0; j < 6; j++)
                {
                    x += f[j] * Points[j].X;
                    y += f[j] * Points[j].Y;
                }
                points[i]=(new PointF((float)x, (float)y));
            }
            CurveRenderer.DrawLines(g, points, thickness);            
        }

        public void Draw(Graphics g, int steps, double thickness, Color color)
        {
            var points = new PointF[steps + 1];
            for (int i = 0; i <= steps; i++)
            {
                double t = 1.0 * i / steps;
                var c = 1 - t;
                double t2 = t * t, t3 = t2 * t, t4 = t3 * t, t5 = t4 * t;
                double c2 = c * c, c3 = c2 * c, c4 = c3 * c, c5 = c4 * c;

                var f = new double[]
                {
                    c5, 5*c4*t, 10*c3*t2, 10*c2*t3, 5*c*t4, t5
                };
                double x = 0, y = 0;
                for (int j = 0; j < 6; j++)
                {
                    x += f[j] * Points[j].X;
                    y += f[j] * Points[j].Y;
                }
                points[i] = (new PointF((float)x, (float)y));
            }
            CurveRenderer.DrawLines(g, points, thickness, color);
        }

        public override string ToString() => "B5 " + string.Join("; ", Points);

    }
}
