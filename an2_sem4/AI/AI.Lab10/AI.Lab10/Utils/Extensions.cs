using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Utils
{
    public static class Extensions
    {
        public static double[] VecSum(this double[][] vectors)
        {
            var len = vectors[0].Length;
            var a = new double[len];
            for(int j=0;j<len;j++)
            {
                for (int i = 0; i < vectors.Length; i++)
                    a[j] += vectors[i][j];
            }
            return a;
        }

        public static double[] VecAvg(this double[][] vectors)
        {
            var len = vectors[0].Length;
            var a = new double[len];
            for (int j = 0; j < len; j++)
            {
                for (int i = 0; i < vectors.Length; i++)
                    a[j] += vectors[i][j];
                if (vectors.Length > 0)
                    a[j] /= vectors.Length;
            }
            return a;
        }

        public static string JoinToString<T>(this IEnumerable<T> values, string delimiter)
            => string.Join(delimiter, values);              
        public static int Clamp(this int x, int a, int b)
        {
            if (x < a) return a;
            if (x > b) return b;
            return x;
        }

        public static (List<T> Train, List<T> Test) SplitTrainTest<T>(this IEnumerable<T> items, double pratio = 0.8, int seed = 0)
        {
            var rand = seed == 0 ? new Random() : new Random(seed);
            var pitems = items.Select(i => (Item: i, Category: rand.NextDouble() < pratio ? 0 : 1));
            return
                (
                    pitems.Where(x => x.Category == 0).Select(x => x.Item).ToList(),
                    pitems.Where(x => x.Category == 1).Select(x => x.Item).ToList()
                );
        }

        public static IEnumerable<IEnumerable<T>> Partitions<T>(this IEnumerable<T> items, int noPartitions)
        {
            return items.Shuffle().ToArray().Select((x, i) => (x, i)).GroupBy(_ => _.i % noPartitions)
                .Select(_ => _.Select(p => p.x).ToArray()).ToArray();
        }

        public static double Mean(this IEnumerable<double> data)
            => data.Average();

        public static double Std(this IEnumerable<double> data)
        {
            var mean = data.Mean();
            return Math.Sqrt(data.Select(x => (x - mean) * (x - mean)).Sum() / (data.Count() - 1));
        }        

        public static int ArgMax(this IEnumerable<double> data)
        {
            return !data.Any() ? -1 : data
                .Select((value, index) => (Value: value, Index: index))
                .Aggregate((a, b) => (a.Value > b.Value) ? a : b)
                .Index;
        }

        public static IEnumerable<T> SelectIf<T>(this IEnumerable<T> e, bool condition, Func<T, int, T> selector)
            => condition ? e.Select(selector) : e.ToList();

        public static IEnumerable<T> SkipIf<T>(this IEnumerable<T> e, bool condition, int count)
            => condition ? e.Skip(count) : e.ToList();

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> e)
            => e.OrderBy(_ => Guid.NewGuid());

        public static IEnumerable<double> CumulativeSum(this IEnumerable<double> sequence)
        {
            double sum = 0;
            foreach (var item in sequence)
            {
                sum += item;
                yield return sum;
            }
        }

        public static string[] ReadLinesUTF8(this byte[] bytes)
        {
            var lines = new List<string>();
            using (var stream = new MemoryStream(bytes))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines.ToArray();
        }
    }
}
