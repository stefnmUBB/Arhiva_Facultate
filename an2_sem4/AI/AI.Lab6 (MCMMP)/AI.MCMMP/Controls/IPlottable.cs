﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MCMMP.Controls
{
    public interface IPlottable
    {
        void Plot(IPlotter plotter);
        void Clear(IPlotter plotter);
    }
}
