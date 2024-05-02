using AI.MCMMP.Algebra;
using AI.MCMMP.Controls;
using AI.MCMMP.Data;
using AI.MCMMP.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.MCMMP
{
    public partial class Form1 : Form
    {
        CsvData Csv;
        List<MeasureRecord> Records;

        public Form1()
        {
            InitializeComponent();
            InputSelector.SelectedIndex = 2;
        }

        void SolveGDPFreedom()
        {
            
            var rand = new Random();

            //Records.ForEach(Console.WriteLine);
            //MessageBox.Show(InputSelector.SelectedItem as string);


            if((InputSelector.SelectedItem as string)== "v2_world-happiness-report-2017.csv")
            {
                MessageBox.Show("Correlated data");
                ParametersBox.Text = "GDP + Freedom";
                SolveSingular(true);
                return;
            }

            var ttsplitter = Enumerable.Range(0, Records.Count).Select(i => rand.Next() % 100 < 80 ? 0:1)
                .Select((x, i) => (x, i));
            var trainRecords = ttsplitter.Where(tt => tt.x == 0).Select(tt => Records[tt.i]).ToList();
            var testRecords = ttsplitter.Where(tt => tt.x == 1).Select(tt => Records[tt.i]).ToList();

            var trainY = MCMMPUtils.MatrixFromList(trainRecords.Select(r => r.HappinessScore));
            var trainX = MCMMPUtils.MatrixFromTuples(trainRecords.Select(r => (1.0, r.EconomyGDPPerCapita, r.Freedom).ToTuple()));            

            //var Y = MCMMPUtils.MatrixFromList(Records.Select(r => r.HappinessScore));
            //var X = MCMMPUtils.MatrixFromTuples(Records.Select(r => (1.0, r.EconomyGDPPerCapita, r.Freedom).ToTuple()));       

            //var trainY = MCMMPUtils.MatrixFromList(Records.Select(r => r.HappinessScore));
            //var trainX = MCMMPUtils.MatrixFromTuples(Records.Select(r => (1.0, r.EconomyGDPPerCapita, r.Freedom).ToTuple()));

            var w = (trainX.Transpose * trainX).Inverse * trainX.Transpose * trainY;

            var f = new Func<double, double, double>((double x1, double x2) => w[0, 0] + w[1, 0] * x1 + w[2, 0] * x2);

            GDPFreedomFunctionLabel.Text = $"f(x1,x2) = {w[0, 0]:N3} + {w[1, 0]:N3}*x1 + {w[2, 0]:N3}*x2";
            

            var depthColors = new Dictionary<int, Color>();
            depthColors[-1] = Color.Red;
            depthColors[0] = Color.Green;
            depthColors[1] = Color.Blue;
            
            double amp = 1;

            Color color(double x, double y)
            {
                var g = ((int)((y - x) * 256 / amp) + 256).Clamp(0, 511);
                if (g < 256) 
                {
                    return Color.FromArgb(255 - g, g, 0);
                }
                g -= 256;
                return Color.FromArgb(0, g, 255 - g);

            }

            for (int i = 0; i < testRecords.Count; i++) 
            {
                var r = testRecords[i];
                amp = Math.Max(amp, Math.Abs(r.HappinessScore - f(r.EconomyGDPPerCapita, r.Freedom)));
            }

            var plot = new PointsPlottable();
            for (int i = 0; i < testRecords.Count; i++)
            {
                var r = testRecords[i];
                plot.AddPoint(r.EconomyGDPPerCapita, r.Freedom, color(r.HappinessScore, f(r.EconomyGDPPerCapita, r.Freedom)));
            }
            
            GDPFreedomPlotter.Clear(null);
            plot.Plot(GDPFreedomPlotter);

            new PlanePlottable(w[0, 0], w[1, 0], w[2, 0], -3, -3, 6, 6).Plot(GDPFreedomPlotter);

            GDPFreedomPlotter.Deploy();
        }

        private void InputSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Csv = new CsvData($@"Input\{InputSelector.SelectedItem}");

            foreach (var row in Csv) 
            {
                for (int i = 0; i < row.ColumnsCount; i++)
                    if (row[i] == "") row[i] = "0";
            }
            Records = Csv.ToObjects<MeasureRecord>().ToList();            


            ParametersBox.Items.Clear();
            ParametersBox.Items.AddRange(Csv.Columns);
            ParametersBox.SelectedIndex = 6;
            SolveGDPFreedom();
        }

        private void ParametersBox_SelectedIndexChanged(object sender, EventArgs e)
        {            

            SolveSingular();

        }


        void SolveSingular(bool v2 = false)
        {
            var colName = ParametersBox.SelectedItem as string;
            

            Matrix X = null;
            try
            {
                if (!v2) 
                    X = MCMMPUtils.MatrixFromTuples(Csv.Items.Select(r => (1.0, double.Parse(r[colName])).ToTuple()));
                else
                    X = MCMMPUtils.MatrixFromTuples(Records.Select(r => (1.0, r.EconomyGDPPerCapita + r.Freedom).ToTuple()));
            }
            catch (Exception)
            {
                MessageBox.Show("Cannot use this attribute for regression");
                return;
            }

            var Y = MCMMPUtils.MatrixFromList(Records.Select(r => r.HappinessScore));

            var w = (X.Transpose * X).Inverse * X.Transpose * Y;

            if (v2)
                Text = ($"w0={w[0, 0]}, w1={w[1, 0]}");

            var f = new Func<double, double>((double x) => w[0, 0] + w[1, 0] * x);

            //GDPFreedomFunctionLabel.Text = $"f(x1,x2) = {w[0, 0]:N3} + {w[1, 0]:N3}*x";

            SingularPlotter.Clear(null);

            var plot = new PointsPlottable();
            for (int i = 0; i < X.RowsCount; i++)
            {
                plot.AddPoint(X[i, 1], Y[i, 0], Color.Blue);
            }            
            plot.Plot(SingularPlotter);

            new FunctionPlottable(f, -100, 100, 5).Plot(SingularPlotter);

            SingularPlotter.Deploy();
        }

        private void PythonButton_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Tool\solver.py");
            var p = Process.Start(path, $"\"{(string)InputSelector.SelectedItem}\"");
        }
    }
}
