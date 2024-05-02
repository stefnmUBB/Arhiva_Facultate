using AI.GradientDescend.Algebra;
using AI.GradientDescend.Normalization;
using AI.GradientDescend.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace AI.GradientDescend
{
    public partial class LinnerudForm : Form
    {
        readonly List<(Matrix X, Matrix Y)> Items;

        public LinnerudForm()
        {
            InitializeComponent();

            var norm = new MinMaxNorm();

            var data = new CsvData(@"Input/linnerud.csv");

            Items = new Matrix(6, data.Items.Count, data.ToObjects<Data>()
                .Select(d =>
                    (d.Chins, d.Situps, d.Jumps, d.Weight, d.Waist, d.Pulse)
                )
                .Aggregate<(double, double, double, double, double, double), List<List<double>>>
                (
                    new List<List<double>>() { new List<double>(), new List<double>(), new List<double>(), new List<double>(), new List<double>(), new List<double>() },
                    (l, a) =>
                        new List<List<double>> {
                            l[0].Append(a.Item1).ToList(),
                            l[1].Append(a.Item2).ToList(),
                            l[2].Append(a.Item3).ToList(),
                            l[3].Append(a.Item4).ToList(),
                            l[4].Append(a.Item5).ToList(),
                            l[5].Append(a.Item6).ToList()
                        }
                )
                .Select(l => l.Normalize(norm))
                .SelectMany(l => l)
                .ToArray())
            .Transpose
                .Rows.Select(r => new Data()
                {
                    Chins = r[0], Situps = r[1], Jumps = r[2],
                    Weight = r[3], Waist = r[4], Pulse = r[5]
                })
                .ToList()                
                .Select(d => (
                    X: new Matrix(3, 1, d.Chins, d.Situps, d.Jumps),
                    Y: new Matrix(3, 1, d.Weight, d.Waist, d.Pulse)))
                .ToList();          
        }

        private void Solve(int noIterations = 200)
        {
            double eta = 0.001;
            var w0 = new Matrix(3, 1, 0, 0, 0);
            var w1 = new Matrix(3, 1, 0, 0, 0);            
            var w2 = new Matrix(3, 1, 0, 0, 0);            
            var w3 = new Matrix(3, 1, 0, 0, 0);

            var data = Items.SplitTrainTest(0.8);
            var trainData = data.Train;
            var testData = data.Test;

            var f = new Func<Matrix, Matrix, Matrix, Matrix, Matrix, Matrix>((x, v0, v1, v2, v3)
                => 1 * v0 + x[0, 0] * v1 + x[1, 0] * v2 + x[2, 0] * v3);            

            for(int i=0;i<noIterations;i++)
            {
                var tdata = trainData.Shuffle().ToList();
                foreach (var rec in tdata)
                {
                    var err = f(rec.X, w0, w1, w2, w3) - rec.Y;               
                    w0 -= eta * 1 * err;
                    w1 -= eta * rec.X[0, 0] * err;
                    w2 -= eta * rec.X[1, 0] * err;
                    w3 -= eta * rec.X[2, 0] * err;
                    //MessageBox.Show(w0.ToString());
                }
            }

            W0Label.Text = $"w0 = ({w0[0, 0]:F5},{w0[1, 0]:F5},{w0[2, 0]:F5})";
            W1Label.Text = $"w1 = ({w1[0, 0]:F5},{w1[1, 0]:F5},{w1[2, 0]:F5})";
            W2Label.Text = $"w2 = ({w2[0, 0]:F5},{w2[1, 0]:F5},{w2[2, 0]:F5})";
            W3Label.Text = $"w3 = ({w3[0, 0]:F5},{w3[1, 0]:F5},{w3[2, 0]:F5})";

            var F = new Func<Matrix, Matrix>(x => f(x, w0, w1, w2, w3));

            var testsOut = testData.Select(t => F(t.X) - t.Y);
            var mmean = testsOut.Aggregate((a, b) => a + b) * (1 / testData.Count());
            var error = testData.Count() == 0 ? 0 : testsOut.Select(t => (t - mmean).EuclideanNormSquared).Average();

            ErrorLabel.Text = $"Error = {error:F5}";



        }

        private void LinnerudForm_Load(object sender, EventArgs e)
        {
            Solve();
        }

        class Data
        {
            //Chins, Situps, Jumps, Weight, Waist, Pulse
            public double Chins { get; set; }
            public double Situps { get; set; }
            public double Jumps { get; set; }
            public double Weight { get; set; }
            public double Waist { get; set; }
            public double Pulse { get; set; }

            public override string ToString()
                => $"{Chins} {Situps} {Jumps} | {Weight} {Waist} {Pulse}";

        }
    }
}
