using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using AI.Commons.Utils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace AI.Lab3.Data
{
    internal class ModularityFitnessChromosome : CommunityFindingChromosome
    { 
        public ModularityFitnessChromosome(Graph<int> problemParam) : base(problemParam)
        {
            int[] repr = new int[Graph.Nodes.Count];
            for (int i = 0; i < Graph.Nodes.Count; i++) repr[i] = 0;
            SetRepresentation(repr);
        }

        public override List<IChromosome<Graph<int>, int[], double>> PerformCrossover(IChromosome<Graph<int>, int[], double> other)
        {
            var child = new ModularityFitnessChromosome(ProblemParam);
            child.SetRepresentation(GetCrossoverRepresentation(other));
            return new List<IChromosome<Graph<int>, int[], double>> { child };
        }

        protected override double ComputeFitness()
        {           
            long Q = 0;
            Dictionary<int, long> a = new Dictionary<int, long>();
            foreach (var (u, v) in Graph.Edges)
            {
                if (!a.ContainsKey(Representation[u]))
                    a[Representation[u]] = 0;

                if (Representation[u] == Representation[v]) 
                {
                    Q += Graph.Edges.Count;
                    a[Representation[u]]++;
                }
                else
                    a[Representation[u]]++;
            }
            a.Values.ToList().ForEach(x => Q -= x * x);            

            return Q / (1.0 * Graph.Edges.Count * Graph.Edges.Count);
        }        
    }
}
