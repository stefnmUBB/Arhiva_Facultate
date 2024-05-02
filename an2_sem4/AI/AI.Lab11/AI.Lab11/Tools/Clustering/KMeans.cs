using AI.Lab11.Utils;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab11.Tools.Clustering
{
    public class KMeans
    {
        public int ClustersCount { get; }
        public int IterationsCount { get; set; } = 100;

        public double[][] Centroids { get; private set; }

        public Func<double[], double[], double> Distance { get; set; } = Distances.EuclideanDistance;

        public KMeans(int clustersCount, Func<double[], double[], double> distance = null)
        {
            ClustersCount = clustersCount;
            Centroids = new double[ClustersCount][];
            if (distance != null)
                Distance = distance;
        }

        public double[][] BestCentroids;

        public void Train(double[][] data, Action<double> stateReport=null)
        {
            int dim = data[0].Length;
            Centroids = data.ChooseRandom(ClustersCount);
            BestCentroids = Centroids.Select(_ => _.ToArray()).ToArray();
            int[] group = new int[data.Length];

            double prevState = 0;
            double crtState = 0;
            int iter = 0;
            int penalty = 0;
            double minState = double.PositiveInfinity;
            do
            {
                for (int i = 0; i < data.Length; i++)
                {
                    group[i] = Centroids.ArgMin(c => Distance(data[i], c));
                }

                for (int c = 0; c < Centroids.Length; c++)
                {
                    var cluster = data.Select((x, i) => (x, i)).Where(d => group[d.i] == c)
                        .Select(d => d.x).ToArray();

                    var count = cluster.Length;
                    if (cluster.Length == 0)
                    {
                        //Centroids[c] = Enumerable.Range(0, dim).Select(_ => 0.0).ToArray();
                    }
                    else
                    {
                        var sum = cluster.Aggregate((x, y) => x.Zip(y, (xi, yi) => xi + yi).ToArray()).ToArray();
                        Centroids[c] = sum.Select(x => x / count).ToArray();
                    }
                }

                prevState = crtState;
                crtState = 0;
                for (int i = 0; i < data.Length; i++)
                    crtState += Centroids.Min(c => Distance(data[i], c));

                if (crtState < prevState)
                {
                    minState = crtState;
                    BestCentroids = Centroids.Select(_ => _.ToArray()).ToArray();
                    penalty = 0;
                }
                else if (crtState > prevState || crtState > minState) 
                {
                    penalty++;
                    if (penalty >= 3) // escaped local optimum
                        break;
                }
                stateReport?.Invoke(crtState);                
                iter++;
            }
            while (Math.Abs(prevState - crtState) > 0.001 && iter < IterationsCount);
        }

        public int GetClusterSingle(double[] item)
        {
            return BestCentroids.ArgMin(c => Distance(item, c));
        }


    }
}
