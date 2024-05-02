using AI.GradientDescend.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AI.GradientDescend.Normalization
{
    internal class ZNorm : INormalizationMethod
    {
        bool IsInitialized = false;
        double Mean = 0;
        double Std = 1;

        public IEnumerable<double> Normalize(IEnumerable<double> data)
        {
            if(!IsInitialized)
            {
                Mean = data.Mean();
                Std = data.Std();
                IsInitialized = true;
            }
            return data.Select(x => (x - Mean) / Std);
        }
    }
}
