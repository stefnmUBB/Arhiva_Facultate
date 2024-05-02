using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text
{
    public static class Extensions
    {
        public static List<string> GetWordsFromSentence(this string text, string tokenPattern = "(\\b\\w+\\b)")
        {
            return Regex.Matches(text, tokenPattern)
                .Cast<Match>()
                .Select(m => m.Value.ToLowerInvariant())
                .ToList();
        }

        public static List<(T Key, int Frequency)> ToFrequencies<T>(this IEnumerable<T> items)
        {
            return items.Select((x, i) => (x, i))
                .GroupBy(_ => _.x)
                .Select(g => (key: g.Key, fre: g.Count()))
                .ToList();
        }

        public static List<T> MoreFrequentThan<T>(this IEnumerable<T> items, int freq)
        {
            return items.Select((x, i) => (x, i))
                .GroupBy(_ => _.x)
                .Select(g => (key: g.Key, fre: g.Count()))
                .Where(p => p.fre >= freq)
                .Select(p => p.key)
                .ToList();
        }

        public static List<string> GetNGrams(this string text, int n)
        {
            var words = text.GetWordsFromSentence();
            var result = new List<string>();
            var tmp = new Queue<string>();
            foreach(var word in words)
            {
                tmp.Enqueue(word);
                if (tmp.Count == n + 1)
                    tmp.Dequeue();
                result.Add(tmp.ToList().JoinToString(" "));
            }
            while (tmp.Count > 0)
            {
                tmp.Dequeue();
                result.Add(tmp.ToList().JoinToString(" "));
            }

            return result;
        }

        public static List<string> GetBagOfWords(this string[] data, string tokenPattern = "(\\b\\w+\\b)", int minFreq=10)
        {
            return data
                .Select(text => Regex.Matches(text, tokenPattern).Cast<Match>().Select(m => m.Value.ToLowerInvariant()).ToList())
                .SelectMany(_ => _)
                .Select((x, i) => (x, i))
                .GroupBy(p => p.x)
                .Select(g => (word: g.Key, count: g.Count()))
                .Where(p => p.count >= minFreq)
                .Select(p => p.word)
                .ToList();
        }

        public static double[][] GetBowVectors(this string[] data, List<string> dict)
        {            
            var wordsCount = dict.Count;

            var bow = data
                .Select(text => Regex.Matches(text, "(\\b\\w+\\b)")
                    .Cast<Match>()
                    .Select(m => m.Value.ToLowerInvariant())
                    .Select(word => dict.IndexOf(word))
                    .Where(index => index >= 0)
                 )
                .Select(indices =>
                {
                    var vector = new double[wordsCount];
                    foreach (var index in indices)
                        vector[index]++;
                    return vector;
                })
                .ToArray();
            return bow;
        }
    }
}
