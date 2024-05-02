using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Utils
{
    internal class Thresholds
    {
        public double[] Values { get; }

        public int Fit(double value)
        {
            if (Values.Length == 0) return 0;           
            if (value < Values.First()) return 0;

            for (int i = 1; i < Values.Length; i++)
                if (value >= Values[i - 1] && value < Values[i])
                    return i;
            return Values.Length;
        }

        public Thresholds(IEnumerable<double> values) { Values = values.ToArray(); }

        public static Thresholds Uniform(double a, double b, int partitions)
        {
            if (partitions == 1) return new Thresholds(new double[0]);
            return new Thresholds(Enumerable.Range(1, partitions - 1).Select(i => (b - a) * i / partitions + a));
        }

        public override string ToString() => string.Join(", ", Values);
    }
}
