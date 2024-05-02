using AI.Lab8.Algebra;
using System;
using System.Linq;

namespace AI.Lab8.Utils
{
    internal class MulticlassClassifier : AbstractClassifier
    {
        private readonly IRegressor[] _Regressors;

        public IRegressor[] Regressors => _Regressors.ToArray();


        public MulticlassClassifier(params (string label, IRegressor regressor)[] outputs) 
            : base(outputs.Select(_=>_.label).ToArray())
        {
            _Regressors = outputs.Select(_ => _.regressor).ToArray();
        }

        private double[][] FitMulti(Matrix x)
        {
            var m = new Matrix(x.RowsCount, Labels.Length);

            for(int j=0;j<_Regressors.Length;j++)
            {                
                var fit = _Regressors[j].Predict(x).ToArray();
                for(int i=0;i<fit.Length;i++)
                {
                    m[i, j] = Functions.Sigmoid(fit[i]);
                }
            }
            return m.Rows.ToArray();
            
        }

        public override string[] Predict(Matrix x)
        {
            var result = new string[x.RowsCount];
            var fits = FitMulti(x);

            fits.ToList().ForEach(_=>PredictReporter?.Report(_));

            Logger?.Log(string.Join(", ", Labels.Select((_, j) => _.PadLeft(Math.Max(7, Labels[j].Length)))));
            for (int i=0;i<result.Length;i++)
            {
                Logger?.Log(string.Join(", ", fits[i].Select((_, j) => _.ToString("F4").PadLeft(Math.Max(7, Labels[j].Length)))));
                result[i] = Labels[fits[i].IndexOfMax()];
            }
            return result;            
        }

        public override void Train(Matrix x, int[] outLabelIds)
        {
            for(int i=0;i<_Regressors.Length;i++)
            {
                _Regressors[i].Train(x, outLabelIds.Select(t => t == i ? 1.0 : 0.0).ToArray(), errorUpdateCallback: null);
            }                     
        }
    }


}
