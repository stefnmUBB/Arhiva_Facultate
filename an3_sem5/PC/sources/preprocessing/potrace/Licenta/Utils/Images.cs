using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Parallelization;
using Licenta.Imaging;
using Licenta.Utils.ParallelModels;
using static Licenta.Utils.ParallelModels.PotraceParallelModel;

namespace Licenta.Utils
{
    public static class Images
    {
        public static Matrix<DoubleNumber> ToGrayScaleMatrixLinear(this ImageRGB img, double factorR = 0.299, double factorG = 0.587, double factorB = 0.114)
        {
            return Matrices.DoEachItem(img, c => (DoubleNumber)(factorR * c.R.Value + factorG * c.G.Value + factorB * c.B.Value));
        }

        public static IReadMatrix<double> CannyEdgeDetectionMatrix(this ImageRGB img, CannyEdgeDetectionOptions options = null, TaskManager tm = null)
        {
            var model = new CannyEdgeDetectionParallelModel(options ?? new CannyEdgeDetectionOptions());
            var result = tm == null ? model.RunSync(img) : model.Run(img, tm);
            return result.Select(_ => _.Value);            
        }

        public static ImageRGB PartialCannyEdgeDetection(this ImageRGB img, CannyEdgeDetectionOptions options = null, TaskManager tm = null)
        {            
            var m = img.ToGrayScaleMatrixLinear();

            m = Matrices.Convolve(m, Kernels.GaussianFilter(3, 3));

            var sx = Matrices.Convolve(m, Kernels.SobelX());
            var sy = Matrices.Convolve(m, Kernels.SobelY());

            var g = CannyEdgeDetectionParallelModel.ComputeMagnitude(sx, sy);
            //var d = CannyEdgeDetectionParallelModel.ComputeDirection(sx, sy);
            //var r = CannyEdgeDetectionParallelModel.Refine(g, d);            
            return new ImageRGB(g);
        }

        public static ImageRGB CannyEdgeDetection(this ImageRGB img, CannyEdgeDetectionOptions options = null, TaskManager tm = null)
        {
            /*img.ToBitmap().Save("Original.png");
            var m = img.ToGrayScaleMatrixLinear();
            new ImageRGB(m).ToBitmap().Save("gs.png");

            m = Matrices.Convolve(m, Kernels.GaussianFilter(3, 3));
            new ImageRGB(m).ToBitmap().Save("gauss.png");

            var sx = Matrices.Convolve(m, Kernels.SobelX());
            var sy = Matrices.Convolve(m, Kernels.SobelY());

            var g = CannyEdgeDetectionParallelModel.ComputeMagnitude(sx, sy);
            var d = CannyEdgeDetectionParallelModel.ComputeDirection(sx, sy);
            var r = CannyEdgeDetectionParallelModel.Refine(g, d);
            //r = Matrices.ApplyDoubleThreshold(m, 0, 1, 0.2, 0.8);
            return new ImageRGB(r);*/

            var model = new CannyEdgeDetectionParallelModel(options ?? new CannyEdgeDetectionOptions());

            var result = tm == null ? model.RunSync(img) : model.Run(img, tm);
            return new ImageRGB(result);
        }

        public static ImageRGB ToMonochrome(this ImageRGB img, double threshold = 0.5, TaskManager tm = null)
        { 
            var model = new MonochromeParallelModel(threshold);
            var result = tm == null ? model.RunSync(img) : model.Run(img, tm);
            return new ImageRGB(result);
        }

        public static PotraceOutput ApplyPotrace(this ImageRGB img, double threshold = 0.5, TaskManager tm = null)
        {
            var model = new PotraceParallelModel();
            var result = tm == null ? model.RunSync(img) : model.Run(img, tm);
            return result;
        }

        public static Matrix<ColorHSL> ToHSL(this ImageRGB image)
        {
            return Matrices.DoEachItem<ColorRGB, ColorHSL>(image, ColorHSL.FromRGB);
        }

    }
}
