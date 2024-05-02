using AI.Commons.Data;
using AI.Commons.Utils;
using System.Collections.Generic;
using System.Linq;

namespace AI.Commons.Algorithms.Community.Algorithms
{
    internal class NewmanFinder : ICommunityFinder
    {
        class CommunityTracker
        {
            int NodesCount;
            int EdgesCount;
            int[,] e;
            int[] a;
            public int Q;

            public int EdgesBetweenCommunities(int i, int j) => e[i, j];

            private CommunityTracker(int nodes_count, int edgesCount)
            {
                NodesCount = nodes_count;
                e = new int[NodesCount, NodesCount];
                a = new int[NodesCount];
                EdgesCount = edgesCount;
            }

            /// <summary>
            /// Creates matrix parameters from given graph
            /// Θ(N^2)
            /// </summary>            
            /// <returns></returns>
            public static CommunityTracker FromGraph<T>(Graph<T> graph)
            {
                CommunityTracker tracker = new CommunityTracker(graph.Nodes.Count, graph.Edges.Count);

                tracker.e = graph.GetAdjacencyMatrix();

                for (int u = 0; u < tracker.NodesCount; u++)
                {
                    for (int v = 0; v < tracker.NodesCount; v++)
                        tracker.a[u] += tracker.e[u, v];
                }

                for (int u = 0; u < tracker.NodesCount; u++)
                    tracker.Q += tracker.EdgesCount * tracker.e[u, u] - tracker.a[u] * tracker.a[u];                

                return tracker;
            }

            /// <summary>
            /// Determines how the modularity Q changes supposing communities i and j join (∆Q)
            /// Θ(1)
            /// </summary>            
            public int GetOptimizationDelta(int i, int j)
            {
                return 2 * (EdgesCount * e[i, j] - a[i] * a[j]);
            }

            /// <summary>
            /// Updates the matrix state following joining communities i and j
            /// O(NodesCount)
            /// </summary>            
            public void JoinCommunities(int i, int j)
            {
                if (i == j) return;
                
                int delQ = GetOptimizationDelta(i, j);

                e[i, i] += e[j, j] + 2 * e[i, j];
                a[i] += e[j, j] + e[i, j];
                e[i, j] = e[j, i] = 0;            
                e[j, j] = 0;

                for (int k=0;k<NodesCount;k++)
                {
                    if (k == i || k == j)
                        continue;
                    e[i, k] += e[j, k]; a[i] += e[j, k];
                    e[k, i] += e[k, j];


                    e[j, k] = e[k, j] = 0;
                }

                a[j] = 0;                                
                Q += delQ;               
           }

           public double normQ => 1.0 * Q / (EdgesCount * EdgesCount);
       }        

       public List<double> QStamps = new List<double>();

       /// <summary>
       /// Node in dendogram
       /// </summary>
       class DendrogramNode
       {
           public List<int> Community = new List<int>();
           public DendrogramNode[] Children = new DendrogramNode[2] { null, null };

           public DendrogramNode(List<int> community)
           {
               Community = community;                
           }

           /// <summary>
           /// Joins two dendograms which become part of the same communities
           /// O(N)
           /// </summary>                        
           public DendrogramNode Join(DendrogramNode other)
           {
               var parent = new DendrogramNode(Community.Concat(other.Community).ToList());

               parent.Children[0] = this;
               parent.Children[1] = other;
               return parent;
           }

           public override string ToString()
           {
               var nodes_list = string.Join(", ", Community);

               string text =
               $@"(
Nodes:
   ({nodes_list}),
Child0:
   {(Children[0] == null ? "null" : Children[0].ToString().Tab())}
Child1:
   {(Children[1] == null ? "null" : Children[1].ToString().Tab())}
)";
               return text;
           }

           public bool IsLeaf => Children[0] == null && Children[1] == null;
       }

       /// <summary>
       /// class to store a community splitting configuration along with its modularity
       /// </summary>
       public class CommunityGrouping
       {
           public double Q { get; }
           public List<List<int>> Communities { get; }
           public CommunityGrouping(double q, List<List<int>> communities)
           {
               Q = q;
               Communities = communities.OrderBy(c => c[0]).ToList();
           }
       }

       public List<CommunityGrouping> Groupings = new List<CommunityGrouping>();

