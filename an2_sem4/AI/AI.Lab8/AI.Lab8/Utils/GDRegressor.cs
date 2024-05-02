using AI.Lab8.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Utils
{
    internal class GDRegressor : IRegressor
    {
        private double[] _Weights;
        public double[] Weights => _Weights.ToArray();        
        public int BatchSize { get; set; } = 1;

        public Func<double, double, double> Error { get; set; } = (computed, real) => computed - real;

        public int MaxEpochs { get; set; } = 200;
        public double LearningRate { get; set; } = 0.0021;

        public double[] Predict(Matrix x) => x.PrependColumnOfValues(1).Rows.Select(ComputedOutput).ToArray();

        public void Train(Matrix x, double[] y, Action<double> errorUpdateCallback = null)
        {
            x = x.PrependColumnOfValues(1);
            _Weights = new double[x.ColsCount];

            TrainBatch(x, y, errorUpdateCallback);
        }

        double ComputedOutput(IEnumerable<double> x)
        {
            return x.Zip(_Weights, (xi, w) => xi * w).Sum();
        }

        void TrainStochastic(Matrix x, double[] y, Action<double> errorUpdateCallback = null)
        {
            var records = x.Rows.Zip(y, (r, yi) => (X: r, Y: yi)).ToList();
            for (int i = 0; i < MaxEpochs; i++)
            {
                foreach (var (X, Y) in records.Shuffle().ToArray()) 
                {
                    var err = Error(ComputedOutput(X), Y);
                    for(int k=0;k<_Weights.Length;k++)
                    {
                        _Weights[k] -= LearningRate * err * X[k];
                    }
                }
            }
        }

        void TrainBatch(Matrix x, double[] y, Action<double> errorUpdateCallback = null)
        {
            var records = x.Rows.Zip(y, (r, yi) => (X: r, Y: yi)).Shuffle().ToList();

            int batchSize = BatchSize == 0 ? records.Count : BatchSize;

            var batches = records.Select((r, i) => (r, i)).GroupBy(_ => _.i / batchSize)
                .Select(_ => _.Select(p => p.r).ToArray()).ToArray();

            for (int i=0;i<MaxEpochs;i++)
            {                
                foreach (var batch in batches)
                {
                    var new_weights = _Weights.ToArray();
                    foreach (var (X, Y) in batch) 
                    {
                        for (int k = 0; k < new_weights.Length; k++)
                        {
                            new_weights[k] -= LearningRate * Error(ComputedOutput(X), Y) * X[k];
                        }
                    }
                    _Weights = new_weights;
                }                                                
            }

        }

        public void Reset()
        {
            _Weights = new double[0];
        }

        public double Test(double[] computed, double[] real)
        {
            return real.MeanError(computed);
        }

        public GDRegressor() { }

        public GDRegressor(double[] weights)
        {
            _Weights = weights;
        }
    }
}
