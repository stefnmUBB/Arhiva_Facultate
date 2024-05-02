using AI.Commons.Data;
using AI.Commons.IO;
using AI.Lab3.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AI.LAb3.Console
{
    internal class Program
    {
        static int Iterations = 1000;
        static Graph<int> Graph;        
        static CommunityFinderPopulation Population;

        static void Simulate()
        {
            for(int i=0;i<Iterations && Population.Scope.Count + Population.Gods.Count > 0; i++)            
            {
                try
                {
                    Debug.WriteLine(i);
                    Population.RunStep();
                    System.Console.WriteLine($"Gen. {i} >> Best {Population.Gods.First()}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                    break;
                }
            }
        }

        // args[0] = gml_file
        // args[1] = iterations
        // args[2] = fitness
        static void Main(string[] args)
        {
            System.Console.WriteLine($"Reading Graph: {args[0]}");
            Graph = GraphReader.FromGMLFile(args[0]).NormalizeNodes();

            Iterations = int.Parse(args[1]);
            System.Console.WriteLine($"Generations: {Iterations}");

            Population = new CommunityFinderPopulation(Graph);
            System.Console.WriteLine($"Populating");
            Population.Populate(100, Type.GetType(args[2] + "FitnessChromosome"));
            System.Console.WriteLine($"Running");
            Simulate();
            System.Console.ReadLine();
        }
    }
}
