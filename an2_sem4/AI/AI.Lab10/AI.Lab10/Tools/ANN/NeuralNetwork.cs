using AI.Lab10.Algebra;
using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace AI.Lab10.Tools.ANN
{
    public class NeuralNetwork
    {
        bool Inited = false;
        public List<List<Neuron>> Layers { get; } = new List<List<Neuron>>();

        public NeuralNetwork AddLayer(Func<Neuron> neuronGenerator, int count)
        {
            Layers.Add(Enumerable.Range(0, count).Select(_ => neuronGenerator()).ToList());            
            return this;
        }

        List<Matrix> Weights = new List<Matrix>();
        List<Matrix> DeltaWeights = new List<Matrix>();        
        public List<double[]> ErrW = new List<double[]>();

        public NeuralNetwork Create()
        {
            Assert(Layers.Count > 2);

            Weights = new List<Matrix>();
            DeltaWeights = new List<Matrix>();

            for(int i=0;i<Layers.Count-1;i++)
            {
                Weights.Add(new Matrix(Layers[i].Count, Layers[i + 1].Count));
                DeltaWeights.Add(new Matrix(Layers[i].Count, Layers[i + 1].Count));
                for (int y = 0; y < Weights[i].RowsCount;y++)
                {                                        
                    for (int x = 0; x < Weights[i].ColsCount;x++)
                    {
                        Weights[i][y, x] = Rand.NextDouble();
                    }
                }
            }

            ErrW = new List<double[]>();

            for (int i = 0; i < Layers.Count; i++)
                ErrW.Add(new double[Layers[i].Count]);

            Inited = true;
            return this;
        }

        private static void Assert(bool condition)
        {
            if (!condition) throw new AssertionFailedException("Assertion failed");
        }

        public List<Neuron> InputLayer => Layers[0].ToList();
        public List<Neuron> OutputLayer => Layers.Last().ToList();

        private double XW(int layer, int nodeId)
        {
            if (layer == 0) return 0;
            return Weights[layer - 1].GetColumn(nodeId).DotProduct(Layers[layer - 1].Select(_ => _.Value));
        }

        private void ComputeValueOf(int i, int j)
        {            
            Layers[i][j].Value = Layers[i][j].Activate.Function(XW(i, j));
            if(double.IsNaN(Layers[i][j].Value))
            {
                throw new NeuralNetworkNaNException();
            }
        }

        private void PropagateInformation()
        {
            for (int i = 1; i < Layers.Count; i++) 
            {
                for (int j = 0; j < Layers[i].Count; j++)
                    ComputeValueOf(i, j);
            }
        }

        public double LearningRate { get; set; } = 0.1;

        private void PropagateInformation(double[] x)
        {
            for (int i = 0; i < InputLayer.Count; i++)
            {
                InputLayer[i].Activate = ActivationFunction.Constant(x[i]);
                InputLayer[i].Value = x[i];
            }
            PropagateInformation();
        }

        private void PropagateError(double[] x, double[] t)
        {
            for (int r = 0; r < OutputLayer.Count; r++)
            {
                var d = OutputLayer[r].Activate.Derivative(XW(Layers.Count - 1, r));
                var del = d * (t[r] - OutputLayer[r].Value);                
                ErrW[Layers.Count - 1][r] = del;
                //Console.WriteLine($"Err={del}");
            }

            for (int thisLayerId = Layers.Count - 2; thisLayerId >= 0; thisLayerId--) 
            {                
                var nextLayerId = thisLayerId + 1;
                var thisLayer = Layers[thisLayerId];
                var nextLayer = Layers[nextLayerId];

                var w = Weights[thisLayerId];           

                for (int h1 = 0; h1 < thisLayer.Count; h1++) 
                {
                    var d = thisLayer[h1].Activate.Derivative(XW(thisLayerId, h1));
                    //ErrW[thisLayerId][h1] = d * w.GetRow(h1).ToArray().DotProduct(ErrW[nextLayerId]);
                    ErrW[thisLayerId][h1] = 0;
                    for (int h2 = 0; h2 < nextLayer.Count; h2++)
                    {
                        ErrW[thisLayerId][h1] += d * w[h1, h2] * ErrW[nextLayerId][h2];
                    }

                    for (int h2 = 0; h2 < nextLayer.Count; h2++) 
                    {
                        w[h1, h2] += LearningRate * ErrW[nextLayerId][h2] * thisLayer[h1].Value;
                    }
                }                
            }          
        }

        private void CommitDeltaWeights()
        {
            for(int k=0;k<Weights.Count;k++)
            {
                var w = Weights[k];
                var d = DeltaWeights[k];
                for (int i = 0; i < w.RowsCount; i++)
                {
                    for (int j = 0; j < w.ColsCount; j++)
                    {
                        w[i, j] += d[i, j];
                        d[i, j] = 0;
                    }
                }
            }

        }

        private void PropagateErrorBatch(double[] x, double[] t)
        {
            for (int r = 0; r < OutputLayer.Count; r++)
            {
                var d = OutputLayer[r].Activate.Derivative(XW(Layers.Count - 1, r));
                var del = d * (t[r] - OutputLayer[r].Value);
                ErrW[Layers.Count - 1][r] = del;                
            }

            for (int thisLayerId = Layers.Count - 2; thisLayerId >= 0; thisLayerId--)
            {
                var nextLayerId = thisLayerId + 1;
                var thisLayer = Layers[thisLayerId];
                var nextLayer = Layers[nextLayerId];

                var w = DeltaWeights[thisLayerId];

                for (int h1 = 0; h1 < thisLayer.Count; h1++)
                {
                    var d = thisLayer[h1].Activate.Derivative(XW(thisLayerId, h1));                    
                    ErrW[thisLayerId][h1] = 0;
                    for (int h2 = 0; h2 < nextLayer.Count; h2++)
                    {
                        ErrW[thisLayerId][h1] += d * w[h1, h2] * ErrW[nextLayerId][h2];
                    }

                    for (int h2 = 0; h2 < nextLayer.Count; h2++)
                    {
                        //Console.WriteLine($"This : {thisLayer[h1].Value}");

                        w[h1, h2] += LearningRate * ErrW[nextLayerId][h2] * thisLayer[h1].Value;
                    }
                }
            }
        }

        private void Process(double[] x, double[] t, bool batch = false)
        {
            PropagateInformation(x);
            if (!batch)
                PropagateError(x, t);
            else
                PropagateErrorBatch(x, t);
        }

        public void DumpWeights(string filename)
        {
            using(var ms=new MemoryStream())
            {
                using(var w=new StreamWriter(ms))
                {
                    w.WriteLine("---------------------------------------");
                    for (int i = 0; i < Weights.Count; i++)
                    {
                        w.WriteLine($"__W{i}_____");
                        w.WriteLine(Weights[i]);
                    }
                }
                File.WriteAllBytes(filename, ms.ToArray());
            }                        
        }

        public void DumpWeights()
        {
            Console.WriteLine("---------------------------------------");
            for (int i=0;i<Weights.Count;i++)
            {
                Console.WriteLine($"__W{i}_____");
                Console.WriteLine(Weights[i]);
            }            
        }

        public void DumpValues()
        {
            Console.WriteLine("VALUE-----------------------------------");
            for (int i = 0; i < Layers.Count; i++)
            {
                Console.WriteLine(Layers[i].Select(_ => _.Value).Select(_ => _.ToString("F4")).JoinToString(", "));
            }
        }

        public int BatchSize { get; set; } = 0;

        public int IterationsCount { get; set; } = 100;


        List<Matrix> BackupWeights = new List<Matrix>();

        public void MakeBackup()
        {
            BackupWeights.Clear();
            foreach(var w in Weights)
            {
                BackupWeights.Add(w.Clone());
            }
        }

        public void RestoreBackup()
        {
            Weights.Clear();
            foreach(var w in BackupWeights)
            {
                Weights.Add(w.Clone());
            }
        }

        public Func<double[][], double[][], double> Loss { get; set; } = Losses.LogLoss;

        public void Train(IEnumerable<(double[] x, double[] t)> data, Action<double> lossReport = null)
        {
            if (BatchSize == 0)
            {
                MakeBackup();
                var prevLoss = double.PositiveInfinity;                
                int abat = 0;
                for (int i = 0; i < IterationsCount; i++) 
                {
                    //Console.WriteLine(i);
                    foreach (var (x, t) in data)
                    {
                        try
                        {
                            Process(x, t);
                        }
                        catch(NeuralNetworkNaNException)
                        {
                            break;
                        }
                    }
                    Thread.Sleep(10);

                    var pred = data.Select(p => PredictSingle(p.x)).ToArray();
                    var real = data.Select(p => p.t).ToArray();
                    var loss = Loss(real, pred);
                    Console.WriteLine($"{i} = {loss}");
                    lossReport?.Invoke(loss);

                    if(loss<prevLoss)
                    {
                        prevLoss = loss;
                        MakeBackup();
                        abat = 0;
                    }
                    else
                    {
                        abat++;
                        if (abat > 3) 
                            break;
                    }
                    Thread.Sleep(10);
                }
                RestoreBackup();
            }
            else
            {
                var batches = data.Select((d, i) => (data: d, batchId: i % BatchSize))
                    .GroupBy(_ => _.batchId)
                    .Select(_ => _.Select(v => v.data).ToList())
                    .ToList();

                for (int i = 0; i < IterationsCount; i++)
                    foreach (var batch in batches)
                    {
                        foreach (var (x, t) in batch)
                        {
                            Process(x, t, batch: true);
                        }
                        CommitDeltaWeights();
                    }

            }
        }

        public void Train(IEnumerable<double[]> _inputs, IEnumerable<double[]> _outputs)
        {
            var inputs = _inputs.ToArray();
            var outputs = _outputs.ToArray();
            var data = inputs.Zip(outputs, (i, o) => (x: i, t: o));
            Train(data);
        }

        public double[] PredictSingle(double[] input)
        {
            try
            {
                PropagateInformation(input);
            }
            catch(NeuralNetworkNaNException)
            {
                return OutputLayer.Select(_ => double.NaN).ToArray();
            }
            //DumpValues();
            return OutputLayer.Select(_ => _.Value).ToArray();
        }
    }
}
