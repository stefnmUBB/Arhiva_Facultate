using AI.Lab8.Algebra;
using System;

namespace AI.Lab8.Utils
{
    internal interface IRegressor
    {
        void Train(Matrix x, double[] y, Action<double> errorUpdateCallback = null);

        double[] Predict(Matrix x);

        double[] Weights { get; }

        void Reset();

        double Test(double[] computed, double[] real);
    }
}
