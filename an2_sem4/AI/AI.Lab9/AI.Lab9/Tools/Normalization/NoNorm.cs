﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab9.Normalization
{
    internal class _NoNorm : INormalizationMethod
    {
        public IEnumerable<double> Normalize(IEnumerable<double> data) => data;        
    }
}
