using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab4Opt.Data
{
    internal class Ant
    {
        HashSet<(int Source, int Target)> UsedEdges = new HashSet<(int Source, int Target)>();
        HashSet<int> VisitedNodes = new HashSet<int>();
        public int CurrentNode { get; private set; }

        public List<int> Solution { get; } = new List<int>();

        public bool HasBeenOn(int i, int j) => UsedEdges.Contains((i, j));
        public bool HasVisited(int n) => VisitedNodes.Contains(n);
        public int TourLength { get; private set; } = 0;

        public void Reset(int startingNode)
        {
            UsedEdges.Clear();
            VisitedNodes.Clear();
            Solution.Clear();
            TourLength = 1;
            VisitedNodes.Add(startingNode);
            CurrentNode = startingNode;
            Solution.Add(startingNode);
        }        

        public void Travel(int j)
        {
            UsedEdges.Add((CurrentNode, j));
            VisitedNodes.Add(j);
            CurrentNode = j;
            Solution.Add(j);
            TourLength++;
        }
    }
}
