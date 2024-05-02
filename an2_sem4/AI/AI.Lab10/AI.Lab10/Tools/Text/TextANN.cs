using AI.Lab10.Tools.ANN;
using AI.Lab10.Tools.Text.Vectorization;
using AI.Lab10.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text
{
    public class TextANN
    {
        public IVectorizer Vectorizer { get; set; } = new TFIDF();

        private List<LayerData> ANNLayers { get; } = new List<LayerData>();
        private NeuronGenerator OutputGenerator = NeuronGenerators.ReLUActivated();
        private double[] OutputGeneratorArgs = new double[0];

        public void AddHiddenLayer(int count, NeuronGenerator generator, params double[] args)
        {
            ANNLayers.Add(new LayerData(count, generator, args));
        }

        public void SetOutputLayer(NeuronGenerator generator, params double[] args)
        {
            OutputGenerator = generator;
            OutputGeneratorArgs = args;
        }

        private ANNModel ANNModel { get; set; }

        public List<string> Labels;

        public NeuralNetwork Network { get; private set; }

        public int IterationsCount { get; set; } = 1000;

        public void TrainPrecompiled(double[][] vinput, string[] output)
        {
            Labels = output.Distinct().ToList();
            
            var voutput = output.Select(_ => Labels.IndexOf(_)).Select(i =>
            {
                var v = new double[Labels.Count];
                v[i] = 1;
                return v;
            }).ToArray();
            Console.WriteLine($"Features count = {vinput[0].Length}");

            ANNModel = new ANNModel(vinput[0].Length, Labels.Count, false);
            ANNLayers.ForEach(ANNModel.HiddenLayers.Add);
            ANNModel.OutputLayer = new LayerData(Labels.Count, OutputGenerator, OutputGeneratorArgs);
            Network = ANNModel.GetANN();
            Network.LearningRate = 0.001;
            Network.IterationsCount = IterationsCount;
            Network.Loss = Losses.MSE;
            Network.Train(vinput.Zip(voutput, (i, o) => (i, o)), loss =>
            {
                if (double.IsNaN(loss))
                    throw new ArgumentException();
            });
        }

        public Action<double> LossReport { get; set; } = _ => { };
        public void Train(string[] input, string[] output)
        {
            Labels = output.Distinct().ToList();

            Vectorizer.Prepare(input);
            Console.WriteLine("extracting features...");
            var vinput = Vectorizer.ExtractFeatures(input);
            Console.WriteLine("done.");
            var voutput = output.Select(_ => Labels.IndexOf(_)).Select(i =>
            {
                var v = new double[Labels.Count];
                v[i] = 1;
                return v;
            }).ToArray();
            Console.WriteLine($"Features count = {Vectorizer.FeaturesCount}");

            ANNModel = new ANNModel(Vectorizer.FeaturesCount, Labels.Count, false);            
            ANNLayers.ForEach(ANNModel.HiddenLayers.Add);
            ANNModel.OutputLayer = new LayerData(Labels.Count, OutputGenerator, OutputGeneratorArgs);            
            Network = ANNModel.GetANN();
            Network.LearningRate = 0.001;
            Network.IterationsCount = IterationsCount;
            Network.Loss = Losses.MSE;
            Network.Train(vinput.Zip(voutput, (i, o) => (i, o)), LossReport);
        }

        public string[] PredictPrecompiled(double[][] vinput)
        {
            var outputs = new List<string>();            
            foreach (var i in vinput)
            {
                var o = Network.PredictSingle(i);
                Console.WriteLine(o.JoinToString(" "));
                var index = o.ArgMax(_ => _);
                outputs.Add(Labels[index]);
            }
            return outputs.ToArray();
        }

        public string[] Predict(string[] input)
        {
            var outputs = new List<string>();
            var vinput = Vectorizer.ExtractFeatures(input);
            foreach (var i in vinput)
            {
                var o = Network.PredictSingle(i);
                Console.WriteLine(o.JoinToString(" "));
                var index = o.ArgMax(_ => _);
                outputs.Add(Labels[index]);
            }
            return outputs.ToArray();
        }

        public string[] PredictPrecompiled(double[][] vinput, string[] toutputs)
        {
            var outputs = new List<string>();            

            var voutput = toutputs.Select(_ => Labels.IndexOf(_)).Select(it =>
            {
                var v = new double[Labels.Count];
                v[it] = 1;
                return v;
            }).ToArray();

            int acc = 0;

            double s1 = 0;
            double s2 = 0;
            for (int i = 0; i < vinput.Length; i++)
            {
                var o = Network.PredictSingle(vinput[i]);
                s1 += o[0];
                s2 += o[1];
                /*Console.WriteLine(voutput[i].JoinToString(" ") + " " + o.JoinToString(" ")
                    + " " + (voutput[i].ArgMax(q => q) == o.ArgMax(x => x)));*/
                if (voutput[i].ArgMax(q => q) == o.ArgMax(x => x))
                    acc++;
                var index = o.ArgMax(_ => _);
                outputs.Add(Labels[index]);
            }
            //Console.WriteLine(s1 / vinput.Length);
            //Console.WriteLine(s2 / vinput.Length);

            Console.WriteLine("acc=" + (1.0 * acc / vinput.Length));
            return outputs.ToArray();
        }

        public string[] Predict(string[] input, string[] toutputs)
        {
            var outputs = new List<string>();
            var vinput = Vectorizer.ExtractFeatures(input);

            var voutput = toutputs.Select(_ => Labels.IndexOf(_)).Select(it =>
            {
                var v = new double[Labels.Count];
                v[it] = 1;
                return v;
            }).ToArray();

            int acc = 0;

            double s1 = 0;
            double s2 = 0;
            for (int i = 0; i < vinput.Length; i++) 
            {
                var o = Network.PredictSingle(vinput[i]);
                s1 += o[0];
                s2 += o[1];
                /*Console.WriteLine(voutput[i].JoinToString(" ") + " " + o.JoinToString(" ")
                    + " " + (voutput[i].ArgMax(q => q) == o.ArgMax(x => x)));*/
                if (voutput[i].ArgMax(q => q) == o.ArgMax(x => x))
                    acc++;
                var index = o.ArgMax(_ => _);
                outputs.Add(Labels[index]);
            }
            //Console.WriteLine(s1 / vinput.Length);
            //Console.WriteLine(s2 / vinput.Length);

            Console.WriteLine("acc=" + (1.0 * acc / vinput.Length));
            return outputs.ToArray();
        }

    }
}
