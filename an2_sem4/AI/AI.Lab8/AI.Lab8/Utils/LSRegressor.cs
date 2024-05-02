using AI.Lab8.Algebra;
using System;
using System.Linq;

namespace AI.Lab8.Utils
{
    internal class LSRegressor : IRegressor
    {
        private Matrix _Weights;

        public double[] Weights => _Weights?.GetColumn(0)?.ToArray() ?? new double[0];

        public double[] Predict(Matrix x)
        {
            x = x.PrependColumnOfValues(1);
            return (x * _Weights).GetColumn(0).ToArray();
        }

        public void Train(Matrix x, double[] y, Action<double> errorUpdateCallback = null)
        {
            x = x.PrependColumnOfValues(1);
            _Weights = ((x.Transpose * x).Inverse * x.Transpose * Matrix.ColumnOf(y));
            Console.WriteLine("W=");
            Console.WriteLine(_Weights);
            Console.WriteLine(_Weights.GetColumn(0));
        }

        public void Reset() { _Weights = null; }

        public double Test(double[] computed, double[] real)
        {
            return real.MeanError(computed);
        }
    }    
}
