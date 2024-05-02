using AI.Lab8.Algebra;
using AI.Lab8.Controls;
using AI.Lab8.Data;
using AI.Lab8.Normalization;
using AI.Lab8.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AI.Lab8
{
    public partial class Form1 : Form
    {

        // WDBC -> ZNorm, LS
        public Form1()
        {            
            InitializeComponent();

            D1InputSelector.SelectedIndex = 1;

            D1LSR_Selector.Checked = true;
            D1OutputsView.RowPrePaint += D1OutputsView_RowPrePaint;

            D1Logger.Write += (msg) => D1Logs.AppendText(msg + Environment.NewLine);

            Solve1();

            D2OutputsView.RowPrePaint += D2OutputsView_RowPrePaint;
            D2Logger.Write += (msg) => D2Logs.AppendText(msg + Environment.NewLine);

            D2LSR_Selector.Checked = true;

            D2PlotXSelector.SelectedIndex = 0;
            D2PlotYSelector.SelectedIndex = 1;

            Solve2();   
        }

        private Logger D1Logger = new Logger();

        private void D1OutputsView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var grid = D1OutputsView;
            if (grid.Rows[e.RowIndex].Cells[0].Value as string == grid.Rows[e.RowIndex].Cells[1].Value as string)
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lime;
            else
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
        }

        List<Analysis> D1TrainData = new List<Analysis>();
        List<Analysis> D1TestData = new List<Analysis>();
        List<Analysis> D1PredictedData = new List<Analysis>();

        string ConfMatrixStr(string[] real, string[] predicted)
        {
            var m = Statistics.GetConfusionMatrix(real, predicted);

            var keys = m.Index.Keys.ToArray();
            int cellSize = Math.Max(5, keys.Max(_ => _.Length));
            string str = "";

            str += "".PadLeft(cellSize) + " ";

            foreach (var k in keys)
                str += k.PadLeft(cellSize) + " ";
            str += Environment.NewLine;

            foreach(var k1 in keys)
            {
                str += k1.PadLeft(cellSize) + " ";
                int i = m.Index[k1];
                foreach(var k2 in keys)
                {
                    int j = m.Index[k2];
                    str += m.Matrix[i, j].ToString().PadLeft(cellSize);
                }
                str += Environment.NewLine;
            }
            return str;
        }

        void Solve1()
        {
            var features = new string[] { "Texture", "Radius" };
            var csv = new CsvData($@"Input\Uni\{D1InputSelector.SelectedItem}", hasHeader: false);

            D1Logs.Clear();

            List<Analysis> items;

            if (D1InputSelector.SelectedItem as string == "wdbc.data") 
            {

                items = csv.Select(r => new Analysis(double.Parse(r[2]), double.Parse(r[3]), r[1])).ToList();                
            }
            else // if(D1InputSelector.SelectedItem as string == "wpbc.data") 
            {
                items = csv.Select(r => new Analysis(double.Parse(r[3]), double.Parse(r[4]), r[1] == "N" ? "M" : "B")).ToList();
            }

            var data = items.SplitTrainTest();
            var train = data.Train;
            var test = data.Test;

            var labelToInt = new Dictionary<string, double>
                {
                    { "B", 0 },
                    { "M", 1 },
                };
            var labels = labelToInt.Keys.ToArray();

            var getRegressor = new Func<ObjectRegressor<Analysis>>(() =>
            {
                var r = new ObjectRegressor<Analysis>(D1GetRegressor(), _ => labelToInt[_.Type], features);
                r.NormalizeFeature(features[0], InstantiateNormalizer(D1F0NormSelector.Value));
                r.NormalizeFeature(features[1], InstantiateNormalizer(D1F1NormSelector.Value));
                r.NormalizeOutput(InstantiateNormalizer(D1OutNormSelector.Value));
                return r;
            });

            if (!D1CrossValidation.Checked)
            {

                var fitOutputs = new List<double>();
                var fitReporter = new PredictReporter();
                fitReporter.Reported += v => fitOutputs.Add(v[0]);

                var classifier = new BinaryClassifier(getRegressor(), "B", "M");
                classifier.Threshold = ThresholdBar.Value * 0.1;                
                classifier.Logger = D1Logger;
                classifier.PredictReporter = fitReporter;

                var c = new ObjectClassifier<Analysis>(classifier, _ => labelToInt[_.Type], features);
                c.Logger = D1Logger;

                c.Train(train);

                var predictedOutputs = c.Predict(test).ToList();
                var realOutputs = test.Select(_ => _.Type).ToList();

                var d1Outputs = realOutputs.Zip(predictedOutputs, (_r, _p) => new RealPredictedItem(_r, _p))
                    .ToList();

                D1OutputsView.DataSource = d1Outputs;

                D1TrainData = train.ToList();
                D1TestData = test.ToList();


                D1PredictedData = test.Select((f, i) => new Analysis
                (
                    f.Radius,
                    f.Texture,
                    predictedOutputs[i]
                )).ToList();

                var regressionLoss = fitOutputs.Select((x, i)
                  => Math.Pow(x - classifier.GetLabelId(realOutputs[i]), 2)).CumulativeSum();

                var logLoss = fitOutputs.Select((x, i)
                    => -classifier.GetLabelId(realOutputs[i]) * Math.Log(x)).CumulativeSum();

                D1WeightsLabel.Text = string.Join(", ", classifier.Weights.Select(_ => _.ToString("F4")));

                var q = Statistics.ClassificationQuality(realOutputs.ToArray(), predictedOutputs.ToArray());
                D1AccuracyLabel.Text = q.Accuracy.ToString();
                D1ConfMatrix.Text = ConfMatrixStr(realOutputs.ToArray(), predictedOutputs.ToArray());                

                D1LossPlotter.Clear(null);
                PointsPlottable.MakePlot(regressionLoss, Color.Red).Plot(D1LossPlotter);
                PointsPlottable.MakePlot(logLoss, Color.Blue).Plot(D1LossPlotter);

                D1Plot();
            }
            else
            {
                int noPartitions = (int)D1PartitionsBox.Value;
                int noIterations = (int)D1CrossValidationItersBox.Value;

                var parts = train.Partitions(noPartitions);

                List<double[]> weightsList = new List<double[]>();

                int k = 0;

                for(int i=0;i<noIterations;i++)
                {
                    var _train = parts.Select((p, it) => (p, it)).Where(_ => _.it != k).Select(_ => _.p).SelectMany(_ => _)
                        .ToList();
                    var _test = parts.Select((p, it) => (p, it)).Where(_ => _.it == k).Select(_ => _.p).First()
                        .ToList();

                    var classifier = new BinaryClassifier(getRegressor(), "B", "M");
                    classifier.Threshold = ThresholdBar.Value * 0.1;
                    classifier.Logger = D1Logger;

                    var _c = new ObjectClassifier<Analysis>(classifier, _ => labelToInt[_.Type], features);                    
                    _c.Train(_train);

                    weightsList.Add(classifier.Weights);                   
                }

                var fitOutputs = new List<double>();

                var fitReporter = new PredictReporter();
                fitReporter.Reported += v => fitOutputs.Add(v[0]);

                var weights = weightsList.ToMatrix().Columns.Select(_ => _.Average()).ToArray();
                var regressor = new GDRegressor(weights);
                var finalClassifier = new BinaryClassifier(regressor, "B", "M");
                finalClassifier.Logger = D1Logger;
                finalClassifier.PredictReporter = fitReporter;


                var c = new ObjectClassifier<Analysis>(finalClassifier, _ => labelToInt[_.Type], features);
                var predictedOutputs = c.Predict(test).ToList();
                var realOutputs = test.Select(_ => _.Type).ToList();

                var d1Outputs = realOutputs.Zip(predictedOutputs, (_r, _p) => new RealPredictedItem(_r, _p))
                    .ToList();

                D1OutputsView.DataSource = d1Outputs;

                D1TrainData = train.ToList();
                D1TestData = test.ToList();


                D1PredictedData = test.Select((f, i) => new Analysis
                (
                    f.Radius,
                    f.Texture,
                    predictedOutputs[i]
                )).ToList();

                var regressionLoss = fitOutputs.Select((x, i)
                    => Math.Pow(x - finalClassifier.GetLabelId(realOutputs[i]), 2)).CumulativeSum();                

                var logLoss = fitOutputs.Select((x, i)
                    => - finalClassifier.GetLabelId(realOutputs[i]) * Math.Log(x)).CumulativeSum();
                
                D1WeightsLabel.Text = string.Join(", ", finalClassifier.Weights.Select(_ => _.ToString("F4")));

                var q = Statistics.ClassificationQuality(realOutputs.ToArray(), predictedOutputs.ToArray());
                D1AccuracyLabel.Text = q.Accuracy.ToString();
                D1ConfMatrix.Text = ConfMatrixStr(realOutputs.ToArray(), predictedOutputs.ToArray());

                D1LossPlotter.Clear(null);
                PointsPlottable.MakePlot(regressionLoss, Color.Red).Plot(D1LossPlotter);
                PointsPlottable.MakePlot(logLoss, Color.Blue).Plot(D1LossPlotter);

                D1Plot();
            }
        }

        private void D1SetAllNorms(Type t)
        {
            D1F0NormSelector.Value = t;
            D1F1NormSelector.Value = t;            
            D1OutNormSelector.Value = t;
        }       

        class RealPredictedItem
        {
            public string Real { get; set; }
            public string Predicted { get; set; }
            public RealPredictedItem(string real, string predicted)
            {
                Real = real;
                Predicted = predicted;
            }
        }

        private void D1AllZButton_Click(object sender, EventArgs e) => D1SetAllNorms(typeof(ZNorm));

        private void D1AllMinMaxButton_Click(object sender, EventArgs e) => D1SetAllNorms(typeof(MinMaxNorm));

        private void D1AllNoNormButton_Click(object sender, EventArgs e) => D1SetAllNorms(typeof(_NoNorm));

        private IRegressor D1GetRegressor()
        {
            if (D1LSR_Selector.Checked)
            {
                return new LSRegressor();
            }
            else
            {
                return new GDRegressor
                {
                    LearningRate = (double)D1GDLearningRate.Value,
                    MaxEpochs = (int)D1GDMaxEpochs.Value,
                    BatchSize = (int)D1BatchSize.Value,
                    Error = (computed, real) => Functions.Sigmoid(computed) - real
                };
            }
        }

        private void D1LSR_Selector_CheckedChanged(object sender, EventArgs e)
        {
            D2LsSettings.Enabled = D1LSR_Selector.Checked;
        }

        private void D1GDR_Selector_CheckedChanged(object sender, EventArgs e)
        {
            D1GdSettings.Enabled = D1GD_Selector.Checked;
        }

        private void D1Run_Click(object sender, EventArgs e) => Solve1();

        private void D1ShowPredicted_CheckedChanged(object sender, EventArgs e) => D1Plot();        

        void D1Plot()
        {
            
            D1PlotSpace.Clear(null);

            PointsPlottable.MakePlot(
                D1TrainData.Select(_ => _.Texture).ToList(),
                D1TrainData.Select(_ => _.Radius).ToList(),
                D1TrainData.Select(x => Color.FromArgb(50, D1GetColor(x))).ToList()
            ).Plot(D1PlotSpace);


            if (!D1ShowPredicted.Checked)
            {
                PointsPlottable.MakePlot(
                   D1TestData.Select(_ => _.Texture).ToList(),
                   D1TestData.Select(_ => _.Radius).ToList(),
                   D1TestData.Select(D1GetColor).ToList()
               ).Plot(D1PlotSpace);
            }
            else
            {
                PointsPlottable.MakePlot(
                   D1PredictedData.Select(_ => _.Texture).ToList(),
                   D1PredictedData.Select(_ => _.Radius).ToList(),
                   D1PredictedData.Select(D1GetColor).ToList()
               ).Plot(D1PlotSpace);
            }
        }

        private Color D1GetColor(Analysis a)
        {
            if (a.Type == "B") return Color.Green;
            else return Color.Red;            
        }


    }
}
