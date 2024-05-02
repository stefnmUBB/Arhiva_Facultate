using AI.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.ML.Utils
{
    internal static class Classification
    {
        public static (int[,] Matrix, Dictionary<T, int> Index) GetConfusionMatrix<T>(this IEnumerable<PredictedData<T>> data)
        {
            var Index = data.Select(d => new List<T> { d.Real, d.Predicted }).SelectMany(l => l).Distinct()
                .Select((c, i) => (c, i)).ToDictionary(x => x.c, x => x.i);

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

            for(int i=0;i<len;i++)
            {
                for(int j=0;j<len;j++)
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
            ClassificationQuality<T>(this IEnumerable<PredictedData<T>> data)
        {
            if (data.Count() == 0)
                return (0, 0, 0, 0, 0);
            
            double accuracy = 0;
            double precisionPos = 0;
            double precisionNeg = 0;
            double recallPos = 0;
            double recallNeg = 0;

            var cm = GetConfusionMatrix(data);

            cm.Index.Keys.ToList().ForEach(x=>Console.WriteLine(x));

            var confusionMatrix = cm.Matrix;
            int len = confusionMatrix.GetLength(0);

            Console.WriteLine("\nConfusion Matrix");

            for (int i=0;i< len; i++)
            {
                for(int j=0;j< len; j++)
                {
                    Console.Write($"{confusionMatrix[i, j]} ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nTFPN");

            var tfpn = GetTFPN(confusionMatrix);

            var labels = new string[] { "TP", "TN", "FP", "FN" };

            for(int i=0;i<4;i++)
            {
                Console.Write(labels[i] + " ");
                for (int j = 0; j < len; j++)
                    Console.Write($"{tfpn[i, j]} ");
                Console.WriteLine();
            }

            foreach(var p in cm.Index)
            {
                var cat = p.Key;
                var i = p.Value;

                Console.WriteLine($"{cat} P+ = {1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FP, i])}");
                Console.WriteLine($"{cat} P- = {1.0 * tfpn[TN, i] / (tfpn[TN, i] + tfpn[FN, i])}");
                Console.WriteLine($"{cat} R+ = {1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FN, i])}");
                Console.WriteLine($"{cat} R- = {1.0 * tfpn[TN, i] / (tfpn[TN, i] + tfpn[FP, i])}");
            }

            double precisionPosDen = 0;
            double precisionPosNom = 0;

            double precisionNegDen = 0;
            double precisionNegNom = 0;

            double recallPosDen = 0;
            double recallPosNom = 0;

            double recallNegDen = 0;
            double recallNegNom = 0;

            /*for (int i = 0; i < len; i++)
            {
                accuracy += tfpn[TP, i];

                precisionPosDen += tfpn[TP, i];
                precisionPosNom += tfpn[TP, i] + tfpn[FP, i];

                precisionNegDen += tfpn[TN, i];
                precisionNegNom += tfpn[TN, i] + tfpn[FN, i];


                recallPosDen += tfpn[TP, i];
                recallPosNom += tfpn[TP, i] + tfpn[FN, i];

                recallNegDen += tfpn[TN, i];
                recallNegNom += tfpn[TN, i] + tfpn[FP, i];
            }
            
            precisionPos = precisionPosDen / precisionPosNom;
            precisionNeg = precisionNegDen / precisionNegNom;
            recallPos = recallPosDen / recallPosNom;
            recallNeg = recallNegDen / recallNegNom;
            */

            for (int i=0;i<len;i++)
            {
                accuracy += tfpn[TP, i];

                precisionPos += 1.0 * tfpn[TP, i] / (tfpn[TP, i] + tfpn[FP, i]);
                precisionNeg += 1.0 *  tfpn[TN, i] / (tfpn[TN, i] + tfpn[FN, i]);
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
