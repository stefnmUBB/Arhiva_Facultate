using AI.GradientDescend.Algebra;
using AI.GradientDescend.Controls;
using AI.GradientDescend.Normalization;
using AI.GradientDescend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.GradientDescend
{
    public partial class Form1 : Form
    {
        CsvData CSV = new CsvData(@"Input\2017.csv");

        public Form1()
        {
            InitializeComponent();

            SolveUnivariate();
            SolveMultivariate();
        }

        public void SolveUnivariate(int noIterations = 200,
            INormalizationMethod normGdp=null, INormalizationMethod normHappiness = null)
        {
            var Y = CSV.Select(r => r.Get<double>("Happiness.Score")).Normalize(normHappiness).ToList();
            var X = CSV.Select(r => r.Get<double>("Economy..GDP.per.Capita.")).Normalize(normGdp).ToList();
            var data = X.Zip(Y, (x, y) => (X: x, Y: y)).ToList().SplitTrainTest();
            var trainData = data.Train;
            var testData = data.Test;
           
            double w0 = 0;
            double w1 = 0;
            double eta = 0.001;

            var f = new Func<double, double, double, double>((x, v0, v1) => 1 * v0 + x * v1);

            if (!UniBatchBox.Checked)
            {
                for (int i = 0; i < noIterations; i++)
                {
                    var tdata = trainData.Shuffle().ToList();
                    foreach (var rec in tdata)
                    {
                        var err = f(rec.X, w0, w1) - rec.Y;
                        w0 -= eta * err * 1;
                        w1 -= eta * err * rec.X;
                    }
                }
            }
            else
            {

                eta = 0.000021;
                //var err = trainData.Select(rec => (f(rec.X1, rec.X2, w0, w1, w2) - rec.Y)).Average();
                for (int i = 0; i < noIterations; i++)
                {
                    var _w0 = w0;
                    var _w1 = w1;                    
                    w0 -= eta * trainData.Select(rec => (f(rec.X, _w0, _w1) - rec.Y)).Sum();
                    w1 -= eta * trainData.Select(rec => (f(rec.X, _w0, _w1) - rec.Y) * rec.X).Sum();                    
                }                
            }

            Console.WriteLine($"{w0} {w1}");

            UniPlotter.Clear(null);

            var F = new Func<double, double>(x => f(x, w0, w1));

            PointsPlottable.MakePlot(trainData.Select(d => d.X), trainData.Select(d => d.Y), Color.FromArgb(64, 0, 255, 0))
                .Plot(UniPlotter);

            var dX = testData.Select(d => d.X).ToList();
            var dY = testData.Select(d => d.Y).ToList();           
            PointsPlottable.MakePlot(dX, dX.Select(F), Color.Blue).Plot(UniPlotter);
            PointsPlottable.MakePlot(dX, dY, Color.Red).Plot(UniPlotter);
            
            new FunctionPlottable(x => f(x, w0, w1), -100, 100, 10).Plot(UniPlotter);

            UniOutputLabel.Text = $"f(x) = {w0:F4} + {w1:F4} * x";
            var mean = testData.Select(rec => F(rec.X) - rec.Y).Average();
            var meanSq = testData.Select(rec => (F(rec.X) - rec.Y)* (F(rec.X) - rec.Y)).Average();
            UniErrorLabel.Text = $"Error = Mean:{mean:F4};\tMeanSquared:{meanSq:F4}";
        }

        public void SolveMultivariate(int noIterations = 200,
            INormalizationMethod normGdp = null, INormalizationMethod normFreedom = null, INormalizationMethod normHappiness = null)
        {
            var Y = CSV.Select(r => r.Get<double>("Happiness.Score")).Normalize(normHappiness).ToList();
            var X1 = CSV.Select(r => r.Get<double>("Economy..GDP.per.Capita.")).Normalize(normGdp).ToList();
            var X2 = CSV.Select(r => r.Get<double>("Freedom")).Normalize(normFreedom).ToList();
            var X = X1.Zip(X2, (x1, x2) => (X1: x1, X2: x2)).ToList();

            var data = X.Zip(Y, (x, y) => (X1: x.X1, X2: x.X2, Y: y)).ToList().SplitTrainTest();
            var trainData = data.Train;
            var testData = data.Test;

            double w0 = 0;
            double w1 = 0;
            double w2 = 0;
            double eta = 0.001;

            var f = new Func<double, double, double, double, double, double>((x1, x2, v0, v1, v2)
                => 1 * v0 + x1 * v1 + x2 * v2);

            if (!MultiBatchBox.Checked)
            {
                for (int i = 0; i < noIterations; i++)
                {
                    var tdata = trainData.Shuffle().ToList();
                    foreach (var rec in tdata)
                    {
                        var err = f(rec.X1, rec.X2, w0, w1, w2) - rec.Y;
                        w0 -= eta * err * 1;
                        w1 -= eta * err * rec.X1;
                        w2 -= eta * err * rec.X2;
                    }
                }
            }
            else
            {
                eta = 0.000021;
                //var err = trainData.Select(rec => (f(rec.X1, rec.X2, w0, w1, w2) - rec.Y)).Average();
                for (int i = 0; i < noIterations; i++) 
                {
                    var _w0 = w0;
                    var _w1 = w1;
                    var _w2 = w2;
                    w0 -= eta * trainData.Select(rec => (f(rec.X1, rec.X2, _w0, _w1, _w2) - rec.Y)).Sum();
                    w1 -= eta * trainData.Select(rec => (f(rec.X1, rec.X2, _w0, _w1, _w2) - rec.Y) * rec.X1).Sum();
                    w2 -= eta * trainData.Select(rec => (f(rec.X1, rec.X2, _w0, _w1, _w2) - rec.Y) * rec.X2).Sum(); ;
                }

            }

            Console.WriteLine($"{w0} {w1} {w2}");            

            var F = new Func<double, double, double>((x1, x2) => f(x1, x2, w0, w1, w2));

            MultiPlotter.Clear(null);

            new PlanePlottable(w0, w1, w2, -3, -3, 6, 6).Plot(MultiPlotter);

            double amp = 1;
            var plot = new PointsPlottable();

            foreach(var r in testData.ToList())
            {
                amp = Math.Max(amp, Math.Abs(r.Y - F(r.X1, r.X2)));
            }

            foreach (var r in testData.ToList())
            {                
                plot.AddPoint(r.X1, r.X2, color(r.Y, F(r.X1, r.X2), amp));
            }

            plot.Plot(MultiPlotter);

            MultiPlotter.Deploy();

            MultiOutputLabel.Text = $"f(x1,x2) = {w0:F4} + {w1:F4} * x1 + {w2:F4} * x2";

            var mean = testData.Select(rec => F(rec.X1, rec.X2) - rec.Y).Average();
            var meanSq = testData.Select(rec => (F(rec.X1, rec.X2) - rec.Y) * (F(rec.X1, rec.X2) - rec.Y)).Average();
            MultiErrorLabel.Text = $"Error = Mean:{mean:F4};\tMeanSquared:{meanSq:F4}";
        }

        private static Color color(double x, double y, double amp)
        {
            var g = ((int)((y - x) * 256 / amp) + 256).Clamp(0, 511);
            if (g < 256)
            {
                return Color.FromArgb(255 - g, g, 0);
            }
            g -= 256;
            return Color.FromArgb(0, g, 255 - g);

        }

        private void UniRunButton_Click(object sender, EventArgs e)
        {
            INormalizationMethod gdpNorm = null;
            var gdpNormType = UniGdpNormSelector.Value;
            if (gdpNormType == typeof(ClipNorm))
                gdpNorm = new ClipNorm(20);
            else
                gdpNorm = Activator.CreateInstance(gdpNormType) as INormalizationMethod;

            INormalizationMethod happinessNorm = null;
            var happinessNormType = UniHappinessNormSelector.Value;
            if (happinessNormType == typeof(ClipNorm))
                happinessNorm = new ClipNorm(20);
            else
                happinessNorm = Activator.CreateInstance(happinessNormType) as INormalizationMethod;


            SolveUnivariate(200, gdpNorm, happinessNorm);
        }

        private void MultiRunButton_Click(object sender, EventArgs e)
        {
            INormalizationMethod gdpNorm = null;
            var gdpNormType = MultiGdpNormSelector.Value;
            if (gdpNormType == typeof(ClipNorm))
                gdpNorm = new ClipNorm(20);
            else
                gdpNorm = Activator.CreateInstance(gdpNormType) as INormalizationMethod;


            INormalizationMethod freedomNorm = null;
            var freedomNormType = MultiFreedomNormSelector.Value;
            if (freedomNormType == typeof(ClipNorm))
                freedomNorm = new ClipNorm(20);
            else
                freedomNorm = Activator.CreateInstance(freedomNormType) as INormalizationMethod;

            INormalizationMethod happinessNorm = null;
            var happinessNormType = MultiHappinessNormSelector.Value;
            if (happinessNormType == typeof(ClipNorm))
                happinessNorm = new ClipNorm(20);
            else
                happinessNorm = Activator.CreateInstance(happinessNormType) as INormalizationMethod;


            SolveMultivariate(200, gdpNorm, freedomNorm, happinessNorm);

        }

        private void LinnerudButton_Click(object sender, EventArgs e)
        {
            new LinnerudForm().ShowDialog();
        }
    }
}
