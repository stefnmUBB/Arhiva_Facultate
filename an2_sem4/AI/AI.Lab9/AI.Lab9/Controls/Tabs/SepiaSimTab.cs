using AI.Lab9.Controls.Plot;
using AI.Lab9.Solvers;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab9.Controls.Tabs
{    
    public partial class SepiaSimTab : UserControl
    {
        ANNController ANNController;
        
        (
            List<(double[] Inputs, double[] Outputs)> Train, 
            List<(double[] Inputs, double[] Outputs)> Test
        ) Data { get; set; }

        public SepiaSimTab()
        {
            InitializeComponent();            

            TestResultsBindings = new BindingList<SepiaTestResult>(TestResults);
            TestViewer.DataSource = TestResultsBindings;

            ConfusionMatrix.SetLabels("No Filters", "Has Filters");
        }

        private void IrisSimTab_Load(object sender, EventArgs e)
        {
            ANNController = new ANNController(ANNStatusBar);
            ANNController.NeuralNetwork = Networks.SepiaANN();
            ANNController.NeuralNetwork.IterationsCount = 20;
            ANNController.TestFinished += ANNController_TestFinished;

            ExecuteButton_Click(null, null);            
        }

        List<SepiaTestResult> TestResults = new List<SepiaTestResult>();
        BindingList<SepiaTestResult> TestResultsBindings;

        private void ANNController_TestFinished(Algebra.Matrix results)
        {
            BeginInvoke(new Action(() =>
            {
                var rows = results.Rows.ToArray();
                var predtest = new List<(double, double, int)>();
                for(int i=0;i<rows.Length;i++)
                {
                    predtest.Add((Data.Test[i].Inputs[3], Data.Test[i].Inputs[0], (int)rows[i][3]));
                }                

                TestResults.Clear();
                TestResults.AddRange(
                    rows.Select(_ => new SepiaTestResult(
                        _[0].ToString("F4"), _[1].ToString("F4"), ((int)_[2]).ToString(), ((int)_[3]).ToString())
                    ));
                TestResultsBindings.ResetBindings();

                var real = results.GetColumn(results.ColsCount - 1).Select(_ => (int)_).ToArray();
                var pred = results.GetColumn(results.ColsCount - 2).Select(_ => (int)_).ToArray();
                ConfusionMatrix.SetData(real, pred);
                ConfMatrixStats.Text = ConfusionMatrix.GetStatsInfo();
            }));
        }

        private void GenerateTrainDataButton_Click(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() => GenerateTrainData()));
        }

        private void TestData()
        {
            try
            {
                ANNController.Test(Data.Test);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void TestButton_Click(object sender, EventArgs e) => TestData();


        private List<double> Loss = new List<double>();

        private void LossReport(double loss)
        {
            if (double.IsNaN(loss)) loss = 10000;
            Loss.Add(loss);
            if (Loss.Count % 15 == 0) 
            {
                BeginInvoke(new Action(() =>
                {
                    var max = Math.Min(1000, Loss[0] + 1);
                    LossPlot.Clear(null);

                    new FunctionPlottable(Loss.ToList().Select(_ => _ * 10 / max)).Plot(LossPlot);
                }));
            }
        }

        private void GenerateTrainData()
        {
            if (ANNController.IsBusy)
            {
                MessageBox.Show("Neural Network is running. Wait for it to finish.");
                return;
            }
            Loss.Clear();
            var rawHSB = DataSets.FilterImages(@"C:\Users\Stefan\Desktop\sepiaData\c\sepia.dat");

            Data = rawHSB.Select(_ => (_.Input.Select((x, i) => (x, i)).Where(xi => xi.i % 3 == 0).Select(xi => xi.x).ToArray(), _.Output))
                    .SplitTrainTest();

            ANNController.Train(Data.Train, LossReport);


            var hsbPoints = rawHSB.SelectMany(rec
                => rec.Input
                    .Select((x, i) => (x, i: i / 3))
                    .GroupBy(xi => xi.i)
                    .Shuffle()
                    .Take(20)
                    .Select(g => g.Select(xi => xi.x).Concat(new double[] { rec.Output.ArgMax() }).ToArray())
                    .ToArray()
                )
                .Take(100)
                .ToArray();

            var H = hsbPoints.Select(_ => _[0]).ToArray();
            var S = hsbPoints.Select(_ => _[1]).ToArray();
            var B = hsbPoints.Select(_ => _[2]).ToArray();
            var C = hsbPoints.Select(_ => _[3] == 0 ? Color.Red : Color.Green).ToArray();


            BeginInvoke(new Action(() =>
            {
                HSPlot.Clear(null);
                PointsPlottable.MakePlot(H, S, C).Plot(HSPlot);
                PointsPlottable.MakePlot(H, B, C).Plot(HBPlot);
            }));            
        }        

        public class SepiaTestResult
        {
            public SepiaTestResult(string nonSepiaP, string sepiaP, string predicted, string real)
            {
                NonSepiaP = nonSepiaP;
                SepiaP = sepiaP;
                Predicted = predicted;
                Real = real;
            }

            public string NonSepiaP { get; set; }
            public string SepiaP { get; set; }            
            public string Predicted { get; set; }
            public string Real { get; set; }
            
        }

        private void TestViewer_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid.Rows[e.RowIndex].Cells[2].Value as string == grid.Rows[e.RowIndex].Cells[3].Value as string) 
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lime;
            else
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => {
                while (ANNController.IsBusy) ;
                Invoke(new Action(() => { GenerateTrainData(); }));
                while (ANNController.IsBusy) ;
                Invoke(new Action(() => TestData()));
            }));            

        }
    }
}
