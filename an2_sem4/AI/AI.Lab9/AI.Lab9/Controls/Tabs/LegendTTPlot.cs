using AI.Lab9.Controls.Plot;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab9.Controls.Tabs
{
    public partial class LegendTTPlot : UserControl
    {
        public LegendTTPlot()
        {
            InitializeComponent();
        }

        public Dictionary<int, (string Name, Color Color)> LegendLabels = new Dictionary<int, (string, Color)>();

        public void AddLegend(int _class, string name)
        {
            LegendLabels[_class] = (name, Color.Black);
            UpdateLegend();
        }

        private void UpdateLegend()
        {
            int cnt = LegendLabels.Count;
            int i = 0;

            foreach (var k in LegendLabels.Keys.ToArray()) 
            {
                LegendLabels[k] = (LegendLabels[k].Name, ColorFromHue(i * 360.0f / cnt));
                i++;
            }

            LegendLabel.Text = "Legend\n" + LegendLabels.OrderBy(kv => kv.Key).Select(kv => "    " + kv.Value.Name).JoinToString("\n");
            LegendLabel.Left = Width - LegendLabel.Width - 3;
        }


        public (double X, double Y, int C)[] TrainData = new (double X, double Y, int C)[0];
        public (double X, double Y, int C)[] TestRealData = new (double X, double Y, int C)[0];
        public (double X, double Y, int C)[] TestPredictedData = new (double X, double Y, int C)[0];

        public void SetTrainData((double X, double Y, int C)[] data)
        {
            TrainData = data.ToArray();

            var minX = data.Min(_ => _.X);
            var maxX = data.Max(_ => _.X);
            var minY = data.Min(_ => _.Y);
            var maxY = data.Max(_ => _.Y);
            var vsize = Math.Max(1, Math.Min(maxX - minX, maxY - minY));

            Plotter.ViewPoint = new Point((int)(-20 * (minX + maxX) / 2), (int)(-20 * (minY + maxY) / 2));
            Plotter.Zoom = 5;
        }

        public void SetTestData((double X, double Y, int C)[] data)
        {
            TestRealData = data.ToArray();
        }

        public void SetPredictedData((double X, double Y, int C)[] data)
        {
            TestPredictedData = data.ToArray();
        }

        public void PlotTrain()
        {
            var trainX = TrainData.Select(_ => _.X).ToArray();
            var trainY = TrainData.Select(_ => _.Y).ToArray();
            var trainC = TrainData.Select(_ => Color.FromArgb(60, LegendLabels[_.C].Color)).ToArray();
            PointsPlottable.MakePlot(trainX, trainY, trainC).Plot(Plotter);
        }

        public void PlotRealTest()
        {
            var trainX = TestRealData.Select(_ => _.X).ToArray();
            var trainY = TestRealData.Select(_ => _.Y).ToArray();
            var trainC = TestRealData.Select(_ => LegendLabels[_.C].Color).ToArray();
            PointsPlottable.MakePlot(trainX, trainY, trainC).Plot(Plotter);
        }

        public void PlotPredictedTest()
        {
            var trainX = TestPredictedData.Select(_ => _.X).ToArray();
            var trainY = TestPredictedData.Select(_ => _.Y).ToArray();
            var trainC = TestPredictedData.Select(_ => LegendLabels[_.C].Color).ToArray();
            PointsPlottable.MakePlot(trainX, trainY, trainC).Plot(Plotter);
        }

        public void Plot()
        {
            Plotter.Clear(null);
            PlotTrain();
            if (!ShowPredictedCheckBox.Checked)
                PlotRealTest();
            else
                PlotPredictedTest();
        }


        static Color ColorFromHue(float hue)
        {
            var temp = new HSV();
            temp.h = hue;
            temp.s = 1;
            temp.v = 0.5f;
            return ColorFromHSL(temp);
        }

        public struct HSV { public float h; public float s; public float v; }
        
        static public Color ColorFromHSL(HSV hsl)
        {
            if (hsl.s == 0)
            { int L = (int)hsl.v; return Color.FromArgb(255, L, L, L); }

            double min, max, h;
            h = hsl.h / 360d;

            max = hsl.v < 0.5d ? hsl.v * (1 + hsl.s) : (hsl.v + hsl.s) - (hsl.v * hsl.s);
            min = (hsl.v * 2d) - max;

            Color c = Color.FromArgb(255, (int)(255 * RGBChannelFromHue(min, max, h + 1 / 3d)),
                                          (int)(255 * RGBChannelFromHue(min, max, h)),
                                          (int)(255 * RGBChannelFromHue(min, max, h - 1 / 3d)));
            return c;
        }

        static double RGBChannelFromHue(double m1, double m2, double h)
        {
            h = (h + 1d) % 1d;
            if (h < 0) h += 1;
            if (h * 6 < 1) return m1 + (m2 - m1) * 6 * h;
            else if (h * 2 < 1) return m2;
            else if (h * 3 < 2) return m1 + (m2 - m1) * 6 * (2d / 3d - h);
            else return m1;

        }

        private void LegendLabel_Paint(object sender, PaintEventArgs e)
        {
            float y = LegendLabel.Font.GetHeight() + LegendLabel.Font.GetHeight() / 2 - 3;
            foreach (var c in LegendLabels.OrderBy(kv => kv.Key).Select(_ => _.Value.Color).ToArray())
            {
                e.Graphics.FillEllipse(new SolidBrush(c), 3, y, 6, 6);
                y += LegendLabel.Font.GetHeight();
            }
        }

        private void ShowPredictedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Plot();
        }      
    }
}
