using AI.Lab9.Algebra;
using AI.Lab9.Tools;
using AI.Lab9.Tools.ANN;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static AI.Lab9.Solvers.SepiaSolver;

namespace AI.Lab9.Solvers
{
    public class SepiaSolver
    {
        public List<ImageData32> LoadData(string fname)
        {
            var rawDigits = File.ReadAllLines(fname)
                .Select(_ => _.Split(' ').Select(s => int.Parse(s)).ToArray())
                .Select(_ => (Data: _.Take(3 * 1024).Select(x => (byte)x).ToArray(), IsSepia: _.Last() == 1 ? 1 : 0))
                .Select(_=>new ImageData32(_.Data, _.IsSepia))
                .ToList();
            return rawDigits;
        }

        public double[][] TrainInputData { get; private set; }
        public double[][] TrainOutputData { get; private set; }
        public double[][] TestInputData { get; private set; }
        public double[][] TestOutputData { get; private set; }

        /*
         FMAX = Model Fit=0,905660377358491; Batch=0; LL=0.088669556350759 {
            64 of Input
            23 of Gauss -3.04926678060403, -0.543287212398503
            30 of Sign
            14 of LocalizedSigmoid 0.159845345611612, -0.923969837714206, 1.95574179464287, -0.194074992442771
            2 of Sigmoid
        }
        */

        public NeuralNetwork NeuralNetwork { get; } = new NeuralNetwork()
        {
            IterationsCount = 200,
            LearningRate = 0.088669556350759
        }
                .AddLayer(Neurons.Input, 64)
                .AddLayer(Neurons.Hidden(ActivationFunction.Gauss(-3.04926678060403, -0.543287212398503)), 23)
                .AddLayer(Neurons.Hidden(ActivationFunction.Sign), 30)
                .AddLayer(Neurons.Output(ActivationFunction.LocalizedSigmoid(0.159845345611612, -0.923969837714206, 1.95574179464287, -0.194074992442771)), 14)
                .AddLayer(Neurons.Output(ActivationFunction.Sigmoid), 2)
                .Create();        
        
        public void Solve(string fname)
        {
            var data = LoadData(fname).Reduce().ToHSB().ToData().Shuffle().SplitTrainTest(0.8);
            TrainInputData = data.Train.Select(_ => _.x).ToArray();
            TrainOutputData = data.Train.Select(_ => _.t).ToArray();
            TestInputData = data.Test.Select(_ => _.x).ToArray();
            TestOutputData = data.Test.Select(_ => _.t).ToArray();


            Action f = () =>
            {
                var _data = LoadData(fname).Reduce().ToHSB().ToData().Shuffle().Take(250).SplitTrainTest(0.8);
                var finder = new ANNFinder(_data.Train, _data.Test, 0);
                
                finder.Rafinate();                
                //finder.OneIteration();
                Thread.Sleep(50000);
            };

            var qq=Task.Run(f);
            qq.Wait();

            /*Action f50 = () =>
            {
                var _data = LoadData(fname).Reduce().ToHSB().ToData().Shuffle().Take(250).SplitTrainTest(0.8);
                var finder = new ANNFinder(_data.Train, _data.Test);

                for (int i = 0; i < 1000000; i++)
                {
                    Console.WriteLine($"[00] Iteration {i}");
                    finder.OneIteration();
                    Thread.Sleep(50);
                }
            };

            Action f00 = () =>
            {
                var _data = LoadData(fname).Reduce().ToHSB().ToData().Shuffle().Take(250).SplitTrainTest(0.8);
                var finder = new ANNFinder(_data.Train, _data.Test);

                for (int i = 0; i < 1000000; i++)
                {
                    Console.WriteLine($"[00] Iteration {i}");
                    finder.OneIteration();
                    Thread.Sleep(50);
                }
            };


            var tk1 = Task.Run(f50);
            Thread.Sleep(500);
            var tk2 = Task.Run(f00);
            Thread.Sleep(500);            

            tk1.Wait();
            tk2.Wait();*/
            

            NeuralNetwork.Train(TrainInputData, TrainOutputData);
        }


