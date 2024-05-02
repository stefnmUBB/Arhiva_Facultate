using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using AI.Commons.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab3.Data
{
    internal class CommunityFinderPopulation : AbstractPopulation<Graph<int>, int[], double, CommunityFindingChromosome>        
    {
        protected Graph<int> Graph => ProblemParam;
        public CommunityFinderPopulation(Graph<int> problemParam) : base(problemParam)
        { }        

        protected int[] GetRandomRepresentation()
        {
            int[] repr = Commons.Utils.Random.RandomInts(Graph.Nodes.Count).ToArray();

            HashSet<int> visited = new HashSet<int>();

            foreach (var (u,v) in Graph.Edges.Shuffle()) 
            {
                if (visited.Count == Graph.Nodes.Count) 
                    break;

                visited.Add(u);
                visited.Add(v);

                if (repr[u] == repr[v]) continue;

                if (Commons.Utils.Random.Decision(0.3)) 
                {
                    int oldr = repr[v];
                    int newr = repr[u];

                    for (int i = 0; i < Graph.Nodes.Count; i++)
                    {
                        if (repr[i] == oldr)
                            repr[i] = newr;
                    }
                }
            }

            return repr;
        }


        public void Populate(int count, Type chromosomeType = null)
        {
            if (chromosomeType == null)
                chromosomeType = typeof(ModularityFitnessChromosome);

            for (int i = 0; i < count; i++)
            {
                var chromo = Activator.CreateInstance(chromosomeType, new object[] { Graph })
                    as CommunityFindingChromosome;
                chromo.SetRepresentation(GetRandomRepresentation());
                AddIndividual(chromo);
            }
        }        
    }
}
