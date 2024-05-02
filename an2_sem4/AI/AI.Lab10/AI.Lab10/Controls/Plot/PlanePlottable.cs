using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Controls.Plot
{
    internal class PlanePlottable : AbstractPlottable
    {
        double W0 = 0, W1 = 0, W2 = 0;
        double Amplitude = 1;
        Func<double, double, double> Function;

        List<(double X1, double Y1, double X2, double Y2, Color C)> Rectangles = new List<(double X1, double Y1, double X2, double Y2, Color C)>();

        public PlanePlottable(double w0, double w1, double w2, double x, double y, double w, double h, int squaresCount = 20)
        {
            W0 = w0;
            W1 = w1;
            W2 = w2;
            Function = (x1, x2) => W0 + W1 * x1 + W2 * x2;

            double stepx = w / squaresCount;
            double stepy = h / squaresCount;
            Console.WriteLine($"SSSS = {stepx} {stepy}");

            for (int i = 0; i < squaresCount; i++)
            {
                for (int j = 0; j < squaresCount; j++)
                {
                    var x1 = x + i * stepx;
                    var y1 = y + j * stepy;
                    var x2 = x + (i + 1) * stepx;
                    var y2 = y + (j + 1) * stepy;
                    Amplitude = Math.Max(Amplitude, Avg(x1, y1, x2, y2));
                }
            }

            for (int i = 0; i < squaresCount; i++)
            {
                for (int j = 0; j < squaresCount; j++)
                {
                    var x1 = x + i * stepx;
                    var y1 = y + j * stepy;
                    var x2 = x + (i + 1) * stepx;
                    var y2 = y + (j + 1) * stepy;
                    Rectangles.Add((x1, y1, x2, y2, color(Function((x1 + x2) / 2, (y1 + y2) / 2), 0)));
                }
            }
        }

        double Avg(double x1, double y1, double x2, double y2)
        {
            return (Function(x1, y2) + Function(x2, y1)) / 2;
        }

        public override void Plot(IPlotter plotter)
        {
            plotter.Clear(this);
            foreach (var r in Rectangles)
                plotter.DrawRectangle(this, r.X1, r.Y1, r.X2, r.Y2, r.C);
        }

        Color color(double x, double y)
        {
            var g = (int)((y - x) * 256 / Amplitude);
            //g = g * g * g / (256 * 256);
            g = (g + 256).Clamp(0, 511);
            if (g < 256)
            {
                return Color.FromArgb(128, 255 - g, g, 0);
            }
            g -= 256;
            return Color.FromArgb(128, 0, 255 - g, g);

        }
    }
}
