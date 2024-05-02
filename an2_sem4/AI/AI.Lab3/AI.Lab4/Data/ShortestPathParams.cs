using AI.Commons.Data;
using System.Drawing.Text;

namespace AI.Lab4.Data
{
    internal enum ShortestPathGoal
    {
        Normal,
        AllNodes,
        Cycle
    }

    internal class ShortestPathParams
    {
        public WeightedGraph<int,int> Graph { get; }
        public int[,] EdgeCost { get; }

        public int StartNode { get; set; }
        public int FinishNode { get; set; }


        public int MissingNodePenalty { get; set; }
        public int DupeNodePenalty { get; set; }
        public ShortestPathGoal Goal { get; set; } = ShortestPathGoal.Normal;

        public ShortestPathParams(WeightedGraph<int, int> graph, int[,] edgeCost, int startNode, int finishNode)
        {
            Graph = graph;
            EdgeCost = edgeCost;
            StartNode = startNode;
            FinishNode = finishNode;
        }

        public ShortestPathParams(WeightedGraph<int, int> graph, int startNode, int finishNode)
            : this(graph, graph.GetAdjacencyMatrixEdgeData(int.MaxValue), startNode, finishNode)
        { }
    }
}
