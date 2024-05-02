using AI.Lab10.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text.Vectorization
{
    public class TFIDF : IVectorizer
    {
        List<string> Vocabulary;

        List<string>[] Documents;
        Dictionary<string, int> DocumentsContainingWord;

        public int FeaturesCount => Vocabulary.Count;

        public double[][] ExtractFeatures(string[] texts)
        {
            return texts.Select(text =>
            {
                var vec = new double[Vocabulary.Count];
                var doc = text.GetWordsFromSentence();

                for (int i = 0; i < vec.Length; i++) 
                {
                    var word = Vocabulary[i];
                    var tf = doc.Count == 0 ? 1 : 1.0 * doc.Where(_ => _ == word).Count() / doc.Count;
                    var idf = 1.0 * (1 + Documents.Length) / (1 + GetDocsContainingWord(word));
                    idf = Math.Log(idf) + 1;
                    vec[i] = tf * idf;
                }

                return vec;

            }).ToArray();                
        }

        private int GetDocsContainingWord(string w)
        {
            if (DocumentsContainingWord.ContainsKey(w))
                return DocumentsContainingWord[w];
            return 0;
        }

        public int MinimumRelevantFrequency { get; set; } = 10;

        public void Prepare(string[] text)
        {
            Vocabulary = text.GetBagOfWords(minFreq: MinimumRelevantFrequency);
            Console.WriteLine(Vocabulary.JoinToString("\n"));
            Documents = text.Select(_ => _.GetWordsFromSentence()).ToArray();

            DocumentsContainingWord = new Dictionary<string, int>();
            foreach (var w in Vocabulary)
                DocumentsContainingWord[w] = Documents.Where(d => d.Contains(w)).Count();            
        }
    }
}
