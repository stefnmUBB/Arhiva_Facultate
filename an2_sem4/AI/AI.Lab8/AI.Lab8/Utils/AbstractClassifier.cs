using AI.Lab8.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Utils
{
    internal abstract class AbstractClassifier : IClassifier
    {
        public Logger Logger { get; set; } = null;
        public PredictReporter PredictReporter { get; set; } = null;
        protected string[] Labels;
        protected Dictionary<string, int> LabelIds;

        public string[] GetLabels() => Labels.ToArray();

        public AbstractClassifier(string[] labels)
        {
            Labels = labels;
            LabelIds = Labels.Select((l, i) => (l, i)).ToDictionary(_ => _.l, _ => _.i);            
        }

        public int GetLabelId(string label) => LabelIds[label];

        public abstract string[] Predict(Matrix x);

        public abstract void Train(Matrix x, int[] outLabelIds);

        public void Train(Matrix x, string[] outLabels) => Train(x, outLabels.Select(GetLabelId).ToArray());

        public double Test(string[] computed, string[] real)
        {
            return computed.Zip(real, (c, r) => c == r ? 1 : 0).Sum();
        }
    }
}
