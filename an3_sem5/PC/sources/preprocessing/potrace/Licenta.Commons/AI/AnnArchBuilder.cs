using Licenta.Commons.AI.Perceptrons;
using Licenta.Commons.Utils;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Licenta.Commons.AI
{
    public class AnnArchBuilder
    {
        public List<Type> PerceptronTypes = new List<Type>();

        public Func<double[], double[], double> Loss = null;

        public AnnArchBuilder()
        {
            PerceptronTypes = Reflection.AppTypes
                .Where(typeof(Perceptron).IsAssignableFrom)
                .Where(t => t.GetCustomAttribute<HiddenLayerAttribute>() != null)
                .ToList();
        }

        struct Result
        {
            public AnnModel Model;
            public SavedStateAnn BestState;
            public double Loss;

            public Result(AnnModel model)
            {
                Model = model;
                BestState = null;
                Loss = double.PositiveInfinity;
            }
        }

        private AnnModel[] GenerateModels(AnnModel sourceModel, int count)
        {
            var r = new Random();
            Console.WriteLine("Generating models");
            var results = new AnnModel[count];
            results[0] = sourceModel;
            for (int i = 1; i < count; i++)
            {
                if(i%100==0) Console.WriteLine($"{i}/{count}");
                results[i] = LocalChange(results[r.Next(i)]);
            }
            return results;
        }


        public double LearningRate { get; set; } = 0.01;

        public void Optimize(double[][] inputs, double[][] outputs, Action<SavedStateAnn, int, double> callback = null, int attemptsCount = 2000,                        
            List<AnnLayerModel> layers = null)
        {            
            var inputLength = inputs[0].Length;
            var outputLength = outputs[0].Length;
            
            if (inputs.Length != outputs.Length)
                throw new ArgumentException("Inputs and outputs count mismatch");

            if (inputs.Any(_ => _.Length != inputLength))
                throw new ArgumentException("Not all inputs have the same dimensions");
            if (outputs.Any(_ => _.Length != outputLength))
                throw new ArgumentException("Not all outputs have the same dimensions");

            var data = inputs.Zip(outputs, (i, o) => (i, o)).ToArray();

            var model = new AnnModel { InputLength = inputLength, OutputLength = outputLength };
            if (layers != null) model.HiddenLayers.AddRange(layers);

            Console.WriteLine("Compiling models");
            var anns = GenerateModels(model, attemptsCount).Select((_,i) =>
            {
                if (i % 10 == 0) Console.WriteLine($"{i}/{attemptsCount}");                
                return _.Compile();
            }).ToArray();

            double lastLoss = double.PositiveInfinity;
            var steps = (int)System.Math.Log(data.Length);
            Console.WriteLine($"Steps = {steps}");

            var len = data.Length;

            for (int i = 0; i < steps; i++) 
            {
                ConcurrentQueue<RuntimeAnn> r = new ConcurrentQueue<RuntimeAnn>();
                ConcurrentDictionary<RuntimeAnn, double> losses = new ConcurrentDictionary<RuntimeAnn, double>();

                var currentLen = System.Math.Min(len, (int)System.Math.Exp(i + 1));
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Training {anns.Length} networks on {currentLen} samples.");
                Console.ForegroundColor = ConsoleColor.White;

                var b = data.Select((_, q) => (l:_, q: q % 5)).GroupBy(_ => _.q).Select(_ => _.Select(p=>p.l).ToArray()).ToArray();

                for (int k = 0; k < 5; k++)
                {
                    var q = b.Select((_, p) => (l: _, p)).Where(_ => _.p != k).SelectMany(_ => _.l).ToArray();
                    Console.WriteLine(q.Length);
                    var ann = model.Compile();
                    var history = ann.Train(q, lossFunction: Loss, learningRate: LearningRate, epochsCount: 2000);
                    callback(ann.ToSavedState(), 100+k, history.Loss.Last());
                }

                return;

                Action train(int s, int e) => () =>
                {
                    for (int k = s; k < e; k++)
                    {                        
                        var ann = anns[k];

                        Console.WriteLine($"Trying {k}\n" + ann.ToSavedState().Model.ToString());

                        var history = ann.Train(data.OrderBy(_ => Guid.NewGuid()).Take(currentLen).ToArray(), lossFunction: Loss, learningRate: LearningRate, epochsCount:2000);
                        var loss = history.Loss.Count > 0 ? history.Loss.Min() : double.PositiveInfinity;
                        if (!double.IsNaN(loss) && loss <= lastLoss)
                        {
                            r.Enqueue(ann);
                            losses.GetOrAdd(ann, loss);
                            callback(ann.ToSavedState(), i, loss);
                        }
                    }
                };

                train(0, anns.Length)();

                /*var taskCnt = 3;
                var tasks = Enumerable.Range(0, taskCnt)
                    .Select(_ => Task.Run(train(_ * anns.Length / taskCnt, (_ + 1) * anns.Length / taskCnt))).ToArray();
                tasks.ForEach(_ => _.Wait());*/
               
                anns = r.ToArray();

                var _losses = losses.ToArray().ToDictionary(_ => _.Key, _ => _.Value);



                anns = anns.Select(a => (a, l: _losses[a])).OrderBy(_ => _.l).Take(System.Math.Max(1, anns.Length - 1))
                    .Select(_ => _.a).ToArray();

                if(anns.Length==0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"No ANN Selected!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Best Loss = {_losses.Values.Min()} for iteration {i}/{steps}");
                Console.ForegroundColor = ConsoleColor.White;
            }                                  
        }

        class ResultComparer : IComparer<Result>
        {
            public int Compare(Result x, Result y) => x.Loss.CompareTo(y.Loss);
        }

        private Result Train(RuntimeAnn ann, double[][] inputs, double[][] outputs)
        {
            Result result = new Result(ann.ToSavedState().Model);                                
            Debug.WriteLine($"Training...");
            var history = ann.Train(inputs, outputs, lossFunction: Loss);
            var loss = history.Loss.Count > 0 ? history.Loss.Min() : double.PositiveInfinity;
            result.BestState = ann.ToSavedState();
            result.Loss = loss;            
            Debug.WriteLine($"Done...");            
            return result;
        }

        Random Random = new Random();

        public AnnModel LocalChange(AnnModel model)
        {
            var p = Random.NextDouble();
            var newModel = model.Clone();

            if (model.HiddenLayers.Count == 0) 
            {
                newModel.HiddenLayers.Add(GenerateRandomLayer((model.InputLength + model.OutputLength) / 2));
            }
            else
            {
                var layerIndex = Random.Next(model.HiddenLayers.Count);
                if (p < 0.5)
                {
                    var d = 1 + (Random.NextDouble() / 2 - 0.5);
                    newModel.HiddenLayers[layerIndex].PerceptronsCount = (int)(d * newModel.HiddenLayers[layerIndex].PerceptronsCount);
                }
                else if (p < 0.75)
                {
                    if (newModel.HiddenLayers.Count < 30)
                        newModel.HiddenLayers.Add(GenerateRandomLayer((model.HiddenLayers.Last().PerceptronsCount + model.OutputLength) / 2));
                }
                else if (p < 0.85)
                {
                    var otherLayerIndex = Random.Next(model.HiddenLayers.Count);
                    (newModel.HiddenLayers[layerIndex], newModel.HiddenLayers[otherLayerIndex]) = (newModel.HiddenLayers[otherLayerIndex], newModel.HiddenLayers[layerIndex]);
                }
                else if (p < 0.93) 
                {
                    for (int k = 0; newModel.HiddenLayers[layerIndex].PerceptronsCount > 0 && k < 10; k++)
                        layerIndex = Random.Next(model.HiddenLayers.Count);
                    if (newModel.HiddenLayers[layerIndex].PerceptronsCount > 1)
                    {
                        var d = Random.NextDouble() / 2;
                        newModel.HiddenLayers[layerIndex].PerceptronsCount = System.Math.Max((int)(d * newModel.HiddenLayers[layerIndex].PerceptronsCount), 1);
                    }
                }
                else if (p < 0.97) 
                {
                    newModel.HiddenLayers.RemoveAt(layerIndex);
                }
                else
                {
                    var layer = newModel.HiddenLayers[layerIndex];
                    var param = layer.Parameters.Keys.OrderBy(_ => _).FirstOrDefault();
                    if(param!=null)
                    {
                        layer.Parameters[param] += (Random.NextDouble() - 0.5) * 0.05;
                    }
                }
            }
            return newModel;
        }        
        

        private AnnLayerModel GenerateRandomLayer(int nodesCount)
        {
            var type = PerceptronTypes.OrderBy(_ => Guid.NewGuid()).First();
            var layer = new AnnLayerModel(type, nodesCount);
            return layer;
        }
    }
}
