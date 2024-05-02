using AI.Lab9.Algebra;
using AI.Lab9.Tools.ANN;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab9.Tools
{
    public class ANNFinder
    {
        public List<(double[] x, double[] t)> TrainData { get; }
        public List<(double[] x, double[] t)> TestData { get; }

        public int InputsCount { get; }
        public int OutputsCount { get; }

        double[][] TrainInputData;
        double[][] TrainOutputData;

        double[][] TestInputData;
        double[][] TestOutputData;

        int BatchSize { get; }

        public ANNFinder(List<(double[] x, double[] t)> train, List<(double[] x, double[] t)> test, int batchSize = 0)
        {
            TrainData = train;
            TestData = test;
            InputsCount = TrainData[0].x.Length;
            OutputsCount = TrainData[0].t.Length;

            TrainInputData = TrainData.Select(_ => _.x).ToArray();
            TrainOutputData = TrainData.Select(_ => _.t).ToArray();

            TestInputData = TestData.Select(_ => _.x).ToArray();
            TestOutputData = TestData.Select(_ => _.t).ToArray();

            BatchSize = batchSize;
        }

        public double GetFit(NeuralNetwork ann, int iters = 2)
        {
            try
            {
                ann.IterationsCount = iters;
                ann.Train(TrainInputData, TrainOutputData);
                var maxIndices = new List<int>();
                var result = TestInputData.Zip(TestOutputData, (i, o) => (i, o)).Select((_) =>
                {
                    var results = ann.PredictSingle(_.i);
                    var maxIndex = results.ArgMax();
                    maxIndices.Add(maxIndex);
                    return _.o[maxIndex] == 1 ? 1 : 0;                    

                }).Average();
                if (double.IsNaN(result))
                    return -1;
                if (maxIndices.Distinct().Count() == 1 && TrainOutputData.Select(_ => _.ArgMax()).Count() != 1)
                    return -maxIndices.FirstOrDefault() - 0.0001;
                return result;
            }
            catch (Exception) { return -1; }
        }

        public List<ANNModel> Models { get; set; } = new List<ANNModel>();

        public ANNModel Blend(ANNModel m1, ANNModel m2)
        {
            int cut1 = Rand.NextInt(m1.HiddenLayers.Count);
            int cut2 = Rand.NextInt(m2.HiddenLayers.Count);

            var hLayers = m1.HiddenLayers.Take(cut1 + 1).Concat(m2.HiddenLayers.Skip(cut2)).Select(_ => _.Clone()).ToList();

            var model = new ANNModel(InputsCount, OutputsCount, true);
            model.HiddenLayers = hLayers;

            if (Rand.NextInt(5) % 2 == 0)
                model.OutputLayer = m1.OutputLayer.Clone();
            else
                model.OutputLayer = m2.OutputLayer.Clone();
            return model;
        }

        public void Mutate(ANNModel m)
        {
            int r = Rand.NextInt(5);
            if(r==0)
            {
                m.OutputLayer = new LayerData(OutputsCount, ANNModel.RandomGenerator());
            }
            else if(r==1)
            {
                if(m.HiddenLayers.Count>0)
                {
                    var c = Rand.NextInt(m.HiddenLayers.Count);
                    var p = m.HiddenLayers[c].Generator.Params;
                    if(p.Length>0)
                    {
                        p[Rand.NextInt(p.Length)] += Rand.NextDouble() - 0.5;
                    }
                }
            }
            else if(r==2)
            {
                m.HiddenLayers.Insert(Rand.NextInt(m.HiddenLayers.Count + 1), new LayerData(1 + Rand.NextInt(13), ANNModel.RandomGenerator()));
            }
            else if(r==3)
            {
                if(m.HiddenLayers.Count>1)
                {
                    m.HiddenLayers.RemoveAt(Rand.NextInt(m.HiddenLayers.Count));
                }
            }
        }

        ANNModel GlobalMax { get; set; } = null;

        public IEnumerable<double[]> GenerateParams(int count, double[] centers, double radius)
        {
            if (count == 0)
            {
                yield return new double[0];
                yield break;
            }

            double a = centers[0] - radius;
            double b = centers[0] + radius;
            var step = (b - a) / 2;
            for (double i = a; i <= b + 0.01; i += step) 
            {
                foreach (var ss in GenerateParams(count - 1, centers.Skip(1).ToArray(), radius)) 
                    yield return ss.ToArray().Concat(new double[] { i }).ToArray();
            }
            yield break;
        }

        public void Rafinate()
        {
            var m = new ANNModel(64, 2, false) { BatchSize = 0 };                
            m.LearningRate = 0.11504956607942;
            m.HiddenLayers = new List<LayerData>();
            var l = new LayerData(23, NeuronGenerators.GaussActivated());            
            l.Generator.Params[0]= -3.04926678060403;
            l.Generator.Params[1]= -0.543287212398503;
            m.HiddenLayers.Add(l);
            m.HiddenLayers.Add(new LayerData(30, NeuronGenerators.SignActivated()));
            var l2 = new LayerData(14, NeuronGenerators.LocalizedSigmoidActivated());
            l2.Generator.Params[0] = 0.159845345611612;
            l2.Generator.Params[1] = -0.923969837714206;
            l2.Generator.Params[2] = 1.95574179464287;
            l2.Generator.Params[3] = -0.194074992442771;
            m.HiddenLayers.Add(l2);

            var o = new LayerData(OutputsCount, NeuronGenerators.SigmoidActivated()); ;            
            m.OutputLayer = o;

            try { m.Fit = GetFit(m.GetANN()); } catch (Exception) { m.Fit = -10; }

            if (GlobalMax == null || m.Fit > GlobalMax.Fit)
            {
                GlobalMax = m.Clone();
                Console.WriteLine($"GMAX = {GlobalMax}");
            }

            var parameters = new List<(NeuronGenerator Gen, int Pos)>();

            parameters = m.HiddenLayers.Append(m.OutputLayer).Select(_ => _.Generator)
                .Select(g => Enumerable.Range(0, g.ParamsCount).Select(i => (Generator: g, Pos: i))).SelectMany(_ => _).ToList();

            var totPCount = parameters.Count;            

            double[] seqMax = parameters.Select(_ => _.Gen.Params[_.Pos]).ToArray();
            double fitMax = m.Fit;

            double[] centers = new double[seqMax.Length];
            for (int i = 0; i < parameters.Count; i++)
            {
                centers[i] = parameters[i].Gen.Params[parameters[i].Pos];
            }

            if (parameters.Count > 0) 
            {

                double radius = 2;

                Console.WriteLine("Entering radius:");
                while (radius > 0.01 * Math.Max(1, totPCount - 3))
                {
                    int k = 0;
                    Console.WriteLine($"Radius={radius}, cnt={parameters.Count}/{totPCount}");
                    foreach (var seq in GenerateParams(parameters.Count, centers.ToArray(), radius))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            parameters[i].Gen.Params[parameters[i].Pos] = seq[i];
                        }
                        Console.WriteLine($"S{k++}={seq.JoinToString(", ")}");
                        try { m.Fit = GetFit(m.GetANN()); } catch (Exception) { m.Fit = -10; }
                        if (m.Fit > fitMax && !double.IsInfinity(m.Fit))
                        {
                            fitMax = m.Fit;
                            seqMax = seq.ToArray();
                            GlobalMax = m.Clone();
                            Console.WriteLine($"PMax = \n{GlobalMax}");
                        }
                    }

                    centers = seqMax.ToArray();
                    radius /= 2;
                }
            }

            Console.WriteLine($"FMAX = {GlobalMax}");
        }

        public void OneIteration()
        {                        
            var m = new ANNModel(InputsCount, OutputsCount) { BatchSize = BatchSize };
            try { m.Fit = GetFit(m.GetANN()); } catch (Exception) { m.Fit = -10; }

            if (GlobalMax == null || m.Fit >= GlobalMax.Fit)
            {
                GlobalMax = m.Clone();
                Console.WriteLine($"GMAX = {GlobalMax}");
            }

            if (m.HiddenLayers.Count > 0) m.HiddenLayers.Add(new LayerData(Rand.NextInt(15), ANNModel.RandomGenerator()));            

            var parameters = new List<(NeuronGenerator Gen, int Pos)>();

            parameters = m.HiddenLayers.Append(m.OutputLayer).Select(_ => _.Generator)
                .Select(g => Enumerable.Range(0, g.ParamsCount).Select(i => (Generator:g, Pos:i))).SelectMany(_ => _).ToList();

            var totPCount = parameters.Count;
            if (parameters.Count > 3) 
            {
                parameters = parameters.Shuffle().Take(3).ToList();
            }

            double[] seqMax = parameters.Select(_ => _.Gen.Params[_.Pos]).ToArray();
            double fitMax = m.Fit;

            double[] centers = new double[seqMax.Length];
            for (int i = 0; i < parameters.Count; i++)
            {
                centers[i] = parameters[i].Gen.Params[parameters[i].Pos];
            }

            if (parameters.Count > 0 && m.Fit > 0)  
            {

                double radius = 2;

                Console.WriteLine("Entering radius:");
                while (radius > 0.01 * Math.Max(1, totPCount - 3)) 
                {
                    Console.WriteLine($"Radius={radius}, cnt={parameters.Count}/{totPCount}");
                    foreach (var seq in GenerateParams(parameters.Count, centers.ToArray(), radius))
                    {
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            parameters[i].Gen.Params[parameters[i].Pos] = seq[i];
                        }
                        //Console.WriteLine($"S={seq.JoinToString(", ")}");
                        try { m.Fit = GetFit(m.GetANN()); } catch (Exception) { m.Fit = -10; }
                        if (m.Fit >= fitMax && !double.IsInfinity(m.Fit))
                        {
                            fitMax = m.Fit;
                            seqMax = seq.ToArray();
                            GlobalMax = m.Clone();
                            Console.WriteLine($"PMax = \n{GlobalMax}");
                        }
                    }

                    centers = seqMax.ToArray();
                    radius /= 2;
                }
            }

            Console.WriteLine($"FMAX = {GlobalMax}");



            /*foreach (var m in Models)
            {
                bool changed = false;
                if (Rand.NextInt(10) < 5) 
                {
                    Mutate(m);
                    changed = true;                    
                }
                if (m.HiddenLayers.Count == 0)
                {
                    m.HiddenLayers.Add(new LayerData(1 + Rand.NextInt(10), ANNModel.RandomGenerator()));
                    changed = true;                    
                }
                if(changed)
                {
                    try { m.Fit = GetFit(m.GetANN()); } catch (Exception e) { m.Fit = -Rand.NextInt(10); }
                }
             
                //Debug.WriteLine($"Run fit on model:\n{m}");
            }

            Models = Models.Where(_ => _.Fit >= 0 || Rand.NextInt(5) == 2).ToList();
            Models.Sort((m1, m2) => Math.Sign(m2.Fit - m1.Fit));

            var newModels = new List<ANNModel>();
            Console.WriteLine($"Breeding....");
            for (int i = 0; i < Models.Count; i++) 
            {
                for (int j = i + 1; j < Models.Count; j++) 
                {
                    if (Rand.NextInt(2 * Models.Count) >= i + j)
                    {
                        Debug.WriteLine($"Breeding {i} {j}");
                        var m = Blend(Models[i], Models[j]);
                        try { m.Fit = GetFit(m.GetANN()); } catch (Exception e) { m.Fit = -10; }
                        newModels.Add(m);
                    }
                }
            }
            Models.AddRange(newModels);*/


        }

    }
}
