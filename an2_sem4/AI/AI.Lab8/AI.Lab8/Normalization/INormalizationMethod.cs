
using System.Collections.Generic;

namespace AI.Lab8.Normalization
{
    public interface INormalizationMethod 
    {
        IEnumerable<double> Normalize(IEnumerable<double> data);
    }
}
