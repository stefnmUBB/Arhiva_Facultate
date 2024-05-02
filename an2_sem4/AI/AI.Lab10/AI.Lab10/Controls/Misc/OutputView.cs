using AI.Lab10.Controls.Plot;
using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab10.Controls.Misc
{
    public partial class OutputView : UserControl
    {
        public OutputView()
        {
            InitializeComponent();
        }

        private void groupBox2_Resize(object sender, EventArgs e)
        {
            int l = Math.Min(groupBox2.Height - 4 - groupBox2.Font.Height - 4, groupBox2.Width - 4);
            ConfusionMatrix.Height = ConfusionMatrix.Width = l;
            ConfusionMatrix.Left = 2+(groupBox2.Width - 4 - l) / 2;
            ConfusionMatrix.Top = 2 + groupBox2.Font.Height + 2 + (groupBox2.Width - 4 - l) / 2;
        }

        public (string Real, string Predicted)[] Results
        {
            set
            {
                var real = value.Select(_ => _.Real).ToArray();
                var pred = value.Select(_ => _.Predicted).ToArray();
                var labels = real.Concat(pred).Distinct().ToList();

                ConfusionMatrix.SetLabels(labels.ToArray());

                var ireal = real.Select(_ => labels.IndexOf(_)).ToArray();
                var ipred = pred.Select(_ => labels.IndexOf(_)).ToArray();

                ConfusionMatrix.SetData(ireal, ipred);

                AccuracyLabel.Text = ConfusionMatrix.Accuracy.ToString();

                PrecisionLabel.Text = ConfusionMatrix.Precision.Select((p, i) => $"{labels[i]}:{p:F4}").JoinToString(", ");
                RecallLabel.Text = ConfusionMatrix.Recall.Select((p, i) => $"{labels[i]}:{p:F4}").JoinToString(", ");                
            }
        }

        public List<double> PlotValues
        {
            set
            {
                LossPlotter.Clear(null);
                new FunctionPlottable(value).Plot(LossPlotter);
            }
        }
    }
}
