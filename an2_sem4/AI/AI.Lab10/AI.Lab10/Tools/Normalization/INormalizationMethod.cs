
using System.Collections.Generic;

namespace AI.Lab10.Normalization
{
    public interface INormalizationMethod 
    {
        IEnumerable<double> Normalize(IEnumerable<double> data);
    }
}
