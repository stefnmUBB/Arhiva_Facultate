using AI.Lab8.Normalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Utils
{
    internal static class Extensions
    {
        public static int Clamp(this int x, int a, int b)
        {
            if (x < a) return a;
            if (x > b) return b;
            return x;
        }

        public static (IEnumerable<T> Train, IEnumerable<T> Test) SplitTrainTest<T>(this IEnumerable<T> items, double pratio = 0.8)
        {
            var rand = new Random();
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

        public static IEnumerable<double> Normalize(this IEnumerable<double> data, INormalizationMethod norm)
        {
            return (norm ?? new _NoNorm()).Normalize(data);
        }

        public static int IndexOfMax(this IEnumerable<double> data)
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
    }
}
