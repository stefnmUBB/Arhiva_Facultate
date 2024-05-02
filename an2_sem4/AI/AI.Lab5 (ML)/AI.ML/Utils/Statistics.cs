using AI.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AI.ML.Utils
{
    public static class Statistics
    {        
        public static double MeanErrorRawValuesAvg<T>(this T[] real, T[] computed, bool squared=false)
        {
            Assert(real.Length == computed.Length);
            if (real.Length == 0)
                return 0;

            var props = typeof(T).GetProperties().Where(p => (p.PropertyType == typeof(int)));
            Dictionary<PropertyInfo, double> avgs = new Dictionary<PropertyInfo, double>();

            double m = 0;
            foreach(var p in props)
            {
                //Console.WriteLine(p.Name);

                var realV = real.Select(i => (int)p.GetValue(i));
                var predV = computed.Select(i => (int)p.GetValue(i));
                var avg = realV.Zip(predV, (x, y) => Math.Abs(x - y))
                    .Select(x => squared ? x * x : x)
                    .Average();
                //Console.WriteLine(avg);
                m += avg;

            }            
            return m / props.Count();
        }

        public static double MeanError<T>(this T[] real, T[] computed, Func<T, T, double> difference)
        {
            Assert(real.Length == computed.Length);
            if (real.Length == 0)
                return 0;

            double m = 0;
            for (int i = 0; i < real.Length; i++)
                m += Math.Abs(difference(real[i], computed[i]));
            return m / real.Length;
        }

        public static double MeanSquaredError<T>(this T[] real, T[] computed, Func<T, T, double> difference)
        {
            Assert(real.Length == computed.Length);
            if (real.Length == 0)
                return 0;

            double m = 0;
            for (int i = 0; i < real.Length; i++)
            {
                var d = difference(real[i], computed[i]);
                m += d * d;
            }
            return m / real.Length;
        }

        public static double RootMeanSquaredError<T>(this T[] real, T[] computed, Func<T, T, double> difference)
            => Math.Sqrt(MeanSquaredError(real, computed, difference));

        public static double MeanError(this double[] real, double[] computed)
            => MeanError(real, computed, (a, b) => a - b);

        public static double MeanSquaredError(this double[] real, double[] computed)
            => MeanSquaredError(real, computed, (a, b) => a - b);

        public static double RootMeanSquaredError(this double[] real, double[] computed)
            => Math.Sqrt(MeanSquaredError(real, computed));     

        private static void Assert(bool condition)
        {
            if (!condition)
                throw new Exception("Assertion failed");
        }

        public static double MeanError<T>(this IEnumerable<PredictedData<T>> data, Func<T, T, double> difference)
            => data.Select(d => d.Real).ToArray()
                   .MeanError(data.Select(d => d.Predicted).ToArray(),difference);

        public static double MeanSquaredError<T>(this IEnumerable<PredictedData<T>> data, Func<T, T, double> difference)
            => data.Select(d => d.Real).ToArray()
                   .MeanSquaredError(data.Select(d => d.Predicted).ToArray(), difference);

        public static double RootMeanSquaredError<T>(this IEnumerable<PredictedData<T>> data, Func<T, T, double> difference)
            => Math.Sqrt(MeanSquaredError(data, difference));

        public static double MeanErrorRawValuesAvg<T>(this IEnumerable<PredictedData<T>> data, bool squared=false)
            => data.Select(d => d.Real).ToArray().MeanErrorRawValuesAvg(data.Select(d => d.Predicted).ToArray(), squared);
    }
}
