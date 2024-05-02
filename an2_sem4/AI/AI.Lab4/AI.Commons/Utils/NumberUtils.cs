using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Commons.Utils
{
    public static class NumberUtils
    {
        public static int Clamp(this int x, int a, int b)
        {
            if (x < a) return a;
            if (x > b) return b;
            return x;
        }
    }
}
