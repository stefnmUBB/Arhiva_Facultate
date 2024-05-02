using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MCMMP.Utils
{
    internal static class Extensions
    {
        public static int Clamp(this int x, int a, int b)
        {
            if (x < a) return a;
            if (x > b) return b;
            return x;
        }
    }
}
