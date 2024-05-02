﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.GradientDescend.Normalization
{
    internal class ClipNorm : INormalizationMethod
    {
        private readonly int Threshold;

        public ClipNorm(int threshold)
        {
            Threshold = threshold;
        }

        public IEnumerable<double> Normalize(IEnumerable<double> data)
            => data.Select(x => Math.Min(x, Threshold));
    }
}
