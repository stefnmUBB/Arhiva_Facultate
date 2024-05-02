using AI.Lab11.Data;
using AI.Lab11.Extensions;
using AI.Lab11.Tools.Clustering;
using AI.Lab11.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab11.Solvers
{
    internal class EmojiSolver
    {
        private KMeans KMeans = new KMeans(2, Distances.EuclideanDistance);
        IVectorizer<Emoji> Vectorizer { get; set; } = Vectorizers.CropFace; //Vectorizers.Pixels;

        public List<Emoji> TrainInput { get; }
        public List<Emoji> TestInput { get; }
        public double[][] VecTrainInput { get; }
        public double[][] VecTestInput { get; }


        public List<int> TrainOutput { get; private set; }
        public List<int> TestOutput { get; private set; }

        public EmojiSolver()
        {
            (TrainInput, TestInput) = DataSets.Emojis().Where(_ => _.Name.ToLower().Contains("face")).SplitTrainTest();
            VecTrainInput = TrainInput.Select(Vectorizer.Vectorize).ToArray();
            VecTestInput = TestInput.Select(Vectorizer.Vectorize).ToArray();
        }

        public void Solve()
        {
            KMeans.Train(VecTrainInput);
            TrainOutput = VecTrainInput.Select(KMeans.GetClusterSingle).ToList();
            TestOutput = VecTestInput.Select(KMeans.GetClusterSingle).ToList();
        }

        public IEnumerable<(Emoji Emoji, int LabelIndex)> GetTrainResults()
            => TrainInput.Zip(TrainOutput, (i, o) => (i, o));

        public IEnumerable<(Emoji Emoji, int LabelIndex)> GetTestResults()
            => TestInput.Zip(TestOutput, (i, o) => (i, o));

        public (Emoji Emoji, int LabelIndex)[] GetResults()
            => GetTrainResults().Concat(GetTestResults()).ToArray();

        public static class Vectorizers
        {
            public static IVectorizer<Emoji> Pixels = new PropertyVectorizer<Emoji, byte[]>("Bytes",
                bytes => bytes.Select(_ => _ / 255.0).ToArray());

            public static IVectorizer<Emoji> Grayscale = new GrayscaleVectorizer();
            public static IVectorizer<Emoji> CropFace = new CropFaceVectorizer();


            class CropFaceVectorizer : FilterVectorizer
            {
                public CropFaceVectorizer()
                    : base(bmp => bmp.Crop(7, 7, 18, 18).ToGrayscale(),
                          _ => _.Select((x, i) => (x, i)).Where(p => p.i % 3 == 0).Select(p => p.x).ToArray())
                {
                }
            }

            class FilterVectorizer : PropertyVectorizer<Emoji, Bitmap>
            {
                public FilterVectorizer(Func<Bitmap, Bitmap> filter, Func<double[], double[]> pxTransform = null)
                    : base("Image", bmp => (pxTransform??new Func<double[], double[]>(_=>_)) 
                        .Invoke(filter(bmp).ToBytes().Select(_ => _ / 255.0).ToArray()))
                { }
            }

            class GrayscaleVectorizer : PropertyVectorizer<Emoji, Bitmap>
            {
                public GrayscaleVectorizer()
                    : base("Image", bmp => bmp.ToGrayscale().ToBytes().Select((b, i) => (b, i))
                    .Where(_ => _.i % 3 == 0).Select(_ => _.b / 255.0).ToArray())  
                { }
            }
        }

    }
}
