using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab1.Lib.Utils
{
    internal static class ListUtils
    {
        public static T Max<T>(this List<T> list, Func<T,T, int> comparer) where T:IComparable
        {
            if (list.Count == 0)
                return default(T);

            T max = list[0];
            foreach(var elem in list)
            {
                if (comparer(elem, max) > 0) 
                {
                    max = elem;
                }
            }

            return max;
        }      
    }
}
