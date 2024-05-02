using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text.Vectorization
{
    public interface IVectorizer
    {
        void Prepare(string[] text);
        double[][] ExtractFeatures(string[] text);

        int FeaturesCount { get; }
    }
}
