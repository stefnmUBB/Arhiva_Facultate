using AI.Lab11.Tools.Algebra;
using AI.Lab11.Utils;
using Keras;
using Keras.Callbacks;
using Keras.Layers;
using Keras.Models;
using Numpy;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Effects;

namespace AI.Lab11.Solvers
{
    internal class GoodEmojiSolver
    {
        Sequential Model = new Sequential();

        public GoodEmojiSolver(string trainFolder, string testFolder)
        {
            Model.Add(new Conv2D(62, Tuple.Create(3, 3), activation: "relu", input_shape: new Shape(48, 48, 1)));
            Model.Add(new Conv2D(/*64*/50, Tuple.Create(3, 3), activation: "relu"));
            Model.Add(new MaxPooling2D(Tuple.Create(2, 2)));
            Model.Add(new Dropout(0.25));

            Model.Add(new Conv2D(40 /*128*/, Tuple.Create(3, 3), activation: "relu"));
            Model.Add(new MaxPooling2D(Tuple.Create(2, 2)));
            Model.Add(new Conv2D(33 /*128*/, Tuple.Create(3, 3), activation: "relu"));
            Model.Add(new MaxPooling2D(Tuple.Create(2, 2)));
            Model.Add(new Dropout(0.25));

            Model.Add(new Flatten());
            Model.Add(new Dense(/*1024*/128, activation: "relu"));
            Model.Add(new Dropout(0.5));
            Model.Add(new Dense(5, activation: "softmax"));
            Model.Compile(optimizer: "Adam", loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });            
        }

        public string[] Labels { get; private set; }

        private List<(NDarray<byte> Pixels, NDarray<double> Labels)> TrainData;
        private List<(NDarray<byte> Pixels, NDarray<double> Labels)> TestData;

        public void Train()
        {
            var src = @"C:\Users\Stefan\Desktop\img-facebook-64\ok";

            var data = Directory.GetFiles(src).Select(f =>
            {
                var labels = Path.GetFileNameWithoutExtension(f).Split('-').Skip(1).ToArray();
                var img = new Mat(f, ImreadModes.Grayscale);

                var mat = img.Resize(new Size(48, 48));
                mat.GetArray(out byte[] array);
                var m = new byte[48, 48, 1];
                for (int y = 0; y < 48; y++)
                {
                    for (int x = 0; x < 48; x++)
                        m[y, x, 0] = array[48 * y + x];
                }
                var px = np.array(m, dtype: np.uint8);
                return (Pixels: px, Labels: labels);
            }).ToArray();
            Labels = data.SelectMany(_ => _.Labels).Distinct().ToArray();
            Console.WriteLine(Labels.Length);
            var uData = data.Select(_ =>
                (Pixels: _.Pixels, Labels: np.array(
                    Labels.Select(l => _.Labels.Contains(l) ? 1.0 : 0.0).ToArray(), dtype: np.@double
                    ))
            ).ToArray();
            (TrainData, TestData) = uData.SplitTrainTest();            
            
            var History = Model.Fit(
                np.array(TrainData.Select(_ => _.Pixels).ToArray()),
                np.array(TrainData.Select(_ => _.Labels).ToArray()),                
                batch_size: BatchSize,
                epochs: EpochsCount
                );

            Console.WriteLine(History.HistoryLogs.Keys.JoinToString(", "));
            Loss = History.HistoryLogs["loss"].ToArray();            
            Accuracy = History.HistoryLogs["accuracy"].ToArray();           
        }

        public void Test()
        {
            var result = Model.Predict(np.array(TestData.Select(_ => _.Pixels).ToArray()))
                .GetData<double>()
                .Select((x, i) => (x, i / 5))
                .GroupBy(_ => _.Item2)
                .Select(g => g.Select(_ => _.x).ToArray())
                .ToArray();                
            
            for(int i=0;i<result.GetLength(0);i++)
            {
                Console.Write(TestData[i].Labels.GetData<double>()
                    .Select((x, p) => (x, p))
                    .Where(_ => _.x != 0)
                    .Select(_ => Labels[_.p])
                    .JoinToString(", ").PadLeft(50));
                Console.Write(" : ");
                Console.WriteLine(result[i]
                    .Select((x, p) => (x, p))
                    .OrderBy(_ => -_.x)
                    .Take(2)
                    .Select(_ => Labels[_.p])
                    .JoinToString(", "));
            }

        }

        public int BatchSize { get; set; } = 7;
        public int EpochsCount { get; set; } = 10;

        public double[] Loss { get; private set; }        
        public double[] Accuracy { get; private set; }        
    }
}
