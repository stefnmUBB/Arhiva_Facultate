using AI.Commons.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace AI.Lab4Opt.Data
{
    internal class Simulation
    {
        DynamicGraph DynamicGraph { get; }

        WeightedGraph<int, double> CurrentGraph;
        double[,] Pheromones;
        double[,] InvDistance;
        public double[,] Distances;
        int[,] AdjMatrix;

        public Ant[] Ants;

        public double Alpha { get; set; } = 1;
        public double Beta { get; set; } = 1;
        public double Q { get; set; } = 2;
        public double EvaporationRate { get; set; } = 0.1;

        public int StartNode { get; }
        public int FinishNode { get; }


        public Simulation(DynamicGraph graph, int startNode, int finishNode)
        {
            DynamicGraph = graph;
            StartNode = startNode;
            FinishNode = finishNode;

            Pheromones = new double[DynamicGraph.NodesCount, DynamicGraph.NodesCount];
            InvDistance = new double[DynamicGraph.NodesCount, DynamicGraph.NodesCount];
            Distances = new double[DynamicGraph.NodesCount, DynamicGraph.NodesCount];

            Ants = new Ant[3 * DynamicGraph.NodesCount];
            for (int i = 0; i < Ants.Length; i++) 
            {
                Ants[i] = new Ant();
                Ants[i].Reset(StartNode);                
            }
            NextGraph();
        }

        int NodesCount => DynamicGraph.NodesCount;

        public void NextGraph()
        {
            CurrentGraph = DynamicGraph.GetNextGraph();            
            AdjMatrix = CurrentGraph.GetAdjacencyMatrix();
            for(int i=0;i<NodesCount;i++)
            {
                for(int j=0;j<NodesCount;j++)
                {
                    if (Pheromones[i, j] != 0 && AdjMatrix[i, j] != 0) 
                        Pheromones[i, j] = 0;
                    InvDistance[i, j] = 0;
                    Distances[i, j] = 0;
                }
            }
            
            foreach(var e in CurrentGraph.Edges)
            {
                InvDistance[e.Source, e.Target] = 1 / e.Data;
                Distances[e.Source, e.Target] = e.Data;
            }

            /*Debug.WriteLine("---------------------------------------");
            for(int i=0;i<NodesCount;i++)
            {
                for (int j = 0; j < NodesCount; j++)
                    Debug.Write($"{AdjMatrix[i, j]} ");
                Debug.WriteLine("");
            }*/
        }

        public void DumpEdges()
        {
            foreach (var e in CurrentGraph.Edges)
            {
                Debug.WriteLine(e);
            }
        }

        public void RunStep()
        {
            for (int q = 0; q < Ants.Length; q++) 
            {
                var ant = Ants[q];
                ant.Reset(StartNode);

                while (ant.Solution.Count < NodesCount && ant.Solution.Last() != FinishNode)
                {
                    var i = ant.CurrentNode;
                    int j = ChooseAntsNextNode(q);
                    if (j < 0)
                        break;

                    ant.Travel(j);                    
                }                
            }

            for(int i=0;i<NodesCount;i++)
            {
                for(int j=0;j<NodesCount;j++)
                {
                    if (AdjMatrix[i, j] == 0) continue;
                    double delsum = 0;
                    for (int k = 0; k < Ants.Length; k++)
                    {
                        delsum = Ants[k].HasBeenOn(i, j) ? Q / Ants[k].TourLength : 0;
                    }

                    Pheromones[i, j] = (1 - EvaporationRate) * Pheromones[i, j] + delsum;
                }
            }            
        }

        public void DumpEdgesAndRates()
        {
            foreach(var e in CurrentGraph.Edges)
            {
                Debug.WriteLine($"{e.Source}, {e.Target} : {e.Data} => {Pheromones[e.Source, e.Target]}");
            }
        }

        int ChooseAntsNextNode(int k)
        {
            List<double> scales = new List<double>();
            List<int> values = new List<int>();

            var ant = Ants[k];

            int i = ant.CurrentNode;

            for (int l = 0; l < NodesCount; l++) 
            {
                //Debug.WriteLine($"Trying {i} {l}");
                if (AdjMatrix[i, l] != 0) 
                {                    
                    values.Add(l);
                    scales.Add(Math.Pow(InvDistance[i, l], Alpha) * Math.Pow(InvDistance[i, l], Beta));
                }
            }            

            if (values.Count == 0)
                return -1;

            double s = 0;
            for (int q = 0; q < scales.Count; q++) 
            {
                s += scales[q];
                scales[q] = s;
            }

            var rand = Commons.Utils.Random.Real() * s;

            if (rand < scales[0])
                return values[0];

            for (int j = 1; j < scales.Count; j++) 
            {
                if (scales[j - 1] <= rand && rand < scales[j])
                    return values[j];
            }
            return values.Last();
        }

        public int GetBestNeighbor(int i, IEnumerable<int> visited)
        {
            var neighbors = new SortedDictionary<double, int>();

            for(int j=0;j<NodesCount;j++)
            {
                if (AdjMatrix[i, j] == 0) continue;
                if (visited.Contains(j)) continue;
                neighbors[-Pheromones[i, j]] = j;
            }
            
            if (neighbors.Count() == 0)
                return -1;
            return neighbors.First().Value;
        }

    }
}
