using AI.Lab9.Algebra;
using AI.Lab9.Controls.Plot;
using AI.Lab9.Solvers;
using AI.Lab9.Tools.ANN;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab9.Controls.Tabs
{    
    public partial class IrisSimTab : UserControl
    {
        ANNController ANNController;
        
        (
            List<(double[] Inputs, double[] Outputs)> Train, 
            List<(double[] Inputs, double[] Outputs)> Test
        ) Data { get; set; }
        
        public IrisSimTab()
        {
            InitializeComponent();

            for (int i = 0; i < DataSets.IrisLabels.Length; i++)
                DistributionPlot.AddLegend(i, DataSets.IrisLabels[i]);

            ConfusionMatrix.SetLabels(DataSets.IrisLabels);

            TestResultsBindings = new BindingList<FlowerTestResult>(TestResults);
            TestViewer.DataSource = TestResultsBindings;
        }

        private void IrisSimTab_Load(object sender, EventArgs e)
        {
            ANNController = new ANNController(ANNStatusBar);
            ANNController.NeuralNetwork = Networks.IrisANN();
            ANNController.TestFinished += ANNController_TestFinished;

            ExecuteButton_Click(null, null);
        }

        List<FlowerTestResult> TestResults = new List<FlowerTestResult>();
        BindingList<FlowerTestResult> TestResultsBindings;

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
                DistributionPlot.SetPredictedData(predtest.ToArray());
                DistributionPlot.Update();

                TestResults.Clear();
                TestResults.AddRange(
                    rows.Select(_ => new FlowerTestResult(
                        _[0].ToString("F4"), _[1].ToString("F4"), _[2].ToString("F4"),
                        DataSets.IrisLabels[(int)_[3]], DataSets.IrisLabels[(int)_[4]]))
                );
                TestResultsBindings.ResetBindings();

                var real = results.GetColumn(results.ColsCount - 1).Select(_ => (int)_).ToArray();
                var pred = results.GetColumn(results.ColsCount - 2).Select(_ => (int)_).ToArray();
                ConfusionMatrix.SetData(real, pred);
                ConfMatrixStats.Text = ConfusionMatrix.GetStatsInfo();
            }));
        }

        private void GenerateTrainDataButton_Click(object sender, EventArgs e)
        {
            GenerateTrainData();
            UpdateDistributionPlot();
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
            Loss.Add(loss);
            if (Loss.Count % 3 == 0) 
            {
                BeginInvoke(new Action(() =>
                {
                    var max = Math.Min(1000, Loss[0] + 1);
                    LossPlot.Clear(null);

                    new FunctionPlottable(Loss.ToList().Select(_ => _ * 10 / max)).Plot(LossPlot);
                    //FunctionPlottable.MakePlot(Loss.ToList().Select(_ => _ * 10 / max), Color.Red).Plot(LossPlot);
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
            Data = DataSets.Iris().SplitTrainTest();
            ANNController.Train(Data.Train, LossReport);

            DistributionPlot.SetTrainData(Data.Train.Select(_ => (_.Inputs[3], _.Inputs[0], _.Outputs.ArgMax())).ToArray());
            DistributionPlot.SetTestData(Data.Test.Select(_ => (_.Inputs[3], _.Inputs[0], _.Outputs.ArgMax())).ToArray());            
        }


        private void UpdateDistributionPlot()
        {
            DistributionPlot.Plot();
        }

        public class FlowerTestResult
        {
            public string Setosa { get; set; }
            public string Versicolor { get; set; }
            public string Virginica { get; set; }
            public string Predicted { get; set; }
            public string Real { get; set; }

            public FlowerTestResult(string setosa, string versicolor, string virginica, string predicted, string real)
            {
                Setosa = setosa;
                Versicolor = versicolor;
                Virginica = virginica;
                Predicted = predicted;
                Real = real;
            }
        }

        private void TestViewer_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid.Rows[e.RowIndex].Cells[3].Value as string == grid.Rows[e.RowIndex].Cells[4].Value as string) 
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lime;
            else
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(() => {
                while (ANNController.IsBusy) ;
                Invoke(new Action(() => { GenerateTrainData(); UpdateDistributionPlot(); }));
                while (ANNController.IsBusy) ;
                Invoke(new Action(() => TestData()));
            }));            

        }
    }
}
