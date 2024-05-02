
using System.Collections.Generic;

namespace AI.Lab9.Normalization
{
    public interface INormalizationMethod 
    {
        IEnumerable<double> Normalize(IEnumerable<double> data);
    }
}
