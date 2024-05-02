using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab8.Normalization
{
    internal class MinMaxNorm : INormalizationMethod
    {
        bool IsInitialized = false;
        double _Min = 0;
        double _Max = 1;

        public IEnumerable<double> Normalize(IEnumerable<double> data)
        {
            if (!IsInitialized)
            {
                _Min = data.Min();
                _Max = data.Max();
                IsInitialized = true;
            }

            var del = _Max - _Min;

            return data.Select(x => del == 0 ? 0 : (x - _Min) / del);
        }
    }
}
