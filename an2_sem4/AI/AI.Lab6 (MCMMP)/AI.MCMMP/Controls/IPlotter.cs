using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MCMMP.Controls
{
    public interface IPlotter
    {
        void Clear(IPlottable plottable = null);
        void DrawPoint(IPlottable plottable, double x, double y, Color color);
        void DrawSegment(IPlottable plottable, double x1, double y1, double x2, double y2, Color color);
        void DrawRectangle(IPlottable plottable, double x1, double y1, double x2, double y2, Color color);
        void Deploy();
    }
}
