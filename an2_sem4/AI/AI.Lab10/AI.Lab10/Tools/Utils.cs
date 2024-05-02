using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools
{
    public static class Utils
    {
        public static T ChooseRandom<T>(this T[] values)
        {
            if (values.Length == 0) return default;
            return values[Rand.Int(values.Length)];
        }

        public static T[] ChooseRandom<T>(this T[] values, int count)
            => values.OrderBy(_ => Guid.NewGuid()).Take(count).ToArray();

        public static int ArgMax<T>(this IEnumerable<T> values, Func<T, double> attribute)
        {
            if (values.Count() == 0) return -1;
            var max = values.Max(attribute);
            if (double.IsNaN(max)) return 0;
            return values.Select((x, i) => (x, i)).Where(_ => attribute(_.x) == max).First().i;
        }

        public static int ArgMin<T>(this IEnumerable<T> values, Func<T, double> attribute)
        {
            if (values.Count() == 0) return -1;
            var min = values.Min(attribute);
            if(double.IsNaN(min))
            {
                Console.WriteLine(((dynamic)values.First() as double[]).JoinToString(", "));
            }
            return values.Select((x, i) => (x, i)).Where(_ => attribute(_.x) == min).First().i;
        }
    }
}
