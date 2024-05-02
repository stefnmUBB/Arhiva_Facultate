using AI.Lab10.Tools.Clustering;
using AI.Lab10.Tools.Text.Vectorization;
using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text
{
    public class TextKMeans
    {        
        private KMeans KMeans;

        private List<string> Labels;

        private int[] ClusterToLabelId;

        public IVectorizer Vectorizer { get; set; } = new BagOfWords();
        public Func<double[], double[], double> Distance { get; set; } = Distances.CosDistance;

        private double[][] Vectorize(string[] input)
        {
            return Vectorizer.ExtractFeatures(input);            
        }

        public Action<double> StateReport = null;


        public void TrainPrecompiled(double[][] vinput, string[] output)
        {
            Labels = output.Distinct().ToList();
            Console.WriteLine(Labels.JoinToString("\n"));            

            KMeans = new KMeans(Labels.Count, Distance) { IterationsCount = 5 };
            KMeans.Train(vinput, StateReport);

            var labelscnt = Labels.Count;
            ClusterToLabelId = new int[labelscnt];

            var predictedOutputs = vinput.Select(KMeans.GetClusterSingle).ToArray();

            var matrix = new int[labelscnt, labelscnt];
            for (int i = 0; i < vinput.Length; i++)
            {
                matrix[Labels.IndexOf(output[i]), predictedOutputs[i]]++;
            }

            var colsSum = new int[labelscnt];

            for (int i = 0; i < labelscnt; i++)
            {
                for (int j = 0; j < labelscnt; j++)
                {
                    colsSum[j] += matrix[i, j];
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            for (int j = 0; j < labelscnt; j++)
                if (colsSum[j] == 0)
                    colsSum[j] = 1;


            double fitness = 0;

            foreach (var p in GetPermutations(Enumerable.Range(0, labelscnt), labelscnt))
            {
                var new_fitness = p.Select((i, j) => matrix[i, j]).Sum();
                if (new_fitness > fitness)
                {
                    fitness = new_fitness;
                    ClusterToLabelId = p.ToArray();
                }
            }
            Console.WriteLine(ClusterToLabelId.JoinToString(" "));
        }

        public void Train(string[] input, string[] output)
        {            
            Labels = output.Distinct().ToList();
            Console.WriteLine(Labels.JoinToString("\n"));
            Vectorizer.Prepare(input);
            var vinput = Vectorize(input);
            TrainPrecompiled(vinput, output);
        }

        public string[] Predict(string[] input)
        {
            var vinput = Vectorize(input);
            return PredictPrecompiled(vinput);            
        }

        public string[] PredictPrecompiled(double[][] vinput)
        {            
            return vinput.Select(_ => Labels[ClusterToLabelId[KMeans.GetClusterSingle(_)]]).ToArray();
        }



        static IEnumerable<T[]> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }).ToArray());
        }


    }
}
