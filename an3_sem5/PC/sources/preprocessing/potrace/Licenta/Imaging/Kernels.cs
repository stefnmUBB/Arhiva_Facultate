using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Imaging
{
    public static class Kernels
    {
        public static Matrix<DoubleNumber> AverageBox(int w, int h)
            => Matrix<DoubleNumber>.Fill(h, w, (DoubleNumber)(1.0 / (w * h)));        

        public static Matrix<DoubleNumber> GaussianFilter(int w, int h, double s = 1.4, bool normalize = true)
        {
            double s2 = s * s;
            double g(double x, double y) => Math.Exp(-((x * x + y * y) / s2)) / (2 * Math.PI * s2);

            var m = Matrix<DoubleNumber>.CreateByRule(h, w, (i, j) =>
            {
                i -= h / 2 + 1;
                j -= w / 2 + 1;
                double v = 0;
                for (int p=-5;p<5;p++)                
                    for(int q=-5;q<5;q++)                    
                        v += g(i + p * 0.1, j + q * 0.1) * 0.001;

                return v;
            });
            if (normalize)
            {                
                m = Matrices.Divide(m, Matrices.ItemsSum(m));
            }            
            return m;
        }

        public static Matrix<DoubleNumber> SobelX()
            => new Matrix<DoubleNumber>(3, 3, new DoubleNumber[] { -1, 0, 1, -2, 0, 2, -1, 0, 1 });

        public static Matrix<DoubleNumber> SobelY()
            => new Matrix<DoubleNumber>(3, 3, new DoubleNumber[] { -1, -2, -1, 0, 0, 0, 1, 2, 1 });
        public static Matrix<DoubleNumber> Laplacian()
            => new Matrix<DoubleNumber>(3, 3, new DoubleNumber[] { 0, 1, 0, 1, -4, 1, 0, 1, 0 });

    }
}
