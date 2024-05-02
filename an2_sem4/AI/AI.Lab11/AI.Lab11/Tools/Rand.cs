using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab11.Tools
{
    public static class Rand
    {
        private static Random Random = new Random();
        public static int Int(int a, int b) => a + Random.Next() % (b - a);
        public static int Int(int lim) => Random.Next() % lim;

        public static double NextDouble() => Random.NextDouble();        


    }
}
