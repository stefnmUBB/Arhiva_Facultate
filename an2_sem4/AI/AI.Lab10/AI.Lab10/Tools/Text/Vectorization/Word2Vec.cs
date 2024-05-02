using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text.Vectorization
{
    public class Word2Vec : IVectorizer
    {
        public int FeaturesCount => 300;

        public double[][] ExtractFeatures(string[] texts)
        {
            return texts.Select(text =>
            {
                Console.WriteLine("Features from... " + text);
                var words= text
                    .GetWordsFromSentence()
                    .Select(w => W2VecReader.Global.GetVector(w))
                    .ToArray()
                    .Where(v => v != null).ToArray();
                var res = new double[300];
                for(int i=0;i<300;i++)
                {
                    for (int j = 0; j < words.Length; j++)
                        res[i] += words[j][i];
                }
                return res;

            }).ToArray();
        }

        public void Prepare(string[] text) { }
    }
}
