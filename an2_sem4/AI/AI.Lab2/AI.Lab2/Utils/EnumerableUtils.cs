using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab2.Utils
{
    internal static class EnumerableUtils
    {
        public static T MaxBy<T,U>(this IEnumerable<T> items, Func<T,U> mapper)
            where U : IComparable
        {
            if (items.Count() == 0)
                return default(T);
            return items.Aggregate((agg, next)
                => mapper(agg).CompareTo(mapper(next)) <= 0 ? next : agg);
        }
    }
}
