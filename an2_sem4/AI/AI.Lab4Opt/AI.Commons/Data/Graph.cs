using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Commons.Data
{
    public class Graph<T>
    {
        public List<T> Nodes { get; set; } = new List<T>();
        public List<(int Source, int Target)> Edges=new List<(int Source, int Target)>();

        public Graph() { }
        public Graph(List<T> nodes)
        {
            Nodes = nodes;
        }
        
        public Graph(List<T> nodes, List<(int, int)> edges) : this(nodes)
        {
            Edges = edges;
        }

        public int[,] GetAdjacencyMatrix()
        {
            int[,] matrix = new int[Nodes.Count, Nodes.Count];

            foreach (var (u, v) in Edges) 
            {
                matrix[u, v] = matrix[v, u] = 1;
            }
            return matrix;
        }


        public string ToString(Func<T, string> formatter)
        {
            List<string> s = new List<string>();
            s.Add("graph");
            s.Add("[");
            s.Add("directed 0");
            foreach (var n in Nodes)
                s.Add($"  node [ {formatter(n)} ]");
            foreach (var ed in Edges.Distinct())
                if (ed.Source < ed.Target)
                    s.Add($"  edge [ source {ed.Source} target {ed.Target}]");
            s.Add("]");
            return string.Join(Environment.NewLine, s);
        }

        public override string ToString() => ToString(n => n.ToString());

        public Graph<U> CastNodes<U>(Func<T, U> converter)
            => new Graph<U>(Nodes.Select(converter).ToList(), Edges);

        public Graph<int> NormalizeNodes()
            => new Graph<int>(Nodes.Select((n, i) => i).ToList(), Edges);
    }
}
