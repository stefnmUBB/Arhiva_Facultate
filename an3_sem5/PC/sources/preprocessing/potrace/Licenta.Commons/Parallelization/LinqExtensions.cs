using System;
using System.Collections.Generic;
using System.Linq;

namespace Licenta.Commons.Parallelization
{
    public static class LinqExtensions
    {
        public static ParallelQuery<T3> ZipAsync<T1, T2, T3>(this IEnumerable<T1> a, IEnumerable<T2> b, Func<T1, T2, T3> f)
            => ParallelEnumerable.Zip(a.AsParallel(), b.AsParallel(), f);

        public static ParallelQuery<T2> SelectAsync<T1, T2>(this IEnumerable<T1> a, Func<T1, T2> f)
            => ParallelEnumerable.Select(a.AsParallel(), f);

    }
}
