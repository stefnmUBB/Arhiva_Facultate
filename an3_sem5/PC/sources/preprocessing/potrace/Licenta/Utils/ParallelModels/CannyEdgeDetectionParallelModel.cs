using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Parallelization;
using Licenta.Commons.Utils;
using Licenta.Imaging;
using System;

namespace Licenta.Utils.ParallelModels
{
    using DoubleMatrix = Matrix<DoubleNumber>;
    internal class CannyEdgeDetectionParallelModel : ParallelGraphModel
    {
        private double Threshold0;
        private double Threshold1;

        public CannyEdgeDetectionParallelModel(CannyEdgeDetectionOptions options)
        {
            Threshold0 = options.Threshold0;
            Threshold1 = options.Threshold1;


            var inputImage = CreateInput<ImageRGB>();

            //var grayScaleFilter1 = CreateNode<ImageRGB>(ToGrayscaleDefault, inputImage);
            //var grayScaleFilter2 = CreateNode<ImageRGB>(ToGrayscaleUniform, inputImage);
            var grayScaleFilter = CreateNode<ImageRGB>(ToGrayscaleDefault, inputImage);


            var gaussianFilter = CreateNode<DoubleMatrix>(GaussianFilter, grayScaleFilter);

            var gradX = CreateNode<DoubleMatrix>(ComputeGradX, gaussianFilter);
            var gradY = CreateNode<DoubleMatrix>(ComputeGradY, gaussianFilter);

            var magnitude = CreateNode<DoubleMatrix, DoubleMatrix>(ComputeMagnitude, gradX, gradY);
            var direction = CreateNode<DoubleMatrix, DoubleMatrix>(ComputeDirection, gradX, gradY);

            var refined = CreateNode<DoubleMatrix, DoubleMatrix>(Refine, magnitude, direction);

            //var normalized = CreateNode<DoubleMatrix>(Normalize, refined);            

            var doubleThreshold = CreateNode<DoubleMatrix>(DoubleThreshold, refined);
            var hysteresis = CreateOutput<DoubleMatrix>(Hysteresis, refined);
        }

        public DoubleMatrix DoubleThreshold(DoubleMatrix m)
        {
            return Matrices.ApplyDoubleThreshold(m, 0, 1, Threshold0, Threshold1);
        }        

        public DoubleMatrix Run(ImageRGB image, TaskManager tm = null) => Run(tm, new object[] { image })[0] as DoubleMatrix;
        public DoubleMatrix RunSync(ImageRGB image) => RunSync(new object[] { image })[0] as DoubleMatrix;


        private static DoubleMatrix Normalize(DoubleMatrix m)
        {
            var max = Matrices.MaxElement(m);
            Console.WriteLine($"Max={max}");
            if (max.Value == 0)
                return new DoubleMatrix(m);
            return Matrices.DoEachItem(m, x => x.Divide(max));
        }
        private static DoubleMatrix ToGrayscaleDefault(ImageRGB img) => img.ToGrayScaleMatrixLinear();
        private static DoubleMatrix ToGrayscaleUniform(ImageRGB img) => img.ToGrayScaleMatrixLinear(1.0 / 3, 1.0 / 3, 1.0 / 3);
        private static DoubleMatrix Average(DoubleMatrix m1, DoubleMatrix m2)
            => Matrices.DoItemByItem(m1, m2, (a, b) => a.Add(b).Multiply(0.5));
        private static DoubleMatrix GaussianFilter(DoubleMatrix m) => Matrices.Convolve(m, Kernels.GaussianFilter(5, 5, 1.0), Matrices.ConvolutionBorder.Extend);
        private static DoubleMatrix ComputeGradX(DoubleMatrix m) => Matrices.Convolve(m, Kernels.SobelX(), Matrices.ConvolutionBorder.Extend);
        private static DoubleMatrix ComputeGradY(DoubleMatrix m) => Matrices.Convolve(m, Kernels.SobelY(), Matrices.ConvolutionBorder.Extend);
        public static DoubleMatrix ComputeMagnitude(DoubleMatrix gx, DoubleMatrix gy) => Matrices.DoItemByItem(gx, gy, Functions.Hypot);
        public static DoubleMatrix ComputeDirection(DoubleMatrix gx, DoubleMatrix gy) => Matrices.DoItemByItem(gx, gy, Functions.Atan2);

        public static DoubleMatrix Refine(DoubleMatrix magn, DoubleMatrix dir)
        {
            magn = new DoubleMatrix(magn);
            (int X, int Y)[] dirs = { (1, 0), (1, 1), (0, 1), (-1, -1), (-1, 0) };

            (int X, int Y) getGradientDirection(double angle)
            {
                var i = (int)(angle * 4 / Math.PI);
                var d = dirs[(i < 0 ? i : -i).Clamp(0, 4)];
                if (i < 0) d.Y = -d.Y;
                return d;
            }

            for (int r = 0; r < magn.RowsCount; r++)
            {
                for (int c = 0; c < magn.ColumnsCount; c++)
                {
                    var d = getGradientDirection(dir[r, c].Value);
                    var r2 = r + d.Y;
                    var c2 = c + d.X;
                    if (0 <= r2 && r2 < magn.RowsCount && 0 <= c2 && c2 < magn.ColumnsCount)
                    {
                        if (magn[r, c].Value < magn[r2, c2].Value)
                        {
                            magn[r, c] = new DoubleNumber(0);
                            continue;
                        }
                    }
                    d = (-d.X, -d.Y);
                    r2 = r + d.Y;
                    c2 = c + d.X;
                    if (0 <= r2 && r2 < magn.RowsCount && 0 <= c2 && c2 < magn.ColumnsCount)
                    {
                        if (magn[r, c].Value < magn[r2, c2].Value)
                        {
                            magn[r, c] = new DoubleNumber(0);
                            continue;
                        }
                    }
                }
            }
            return magn;
        }

        public DoubleMatrix Hysteresis(DoubleMatrix m) => Hysteresis(m, (Threshold0 + Threshold1) / 3);

        public static DoubleMatrix Hysteresis(DoubleMatrix m, double threshold) => Matrices.DoEachItem(m, (x, i, j) =>
        {                        
            for(int di=-1;di<=1;di++)            
                for(int dj=-1;dj<=1;dj++)
                {
                    int ni = (i + di).Clamp(0, m.RowsCount - 1);
                    int nj = (j + dj).Clamp(0, m.ColumnsCount - 1);
                    if (m[ni, nj].Value > threshold)                    
                        return x;                    
                }
            return new DoubleNumber(0d);
        });


    }
}
