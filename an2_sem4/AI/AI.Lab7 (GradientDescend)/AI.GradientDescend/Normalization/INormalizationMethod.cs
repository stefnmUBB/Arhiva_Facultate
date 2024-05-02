
using System.Collections.Generic;

namespace AI.GradientDescend.Normalization
{
    public interface INormalizationMethod 
    {
        IEnumerable<double> Normalize(IEnumerable<double> data);
    }
}
