using AI.ML.Data;
using AI.ML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.ML
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());*/


            Console.WriteLine("\nProcessing Sport...");

            var csv = new CsvData(@"Input\sport.csv");

            var sports = csv.ToObjects<SportPrediction>().Select(d => d.ToPredictedData()).ToList();            

            Console.WriteLine($"MeanError            = {sports.MeanError(SportData.DefaultDifference)}");            
            Console.WriteLine($"MeanSqauredError     = {sports.MeanSquaredError(SportData.DefaultDifference)}");            
            Console.WriteLine($"RootMeanSqauredError = {sports.RootMeanSquaredError(SportData.DefaultDifference)}");

            Console.WriteLine($"\nMeanErrorRaw            = {sports.MeanErrorRawValuesAvg()}");
            Console.WriteLine($"MeanSquaredErrorRaw     = {sports.MeanErrorRawValuesAvg(true)}");
            Console.WriteLine($"RootMeanSquaredErrorRaw = {Math.Sqrt(sports.MeanErrorRawValuesAvg(true))}");

            Console.WriteLine("\nProcessing Flowers...");

            csv = new CsvData(@"Input\flowers.csv");

            //csv.ToObjects<FlowerPrediction>().ToList().ForEach(Console.WriteLine);

            var flowers = csv.ToObjects<FlowerPrediction>().Select(d => d.ToPredictedData()).ToList();

            var q = flowers.ClassificationQuality();

            Console.WriteLine($"Accuracy  = {q.Accuracy}");
            Console.WriteLine($"Precision+ = {q.PrecisionPos}");
            Console.WriteLine($"Precision- = {q.PrecisionNeg}");
            Console.WriteLine($"Recall+    = {q.RecallPos}");
            Console.WriteLine($"Recall-    = {q.RecallNeg}");


            Console.WriteLine(Functions.CrossEntropyLoss(
                new double[,] { { 0.9, 0.1 }, { 0.4, 0.6 }, { 0.1, 0.9 }, { 0.8, 0.2 } },
                new double[,] { { 0, 1 }, { 1, 0 }, { 1, 0 }, { 0, 1 } }
             ));

            Console.ReadLine();
        }
    }
}
