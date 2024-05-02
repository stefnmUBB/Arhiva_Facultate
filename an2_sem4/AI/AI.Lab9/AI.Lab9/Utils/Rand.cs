using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab9.Utils
{
    internal class Rand
    {
        private static Random Random = new Random();
        public static int NextInt(int lim) => Random.Next() % lim;
        public static double NextDouble() => Random.NextDouble();
    }
}
