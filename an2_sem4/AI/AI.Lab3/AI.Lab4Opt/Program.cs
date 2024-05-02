using AI.Lab4Opt.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab4Opt
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Test();
            //return;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        static void Test()
        {
            var g = DynamicGraph.FromBytes(Properties.Resources.reptilia_tortoise_network_hw);
            var sim = new Simulation(g, 16, 13);
            sim.NextGraph();
            sim.NextGraph();

            for (int i = 0; i < 100; i++) 
            {
                Debug.WriteLine($"---------{i}---------");
                sim.RunStep();
                sim.DumpEdgesAndRates();
            }


        }
    }
}
