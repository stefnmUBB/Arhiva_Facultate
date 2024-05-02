using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab11.Tools.Algebra
{
    internal class Convolution
    {
        public static double[,] Compute(double[,] source, double[,] conv)
        {
            int ch = conv.GetLength(0);
            int cw = conv.GetLength(1);
            int cx = cw / 2;
            int cy = ch / 2;

            int sh = source.GetLength(0);
            int sw = source.GetLength(1);

            var result = new double[sh, sw];

            for (int y = 0; y < sh; y++)
            {
                for (int x = 0; x < sw; x++)
                {
                    double s = 0;

                    for (int ciy = 0; ciy < ch; ciy++)
                    {
                        for (int cix = 0; cix < cw; cix++)
                        {
                            var ty = y + ciy - cy;
                            var tx = x + cix - cx;

                            if (0 <= ty && ty < sh && 0 <= tx && tx <= sw)
                                s += source[ty, tx] * conv[ciy, cix];
                        }
                    }
                    result[y, x] = s / (cw * ch);
                }
            }
            return result;
        }
    }
}
