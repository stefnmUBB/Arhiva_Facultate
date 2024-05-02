using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Utils
{
    public static class Statistics
    {
        public static double MeanErrorRawValuesAvg<T>(this T[] real, T[] computed, bool squared = false)
        {
            Assert(real.Length == computed.Length);
            if (real.Length == 0)
                return 0;

            var props = typeof(T).GetProperties().Where(p => (p.PropertyType == typeof(int)));
            Dictionary<PropertyInfo, double> avgs = new Dictionary<PropertyInfo, double>();

            double m = 0;
            foreach (var p in props)
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

        public static (int[,] Matrix, Dictionary<T, int> Index) GetConfusionMatrix<T>(T[] real, T[] predicted)
        {
            var Index = real.Concat(predicted).Distinct().Select((c, i) => (c, i)).ToDictionary(x => x.c, x => x.i);

            var data = real.Zip(predicted, (r, p) => (Real: r, Predicted: p)).ToList();  

            int[,] Matrix = new int[Index.Count, Index.Count];

            foreach (var item in data)
            {
                int i = Index[item.Real];
                int j = Index[item.Predicted];
                Matrix[i, j]++;
            }
            return (Matrix, Index);
        }

        const int TP = 0;
        const int TN = 1;
        const int FP = 2;
        const int FN = 3;

        public static int[,] GetTFPN(int[,] confMatrix)
        {
            var len = confMatrix.GetLength(0);
            int[,] result = new int[4, len];

            int sum = 0;

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (i == j) result[TP, i] += confMatrix[i, i];
                    else
                    {
                        result[FN, i] += confMatrix[i, j];
                        result[FP, j] += confMatrix[i, j];
                    }
                    sum += confMatrix[i, j];
                }
            }

            for (int i = 0; i < len; i++)
                result[TN, i] = sum - result[TP, i] - result[FP, i] - result[FN, i];

            return result;
        }

        public static (double Accuracy, double PrecisionPos, double PrecisionNeg, double RecallPos, double RecallNeg)
            ClassificationQuality<T>(T[] real, T[] predicted)
        {
            var data = real.Zip(predicted, (r, p) => (Real: r, Predicted: p)).ToList();
            if (data.Count() == 0)
                return (0, 0, 0, 0, 0);

            double accuracy = 0;
            double precisionPos = 0;
            double precisionNeg = 0;
            double recallPos = 0;
            double recallNeg = 0;

            var cm = GetConfusionMatrix(real, predicted);

            cm.Index.Keys.ToList().ForEach(x => Console.WriteLine(x));

            var confusionMatrix = cm.Matrix;
            int len = confusionMatrix.GetLength(0);

            Console.WriteLine("\nConfusion Matrix");

            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    Console.Write($"{confusionMatrix[i, j]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nTFPN");

            var tfpn = GetTFPN(confusionMatrix);

            var labels = new string[] { "TP", "TN", "FP", "FN" };

            for (int i = 0; i < 4; i++)
            {
                Console.Write(labels[i] + " ");
                for (int j = 0; j < len; j++)
                    Console.Write($"{tfpn[i, j]} ");
                Console.WriteLine();
            }

            foreach (var p in cm.Index)
            {
                var cat = p.Key;
                var i = p.Value;

                Console.WriteLine($"{cat} P+ = {1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FP, i])}");
                Console.WriteLine($"{cat} P- = {1.0 * tfpn[TN, i] / (tfpn[TN, i] + tfpn[FN, i])}");
                Console.WriteLine($"{cat} R+ = {1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FN, i])}");
                Console.WriteLine($"{cat} R- = {1.0 * tfpn[TN, i] / (tfpn[TN, i] + tfpn[FP, i])}");
            }           

            for (int i = 0; i < len; i++)
            {
                accuracy += tfpn[TP, i];

                precisionPos += 1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FP, i]);
                precisionNeg += 1.0 * tfpn[TN, i] / (tfpn[TN, i] + tfpn[FN, i]);
                recallPos += 1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FN, i]);
                recallNeg += 1.0 * tfpn[TN, i] / (tfpn[TN, i] + tfpn[FP, i]);
            }
            precisionPos /= len;
            precisionNeg /= len;
            recallPos /= len;
            recallNeg /= len;

            accuracy /= data.Count();

            return (accuracy, precisionPos, precisionNeg, recallPos, recallNeg);
        }
    }
}
