using AI.Lab9.Algebra;
using AI.Lab9.Data;
using AI.Lab9.Tools.ANN;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab9.Solvers
{
    internal class IrisSolver
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

        public IrisSolver()
        {
            var features = new string[] { "SepalLength", "SepalWidth", "PetalLength", "PetalWidth" };
            var csv = new CsvData(@"Input\iris.data", features.Append("Class").ToArray());
            var data = csv.ToObjects<Flower>().ToList().Shuffle().SplitTrainTest();

            var labelToInt = new Dictionary<string, double>
            {
                { "Iris-setosa", 0 },
                { "Iris-versicolor", 1 },
                { "Iris-virginica", 2 }
            };
            var labels = labelToInt.Keys.ToArray();

            var trainData = data.Train;
            var testData = data.Test;

            TrainInputData = trainData.Select(f => new double[] { f.SepalWidth, f.PetalWidth, f.SepalLength, f.PetalLength }).ToArray();
            TrainOutputData = trainData.Select(f => new double[]
            {
                labelToInt[f.Class] == 0 ? 1:0,
                labelToInt[f.Class] == 1 ? 1:0,
                labelToInt[f.Class] == 2 ? 1:0,
            }).ToArray();

            TestInputData = testData.Select(f => new double[] { f.SepalWidth, f.PetalWidth, f.SepalLength, f.PetalLength }).ToArray();
            TestOutputData = testData.Select(f => new double[] {
                labelToInt[f.Class] == 0 ? 1:0,
                labelToInt[f.Class] == 1 ? 1:0,
                labelToInt[f.Class] == 2 ? 1:0,
            }).ToArray();            


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
