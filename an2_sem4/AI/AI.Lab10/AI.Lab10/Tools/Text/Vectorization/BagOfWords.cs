using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text.Vectorization
{
    internal class BagOfWords : IVectorizer
    {
        List<string> Dictionary;
        public int MinimumRelevantFrequency { get; set; } = 10;

        public int FeaturesCount => Dictionary.Count;

        public double[][] ExtractFeatures(string[] text)
        {
            return text.GetBowVectors(Dictionary);
        }

        public void Prepare(string[] text)
        {
            Dictionary = text.GetBagOfWords(minFreq: MinimumRelevantFrequency);
        }
    }
}
