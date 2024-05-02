using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Commons.Data
{
    public class WeightedGraph<T, E>
    {
        public List<T> Nodes { get; set; } = new List<T>();
        public List<(int Source, int Target, E Data)> Edges = new List<(int Source, int Target, E Data)>();

        public WeightedGraph() { }
        public WeightedGraph(IEnumerable<T> nodes)
        {
            Nodes = nodes.ToList();
        }

        public WeightedGraph(int nodesCount, IEnumerable<(int Source, int Target, E)> edges)
        {
            Nodes = Enumerable.Range(0, nodesCount).Select(x => default(T)).ToList();
            Edges = edges.Where(e => e.Source < nodesCount && e.Target < nodesCount).ToList();
        }

        public WeightedGraph(IEnumerable<(int Source, int Target, E)> edges)
        {
            var nodesCount = edges.Select(e => new int[2] { e.Source, e.Target })
                .SelectMany(x => x)
                .Max() + 1;
            Nodes = Enumerable.Range(0, nodesCount).Select(x => default(T)).ToList();
            Edges = edges.ToList();
        }

        public WeightedGraph(List<T> nodes, List<(int, int, E)> edges) : this(nodes)
        {
            Edges = edges;
        }

        public WeightedGraph(List<T> nodes, List<(int, int)> edges) : this(nodes)
        {
            Edges = edges.Select(x => (x.Item1, x.Item2, default(E))).ToList();
        }

        public int[,] GetAdjacencyMatrix()
        {
            int[,] matrix = new int[Nodes.Count, Nodes.Count];

            foreach (var (u, v, _) in Edges) 
            {
                matrix[u, v] = 1;
            }
            return matrix;
        }

        public E[,] GetAdjacencyMatrixEdgeData(E edgePlaceHolder = default)
        {
            E[,] matrix = new E[Nodes.Count, Nodes.Count];
            for(int i=0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < Nodes.Count; j++)
                    matrix[i, j] = edgePlaceHolder;
            }

            foreach (var (u, v, data) in Edges) 
            {
                matrix[u, v] = data;
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
                    s.Add($"  edge [ source {ed.Source} target {ed.Target} data {ed.Data}]");
            s.Add("]");
            return string.Join(Environment.NewLine, s);
        }

        public override string ToString() => ToString(n => n.ToString());

        public WeightedGraph<U, E> CastNodes<U>(Func<T, U> converter)
            => new WeightedGraph<U, E>(Nodes.Select(converter).ToList(), Edges);

        public WeightedGraph<T, E2> CastEdgeData<E2>(Func<E, E2> converter)
        {
            return new WeightedGraph<T, E2>(Nodes.ToList(),
                Edges.Select(e => (e.Source, e.Target, converter(e.Data))).ToList());
        }

        public WeightedGraph<int, E> NormalizeNodes()
            => new WeightedGraph<int, E>(Nodes.Select((n, i) => i).ToList(), Edges);
    }
}
