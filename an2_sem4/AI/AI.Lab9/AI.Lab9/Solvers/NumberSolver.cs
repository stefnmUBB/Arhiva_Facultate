using AI.Lab9.Algebra;
using AI.Lab9.Tools;
using AI.Lab9.Tools.ANN;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using static AI.Lab9.Solvers.NumberSolver;

namespace AI.Lab9.Solvers
{
    public class NumberSolver
    {
        public List<RawDigit> LoadData(string fname)
        {
            var rawDigits = File.ReadAllLines(fname)
                .Skip(21)
                .Select((l, i) => (l, i))
                .GroupBy(_ => _.i / 33)
                .Select(_ => _.Select(li => li.l).ToList())
                .Where(_ => _.Count == 33)
                .Select(l => (
                    data: l.Take(32).JoinToString("").Where(c => c == '0' || c == '1').Select(c => c == '0' ? (byte)0 : (byte)1).ToArray(),
                    value: int.Parse(Regex.Replace(l.Last(), @"\s+", ""))))
                .Select(_ => new RawDigit(_.data, _.value))
                .ToList();
            return rawDigits;
        }

        public double[][] TrainInputData { get; private set; }
        public double[][] TrainOutputData { get; private set; }
        public double[][] TestInputData { get; private set; }
        public double[][] TestOutputData { get; private set; }

        public NeuralNetwork NeuralNetwork { get; } = new NeuralNetwork()
        {
            IterationsCount = 100,
            LearningRate = 0.11504956607942,
        }
                .AddLayer(Neurons.Input, 64)
                .AddLayer(Neurons.Hidden(ActivationFunction.Linear(0.011300861375081, -0.0100627213297704)), 6)
                .AddLayer(Neurons.Output(ActivationFunction.Linear(0.76314877614526, 0.074567726382319)), 10)                
                .Create();
        /*.AddLayer(Neurons.Input, 64)
        .AddLayer(Neurons.Hidden(ActivationFunction.Linear(0.574567726382319, 1.26314877614526)), 6)
        .AddLayer(Neurons.Output(ActivationFunction.Linear(-0.0100627213297704, 0.511300861375081)), 10)
        .Create();*/

        public void Solve(string fname)
        {
            var data = LoadData(fname).Reduce().ToData().Shuffle().SplitTrainTest(0.8);
            TrainInputData = data.Train.Select(_ => _.x).ToArray();
            TrainOutputData = data.Train.Select(_ => _.t).ToArray();
            TestInputData = data.Test.Select(_ => _.x).ToArray();
            TestOutputData = data.Test.Select(_ => _.t).ToArray();


            /*Action f = () =>
            {
                var _data = LoadData(fname).Reduce().ToData().Shuffle().Take(330).SplitTrainTest(0.8);
                var finder = new ANNFinder(_data.Train, _data.Test, 0);
                
                finder.Rafinate();
                Console.WriteLine("OK");
                //finder.OneIteration();
                Thread.Sleep(50000);
            };

            var qq=Task.Run(f);
            qq.Wait();*/

            /*Action f50 = () =>
            {
                var _data = LoadData(fname).Reduce().ToData().Shuffle().Take(550).SplitTrainTest(0.8);
                var finder = new ANNFinder(_data.Train, _data.Test, 50);

                for (int i = 0; i < 10000; i++)
                {
                    Console.WriteLine($"[50] Iteration {i}");
                    finder.OneIteration();
                    Thread.Sleep(50);
                }
            };

            Action f00 = () =>
            {
                var _data = LoadData(fname).Reduce().ToData().Shuffle().Take(550).SplitTrainTest(0.8);
                var finder = new ANNFinder(_data.Train, _data.Test);

                for (int i = 0; i < 10000; i++)
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
            var tk3 = Task.Run(f00);
            Thread.Sleep(500);
            var tk4 = Task.Run(f50);

            tk1.Wait();
            tk2.Wait();
            tk3.Wait();
            tk4.Wait();      */

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
                    if(min<0)
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

        public class RawDigit
        {
            public byte[] Data { get; }
            public int Value { get; }

            public RawDigit(byte[] data, int value)
            {
                Data = data;
                Value = value;
            }

            public ReducedDigit Reduce()
            {
                var data = new byte[8 * 8];

                for (int ty = 0; ty < 8; ty++) 
                {
                    for (int tx = 0; tx < 8; tx++)
                    {
                        byte count = 0;
                        for (int y = 0; y < 4; y++) 
                        {
                            for (int x = 0; x < 4; x++) 
                            {
                                if (Data[32 * (4 * ty + y) + (4 * tx + x)] != 0) 
                                    count++;
                            }
                        }
                        data[ty * 8 + tx] = count;
                    }
                }

                return new ReducedDigit(data, Value);
            }
        }

        public class ReducedDigit
        {
            public byte[] Data { get; }
            public int Value { get; }

            public ReducedDigit(byte[] data, int value)
            {
                Data = data;
                Value = value;
            }
        }        
    }

    public static class NumberExt
    {
        public static List<ReducedDigit> Reduce(this List<RawDigit> digits) => digits.Select(_ => _.Reduce()).ToList();
        public static List<(double[] x, double[] t)> ToData(this List<ReducedDigit> digits)
            => digits.Select(_ => (_.Data.Select(x => (double)x).ToArray(),
            (new Func<int, double[]>(i =>
            {
                var r = new double[10];
                r[i] = 1;
                return r;
            }))(_.Value)))
            .ToList();

    }
}
