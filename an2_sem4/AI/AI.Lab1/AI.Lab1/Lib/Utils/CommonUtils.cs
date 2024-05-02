using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab1.Lib.Utils
{
    internal static class CommonUtils
    {
        public static bool IsBetween<T>(this T a, T b, T c) where T : IComparable
        {
            return b.CompareTo(a) <= 0 && a.CompareTo(c) <= 0;
        }
    }
}
