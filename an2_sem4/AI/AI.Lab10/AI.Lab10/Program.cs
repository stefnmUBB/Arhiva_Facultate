using AI.Lab10.Tools.Clustering;
using AI.Lab10.Tools.Text;
using AI.Lab10.Utils;
using AI.Lab10.Tools.ANN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AI.Lab10.Tools;
using System.Diagnostics;
using AI.Lab10.Tools.Text.Vectorization;
using System.IO;
using System.Text;
using System.Windows.Forms.VisualStyles;

namespace AI.Lab10
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            return;

            //SolveEmotionsANN_W2V();
            //SolveSpam();
            SolveEmotionsKHelper();

            SolveEmotionsK();

            /*var t0 = Task.Run(SolveEmotionsANN_W2V);
            for(int i=0;i<6;i++)
            {
                Thread.Sleep(150);
                Task.Run(SolveEmotionsANN_W2V);
            }
            t0.Wait();*/
            //SolveEmotionsANN_W2V();
            Console.ReadLine();

            return;          


            /*return;
            var ann = new NeuralNetwork() { IterationsCount = 200, LearningRate = 0.0960909660654566 }
                .AddLayer(Neurons.Input, dict.Count)
                .AddLayer(Neurons.Hidden(ActivationFunction.Linear(0.001, 0)), 10)
                //.AddLayer(Neurons.Output(ActivationFunction.Cubic(8.84793, -18.6839, 9.83594, 0)), 50)
                .AddLayer(Neurons.Output(ActivationFunction.Cubic(0.635234, -4.51228, 3.87705, 0)), 20)
                .AddLayer(Neurons.Output(ActivationFunction.ReLU), 7)
                .AddLayer(Neurons.Output(ActivationFunction.Linear(1, 0.1)), 2)                
                .Create();*/
                       

            /*var f = new Action(() =>
            {
                var finder = new ANNFinder(trainI, testI);                
                finder.Rafinate();               
            });
            var t1 = Task.Run(f);
            Thread.Sleep(1000);
            var t2 = Task.Run(f);
            Thread.Sleep(1000);
            var t3 = Task.Run(f);
            Thread.Sleep(1000);
            var t4 = Task.Run(f);
            Thread.Sleep(1000);
            var t5 = Task.Run(f); Thread.Sleep(1000);
            var t6 = Task.Run(f); Thread.Sleep(1000);
            var t7 = Task.Run(f); Thread.Sleep(1000);
            var t8 = Task.Run(f); Thread.Sleep(1000);
            var t9 = Task.Run(f); Thread.Sleep(1000);
            var t10 = Task.Run(f); Thread.Sleep(1000);
            var t11 = Task.Run(f); Thread.Sleep(1000);
            var t12 = Task.Run(f); Thread.Sleep(1000);
            var t13 = Task.Run(f); Thread.Sleep(1000);
            t1.Wait();
            t2.Wait();
            t3.Wait();
            t4.Wait();
            t5.Wait();*/


            //Console.ReadLine();                    

            /*Console.WriteLine("Training");
            ann.Train(testInput, testOutput);

            Console.WriteLine("Testing");

            for (int i = 0; i < testInput.Length; i++)
            {
                var real = testOutput[i];
                var pred = ann.PredictSingle(testInput[i]);

                var pmin = pred.Min();
                if(pmin<0)
                    pred = pred.Select(x => x - pmin).ToArray();
                var sum = pred.Sum();
                if (sum != 0)
                    pred = pred.Select(x => x / sum).ToArray();

                Console.WriteLine($"{real.JoinToString(" ")}; {pred.Select(_=>_.ToString("F4")).JoinToString(" ")}; {pred.ArgMax()==real.ArgMax()}");
            }

            ann.DumpWeights("weights.dump1");
            Thread.Sleep(1000);
            Process.Start("shutdown", "/s /t 0");

            Console.ReadLine();*/            


            //foreach (var item in data)
              //  Console.WriteLine(item);
                
           
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
        }

        private static void SolveEmotionsANN_W2V()
        {
            var data = DataSets.ReviewsMixedW2Vec;
            
            var vals = new double[10] { -1.06020016784789, 2.89577776468162, -1.78866307194748, 0.688428209018161, 2.10817504353271, 2.17757706771492, 1.68825166052592, -2.68507874649255, -0.580558940572924, 0.398388422745461 };            

            var tt = data.ToArray().SplitTrainTest(0.8, 0);

            var train = tt.Train;
            var test = tt.Test;

            var trainInput = train.Select(_ => _.Vector).ToArray();
            var trainOutput = train.Select(_ => _.Type).ToArray();

            var testInput = test.Select(_ => _.Vector).ToArray();
            var testOutput = test.Select(_ => _.Type).ToArray();

            var textANN = new TextANN { IterationsCount = 50 };
            //textANN.Vectorizer = new Word2Vec();
            textANN.AddHiddenLayer(40, NeuronGenerators.LinearActivated(), vals[0], vals[1]);
            textANN.AddHiddenLayer(26, NeuronGenerators.GaussActivated(), vals[4], vals[5]);
            textANN.AddHiddenLayer(10, NeuronGenerators.CubicActivated(), vals[6], vals[7], vals[8], vals[9]);
            textANN.AddHiddenLayer(5, NeuronGenerators.SelfActivated());
            textANN.AddHiddenLayer(3, NeuronGenerators.LinearActivated(), vals[2], vals[3]);
            textANN.SetOutputLayer(NeuronGenerators.SelfActivated());
            try
            {
                textANN.TrainPrecompiled(trainInput, trainOutput);
            }
            catch (ArgumentException)
            {
                
            }

            var outs = textANN.PredictPrecompiled(testInput, testOutput);

            int acc = 0;
            for (int i = 0; i < testOutput.Length; i++)
            {
                if (outs[i] == testOutput[i])
                    acc++;
                Console.WriteLine($"{testOutput[i]} {outs[i]}");
            }
            double accuracy = 1.0 * acc / testOutput.Length;
            Console.WriteLine($"Accuracy = {accuracy}");
        }

        private static void SolveEmotionsANN()
        {
            var data = DataSets.ReviewsMixed;                                  

            var origvals = new double[10] { -1.06020016784789, 2.89577776468162, -1.78866307194748, 0.688428209018161, 2.10817504353271, 2.17757706771492, 1.68825166052592, -2.68507874649255, -0.580558940572924, 0.398388422745461 };
            var vals = new double[10] { -1.06020016784789, 2.89577776468162, -1.78866307194748, 0.688428209018161, 2.10817504353271, 2.17757706771492, 1.68825166052592, -2.68507874649255, -0.580558940572924, 0.398388422745461 };
            double macc = 0;
            var maxVals = new double[10];

            var tt = data.ToArray().SplitTrainTest(0.8, 0);

            var train = tt.Train;
            var test = tt.Test;

            var trainInput = train.Select(_ => _.Text).ToArray();
            var trainOutput = train.Select(_ => _.Type).ToArray();

            var testInput = test.Select(_ => _.Text).ToArray();
            var testOutput = test.Select(_ => _.Type).ToArray();

            //while (true)
            {
                Thread.Sleep(10);
                //Console.WriteLine(data.JoinToString("\n"));                    

                for (int i = 0; i < 10; i++)
                {
                    //vals[i] = Rand.NextDouble() * 6 - 3;
                }

                var textANN = new TextANN { IterationsCount = 50 };
                textANN.Vectorizer = new NGram { MinimumRelevantFrequency = 2 };                                    
                textANN.AddHiddenLayer(40, NeuronGenerators.LinearActivated(), vals[0], vals[1]);
                textANN.AddHiddenLayer(26, NeuronGenerators.GaussActivated(), vals[4], vals[5]);
                textANN.AddHiddenLayer(10, NeuronGenerators.CubicActivated(), vals[6], vals[7], vals[8], vals[9]);
                textANN.AddHiddenLayer(5, NeuronGenerators.SelfActivated());
                textANN.AddHiddenLayer(6, NeuronGenerators.LinearActivated(), vals[2], vals[3]);
                textANN.SetOutputLayer(NeuronGenerators.SelfActivated());
                try
                {
                    textANN.Train(trainInput, trainOutput);
                }
                catch (ArgumentException)
                {
                    //continue;
                }

                var outs = textANN.Predict(testInput, testOutput);

                int acc = 0;
                for (int i = 0; i < testOutput.Length; i++)
                {
                    if (outs[i] == testOutput[i])
                        acc++;
                    Console.WriteLine($"{testOutput[i]} {outs[i]}");
                }
                double accuracy = 1.0 * acc / testOutput.Length;

                if (outs.Distinct().Count() > 1 && accuracy > macc)
                {
                    macc = accuracy;
                    maxVals = vals.ToArray();
                }

                if (macc >= 0.85)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine("MAXXXX = " + maxVals.JoinToString(", ") + " ACC = " + macc);
                if (macc >= 0.85)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        private static void SolveEmotionsK()
        {
            
        }

        private static void SolveEmotionsKHelper()
        {
            var datas = DataSets.ReviewsMixed;                       
                   
            var tt = datas.ToArray().SplitTrainTest(0.8);

            var train = tt.Train;
            var test = tt.Test;

            var trainInput = train.Select(_ => _.Text).ToArray();
            var trainOutput = train.Select(_ => _.Type).ToArray();

            var textKmeans = new TextKMeans();
            textKmeans.Vectorizer = new GoodBadHelper(new BagOfWords());// new NGram(2)
            //textKmeans.Vectorizer = new Word2Vec();// new NGram(2);
            //textKmeans.Vectorizer = new NGram(2);
            //textKmeans.Vectorizer = new BagOfWords();

            textKmeans.Train(trainInput, trainOutput);
            var predOutput = textKmeans.Predict(test.Select(_ => _.Text).ToArray());
            var testOutput = test.Select(_ => _.Type).ToArray();

            int acc = 0;
            for (int i = 0; i < test.Count; i++)
            {
                Console.WriteLine(testOutput[i] + " " + predOutput[i]);
                if (testOutput[i] == predOutput[i]) acc++;
            }
            Console.WriteLine("Acc = " + (1.0 * acc / test.Count));
        }

        private static void SolveEmotionsKW2V()
        {
            var data = DataSets.ReviewsMixedW2Vec;

            //Console.WriteLine(data.JoinToString("\n"));
            var tt = data.ToArray().SplitTrainTest(0.8);

            var train = tt.Train;
            var test = tt.Test;

            var trainInput = train.Select(_ => _.Vector).ToArray();
            var trainOutput = train.Select(_ => _.Type).ToArray();

            var textKmeans = new TextKMeans();            

            textKmeans.TrainPrecompiled(trainInput, trainOutput);
            var predOutput = textKmeans.PredictPrecompiled(test.Select(_ => _.Vector).ToArray());
            var testOutput = test.Select(_ => _.Type).ToArray();

            int acc = 0;
            for (int i = 0; i < test.Count; i++)
            {
                Console.WriteLine(testOutput[i] + " " + predOutput[i]);
                if (testOutput[i] == predOutput[i]) acc++;
            }
            Console.WriteLine("Acc = " + (1.0 * acc / test.Count));
        }

        private static void SolveSpam()
        {
            
        }
    }
}
