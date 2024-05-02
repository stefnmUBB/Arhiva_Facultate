using Licenta.Commons.Math.Arithmetics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Commons.Math
{
    public static class Functions
    {
        public static DoubleNumber Hypot(DoubleNumber a, DoubleNumber b)
            => System.Math.Sqrt(a.Value * a.Value + b.Value * b.Value);

        public static DoubleNumber Atan2(DoubleNumber y, DoubleNumber x)
            => System.Math.Atan2(y.Value, x.Value);

    }
}
