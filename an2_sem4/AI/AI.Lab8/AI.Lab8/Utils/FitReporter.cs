using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Utils
{
    internal class PredictReporter
    {
        public void Report(double[] sampleOutput)
        {
            Reported?.Invoke(sampleOutput);
        }

        public delegate void OnReported(double[] sampleOutput);
        public event OnReported Reported;

    }
}
