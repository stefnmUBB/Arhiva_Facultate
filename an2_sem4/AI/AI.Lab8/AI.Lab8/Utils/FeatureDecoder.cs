using AI.Lab8.Algebra;
using AI.Lab8.Normalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AI.Lab8.Utils
{
    internal class FeatureDecoder<T>
    {
        public FeatureDecoder(Func<T, double> getRealOutput, params string[] features)
        {
            GetRealOutput = getRealOutput;
            Features = features.Select(name => typeof(T).GetProperty(name)).ToArray();
        }


        private readonly Func<T, double> GetRealOutput;
        private readonly PropertyInfo[] Features;

        public Dictionary<PropertyInfo, INormalizationMethod> Normalizers = new Dictionary<PropertyInfo, INormalizationMethod>();
        public INormalizationMethod OutputNormalizer = new _NoNorm();

        public void NormalizeFeature(string property, INormalizationMethod normalizationMethod)
        {
            Normalizers[typeof(T).GetProperty(property)] = normalizationMethod;
        }

        public void NormalizeOutput(INormalizationMethod normalizationMethod)
        {
            OutputNormalizer = normalizationMethod;
        }

        public double[] GetFeatures(T item) => Features.Select(f => (double)f.GetValue(item)).ToArray();

        public Matrix GetFeaturesMatrix(IEnumerable<T> items)
        {
            //File.WriteAllText("c.txt", Matrix.FromColumns(items.Select(GetFeatures).ToMatrix().Columns).ToString());

            var cols = items.Select(GetFeatures).ToMatrix().Columns;
            cols = cols.Select((col, i) =>
            {
                if (!Normalizers.Keys.Any(k => k.Name == Features[i].Name)) 
                {                    
                    return col.ToArray();
                }
                var key = Normalizers.Keys.Where(k => k.Name == Features[i].Name).First();
                return Normalizers[key].Normalize(col).ToArray();
            }).ToArray();
            return Matrix.FromColumns(cols);
        }

        public double[] GetNormalizedOutputs(IEnumerable<T> items)
        {
            return OutputNormalizer.Normalize(items.Select(GetRealOutput).ToArray()).ToArray();
        }
    }
}
