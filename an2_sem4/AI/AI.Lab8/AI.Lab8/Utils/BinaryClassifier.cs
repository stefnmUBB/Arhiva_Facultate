using AI.Lab8.Algebra;
using System;
using System.Linq;

namespace AI.Lab8.Utils
{
    internal class BinaryClassifier : AbstractClassifier
    {
        private readonly IRegressor Regressor;
        public double Threshold { get; set; } = 0.5;

        public double[] Weights => Regressor.Weights.ToArray();

        public BinaryClassifier(IRegressor regressor, string labelPos, string labelNeg) : base(new string[] { labelPos, labelNeg })
        {
            Regressor = regressor;
        }        

        public override void Train(Matrix x, int[] outLabelIds)
        {
            Regressor.Reset();
            Regressor.Train(x, outLabelIds.Select(_=>(double)_).ToArray(), errorUpdateCallback: null);
        }

        public override string[] Predict(Matrix x)
        {
            var fits = Regressor
                .Predict(x)
                .Select(Functions.Sigmoid).ToArray();

            fits.ToList().ForEach(_ => PredictReporter?.Report(new double[1] { _ }));

            var result = fits
                .Select(t => t > Threshold ? 1 : 0)
                .Select(i => Labels[i])
                .ToArray();

            Logger?.Log($"{Labels[0]}_{Labels[1]}");

            for (int i = 0; i < fits.Length; i++) 
            {
                Logger?.Log($"{fits[i]}");
            }            

            return result;
        }        
    }
}
