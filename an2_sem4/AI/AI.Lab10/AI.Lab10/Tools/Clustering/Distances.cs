using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Clustering
{
    public static class Distances
    {
        public static double EuclideanDistance(double[] x, double[] y)
        {
            return Math.Sqrt(x.Zip(y, (xi, yi) => (xi - yi) * (xi - yi)).Sum());
        }

        public static double CosDistance(double[] x, double[] y)
        {
            var nx = NormSquared(x);
            var ny = NormSquared(y);
            if (nx == 0 || ny == 0) return 0;
            return 1 - ScalarProduct(x, y) / Math.Sqrt(NormSquared(x) * NormSquared(y));
        }

        private static double ScalarProduct(double[] x, double[] y)
            => x.Zip(y, (xi, yi) => xi * yi).Sum();

        private static double NormSquared(double[] x)
            => x.Select(xi => xi * xi).Sum();
    }
}
