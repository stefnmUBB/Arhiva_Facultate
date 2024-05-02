
using System.Collections.Generic;

namespace AI.Lab11.Normalization
{
    public interface INormalizationMethod 
    {
        IEnumerable<double> Normalize(IEnumerable<double> data);
    }
}
