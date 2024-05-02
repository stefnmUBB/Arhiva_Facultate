using AI.GradientDescend.Normalization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.GradientDescend.Utils
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
                    pitems.Where(x => x.Category == 0).Select(x => x.Item),
                    pitems.Where(x => x.Category == 1).Select(x => x.Item)
                );
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

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> data)
            => data.OrderBy(i => Guid.NewGuid());
    }
}
