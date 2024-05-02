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
using static AI.Lab9.Solvers.DigitsUtils;

namespace AI.Lab9.Controls.Tabs
{    
    public partial class DigitsSimTab : UserControl
    {
        ANNController ANNController;
        
        (
            List<(double[] Inputs, double[] Outputs)> Train, 
            List<(double[] Inputs, double[] Outputs)> Test
        ) Data { get; set; }
        
        public DigitsSimTab()
        {
            InitializeComponent();            

            TestResultsBindings = new BindingList<DigitTestResult>(TestResults);
            TestViewer.DataSource = TestResultsBindings;

            ConfusionMatrix.SetLabels(Enumerable.Range(0, 10).Select(_ => _.ToString()).ToArray());
        }

        private void IrisSimTab_Load(object sender, EventArgs e)
        {
            ANNController = new ANNController(ANNStatusBar);
            ANNController.NeuralNetwork = Networks.DigitsANN();
            ANNController.TestFinished += ANNController_TestFinished;            
            ExecuteButton_Click(null, null);
        }

        List<DigitTestResult> TestResults = new List<DigitTestResult>();
        BindingList<DigitTestResult> TestResultsBindings;

        private void ANNController_TestFinished(Algebra.Matrix results)
        {
            BeginInvoke(new Action(() =>
            {
                var rows = results.Rows.ToArray();                

                TestResults.Clear();
                TestResults.AddRange(
                    rows.Select(_ => new DigitTestResult(
                        _[(int)_[10]].ToString("F4"), ((int)_[10]).ToString(), ((int)_[11]).ToString()))
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
            BeginInvoke(new Action(() =>
            {
                GenerateTrainData();
            }));
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
            Data = DataSets.OptDigitsOrigCv().SplitTrainTest();
            ANNController.Train(Data.Train, LossReport);            
        }        

        public class DigitTestResult
        {
            public string Confidence { get; set; }
            public string Predicted { get; set; }
            public string Real { get; set; }

            public DigitTestResult(string confidence, string predicted, string real)
            {
                Confidence = confidence;
                Predicted = predicted;
                Real = real;
            }
        }

        private void TestViewer_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid.Rows[e.RowIndex].Cells[1].Value as string == grid.Rows[e.RowIndex].Cells[2].Value as string) 
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lime;
            else
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
        }

        bool Executing = false;

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            if (Executing)
            {
                MessageBox.Show("Execution already in progress");
                return;
            }
            Executing = true;
            Task.Run(new Action(() => {
                try
                {
                    while (ANNController.IsBusy) ;
                    Invoke(new Action(() => GenerateTrainData()));
                    while (ANNController.IsBusy) ;
                    Invoke(new Action(() => TestData()));
                    Executing = false;
                }
                catch(Exception)
                {
                    Executing = false;
                }
            }));            

        }

        bool PaintBoxRunning = false;

        private void PaintBox_ImageChanged()
        {
            if (PaintBoxRunning) return;

            PaintBoxRunning = true;

            Task.Run(() =>
            {
                while (ANNController.IsBusy) ;

                var digit = new List<ReducedDigit> { new DigitsUtils.RawDigit(PaintBox.Image, 0).Reduce() }
                .ToData()[0];

                Invoke(new Action(() =>
                {
                    var pred = ANNController.NeuralNetwork.PredictSingle(digit.x);
                    int pdigit = pred.ArgMax();
                    var confidence = pred[pdigit];
                    PredictedDigitLabel.Text = pdigit.ToString();
                    ConfidenceLabel.Text = confidence.ToString("F4");
                    PaintBoxRunning = false;
                }));

            });
                
        }
    }
}
