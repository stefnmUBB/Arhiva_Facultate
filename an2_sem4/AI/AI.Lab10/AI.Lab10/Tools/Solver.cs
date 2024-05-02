using AI.Lab10.Algebra;
using AI.Lab10.Tools.ANN;
using AI.Lab10.Tools.Text;
using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools
{
    internal class Solver
    {
        public double[][] TrainInputData { get; }
        public double[][] TrainOutputData { get; }
        public double[][] TestInputData { get; }
        public double[][] TestOutputData { get; }

        public NeuralNetwork NeuralNetwork { get; } = new NeuralNetwork()
                .AddLayer(Neurons.Input, 4)
                .AddLayer(Neurons.Hidden(ActivationFunction.Linear(0.001, 0)), 2)
                .AddLayer(Neurons.Hidden(ActivationFunction.Self), 2)
                .AddLayer(Neurons.Hidden(ActivationFunction.ReLU), 3)
                .Create();

        public Solver()
        {
            var data = new CsvData(@"Input\spam.csv")
                .Select(_ => (text: _.Get<string>(0), type: _.Get<string>(1))).ToArray();

            Console.WriteLine(data);
            var tt = data.ToArray().SplitTrainTest(0.8, 17);

            var train = tt.Train;
            var test = tt.Test;

            var dict = train.Select(_ => _.text).ToArray().GetBagOfWords();

            TrainInputData = train.Select(_ => _.text).ToArray().GetBowVectors(dict);
            TrainOutputData = train.Select(_ => new double[] { _.type == "spam" ? 1 : 0 }).ToArray();

            TestInputData = train.Select(_ => _.text).ToArray().GetBowVectors(dict);
            TestOutputData = train.Select(_ =>
            new double[] { _.type == "spam" ? 0 : 1, _.type == "spam" ? 1 : 0 }).ToArray();          


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
                    var sum = results.Sum();
                    if (sum != 0)
                    {
                        results[0] /= sum;
                        results[1] /= sum;
                        results[2] /= sum;
                    }
                }

                return results.Concat(_.o).Concat(new double[] { _.o[maxIndex] == 1 ? 1 : 0 }).ToArray();
            }).ToMatrix();

            return res;
        }
    }
}
