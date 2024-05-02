using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace AI.Lab4.Data
{
    internal class ShortestPathPopulation : AbstractPopulation<ShortestPathParams, int[], long, ShortestPathChromosome>
    {
        WeightedGraph<int, int> Graph => ProblemParam.Graph;
        int[,] EdgeCost => ProblemParam.EdgeCost;

        public ShortestPathPopulation(ShortestPathParams problemParam) : base(problemParam)
        {
            StepEnd += ShortestPathPopulation_StepEnd;
        }

        class IndividualsComparer : IEqualityComparer<Individual<ShortestPathParams, int[], long, ShortestPathChromosome>>
        {            

            public bool Equals(Individual<ShortestPathParams, int[], long, ShortestPathChromosome> x, Individual<ShortestPathParams, int[], long, ShortestPathChromosome> y)
            {
                return x.Chromosome.Representation.SequenceEqual(y.Chromosome.Representation);
            }

            public int GetHashCode(Individual<ShortestPathParams, int[], long, ShortestPathChromosome> obj)
            {
                return obj.Chromosome.Representation.Sum(x => x);
            }
        }

        private void ShortestPathPopulation_StepEnd(object sender)
        {
            var uniques = Scope.Distinct(new IndividualsComparer()).ToList();
            Scope.Clear();
            uniques.ForEach(x => Scope.Add(x));
        }

        public void Populate(int count = 100)
        {
            for (int i = 0; i < count; i++) 
            {
                AddIndividual(new ShortestPathChromosome(ProblemParam));
            }

            if (ProblemParam.Goal != ShortestPathGoal.Normal) 
            {
                for (int i = 0; i < 5; i++) 
                {
                    AddIndividual(ShortestPathChromosome.GenerateGreedyCircular(ProblemParam));
                }
            }
        }
    }
}
