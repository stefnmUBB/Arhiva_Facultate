using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Parallelization;
using Licenta.Commons.Utils;
using Licenta.Imaging;
using System.Diagnostics;
using System.Linq;

namespace Licenta.Utils.ParallelModels
{
    using DoubleMatrix = Matrix<DoubleNumber>;
    internal class MonochromeParallelModel : ParallelGraphModel
    {
        private readonly double Threshold;

        public MonochromeParallelModel(double threshold = 0.5)
        {
            Threshold = threshold;

            var inputImage = CreateInput<ImageRGB>();            
            var grayScaleFilter = CreateNode<ImageRGB>(ToGrayscaleDefault, inputImage);
            var gaussianFilter = CreateNode<DoubleMatrix>(GaussianFilter, grayScaleFilter);                       
            var thresholdFilter = CreateOutput<DoubleMatrix>(ApplyThreshold, gaussianFilter);            
        }

        public DoubleMatrix ApplyThreshold(DoubleMatrix m)
        {
            var th = Threshold;

            if (th == 0)
                th = m.Items.Select(_ => (int)(_.Value * 255)).Distinct().Average() / 255d;

            Debug.WriteLine($"MyThreshold = {th}");

            return Matrices.DoEachItem(m, x => new DoubleNumber(x.Value < th ? 0 : 1));
        }        

        public DoubleMatrix Run(ImageRGB image, TaskManager tm = null) => Run(tm, new object[] { image })[0] as DoubleMatrix;
        public DoubleMatrix RunSync(ImageRGB image) => RunSync(new object[] { image })[0] as DoubleMatrix;

   
        private static DoubleMatrix ToGrayscaleDefault(ImageRGB img) => img.ToGrayScaleMatrixLinear();        
        private static DoubleMatrix Average(DoubleMatrix m1, DoubleMatrix m2)
            => Matrices.DoItemByItem(m1, m2, (a, b) => a.Add(b).Multiply(0.5));
        private static DoubleMatrix GaussianFilter(DoubleMatrix m) => Matrices.Convolve(m, Kernels.GaussianFilter(5, 5, 1.0), Matrices.ConvolutionBorder.Extend);       

    }
}
