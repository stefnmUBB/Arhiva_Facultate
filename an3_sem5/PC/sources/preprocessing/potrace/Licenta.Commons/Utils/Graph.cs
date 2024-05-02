using System.Collections.Generic;
using System.Linq;

namespace Licenta.Commons.Utils
{
    public class Graph<T> where T:class
    {
        private HashSet<(T U, T V)> Edges = new HashSet<(T U, T V)>();

        private List<T> Nodes = new List<T>();
        private Dictionary<T, HashSet<T>> Neighbors = new Dictionary<T, HashSet<T>>();

        public void AddEdge(T u, T v)
        {
            Edges.Add((u, v));
        }

        public void Compile()
        {
            Nodes.Clear();
            Neighbors.Clear();

            Nodes.AddRange(Edges.SelectMany(_ => new[] { _.U, _.V }).Distinct());

            foreach (var e in Edges)
                GetNeighbors(e.U).Add(e.V);
        }

        private HashSet<T> GetNeighbors(T t)
            => Neighbors.TryGetValue(t, out var result) ? result : (Neighbors[t] = new HashSet<T>());     
    }
}
