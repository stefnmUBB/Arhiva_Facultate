﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MCMMP.Controls
{
    internal class PointsPlottable : AbstractPlottable
    {
        protected List<(double X, double Y, Color Color)> Points { get; } = new List<(double X, double Y, Color Color)>();
        public override void Plot(IPlotter plotter)
        {
            plotter.Clear(this);
            Points.ForEach(p => plotter.DrawPoint(this, p.X, p.Y, p.Color));
        }

        public void AddPoint(double x, double y, Color color) => Points.Add((x, y, color));
        public void AddPoint(double x, double y) => Points.Add((x, y, Color.Black));
        
    }
}
