using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AI.Commons.Utils
{
    public static class Random
    {
        static readonly System.Random Rand = new System.Random();

        public static int FromRange(int a, int b) => Rand.Next(a, b);
        public static int IndexFromContainer<T>(IEnumerable<T> values) => Rand.Next(values.Count());
        public static T ValueFromContainer<T>(IEnumerable<T> values) 
            => values.ElementAt(IndexFromContainer(values));

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> values)
            => values.OrderBy(order => Rand.Next());

        public static IEnumerable<int> RandomInts(int count, int min, int max) => Enumerable
            .Repeat(0, count)
            .Select(i => Rand.Next(min, max));

        public static IEnumerable<int> RandomInts(int count) => Enumerable
            .Repeat(0, count)
            .Select(i => Rand.Next());

        public static bool Decision(double p) => FromRange(0, 1001) <= 1000 * p;

        public static double SelectionProbability(Func<double, double> selF, double x, double max)
        {
            return selF(x / max) / selF(1);
        }

        public static double Real() => Rand.NextDouble();
    }
}
