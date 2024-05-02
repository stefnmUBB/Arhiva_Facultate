using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text.Vectorization
{
    internal class GoodBadHelper : IVectorizer
    {
        readonly Dictionary<string, int> GoodBadWords = File.ReadAllLines(@"Input\goodbadwords.txt")
            .Select(_ => _.Split(' ')).ToDictionary(_ => _[0], _ => int.Parse(_[1]));

        /*readonly string[] BadWords = new string[] { "bad", "worse", "ugly", "worst", "no", "not",
        "smoking", "uncomfortable", "tiny", "noisy", "down", "dirty"};
        readonly string[] GoodWords = new string[]
        { "good", "nice", "best", "delightful", "awesome", "soooo", "comfy", "fine", "free", "excellent" ,
        "comfortable", "spacious", "bright"};*/

        private IVectorizer InnerVectorizer { get; }

        public GoodBadHelper(IVectorizer innerVectorizer)
        {
            InnerVectorizer = innerVectorizer;
        }

        public int FeaturesCount => InnerVectorizer.FeaturesCount + 2;

        public double[][] ExtractFeatures(string[] text)
        {
            var gbindices = text.Select(t =>
            {
                var words = t.GetWordsFromSentence();
                int goodIndex = words.Where(w => GoodBadWords.ContainsKey(w) && GoodBadWords[w] > 0).Count();
                int badIndex = words.Where(w => GoodBadWords.ContainsKey(w) && GoodBadWords[w] < 0).Count();

                return new double[] { goodIndex / (goodIndex + badIndex + 1), badIndex / (goodIndex + badIndex + 1) };
            });
            var vecs = InnerVectorizer.ExtractFeatures(text);
            return vecs.Zip(gbindices, (x, y) => x.Concat(y).ToArray()).ToArray();
        }

        public void Prepare(string[] text)
        {
            InnerVectorizer.Prepare(text);
        }
    }
}
