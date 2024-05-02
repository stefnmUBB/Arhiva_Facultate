using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Lab10.Algebra
{
    internal static class Functions
    {
        public static double Sigmoid(double x) => 1.0 / (1 + Math.Exp(-x));

        public static double DotProduct(this IEnumerable<double> u, IEnumerable<double> v)
            => u.Zip(v, (x, y) => x * y).Sum();
    }
}
