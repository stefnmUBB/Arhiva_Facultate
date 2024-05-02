using AI.Lab8.Algebra;
using AI.Lab8.Controls;
using AI.Lab8.Data;
using AI.Lab8.Normalization;
using AI.Lab8.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab8
{
    partial class Form1
    {
        #region D2
        private void D2OutputsView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            var grid = D2OutputsView;
            if (grid.Rows[e.RowIndex].Cells[0].Value as string == grid.Rows[e.RowIndex].Cells[1].Value as string)
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lime;
            else
                grid.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
        }

        private void Solve2()
        {
            D2Logs.Clear();

            var features = new string[] { "SepalLength", "SepalWidth", "PetalLength", "PetalWidth" };
            var csv = new CsvData(@"Input\Multi\iris.data", features.Append("Class").ToArray());
            var data = csv.ToObjects<Flower>().ToList().SplitTrainTest();

            var train = data.Train;
            var test = data.Test;

            var labelToInt = new Dictionary<string, double>
            {
                { "Iris-setosa", 0 },
                { "Iris-versicolor", 1 },
                { "Iris-virginica", 2 }
            };
            var labels = labelToInt.Keys.ToArray();

            var getRegressor = new Func<ObjectRegressor<Flower>>(() =>
            {
                var r = new ObjectRegressor<Flower>(D2GetRegressor(), _ => labelToInt[_.Class], features);
                r.NormalizeFeature(features[0], InstantiateNormalizer(D2F0NormSelector.Value));
                r.NormalizeFeature(features[1], InstantiateNormalizer(D2F1NormSelector.Value));
                r.NormalizeFeature(features[2], InstantiateNormalizer(D2F2NormSelector.Value));
                r.NormalizeFeature(features[3], InstantiateNormalizer(D2F3NormSelector.Value));
                r.NormalizeOutput(InstantiateNormalizer(D2OutNormSelector.Value));

                return r;
            });

            if (!D2CrossValidation.Checked)
            {                

                var classifier = new MulticlassClassifier(
                        (labels[0], getRegressor()),
                        (labels[1], getRegressor()),
                        (labels[2], getRegressor())
                    );
                classifier.Logger = D2Logger;

                var c = new ObjectClassifier<Flower>(classifier, _ => labelToInt[_.Class], features);
                c.Logger = D2Logger;

                c.Train(train);

                var predictedOutputs = c.Predict(test).ToList();
                var realOutputs = test.Select(_ => _.Class).ToList();

                var d2Outputs = realOutputs.Zip(predictedOutputs, (_r, _p) => new RealPredictedItem(_r, _p))
                    .ToList();

                D2OutputsView.DataSource = d2Outputs;

                D2TrainData = train.ToList();
                D2TestData = test.ToList();

                D2WeightsLabel.Text = string.Join(", ",
                    string.Join(Environment.NewLine,
                        classifier.Regressors.Select(_ => string.Join(", ", _.Weights.Select(w => w.ToString("F4")))))
                    );

                var q = Statistics.ClassificationQuality(realOutputs.ToArray(), predictedOutputs.ToArray());
                D2AccuracyLabel.Text = q.Accuracy.ToString();
                D2ConfMatrixLabel.Text = ConfMatrixStr(realOutputs.ToArray(), predictedOutputs.ToArray());

                D2PredictedData = test.Select((f, i) => new Flower
                {
                    SepalLength = f.SepalLength,
                    SepalWidth = f.SepalWidth,
                    PetalLength = f.PetalLength,
                    PetalWidth = f.PetalWidth,
                    Class = predictedOutputs[i]
                }).ToList();

                D2Plot();
            }
            else
            {
                int noPartitions = (int)D2Partitions.Value;
                int noIterations = (int)D2CVIterations.Value;

                var parts = train.Partitions(noPartitions);

                List<double[]>[] weightsLists = new List<double[]>[3];
                for (int i = 0; i < 3; i++) weightsLists[i] = new List<double[]>();

                int k = 0;

                for (int i = 0; i < noIterations; i++)
                {
                    var _train = parts.Select((p, it) => (p, it)).Where(_ => _.it != k).Select(_ => _.p).SelectMany(_ => _)
                        .ToList();
                    var _test = parts.Select((p, it) => (p, it)).Where(_ => _.it == k).Select(_ => _.p).First()
                        .ToList();

                    var classifier = new MulticlassClassifier(
                        (labels[0], getRegressor()),
                        (labels[1], getRegressor()),
                        (labels[2], getRegressor())
                    );
                    classifier.Logger = D2Logger;

                    var _c = new ObjectClassifier<Flower>(classifier, _ => labelToInt[_.Class], features);
                    _c.Logger = D2Logger;

                    _c.Train(train);

                    for (int j = 0; j < 3; j++)
                        weightsLists[j].Add(classifier.Regressors[j].Weights);
                }

                var regressors = new IRegressor[3];
                for(int i=0;i<3;i++)
                {
                    var weights = weightsLists[i].ToMatrix().Columns.Select(_ => _.Average()).ToArray();
                    regressors[i] = new GDRegressor(weights);
                }

                var finalClassifier = new MulticlassClassifier(
                        (labels[0], regressors[0]),
                        (labels[1], regressors[1]),
                        (labels[2], regressors[2])
                    );
                finalClassifier.Logger = D2Logger;

                var c = new ObjectClassifier<Flower>(finalClassifier, _ => labelToInt[_.Class], features);
                c.Logger = D2Logger;

                var predictedOutputs = c.Predict(test).ToList();
                var realOutputs = test.Select(_ => _.Class).ToList();

                var d2Outputs = realOutputs.Zip(predictedOutputs, (_r, _p) => new RealPredictedItem(_r, _p))
                    .ToList();

                D2OutputsView.DataSource = d2Outputs;

                D2TrainData = train.ToList();
                D2TestData = test.ToList();

                D2WeightsLabel.Text = string.Join(", ",
                    string.Join(Environment.NewLine,
                        finalClassifier.Regressors.Select(_ => string.Join(", ", _.Weights.Select(w => w.ToString("F4")))))
                    );

                var q = Statistics.ClassificationQuality(realOutputs.ToArray(), predictedOutputs.ToArray());
                D2AccuracyLabel.Text = q.Accuracy.ToString();
                D2ConfMatrixLabel.Text = ConfMatrixStr(realOutputs.ToArray(), predictedOutputs.ToArray());

                D2PredictedData = test.Select((f, i) => new Flower
                {
                    SepalLength = f.SepalLength,
                    SepalWidth = f.SepalWidth,
                    PetalLength = f.PetalLength,
                    PetalWidth = f.PetalWidth,
                    Class = predictedOutputs[i]
                }).ToList();

                D2Plot();

            }
        }

        private Color D2GetColor(Flower f)
        {
            if (f.Class == "Iris-setosa")
                return Color.Red;
            if (f.Class == "Iris-versicolor")
                return Color.Green;
            return Color.Blue;
        }

        private void D2RunButton_Click(object sender, EventArgs e)
        {
            Solve2();
        }

        private INormalizationMethod InstantiateNormalizer(Type t)
            => Activator.CreateInstance(t) as INormalizationMethod;

        private Logger D2Logger = new Logger();

        private void D2AllNoNormButton_Click(object sender, EventArgs e)
        {
            D2SetAllNorms(typeof(_NoNorm));
        }

        private void D2SetAllNorms(Type t)
        {
            D2F0NormSelector.Value = t;
            D2F1NormSelector.Value = t;
            D2F2NormSelector.Value = t;
            D2F3NormSelector.Value = t;
            D2OutNormSelector.Value = t;
        }

        private void D2AllMinMaxButton_Click(object sender, EventArgs e)
        {
            D2SetAllNorms(typeof(MinMaxNorm));
        }

        private void D2AllZButton_Click(object sender, EventArgs e)
        {
            D2SetAllNorms(typeof(ZNorm));
        }

        private void D2LSR_Selector_CheckedChanged(object sender, EventArgs e)
        {
            D2LsSettings.Enabled = D2LSR_Selector.Checked;
        }

        private void D2GD_Selector_CheckedChanged(object sender, EventArgs e)
        {
            D2GdSettings.Enabled = D2GD_Selector.Checked;
        }

        private IRegressor D2GetRegressor()
        {
            if (D2LSR_Selector.Checked)
            {
                return new LSRegressor();
            }
            else
            {
                return new GDRegressor
                {
                    LearningRate = (double)D2GDLearningRate.Value,
                    MaxEpochs = (int)D2GDMaxEpochs.Value,
                    BatchSize = (int)D2BatchSize.Value,
                    Error = (computed, real) => Functions.Sigmoid(computed) - real,                    
                };
            }
        }

        private List<Flower> D2TrainData = new List<Flower>();
        private List<Flower> D2TestData = new List<Flower>();
        private List<Flower> D2PredictedData = new List<Flower>();

        private void D2PlotXSelector_SelectedIndexChanged(object sender, EventArgs e) => D2Plot();
        private void D2PlotYSelector_SelectedIndexChanged(object sender, EventArgs e) => D2Plot();

        void D2Plot()
        {
            D2PetalLSepalLPlot.Clear(null);

            PointsPlottable.MakePlot(
                D2TrainData.Select(_ => (double)typeof(Flower).GetProperty(D2PlotXSelector.SelectedItem as string)
                    .GetValue(_)).ToList(),
                D2TrainData.Select(_ => (double)typeof(Flower).GetProperty(D2PlotYSelector.SelectedItem as string)
                    .GetValue(_)).ToList(),
                D2TrainData.Select(x => Color.FromArgb(50, D2GetColor(x))).ToList()
            ).Plot(D2PetalLSepalLPlot);


            if (!D2ShowPredicted.Checked)
            {

                PointsPlottable.MakePlot(
                    D2TestData.Select(_ => (double)typeof(Flower).GetProperty(D2PlotXSelector.SelectedItem as string)
                        .GetValue(_)).ToList(),
                    D2TestData.Select(_ => (double)typeof(Flower).GetProperty(D2PlotYSelector.SelectedItem as string)
                        .GetValue(_)).ToList(),
                    D2TestData.Select(D2GetColor).ToList()
                ).Plot(D2PetalLSepalLPlot);
            }
            else
            {
                PointsPlottable.MakePlot(
                    D2PredictedData.Select(_ => (double)typeof(Flower).GetProperty(D2PlotXSelector.SelectedItem as string)
                        .GetValue(_)).ToList(),
                    D2PredictedData.Select(_ => (double)typeof(Flower).GetProperty(D2PlotYSelector.SelectedItem as string)
                        .GetValue(_)).ToList(),
                    D2PredictedData.Select(D2GetColor).ToList()
                ).Plot(D2PetalLSepalLPlot);
            }
        }

        private void D2ShowPredicted_CheckedChanged(object sender, EventArgs e) => D2Plot();

        #endregion

    }
}
