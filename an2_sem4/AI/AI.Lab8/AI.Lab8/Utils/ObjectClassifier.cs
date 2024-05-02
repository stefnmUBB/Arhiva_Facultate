using AI.Lab8.Algebra;
using AI.Lab8.Normalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Utils
{
    internal class ObjectClassifier<T> : AbstractClassifier
    {
        private readonly IClassifier InnerClassifier;
        private readonly FeatureDecoder<T> FeatureDecoder;

        public ObjectClassifier(IClassifier classifier, Func<T, double> getRealOutput, params string[] features) 
            : base(classifier.GetLabels())
        {
            InnerClassifier = classifier;
            FeatureDecoder = new FeatureDecoder<T>(getRealOutput, features);
        }

        public ObjectClassifier(IClassifier classifier, string outputPropertyName, params string[] features)
            : this(classifier, item => (double)typeof(T).GetProperty(outputPropertyName).GetValue(item), features) { }

        public override void Train(Matrix x, int[] outLabelIds) => InnerClassifier.Train(x, outLabelIds);        

        public void Train(IEnumerable<T> items)
        {
            var x = FeatureDecoder.GetFeaturesMatrix(items.ToList());
            var y = FeatureDecoder.GetNormalizedOutputs(items.ToList()).Select(_ => (int)_).ToArray();
            Train(x, y);
        }

        public override string[] Predict(Matrix x) => InnerClassifier.Predict(x);

        public string[] Predict(IEnumerable<T> items)
        {
            var x = FeatureDecoder.GetFeaturesMatrix(items);
            return Predict(x);            
        }

        public void NormalizeFeature(string property, INormalizationMethod normalizationMethod)
        {
            FeatureDecoder.NormalizeFeature(property, normalizationMethod);
        }

        public void NormalizeOutput(INormalizationMethod normalizationMethod)
        {
            FeatureDecoder.NormalizeOutput(normalizationMethod);
        }
    }
}
