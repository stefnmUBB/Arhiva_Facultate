using AI.Lab8.Algebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Utils
{
    internal interface IClassifier
    {         
        int GetLabelId(string label);

        void Train(Matrix x, int[] outLabelIds);

        void Train(Matrix x, string[] outLabels);

        string[] Predict(Matrix x);
        string[] GetLabels();

        Logger Logger { get; set; }
        PredictReporter PredictReporter { get; set; }

        double Test(string[] computed, string[] real);        
    }
}
