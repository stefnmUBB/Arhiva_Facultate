using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab3.Data
{
    internal class WrongModularityFitnessChromosome : CommunityFindingChromosome
    {
        public WrongModularityFitnessChromosome(Graph<int> problemParam) : base(problemParam)
        {
            int[] repr = new int[Graph.Nodes.Count];
            for (int i = 0; i < Graph.Nodes.Count; i++) repr[i] = 0;
            SetRepresentation(repr);
        }

        public override List<IChromosome<Graph<int>, int[], double>> PerformCrossover(IChromosome<Graph<int>, int[], double> other)
        {
            var child = new WrongModularityFitnessChromosome(ProblemParam);
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
                    Q += 2 * Graph.Edges.Count;
                    a[Representation[u]]++;
                }
                else
                    a[Representation[u]]++;
            }
            a.Values.ToList().ForEach(x => Q -= x * x);

            return Q / (4.0 * Graph.Edges.Count * Graph.Edges.Count);
        }
    }
}
