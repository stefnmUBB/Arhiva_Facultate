using AI.Commons.Algorithms.Genetic;
using AI.Commons.Data;
using System.Linq;
using AI.Commons.Utils;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace AI.Lab4.Data
{
    internal class ShortestPathChromosome : AbstractChromosome<ShortestPathParams, int[], long>
    {
        WeightedGraph<int, int> Graph => ProblemParam.Graph;
        int[,] EdgeCost => ProblemParam.EdgeCost;

        int StartNode => ProblemParam.StartNode;
        int FinishNode => ProblemParam.FinishNode;

        IEnumerable<int> Neighbors(int n)
            => Enumerable.Range(0, Graph.Nodes.Count).Where(i => EdgeCost[n, i] != int.MaxValue
                && EdgeCost[n, i] != 0);

        public int[] NormalizeCycle(int[] repres)
        {
            if (ProblemParam.Goal != ShortestPathGoal.Cycle)
                return repres;
            var mindex = Enumerable.Range(0, repres.Length)
                .Aggregate((a, b) => (repres[a] < repres[b]) ? a : b);
            var repr = new int[repres.Length];
            for(int i=0;i<repres.Length;i++)
            {
                repr[i] = repres[mindex];
                mindex++;
                if (mindex == repres.Length)
                    mindex = 0;
            }
            return repr;
        }


        public ShortestPathChromosome(ShortestPathParams problemParam) : base(problemParam)
        {
            List<int> repr = new List<int>();
            int n = StartNode;
            if (ProblemParam.Goal == ShortestPathGoal.Cycle) 
            {
                n = Commons.Utils.Random.FromRange(0, Graph.Nodes.Count);
            }            
            repr.Add(n);

            if (ProblemParam.Goal != ShortestPathGoal.Cycle)
            {
                while (n != FinishNode && repr.Count < 4 * Graph.Nodes.Count / 3)
                {
                    var neighbors = Neighbors(n).OrderBy(x => ProblemParam.EdgeCost[n, x]);
                    if (neighbors.Count() == 0)
                        break;
                    n = Commons.Utils.Random.Decision(0.2) ?
                        Commons.Utils.Random.ValueFromContainer(neighbors)
                        : neighbors.ElementAt(Commons.Utils.Random.FromRange(0, Math.Min(3, neighbors.Count())));
                    repr.Add(n);
                }
                if (repr[repr.Count - 1] != FinishNode)
                    repr.Add(FinishNode);
            }      
            else
            {
                while(repr.Count < Graph.Nodes.Count)
                {
                    var neighbors = Neighbors(n).OrderBy(x => ProblemParam.EdgeCost[n, x]);
                    if (neighbors.Count() == 0)
                        break;
                    n = Commons.Utils.Random.Decision(0.5) ?
                        Commons.Utils.Random.ValueFromContainer(neighbors)
                        : neighbors.ElementAt(Commons.Utils.Random.FromRange(0, Math.Min(3, neighbors.Count())));
                    repr.Add(n);                    
                }
            }

            SetRepresentation(NormalizeCycle(repr.ToArray()));
        }

        public override void Mutate()
        {
            double p = Commons.Utils.Random.Real();
            if (p < 0.15)
            {
                int index = Commons.Utils.Random.FromRange(1, Representation.Length);
                if (Representation.Length<=2 || Representation.Length < Graph.Nodes.Count || Commons.Utils.Random.Decision(0.5))
                {
                    var repr = new List<int>(Representation);                    
                    int node = Commons.Utils.Random.FromRange(0, Graph.Nodes.Count);
                    repr.Insert(index, node);
                    SetRepresentation(NormalizeCycle(repr.ToArray()));
                }
                else 
                {
                    var repr = Representation.ToList();
                    repr.RemoveAt(index);
                    SetRepresentation(NormalizeCycle(repr.ToArray()));
                }
            }
            else if (p < 0.4)
            {
                var index = Commons.Utils.Random.FromRange(0, Representation.Length);
                var node = Commons.Utils.Random.FromRange(0, Graph.Nodes.Count);
                Representation[index] = node;
                SetRepresentation(NormalizeCycle(Representation));
            }
            else if (p < 0.8) 
            {                
                var dupes = Representation.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key)
                    .OrderBy(x => Guid.NewGuid());
                int cnt = Commons.Utils.Random.FromRange(0, dupes.Count());

                for (int i = 1; i <= cnt; i++)
                {
                    if (dupes.Count() > 0)
                    {
                        var node = dupes.First();
                        int k = 0;
                        SetRepresentation(NormalizeCycle(Representation.Where(n => n == node && (k++) == 0).ToArray()));
                    }

                    dupes = Representation.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key)
                    .OrderBy(x => Guid.NewGuid());
                }
            }
            else
            {
                int n = Commons.Utils.Random.FromRange(1, 5);
                for (int i = 0; i < n; i++) 
                {
                    int u = Commons.Utils.Random.FromRange(1, Representation.Length - 1);
                    int v = Commons.Utils.Random.FromRange(1, Representation.Length - 1);
                    if (ProblemParam.Goal == ShortestPathGoal.Cycle)
                    {
                        u = Commons.Utils.Random.FromRange(0, Representation.Length);
                        v = Commons.Utils.Random.FromRange(0, Representation.Length);
                    }
                    (Representation[u], Representation[v]) = (Representation[v], Representation[u]);
                }
                SetRepresentation(NormalizeCycle(Representation));
            }

        }


        public override List<IChromosome<ShortestPathParams, int[], long>> PerformCrossover(IChromosome<ShortestPathParams, int[], long> other)
        {
            var offsprings = new List<IChromosome<ShortestPathParams, int[], long>>();

            var joints = Representation.Select((n, i) => (n, i))
                .Where(x => x.i < other.Representation.Length && other.Representation[x.i] == x.n
                         && x.i != 0 && x.i != Representation.Length - 1)
                .Select(x => x.i);
            if (joints.Count() == 0)
            {
                if (Commons.Utils.Random.Decision(0.5))
                {
                    var mask = Enumerable.Range(0, Representation.Length).Select(x => Commons.Utils.Random.FromRange(0, 2)).ToArray();
                    var rlen = Math.Min(Representation.Length, other.Representation.Length);
                    int optlen = Commons.Utils.Random.FromRange(rlen,
                        Math.Max(Representation.Length, other.Representation.Length));
                    var repr = new int[optlen];
                    for (int i = 0; i < rlen; i++)
                        repr[i] = mask[i] * Representation[i] + (1 - mask[i]) * other.Representation[i];

                    var r = Representation.Length > other.Representation.Length ? Representation : other.Representation;
                    for (int i = rlen; i < optlen; i++)
                        repr[i] = r[i];

                    repr[repr.Length - 1] = ProblemParam.FinishNode;

                    var chromo = new ShortestPathChromosome(ProblemParam);
                    chromo.SetRepresentation(NormalizeCycle(repr));
                    offsprings.Add(chromo);
                }
                else
                {
                    int i1 = Commons.Utils.Random.IndexFromContainer(Representation);
                    int i2 = Commons.Utils.Random.IndexFromContainer(other.Representation);

                    List<int> repr1 = new List<int>();
                    List<int> repr2 = new List<int>();

                    repr1.AddRange(Representation.Take(i1));
                    repr1.AddRange(other.Representation.Skip(i2));

                    repr2.AddRange(other.Representation.Take(i2));
                    repr2.AddRange(Representation.Skip(i1));

                    var chromo1 = new ShortestPathChromosome(ProblemParam);
                    chromo1.SetRepresentation(NormalizeCycle(repr1.ToArray()));
                    offsprings.Add(chromo1);

                    var chromo2 = new ShortestPathChromosome(ProblemParam);
                    chromo2.SetRepresentation(NormalizeCycle(repr2.ToArray()));
                    offsprings.Add(chromo2);
                }
            }
            else
            {
                var parents = new IChromosome<ShortestPathParams, int[], long>[2] { this, other };
                int pid = Commons.Utils.Random.Decision(0.5) ? 1 : 0;
                List<int> repr1 = new List<int>();
                List<int> repr2 = new List<int>();
                var Q = new Queue<int>(joints);
                int i = 0;
                while (Q.Count > 0)
                {
                    var j = Q.Dequeue();
                    for (; i <= j; i++)
                    {
                        repr1.Add(parents[pid].Representation[i]);
                        repr2.Add(parents[1-pid].Representation[i]);
                    }
                    pid = 1 - pid;
                }

                int k = i;
                for (; k < parents[pid].Representation.Length; k++)
                {
                    repr1.Add(parents[pid].Representation[k]);
                }
                k = i;
                pid = 1 - pid;
                for (; k < parents[pid].Representation.Length; k++)
                {
                    repr2.Add(parents[pid].Representation[k]);
                }

                var chromo1 = new ShortestPathChromosome(ProblemParam);
                chromo1.SetRepresentation(NormalizeCycle(repr1.ToArray()));
                offsprings.Add(chromo1);

                var chromo2 = new ShortestPathChromosome(ProblemParam);
                chromo2.SetRepresentation(NormalizeCycle(repr2.ToArray()));
                offsprings.Add(chromo2);
            }
            return offsprings;
        }

        protected override long ComputeFitness()
        {
            if (Representation.Length == 0)
                return -int.MaxValue;

            switch(ProblemParam.Goal)
            {
                case ShortestPathGoal.Normal:
                    return NormalFitness();                    
                case ShortestPathGoal.AllNodes:
                    return AllNodesFitness();
                case ShortestPathGoal.Cycle:
                    return CycleFitness();
            }

            return -int.MaxValue;
        }

        public long NormalFitness()
        {                            
            long value = 0;

            for (int i = 1; i < Representation.Length; i++)
            {
                var cost = EdgeCost[Representation[i], Representation[i - 1]];
                if (cost == int.MaxValue)
                    cost = Graph.Nodes.Count * ProblemParam.MissingNodePenalty;
                value += cost;
            }

            if (Representation.First() != StartNode || Representation.Last() != FinishNode)
                value += 2 * ProblemParam.MissingNodePenalty;

            return -value;
        }

        public long AllNodesFitness()
        {            
            long value = 0;

            for (int i = 1; i < Representation.Length; i++)
            {
                var cost = EdgeCost[Representation[i], Representation[i - 1]];
                if (cost == int.MaxValue)
                    cost = Graph.Nodes.Count * ProblemParam.MissingNodePenalty;
                value += cost;
            }

            int missingNodes = Graph.Nodes.Count - Representation.Distinct().Count();
            value += Graph.Nodes.Count * ProblemParam.MissingNodePenalty * missingNodes;
            int duplicateNodes = Representation.GroupBy(x => x).Where(g => g.Count() > 1).Count();
            value += Graph.Nodes.Count * ProblemParam.DupeNodePenalty * duplicateNodes;

            if (Representation.First() != StartNode || Representation.Last() != FinishNode)
                value += 2 * ProblemParam.MissingNodePenalty;

            return -value;
        }


        public long CycleFitness()
        {
            long value = 0;

            for (int i = 1; i < Representation.Length; i++)
            {
                var cost = EdgeCost[Representation[i], Representation[i - 1]];
                if (cost == int.MaxValue)
                    cost = Graph.Nodes.Count * ProblemParam.MissingNodePenalty;
                value += cost;
            }
            {
                var cost = EdgeCost[Representation[0], Representation[Representation.Length - 1]];
                if (cost == int.MaxValue)
                    cost = Graph.Nodes.Count * ProblemParam.MissingNodePenalty;
                value += cost;
            }

            int missingNodes = Graph.Nodes.Count - Representation.Distinct().Count();
            value += Graph.Nodes.Count * ProblemParam.MissingNodePenalty * missingNodes;
            int duplicateNodes = Representation.GroupBy(x => x).Where(g => g.Count() > 1).Count();
            value += Graph.Nodes.Count * ProblemParam.DupeNodePenalty * duplicateNodes;

            return -value;
        }
        public override string ToString() => $"R=[{string.Join(" ", Representation)}], F={Fitness}";

        public override bool Equals(object obj)
            => obj is ShortestPathChromosome chromo && Representation.SequenceEqual(chromo.Representation);

        public static ShortestPathChromosome GenerateGreedyCircular(ShortestPathParams problemParams)
        {
            var chromo = new ShortestPathChromosome(problemParams);            
            var nodes = Enumerable.Range(0, chromo.Graph.Nodes.Count).ToList();            
            var n = Commons.Utils.Random.IndexFromContainer(nodes);            
            var repr = new List<int> { n };
            nodes.Remove(n);

            while (nodes.Count>0)
            {
                var neighbors = nodes
                    .Where(x => problemParams.EdgeCost[x, n] != 0 && problemParams.EdgeCost[x, n] != int.MaxValue)
                    .OrderBy(x => problemParams.EdgeCost[x, n]);
                if (neighbors.Count() < 2) 
                    break;
                n = neighbors.ElementAt(Commons.Utils.Random.FromRange(0, neighbors.Count() / 2));
                repr.Add(n);
                nodes.Remove(n);
            }

            while (nodes.Count > 0)
            {
                n = Commons.Utils.Random.ValueFromContainer(nodes);
                nodes.Remove(n);
                repr.Add(n);
            }

            chromo.SetRepresentation(chromo.NormalizeCycle(repr.ToArray()));

            return chromo;
        }
    }
}
