using AI.Lab9.Tools.ANN;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab9.Tools
{
    public class ANNModel
    {        
        public LayerData InputLayer { get; private set; }
        public List<LayerData> HiddenLayers { get; set; }
        public LayerData OutputLayer { get; set; }
        public int BatchSize { get; set; }
        public double LearningRate { get; set; }

        public ANNModel(int inputsCount, int outputsCount, bool autoGen = true)
        {
            InputLayer = new LayerData(inputsCount, new NeuronGenerator("Input", x => Neurons.Input, 0));
            OutputLayer = new LayerData(outputsCount, RandomGenerator());
            LearningRate = Rand.NextDouble() / 4;

            if (autoGen)
            {
                int n = 1 + Rand.NextInt(7);
                HiddenLayers = new List<LayerData>();
                for (int i = 0; i < n; i++)
                {
                    var ncount = 1 + Rand.NextInt(40);
                    HiddenLayers.Add(new LayerData(ncount, RandomGenerator()));
                }
            }
        }

        public double Fit { get; set; }

        public static NeuronGenerator RandomGenerator() => NeuronGenerators.All[Utils.Rand.NextInt(NeuronGenerators.All.Count)]();

        public NeuralNetwork GetANN()
        {
            var ann = new NeuralNetwork() { IterationsCount = 7, BatchSize = BatchSize, LearningRate=LearningRate };
            ann.AddLayer(InputLayer.Generator.Instantiate(), InputLayer.NeuronsCount);
            HiddenLayers.ForEach(l => ann.AddLayer(l.Generator.Instantiate(), l.NeuronsCount));
            ann.AddLayer(OutputLayer.Generator.Instantiate(), OutputLayer.NeuronsCount);
            ann.Create();
            return ann;
        }


        private static string Indent(string x) => x.Split('\n').Select(_ => "    " + _).JoinToString("\n");
        public override string ToString()
        {
            return $"Model Fit={Fit}; Batch={BatchSize}; LL={LearningRate} {{\n    {InputLayer}\n{Indent(HiddenLayers.JoinToString("\n"))}\n    {OutputLayer}\n}}";
        }

        public ANNModel Clone()
        {
            var model = new ANNModel(InputLayer.NeuronsCount, OutputLayer.NeuronsCount, false);
            model.InputLayer = InputLayer.Clone();
            model.OutputLayer = OutputLayer.Clone();
            model.HiddenLayers = HiddenLayers.Select(_=>_.Clone()).ToList();
            model.Fit = Fit;
            model.BatchSize = BatchSize;
            model.LearningRate = LearningRate;
            return model;

        }

    }

    public class LayerData
    {
        public int NeuronsCount { get; set; }
        public NeuronGenerator Generator { get; set; }

        public LayerData(int neuronsCount, NeuronGenerator generator)
        {
            NeuronsCount = neuronsCount;
            Generator = generator;
        }

        public LayerData Clone() => new LayerData(NeuronsCount, Generator.Clone());

        public override string ToString() => $"{NeuronsCount} of {Generator}";
    }

    public class NeuronGenerator
    {
        public string Name { get; }
        public Func<double[], Func<Neuron>> Functor { get; }
        public double[] Params { get; }
        public int ParamsCount => Params.Length;
        public Func<Neuron> Instantiate() => Functor(Params);

        public NeuronGenerator(string name, Func<double[], Func<Neuron>> functor, double[] _params)
        {
            Name = name;
            Functor = functor;
            Params = _params.ToArray();
        }

        public NeuronGenerator(string name, Func<double[], Func<Neuron>> functor, int paramsCount)
        {
            Name = name;
            Functor = functor;
            Params = new double[paramsCount];
            for (int i = 0; i < paramsCount; i++) Params[i] = Rand.NextDouble() * 3 - 0.75;
        }

        public override string ToString() => $"{Name} {Params.JoinToString(", ")}";

        public NeuronGenerator Clone() => new NeuronGenerator(Name, Functor, Params);
    }

    internal static class NeuronGenerators
    {
        public static NeuronGenerator SelfActivated() => new NeuronGenerator("Self",
            (v) => Neurons.Hidden(ActivationFunction.Self), 0);

        public static NeuronGenerator SignActivated() => new NeuronGenerator("Sign",
            (v) => Neurons.Hidden(ActivationFunction.Sign), 0);

        public static NeuronGenerator SigmoidActivated() => new NeuronGenerator("Sigmoid",
            (v) => Neurons.Hidden(ActivationFunction.Sigmoid), 0);

        public static NeuronGenerator ReLUActivated() => new NeuronGenerator("ReLU",
            (v) => Neurons.Hidden(ActivationFunction.ReLU), 0);

        public static NeuronGenerator PragActivated() => new NeuronGenerator("Prag",
            (v) => Neurons.Hidden(ActivationFunction.Prag(v[0], v[1], v[2])), 3);

        public static NeuronGenerator RampaActivated() => new NeuronGenerator("Rampa",
            (v) => Neurons.Hidden(ActivationFunction.Rampa(v[0], v[1], v[2], v[3])), 4);

        public static NeuronGenerator LinearActivated() => new NeuronGenerator("Linear",
            (v) => Neurons.Hidden(ActivationFunction.Linear(v[0], v[1])), 2);

        public static NeuronGenerator GaussActivated() => new NeuronGenerator("Gauss",
            (v) => Neurons.Hidden(ActivationFunction.Gauss(v[0], v[1])), 2);

        public static NeuronGenerator LocalizedSigmoidActivated() => new NeuronGenerator("LocalizedSigmoid",
            (v) => Neurons.Hidden(ActivationFunction.LocalizedSigmoid(v[0], v[1], v[2], v[3])), 4);

        public static List<Func<NeuronGenerator>> All = new List<Func<NeuronGenerator>>
        {
            SelfActivated,
            SignActivated,
            SigmoidActivated,
            ReLUActivated,
            PragActivated,
            RampaActivated,
            LinearActivated,
            GaussActivated,
            LocalizedSigmoidActivated
        };



    }
}
