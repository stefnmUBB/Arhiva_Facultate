using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using AI.Commons.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AI.Lab3.Data
{
    internal abstract class CommunityFindingChromosome : AbstractChromosome<Graph<int>, int[], double>
    {
        protected Graph<int> Graph => ProblemParam;

        protected CommunityFindingChromosome(Graph<int> problemParam) : base(problemParam)
        {
            int[] repr = new int[Graph.Nodes.Count];
            for (int i = 0; i < Graph.Nodes.Count; i++) repr[i] = 0;
            NormalizeRepresentation(repr);
            SetRepresentation(repr);            
        }

        protected static void NormalizeRepresentation(int[] repr)
        {
            Dictionary<int, int> d = new Dictionary<int, int>();
            for(int i=0;i<repr.Length;i++)
            {
                if (!d.ContainsKey(repr[i]))
                    d[repr[i]] = d.Count;
            }
            for (int i = 0; i < repr.Length; i++)
            {
                repr[i] = d[repr[i]];
            }
        }

        public new void SetRepresentation(int[] repr)
        {
            NormalizeRepresentation(repr);
            base.SetRepresentation(repr);
        }

        /*protected int[] GetCrossoverRepresentation(IChromosome<Graph<int>, int[], double> other)
        {
            var source = this;
            var dest = other;            

            int i = Commons.Utils.Random.IndexFromContainer(source.Representation);
            int[] repr = dest.Representation.ToArray();

            int label = source.Representation[i];

            for (int j = 0; j < Graph.Nodes.Count; j++)
            {
                if (source.Representation[j] == label)
                    repr[j] = label;
            }
            NormalizeRepresentation(repr);

            return repr;
        }*/

        protected int[] GetCrossoverRepresentation(IChromosome<Graph<int>, int[], double> other)
        {
            var source = this;
            var dest = other;

            int i = Commons.Utils.Random.IndexFromContainer(source.Representation);
            int[] repr = dest.Representation.ToArray();

            var candidates = Graph.Edges.Where(e => e.Source == i && e.Target!=i).Select(e => e.Target).ToList();

            if (candidates.Count == 0 || Random.Decision(0.5)) 
            {
                int label = source.Representation[i];

                for (int j = 0; j < Graph.Nodes.Count; j++)
                {
                    if (source.Representation[j] == label)
                        repr[j] = label;
                }
            }
            else
            {
                repr[i] = Random.ValueFromContainer(candidates);
            }            

            
            NormalizeRepresentation(repr);

            return repr;
        }

        public override void Mutate()
        {
            if (Random.Decision(0.3))
            {
                var e = Random.ValueFromContainer(Graph.Edges);

                int oldr = Representation[e.Target];
                int newr = Representation[e.Source];

                for (int i = 0; i < Graph.Nodes.Count; i++)
                {
                    if (Representation[i] == oldr)
                        Representation[i] = newr;
                }
            }
            else
            {
                if (Random.Decision(0.5))
                {
                    var e = Random.ValueFromContainer(Graph.Edges);
                    Representation[e.Target] = Representation[e.Source];
                }
                else
                {
                    int k = Representation.Max();
                    for (int i = 0; i < Representation.Count(); i++)
                    {
                        if (Random.Decision(0.1))
                            Representation[i] = Random.FromRange(0, k + 1);
                    }
                    NormalizeRepresentation(Representation);
                }
            }
        }

        public override string ToString()
            => ToString(r => string.Join(" ", r));

        public List<List<int>> ToCommunities()
        {
            return Representation.Select((c, i) => (c, i))
                .GroupBy(x => x.c)
                .Select(g => g.Select(x => x.i).ToList())
                .ToList();
        }

        public override bool Equals(object obj)
        {            
            if (!(obj is CommunityFindingChromosome))
                return false;
            CommunityFindingChromosome other = (CommunityFindingChromosome)obj;
            return Enumerable.SequenceEqual(Representation, other.Representation);
        }
    }
}
