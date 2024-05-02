using Licenta.Commons.Math;
using Licenta.Commons.Parallelization;
using Licenta.Commons.AI.Perceptrons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Threading;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Licenta.Commons.AI
{
    public class RuntimeAnn
    {
        private AnnModel Model;

        private readonly Perceptron[][] Layers;
        private readonly Matrix<double>[] Weights;
        private readonly Input[] InputLayer;
        private readonly Output[] OutputLayer;

        private readonly double[][] PerceptronOutputs;

        public List<double[]> ErrW = new List<double[]>();        
        public List<Matrix<double>> DeltaWeights = new List<Matrix<double>>();        

        private RuntimeAnn(Perceptron[][] perceptronLayers, Matrix<double>[] weights)
        {
            Layers = perceptronLayers;
            Weights = weights;
            InputLayer = perceptronLayers.First().Cast<Input>().ToArray();
            OutputLayer = perceptronLayers.Last().Cast<Output>().ToArray();

            PerceptronOutputs = new double[Layers.Length][];
            for (int i = 0; i < Layers.Length; i++)
            {
                PerceptronOutputs[i] = new double[Layers[i].Length];
                ErrW.Add(new double[Layers[i].Length]);                
            }

            for (int i = 0; i < weights.Length; i++)
                DeltaWeights.Add(new Matrix<double>(weights[i].RowsCount, weights[i].ColumnsCount));
        }        

        private double XW(int layer, int nodeId)
        {
            if (layer == 0) return 0;            
            double result = 0;
            var weights = Weights[layer - 1].Items;
            var len = Weights[layer - 1].ColumnsCount;
            var rowIndex = len * nodeId;
            var outputs = PerceptronOutputs[layer - 1];
            for (int i = 0; i < len; i++) result += weights[rowIndex + i] * outputs[i];
            return result;                        
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ComputeValueOf(int i, int j)
        {
            PerceptronOutputs[i][j] = Layers[i][j].Activate(XW(i, j));
        }

        private void PropagateInformation()
        {
            //ParallelForLoop.RunPartitioned(0, InputLayer.Length, j => PerceptronOutputs[0][j] = InputLayer[j].Value);
            for (int j = 0; j < InputLayer.Length; j++) PerceptronOutputs[0][j] = InputLayer[j].Value;

            for(int i=1;i<Layers.Length;i++)
            {                
                //ParallelForLoop.RunPartitioned(0, Layers[i].Length, j => ComputeValueOf(i, j));
                for (int j = 0; j < Layers[i].Length; j++) ComputeValueOf(i, j);
            }                        
        }

        private void PropagateInformation(double[] x)
        {
            SetInput(x);
            PropagateInformation();
        }

        private void PropagateError(double[] x, double[] t, double learningRate)
        {
            for (int r = 0; r < OutputLayer.Length; r++) 
            {
                var d = OutputLayer[r].Derivative(XW(Layers.Length - 1, r));
                var del = d * (t[r] - PerceptronOutputs[PerceptronOutputs.Length - 1][r]);
                ErrW[Layers.Length - 1][r] = del;
                //Console.WriteLine($"Err={del}");
            }

            for (int thisLayerId = Layers.Length - 2; thisLayerId >= 0; thisLayerId--)
            {
                var nextLayerId = thisLayerId + 1;
                var thisLayer = Layers[thisLayerId];
                var nextLayer = Layers[nextLayerId];

                var w = Weights[thisLayerId];

                for (int h1 = 0; h1 < thisLayer.Length; h1++)
                {
                    var d = thisLayer[h1].Derivative(XW(thisLayerId, h1));

                    ErrW[thisLayerId][h1] = 0;

                    for (int h2 = 0; h2 < nextLayer.Length; h2++) 
                        ErrW[thisLayerId][h1] += d * w[h2, h1] * ErrW[nextLayerId][h2];
                    //ErrW[thisLayerId][h1] = ParallelForLoop.Sum(0, nextLayer.Length, h2 => d * w[h2, h1] * ErrW[nextLayerId][h2]);

                    for (int h2 = 0; h2 < nextLayer.Length; h2++)
                        w[h2, h1] += learningRate * ErrW[nextLayerId][h2] * PerceptronOutputs[thisLayerId][h1];
                    //ParallelForLoop.RunPartitioned(0, nextLayer.Length, h2 => w[h2, h1] += learningRate * ErrW[nextLayerId][h2] * PerceptronOutputs[thisLayerId][h1]);
                }
            }
        }

        private void PropagateErrorBatch(double[] x, double[] t, double learningRate)
        {
            for (int r = 0; r < OutputLayer.Length; r++)
            {
                var d = OutputLayer[r].Derivative(XW(Layers.Length - 1, r));
                var del = d * (t[r] - PerceptronOutputs[PerceptronOutputs.Length-1][r]);
                ErrW[Layers.Length - 1][r] = del;
            }

            for (int thisLayerId = Layers.Length - 2; thisLayerId >= 0; thisLayerId--)
            {
                var nextLayerId = thisLayerId + 1;
                var thisLayer = Layers[thisLayerId];
                var nextLayer = Layers[nextLayerId];

                var w = DeltaWeights[thisLayerId];

                for (int h1 = 0; h1 < thisLayer.Length; h1++)
                {
                    var d = thisLayer[h1].Derivative(XW(thisLayerId, h1));
                    ErrW[thisLayerId][h1] = 0;
                    for (int h2 = 0; h2 < nextLayer.Length; h2++)
                    {
                        ErrW[thisLayerId][h1] += d * w[h2, h1] * ErrW[nextLayerId][h2];
                    }

                    for (int h2 = 0; h2 < nextLayer.Length; h2++)
                    {                        
                        w[h2, h1] += learningRate * ErrW[nextLayerId][h2] * PerceptronOutputs[thisLayerId][h1];
                    }
                }
            }
        }

        private void Process(double[] x, double[] t, bool batch, double learningRate)
        {
            //Debug.WriteLine($"[ANN] PropagateInformation");
            PropagateInformation(x);
            //Debug.WriteLine($"[ANN] PropagateError");
            if (!batch)
                PropagateError(x, t, learningRate: learningRate);
            else
                PropagateErrorBatch(x, t, learningRate: learningRate);
        }

        public History Train(IEnumerable<(double[] x, double[] t)> data, int batchSize = 0, int epochsCount = 200,
            Func<double[], double[], double> lossFunction = null, double learningRate=0.001)
        {
            var history = new History();

            lossFunction = lossFunction ?? LossFunctions.MeanSquaredError;
            var prevLoss = double.PositiveInfinity;
            if (batchSize == 0)
            {
                for (int i = 0; i < epochsCount; i++)
                {
                    Debug.WriteLine($"[ANN] Epoch {i}");                    
                    Debug.WriteLine($"[ANN] Processing {i}");
                    int k = 0;
                    foreach (var (x, t) in data)
                    {
                        if ((++k) % 100 == 0) Console.Title = $"[ANN] Items Cnt {k}";
                        Process(x, t, batch: false, learningRate);
                        
                    }
                    Debug.WriteLine($"[ANN] Testing {i}");
                    double loss = 0;                    
                    foreach (var (x, t) in data)
                    {
                        var o = PredictSingleInternal(x);
                        var l = lossFunction(t, o);
                        Debug.WriteLine(l);
                        loss += l;
                    }
                    history.Loss.Add(loss);
                    if (System.Math.Abs(prevLoss - loss) < 0.00001) 
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[ANN] No Loss Delta");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                    prevLoss = loss;                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"[ANN] End Epoch {i}: Loss = {loss}");
                    Console.ForegroundColor = ConsoleColor.White;

                    if (loss < 0.1) break; // satisfying

                    if (double.IsNaN(loss))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"[ANN] NaN");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"[ANN] Done Training");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                var batches = data.Select((d, i) => (data: d, batchId: i % batchSize))
                    .GroupBy(_ => _.batchId)
                    .Select(_ => _.Select(v => v.data).ToList())
                    .ToList();

                for (int i = 0; i < epochsCount; i++)
                {
                    Debug.WriteLine($"[ANN] Epoch {i}");
                    foreach (var batch in batches)
                    {
                        foreach (var (x, t) in batch)
                        {
                            Process(x, t, batch: true, learningRate);
                        }                        
                        CommitDeltaWeights();


                        double loss = 0;
                        foreach (var (x, t) in data)
                        {
                            var o = PredictSingleInternal(x);
                            loss += lossFunction(t, o);
                        }
                        history.Loss.Add(loss);
                        if (System.Math.Abs(prevLoss - loss) < 0.00001)
                            break;
                        prevLoss = loss;

                        if (double.IsNaN(loss)) break;
                    }
                }

            }
            return history;
        }

        public History Train(IEnumerable<double[]> _inputs, IEnumerable<double[]> _outputs, int batchSize = 0, int epochsCount = 200,
            Func<double[], double[], double> lossFunction = null, double learningRate = 0.001)
        {
            var inputs = _inputs.ToArray();
            var outputs = _outputs.ToArray();
            var data = inputs.Zip(outputs, (i, o) => (x: i, t: o));
            return Train(data, batchSize:batchSize, epochsCount: epochsCount, lossFunction:lossFunction, learningRate:learningRate);
        }

        public double[] PredictSingle(double[] input) => PredictSingleInternal(input).ToArray();        

        private double[] PredictSingleInternal(double[] input)
        {
            PropagateInformation(input);
            return PerceptronOutputs[PerceptronOutputs.Length-1];
        }

        private void CommitDeltaWeights()
        {
            for (int k = 0; k < Weights.Length; k++)
            {
                var w = Weights[k];
                var d = DeltaWeights[k];
                for (int i = 0; i < w.RowsCount; i++)
                {                    
                    for (int j = 0; j < w.ColumnsCount; j++) 
                    {
                        w[i, j] += d[i, j];
                        d[i, j] = 0;
                    }
                }
            }

        }

        public static RuntimeAnn CompileFromModel(AnnModel model)
        {
            var layers = new List<Perceptron[]>();
            var weights = new List<Matrix<double>>();

            layers.Add(new AnnLayerModel(typeof(Input), model.InputLength).Compile());
            var prevDim = model.InputLength;

            var nextLayers = model.HiddenLayers.ToList();
            nextLayers.Add(new AnnLayerModel(typeof(Output), model.OutputLength));

            var rand = new Random();
            foreach (var layer in nextLayers) 
            {
                var m = new Matrix<double>(layer.PerceptronsCount, prevDim);
                m = Matrices.DoEachItem(m, x => rand.NextDouble());
                weights.Add(m);                

                layers.Add(layer.Compile());
                prevDim = layer.PerceptronsCount;
            }

            return new RuntimeAnn(layers.ToArray(), weights.ToArray()) { Model = model.Clone() };
        }       


        public void SetInput(double[] input)
        {
            if (input.Length != InputLayer.Length)
                throw new ArgumentException("Invalid input length");

            for (int i = 0; i < input.Length; i++)
                InputLayer[i].Value = input[i];
        }

        public double[] ComputeOutput()
        {
            var values = new Matrix<double>(InputLayer.Length, 1, InputLayer.Select(_ => _.Value).ToArray());
            for (int i = 1; i < Layers.Length; i++)
            {
                values = Matrices.Multiply(Weights[i - 1], values);
            }
            return values.Items.ToArray();
        }

        public double[] ComputeOutput(double[] input)
        {
            SetInput(input);
            return ComputeOutput();
        }       

        public SavedStateAnn ToSavedState()
        {
            return new SavedStateAnn(Model.Clone(), Weights.Select(_ => new Matrix<double>(_)).ToList());
        }

        public static RuntimeAnn LoadState(SavedStateAnn state)
        {
            var ann = CompileFromModel(state.Model);

            if (state.Weights.Count != ann.Weights.Length)
                throw new InvalidOperationException("Error loading state: wrong number of weight matrices");

            for (int i = 0; i < ann.Weights.Length; i++)
            {
                var src = state.Weights[i];
                var dst = ann.Weights[i];
                if (!Matrices.HaveSameShape(src, dst))
                    throw new InvalidOperationException("Error loading state: wrong weights matrix dimensions relative to model");
                ann.Weights[i] = new Matrix<double>(src);
            }
            return ann;
        }

        public void Save(string filename)
        {
            using(var f=new FileStream(filename, FileMode.Create))
            {
                using(var bw=new BinaryWriter(f))
                {
                    ToSavedState().WriteToBinaryWriter(bw);
                }
            }
        }

        public static RuntimeAnn Load(string filename)
        {
            using(var f=new FileStream(filename, FileMode.Open))
            {
                using (var br = new BinaryReader(f))
                    return LoadState(SavedStateAnn.ReadFromBinaryReader(br));
            }
        }
    }    
}
