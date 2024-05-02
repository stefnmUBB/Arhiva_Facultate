using AI.Lab10.Tools.Text.Vectorization;
using AI.Lab10.Tools.Text;
using AI.Lab10.Tools;
using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AI.Lab10.Controls.Plot;

namespace AI.Lab10.Controls.Tabs
{
    public partial class ReviewANNTab : UserControl
    {
        public ReviewANNTab()
        {
            InitializeComponent();
        }

        private void Solve()
        {
            List<double> losses = new List<double>();
            var data = DataSets.ReviewsMixed;
            
            var vals = new double[10] { -1.06020016784789, 2.89577776468162, -1.78866307194748, 0.688428209018161, 2.10817504353271, 2.17757706771492, 1.68825166052592, -2.68507874649255, -0.580558940572924, 0.398388422745461 };            

            var (train, test) = data.ToArray().SplitTrainTest(0.8, 0);            

            var trainInput = train.Select(_ => _.Text).ToArray();
            var trainOutput = train.Select(_ => _.Type).ToArray();

            var testInput = test.Select(_ => _.Text).ToArray();
            var testOutput = test.Select(_ => _.Type).ToArray();

            var textANN = new TextANN { IterationsCount = 20 };
            textANN.Vectorizer = new NGram { MinimumRelevantFrequency = 2 };
            textANN.AddHiddenLayer(40, NeuronGenerators.LinearActivated(), vals[0], vals[1]);
            textANN.AddHiddenLayer(26, NeuronGenerators.GaussActivated(), vals[4], vals[5]);
            textANN.AddHiddenLayer(10, NeuronGenerators.CubicActivated(), vals[6], vals[7], vals[8], vals[9]);
            textANN.AddHiddenLayer(5, NeuronGenerators.SelfActivated());
            textANN.AddHiddenLayer(6, NeuronGenerators.LinearActivated(), vals[2], vals[3]);
            textANN.SetOutputLayer(NeuronGenerators.SelfActivated());

            textANN.LossReport = l =>
            {
                if (double.IsNaN(l))
                    l = -1;
                losses.Add(l);
            };            

            try
            {
                textANN.Train(trainInput, trainOutput);
            }
            catch (ArgumentException)
            {
                // NaN
            }

            var outs = textANN.Predict(testInput, testOutput);

            Invoke(new Action(() =>
            {
                OutputView.Results = testOutput.Zip(outs, (r, p) => (r, p)).ToArray();
            }));

            var mx = losses.Select(Math.Abs).Max() + 1;
            OutputView.PlotValues = losses.Select(x => x / mx).ToList();            
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            RunButton.Enabled = false;

            Task.Run(() =>
            {
                Solve();
                Invoke(new Action(() => RunButton.Enabled = true));
            });
        }
    }
}