       /// <summary>
       /// Method that finds a community structure in a given graph using the greedy Newman algorithm
       /// Input: 
       ///     the graph
       /// Output:
       ///     1. Return: a community structure chosen based on the maximum modularity while 
       ///        performing community joins until there are no isolated nodes
       ///     2. In this.Groupings : a list of community structures found by trnasversal cuts in 
       ///        the identified dendrogram. The returned structured may or may not be part of the Groupings
       ///                
       ///     Complexity:
       ///     
       ///     Time O(N*(M+N)), Memory Θ(M+N*N) for community joins
       ///     
       ///     Time O(N^3), Memory O(N^2) for transversal BFS & finding groupings
       /// </summary>                
       public List<List<int>> Find<T>(Graph<T> graph)
       {
           Groupings.Clear();

           if (graph.Nodes.Count == 0)
               return new List<List<int>> { new List<int> { } };
           if (graph.Nodes.Count == 1)
               return new List<List<int>> { new List<int> { 0 } };

           int nodes_count = graph.Nodes.Count;
           int[] community = new int[nodes_count];
           HashSet<(int U, int V)> edges = new HashSet<(int U, int V)>();
           graph.Edges.ForEach(e => edges.Add(e));
           Dictionary<int, int> commSize = new Dictionary<int, int>();

           DendrogramNode[] dendograms = new DendrogramNode[nodes_count];

           CommunityTracker tracker = CommunityTracker.FromGraph(graph);

           for (int i = 0; i < nodes_count; i++)
           {
               community[i] = i;
               commSize[i] = 1;
               dendograms[i] = new DendrogramNode(new List<int> { i });
           }

           int Q = int.MinValue;
           List<List<int>> result = new List<List<int>>();

           bool register_result = true;

           int n = edges.Count;
           for (int i = 0; i < n; i++) 
           {
               if (edges.Count == 0)
                   break;

               var edge = edges
                   .Where(e => tracker.EdgesBetweenCommunities(community[e.U], community[e.V]) != 0)
                   .MaxBy(e => tracker.GetOptimizationDelta(community[e.U], community[e.V]));
               if(edge==default)
               {                    
                   break;
               }

               int com1 = community[edge.U];
               int com2 = community[edge.V];

               int dQ = tracker.GetOptimizationDelta(com1, com2);                              

               if (commSize[com1] < commSize[com2]) 
               {
                   (com1, com2) = (com2, com1);
               }

               tracker.JoinCommunities(com1, com2);                

               community[edge.U] = community[edge.V] = com1;

               var new_com = dendograms[com1].Join(dendograms[com2]);

               for (int q = 0; q < nodes_count; q++)
               {
                   if (community[q] == com1)
                   {
                       dendograms[q] = new_com;
                   }
                   else if (community[q] == com2)
                   {
                       dendograms[q] = new_com;
                       community[q] = com1;
                       commSize[com1]++;
                       commSize[com2]--;
                   }
               }

               edges.RemoveWhere(e => community[e.U] == community[e.V]);      

               var res = community
                   .Select((com, node) => (com, node))
                   .GroupBy(x => x.com)
                   .Select(g => g.Select(x => x.node).ToList())
                   .ToList();                

               if (register_result && tracker.Q > Q) 
               {
                   Q = tracker.Q;
                   result = res;
               }

               QStamps.Add(tracker.normQ);

               if (res.All(l => l.Count > 1))
               {
                   register_result = false;
               }
           }

           var dends = dendograms.Distinct().ToList();
           //dends.ForEach(x => Debug.WriteLine(x));

           List<DendrogramNode> level = new List<DendrogramNode>();
           dends.ForEach(level.Add);

           bool next_valid = true;                   

           List<DendrogramNode> leaves = new List<DendrogramNode>();

           QStamps.Clear();                     

           do
           {
               List<List<int>> communities = level.Select(d => d.Community.OrderBy(i=>i).ToList()).ToList();
               //Debug.WriteLine(string.Join(",", communities.Select(c => $"({string.Join(" ", c)})")));

               var comnodes = communities.Select((com, i) => com.Select(nd => (i, nd)).ToList())
               .SelectMany(x => x).OrderBy(x => x.nd).Select(x => x.i).ToArray();

               //Debug.WriteLine(string.Join(",", comnodes));

               int e(int i, int j) => graph.Edges.Count(
                   edge => (comnodes[edge.Source] == i && comnodes[edge.Target] == j)
                        || (comnodes[edge.Source] == j && comnodes[edge.Target] == i)) / (i == j ? 1 : 2);
               int a(int i) => graph.Edges.Count(edge => comnodes[edge.Source] == i && comnodes[edge.Target] == i)
                            + graph.Edges.Count(edge => (comnodes[edge.Source] == i && comnodes[edge.Target] != i)
                                                     || (comnodes[edge.Source] != i && comnodes[edge.Target] == i)) / 2;

               int coms_count = comnodes.Max() + 1;

               /*for (int i = 0; i <coms_count;i++)
               {
                   for(int j=0;j<coms_count;j++)
                   {
                       Debug.Write($"{e(i, j)} ");
                   }
                   Debug.WriteLine($"| {a(i)}");                    
               }
               Debug.WriteLine("-------");*/

                int tQ = 0;
                for (int i = 0; i < coms_count; i++)
                    tQ += graph.Edges.Count * e(i, i) - a(i) * a(i);

                QStamps.Add(1.0 * tQ / (graph.Edges.Count * graph.Edges.Count));

                Groupings.Add(new CommunityGrouping(tQ, communities));                

                leaves = level.Where(d => d.IsLeaf).ToList();                

                next_valid = level.Any(d => !d.IsLeaf);
                if(next_valid)
                {
                    var next_level = new List<DendrogramNode>();
                    level.ForEach(d => next_level.AddRange(d.Children.Where(c=>c!=null)));
                    next_level.AddRange(leaves);
                    level = next_level;
                }

            }
            while (next_valid);                                

            return result;          
        }
    }
}
