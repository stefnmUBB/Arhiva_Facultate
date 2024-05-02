using AI.Lab10.Tools.Clustering;
using AI.Lab10.Tools.Text.Vectorization;
using AI.Lab10.Tools.Text;
using AI.Lab10.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AI.Lab10.Utils;

namespace AI.Lab10.Controls.Tabs
{
    public partial class EmailTab : UserControl
    {
        public EmailTab()
        {
            InitializeComponent();
        }

        private void Solve()
        {
            var losses = new List<double>();
            var data = DataSets.Emails;

            var (train, test) = data.ToArray().SplitTrainTest(0.8);

            var trainInput = train.Select(_ => _.Text).ToArray();
            var trainOutput = train.Select(_ => _.Type).ToArray();

            var textKmeans = new TextKMeans();
            textKmeans.Distance = Distances.CosDistance;
            Invoke(new Action(()
                => textKmeans.Vectorizer = VectorizerSelector.Vectorizer));
            textKmeans.StateReport = l =>
            {
                if (double.IsNaN(l)) l = -1;
                losses.Add(l);
            };

            textKmeans.Train(trainInput, trainOutput);
            var predOutput = textKmeans.Predict(test.Select(_ => _.Text).ToArray());
            var testOutput = test.Select(_ => _.Type).ToArray();

            Invoke(new Action(() =>
            {
                OutputView.Results = testOutput.Zip(predOutput, (r, p) => (r, p)).ToArray();
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
