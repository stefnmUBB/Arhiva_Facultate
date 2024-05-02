using AI.Lab10.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AI.Lab10.Tools
{
    public static class DataSets
    {
        public static readonly Dictionary<string, int> IrisLegend = new Dictionary<string, int>
            {
                { "Iris-setosa", 0}, {"Iris-versicolor",1},{ "Iris-virginica",2}
            };

        public static readonly (double[] Features, int Type, int Index)[] Flowers =
            new CsvData(@"Input\iris.data", false)
               .Select((_, i) =>
                (
                    features: new double[] { _.Get<double>(0), _.Get<double>(1), _.Get<double>(2), _.Get<double>(3) },
                    type: IrisLegend[_.Get<string>(4)],
                    index: i
                ))
               .ToArray();

        public static readonly (string Text, string Type)[] Emails =
            new CsvData(@"Input\spam.csv")
                .Select(_ => (text: _.Get<string>(0), type: _.Get<string>(1))).ToArray();

        public static readonly (double[] Vector, string Type)[] ReviewsMixedW2Vec =
            new CsvData(@"Input\reviews_mixed.w2vec_compiled.csv", false)
                .Select(_ => (vec: Enumerable.Range(0, 300).Select(i => _.Get<double>(i)).ToArray(), Type: _.Get<string>(300)))
                .Distinct()
                .ToArray();

        public static readonly (string Text, string Type)[] ReviewsMixed =
            new CsvData(@"Input\reviews_mixed.csv")
                .Select(_ => (text: _.Get<string>(0), type: _.Get<string>(1)))
                .Distinct()
                .ToArray();


    }
}
