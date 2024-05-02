using AI.Lab8.Algebra;
using AI.Lab8.Normalization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AI.Lab8.Utils
{
    internal class ObjectRegressor<T> : IRegressor
    {
        private readonly FeatureDecoder<T> FeatureDecoder;

        private readonly IRegressor InnerRegressor;        

        public ObjectRegressor(IRegressor innerRegressor, Func<T, double> getRealOutput, params string[] features)
        {
            InnerRegressor = innerRegressor;
            FeatureDecoder = new FeatureDecoder<T>(getRealOutput, features);
        }

        public ObjectRegressor(IRegressor innerRegressor, string outputPropertyName, params string[] features)
            : this(innerRegressor, item=>(double)typeof(T).GetProperty(outputPropertyName).GetValue(item), features) { }

        public void NormalizeFeature(string property, INormalizationMethod normalizationMethod)
        {
            FeatureDecoder.NormalizeFeature(property, normalizationMethod);            
        }

        public void NormalizeOutput(INormalizationMethod normalizationMethod)
        {
            FeatureDecoder.NormalizeOutput(normalizationMethod);
        }        
       
        public void Train(IEnumerable<T> items)
        {
            var x = FeatureDecoder.GetFeaturesMatrix(items);
            var y = FeatureDecoder.GetNormalizedOutputs(items);            
            File.WriteAllText("o.txt",x.ToString());
            Train(x, y, errorUpdateCallback: null);
        }

        public double[] Predict(IEnumerable<T> items)
        {
            var x = items.Select(FeatureDecoder.GetFeatures).ToMatrix();
            return Predict(x);
        }

        public double[] Weights => InnerRegressor.Weights;
        public double[] Predict(Matrix x) => FeatureDecoder.OutputNormalizer.Normalize(InnerRegressor.Predict(x)).ToArray();
        public void Train(Matrix x, double[] y, Action<double> errorUpdateCallback = null) => InnerRegressor.Train(x, y, errorUpdateCallback: null);

        public void Reset()
        {
            InnerRegressor.Reset();
        }
        public double Test(double[] computed, double[] real) => InnerRegressor.Test(computed, real);        
    }    
}
