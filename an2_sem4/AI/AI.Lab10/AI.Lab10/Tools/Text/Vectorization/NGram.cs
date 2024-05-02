using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text.Vectorization
{
    public class NGram : IVectorizer
    {
        public int N { get; }

        public int MinimumRelevantFrequency { get; set; } = 4;

        public List<string> Grams = new List<string>();

        public NGram(int n = 2, int minimumRelevantFreq=4)
        {
            N = n;
            MinimumRelevantFrequency = minimumRelevantFreq;
        }

        public int FeaturesCount => Grams.Count;

        public double[][] ExtractFeatures(string[] texts)
        {
            return texts.Select(text =>
            {
                var grams = text.GetNGrams(N).ToFrequencies()
                    .ToDictionary(p => p.Key, p => p.Frequency);

                var vec = new double[Grams.Count];
                for (int i = 0; i < vec.Length; i++)
                {
                    if (grams.ContainsKey(Grams[i]))
                        vec[i] += grams[Grams[i]];
                }
                return vec;
            }).ToArray();            
        }

        public void Prepare(string[] text)
        {
            Grams = text.Select(_ => _.GetNGrams(N)).SelectMany(_ => _)
                .MoreFrequentThan(MinimumRelevantFrequency)
                .ToList();
            Console.WriteLine(Grams.Count);            
        }
    }
}
