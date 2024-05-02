using System;

namespace AI.Lab8.Algebra
{
    internal static class Functions
    {
        public static double Sigmoid(double x) => 1.0 / (1 + Math.Exp(-x));
    }
}
