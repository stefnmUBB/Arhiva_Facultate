using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Lab3.Data
{
    internal class DensityFitnessChromosome : CommunityFindingChromosome
    {
        public DensityFitnessChromosome(Graph<int> problemParam) : base(problemParam)
        {
        }

        public override List<IChromosome<Graph<int>, int[], double>> PerformCrossover(IChromosome<Graph<int>, int[], double> other)
        {
            var child = new DensityFitnessChromosome(ProblemParam);
            child.SetRepresentation(GetCrossoverRepresentation(other));
            return new List<IChromosome<Graph<int>, int[], double>> { child };
        }

        protected override double ComputeFitness()
        {
            int k = Representation.Max() + 1;
            int[] li = new int[k];           
            int[] le = new int[k];            

            foreach(var (u,v) in Graph.Edges)
            {
                if (Representation[u] == Representation[v])
                    li[Representation[u]]++;
                else
                    le[Representation[u]]++;
            }

            double D = 0;
            for (int i = 0; i < k; i++)
            {
                int cnt = Representation.Where(x => x == i).Count();
                if (cnt > 0)
                    D += 1.0 * (li[i] - le[i]) / cnt;
            }

            return D;
        }
    }
}
