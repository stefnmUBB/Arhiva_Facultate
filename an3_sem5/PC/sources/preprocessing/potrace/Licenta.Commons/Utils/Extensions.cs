using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Licenta.Commons.Utils
{
    public static class Extensions
    {
        public static double Clamp(this double x, double a, double b) => x <= a ? a : x >= b ? b : x;
        public static int Clamp(this int x, int a, int b) => x <= a ? a : x >= b ? b : x;
        public static bool IsBetween(this int x, int a, int b) => a <= x && x <= b;
        public static bool IsBetween(this double x, double a, double b) => a <= x && x <= b;

        public static int Squared(this int x) => x * x;


        public static IEnumerable<T[]> GroupChunks<T>(this IEnumerable<T> items, int chunkSize) =>
            items.Select((x, i) => (x, c: i / chunkSize)).GroupBy(_ => _.c).Select(g => g.Select(_ => _.x).ToArray());

        public static T Average<T>(this T[] items) where T : ISetAdditive<T>, ISetDivisive<IntNumber, T>
        {
            if (items.Length == 0) return default(T);
            return items.Aggregate((x, y) => x.Add(y)).Divide(items.Length);
        }

        public static string JoinToString<T>(this IEnumerable<T> items, string delimiter)
            => string.Join(delimiter, items.Select(_ => _.ToString()));

        public static int IndexOfMax<T>(this IEnumerable<T> sequence) where T : IComparable<T>
        {
            int maxIndex = -1;
            T maxValue = default(T);

            int index = 0;
            foreach (T value in sequence)
            {
                if (value.CompareTo(maxValue) > 0 || maxIndex == -1)
                {
                    maxIndex = index;
                    maxValue = value;
                }
                index++;
            }
            return maxIndex;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> Peek<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach(var item in items)
            {
                action(item);
                yield return item;
            }
        }
    }
}
