using AI.Lab11.Solvers;
using AI.Lab11.Tools.Algebra;
using AI.Lab11.Utils;
using Keras;
using Keras.Layers;
using Keras.Models;
using Keras.PreProcessing.Image;
using Numpy;
using OpenCvSharp;
using OpenCvSharp.Face;
using OpenCvSharp.ML;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Effects;

namespace AI.Lab11
{
    internal static class Program
    {            

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            Console.WriteLine("1-Emojis, 2=Faces, 3=Auto Face");
            Console.Write(">> ");
            var x = Console.ReadLine();
            if(x=="3")
            {
                var faceRec = FisherFaceRecognizer.Create(7);
                {                    
                    Console.WriteLine("Loading..\n");
                    var data = new CsvData(@"C:\Users\Stefan\Desktop\data2\train.csv")
                        .Take(200)
                        .Select(_ => (Emotion: int.Parse(_[0]), Pixels:
                            Mat.FromArray(
                            new Matrix(48, 48, _[1].Split(' ').Select(double.Parse).ToArray())
                            .Data)
                            )).ToArray();

                    Console.WriteLine("Training..\n");                    
                    faceRec.Train(data.Select(_ => _.Pixels).ToArray(), data.Select(_ => _.Emotion).ToArray());
                }
                {
                    Console.WriteLine("Loading Test Data..\n");
                    var testData = new CsvData(@"C:\Users\Stefan\Desktop\data2\icml_face_data.csv")
                        .Where(_ => _[1] == "PublicTest")
                        .Take(100)
                        .Select(_ => (Emotion: int.Parse(_[0]), Pixels:
                            Mat.FromArray(
                            new Matrix(48, 48, _[2].Split(' ').Select(double.Parse).ToArray())
                            .Data)
                            )).ToArray();
                    Console.WriteLine("Testing..\n");
                    int acc = 0;
                    int total = 0;
                    foreach (var t in testData)
                    {
                        var r = t.Emotion;
                        var p = faceRec.Predict(t.Pixels);
                        Console.WriteLine($"{r} {p}");
                        total++;
                        if (r == p) acc++;
                    }
                    Console.WriteLine($"Acc = {1.0 * acc / total}");
                }

                
                Console.ReadLine();

                //faceRec.Train()

            }
            else if (x == "2") 
            {
                string EnvPath = @"C:\Users\Stefan\source\repos\AI.Lab11\AI.Lab11\bin\Debug\py";
                Environment.SetEnvironmentVariable("PATH", EnvPath, EnvironmentVariableTarget.Process);
                PythonEngine.PythonHome = Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);
                PythonEngine.PythonPath = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);
                Console.WriteLine(Runtime.PythonDLL);

                FaceSolver Solver = new FaceSolver(@"..\..\Py\Emotion-Detection\src\data\train", @"..\..\Py\Emotion-Detection\src\data\test");
                Solver.EpochsCount = 3;
                Console.WriteLine("Loading...");
                Solver.Load();
                Console.WriteLine("Training...");
                Solver.Train();
                Console.WriteLine("Training done...");

                Console.WriteLine("Predicting multiclass");
                var confmat = new Matrix(7, 7);

                for (int i = 0; i < 7; i++)
                {
                    var rLabel = Solver.Labels[i];
                    foreach (var p in Solver.PredictClassIndex(DataSets.FacesProvider($@"C:\Users\Stefan\source\repos\AI.Lab11\AI.Lab11\Py\Emotion-detection\src\data\test\{rLabel}"))) 
                    {
                        var pLabel = Solver.Labels[p];
                        confmat[i, p]++;
                        Console.WriteLine($"{rLabel,15} {pLabel,15}");
                    }
                }
                Console.WriteLine("Confusion Matrix");
                Console.WriteLine(confmat);
                Console.WriteLine($"Acc = {Solver.Accuracy.JoinToString(", ")}");
                Console.WriteLine($"ValAcc = {Solver.ValAccuracy.JoinToString(", ")}");
                Console.WriteLine($"Loss = {Solver.Loss.JoinToString(", ")}");
                Console.WriteLine($"ValLoss = {Solver.ValLoss.JoinToString(", ")}");

                Console.ReadLine();
            }
            else
            {
                string EnvPath = @"C:\Users\Stefan\source\repos\AI.Lab11\AI.Lab11\bin\Debug\py";
                Environment.SetEnvironmentVariable("PATH", EnvPath, EnvironmentVariableTarget.Process);
                PythonEngine.PythonHome = Environment.GetEnvironmentVariable("PYTHONHOME", EnvironmentVariableTarget.Process);
                PythonEngine.PythonPath = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);
                Console.WriteLine(Runtime.PythonDLL);

                var solver = new GoodEmojiSolver("", "");
                solver.EpochsCount = 10;

                solver.Train();

                solver.Test();


                Console.WriteLine($"Acc = {solver.Accuracy.Last()}");                
                Console.WriteLine($"Loss = {solver.Loss.Last()}");
                Console.ReadLine();


                /*Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new EmojisForm());*/
            }
        }        
    }
}