        public Matrix GetTestOutputs(bool softmax = true)
        {
            var res = TestInputData.Zip(TestOutputData, (i, o) => (i, o)).Select((_) =>
            {
                var results = NeuralNetwork.PredictSingle(_.i);
                var maxIndex = results.ArgMax();

                if (softmax)
                {
                    var min = results.Min();
                    if (min < 0)
                    {
                        results = results.Select(x => x - min).ToArray();
                    }

                    var sum = results.Sum();
                    if (sum != 0)
                    {
                        for (int i = 0; i < 10; i++)
                            results[i] /= sum;
                    }
                }

                return results.Concat(_.o).Concat(new double[] { _.o[maxIndex] == 1 ? 1 : 0 }).ToArray();
            }).ToMatrix();

            return res;
        }

        public class ImageData32
        {
            public byte[] Data { get; }
            public int Sepia { get; }            

            public ImageData32(byte[] data, int sepia)
            {
                Data = data;
                Sepia = sepia;
            }

            public ImageData8 Reduce()
            {
                var data = new byte[3 * 8 * 8];

                for (int ty = 0; ty < 8; ty++)
                {                    
                    for (int tx = 0; tx < 8; tx++)
                    {
                        int r = 0;
                        int g = 0;
                        int b = 0;
                        for (int y = 0; y < 4; y++)
                        {
                            int row = 4 * ty + y;                            
                            for (int x = 0; x < 4; x++)
                            {
                                int col = 4 * tx + x;
                                int i = 3 * (32 * row + col);
                                r += Data[i];
                                g += Data[i+1];
                                b += Data[i+2];
                            }
                        }
                        r /= 16; g /= 16;b /= 16;
                        data[3 * (ty * 8 + tx)] = (byte)r;
                        data[3 * (ty * 8 + tx) + 1] = (byte)g;
                        data[3 * (ty * 8 + tx) + 2] = (byte)b;
                    }
                }

                return new ImageData8(data, Sepia);
            }
        }

        public class ImageData8
        {
            public byte[] Data { get; }
            public int Sepia { get; }

            public ImageData8(byte[] data, int value)
            {
                Data = data;
                Sepia = value;
            }

            public ImageData8HSB ToHSB()
            {
                var hsb = new double[3 * 64];
                for(int i=0;i<64;i++)
                {
                    int r = Data[3 * i];
                    int g = Data[3 * i + 1];
                    int b = Data[3 * i + 2];
                    var c = Color.FromArgb(r, g, b);

                    hsb[3 * i] = c.GetHue() * 0.01;
                    hsb[3 * i + 1] = c.GetSaturation();
                    hsb[3 * i + 2] = c.GetBrightness();

                }

                return new ImageData8HSB(hsb, Sepia);
            }
        }

        public class ImageData8HSB
        {
            public double[] Data { get; } // H:0.00-3.60, S,B:0.0-1.0
            public int Sepia { get; }

            public ImageData8HSB(double[] data, int value)
            {
                Data = data;
                Sepia = value;
            }
        }
    }

    public static class SepiaExt
    {
        public static List<ImageData8> Reduce(this List<ImageData32> digits) => digits.Select(_ => _.Reduce()).ToList();
        public static List<ImageData8HSB> ToHSB(this List<ImageData8> digits) => digits.Select(_ => _.ToHSB()).ToList();
        public static List<(double[] x, double[] t)> ToData(this List<ImageData8> values)
            => values.Select(_ => (_.Data.Select(x => (double)x).ToArray(), new double[] { _.Sepia==0?1:0, _.Sepia==1?1:0 }))
            .ToList();

        public static List<(double[] x, double[] t)> ToData(this List<ImageData8HSB> values)
            => values.Select(_ => (_.Data.ToArray().Select((x,i)=>(x,i)).Where(w=>w.i%3==0).Select(w=>w.x).ToArray(), new double[] { _.Sepia == 0 ? 1 : 0, _.Sepia == 1 ? 1 : 0 }))
            //=> values.Select(_ => (_.Data.ToArray(), new double[] { _.Sepia == 0 ? 1 : 0, _.Sepia == 1 ? 1 : 0 }))
            .ToList();

    }
}
