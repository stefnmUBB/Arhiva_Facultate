using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.GradientDescend.Controls
{
    internal class FunctionPlottable : PointsPlottable
    {
        private Func<double, double> Function;
        public FunctionPlottable(Func<double, double> function, double minX, double maxX, int pointsCount = 100)
        {
            Function = function;

            double step = (maxX - minX) / pointsCount;
            for (int i = 0; i < pointsCount; i++)
            {
                var x = minX + step * i;
                AddPoint(x, Function(x));
            }

        }
        public override void Plot(IPlotter plotter)
        {
            plotter.Clear(this);
            
            for(int i=1;i<Points.Count;i++)
            {
                plotter.DrawSegment(this, Points[i - 1].X, Points[i - 1].Y, Points[i].X, Points[i].Y, Color.Green);
            }
        }
    }
}
