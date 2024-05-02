using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab3.Data
{
    internal class CommunityScoreFitnessChromosome : CommunityFindingChromosome
    {
        public CommunityScoreFitnessChromosome(Graph<int> problemParam) : base(problemParam)
        {
        }

        internal static double Alpha = 2;
        public override List<IChromosome<Graph<int>, int[], double>> PerformCrossover(IChromosome<Graph<int>, int[], double> other)
        {
            var child = new CommunityScoreFitnessChromosome(ProblemParam);
            child.SetRepresentation(GetCrossoverRepresentation(other));
            return new List<IChromosome<Graph<int>, int[], double>> { child };
        }

        protected override double ComputeFitness()
        {
            var A = Graph.GetAdjacencyMatrix();

            return this.ToCommunities().Select(c =>
            {
                //Debug.WriteLine
                if (c.Count == 0) return 0;                

                double score = 0;
                int p = 0;

                foreach(var i in c)
                {
                    double si = 0;
                    foreach (var j in c)
                    {
                        si += A[i, j];
                        p += A[i, j];
                    }

                    score += Math.Pow(si / c.Count, Alpha);
                }
                score *= p;
                score /= c.Count;

                return score;
            }).Sum();
        }
    }
}

