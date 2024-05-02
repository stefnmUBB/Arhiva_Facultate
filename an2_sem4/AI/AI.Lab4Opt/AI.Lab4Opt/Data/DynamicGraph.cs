using AI.Commons.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AI.Lab4Opt.Data
{
    internal class DynamicGraph
    {
        public class EdgeRecord
        {
            public int Source { get; }
            public int Target { get; }
            public double Cost { get; }
            public int Timestamp { get; }

            public EdgeRecord(int source, int target, double cost, int timestamp)
            {
                Source = source;
                Target = target;
                Cost = cost;
                Timestamp = timestamp;
            }
        }

        SortedDictionary<int, WeightedGraph<int, double>> Graphs;

        public DynamicGraph(WeightedGraph<int,double> graph)
        {
            Graphs = new SortedDictionary<int, WeightedGraph<int, double>>
            {
                { 0, graph }
            };
            NodesCount = graph.Nodes.Count;
            ResetStream();
        }

        public DynamicGraph(IEnumerable<EdgeRecord> records)
        {
            NodesCount = records.Select(x => new int[2] { x.Source, x.Target })
                .SelectMany(x => x)
                .Max() + 1;
            Graphs = new SortedDictionary<int, WeightedGraph<int, double>>(
                records.GroupBy(r => r.Timestamp)
                    .Select(g => (g.Key, new WeightedGraph<int, double>(NodesCount,
                                            g.Select(x => (x.Source, x.Target, x.Cost))))
                    )
                    .ToDictionary(x => x.Key, x => x.Item2)
                );

            ResetStream();
        }

        int TimeIndex = 0;

        public void ResetStream()
        {
            TimeIndex = -1;
        }

        public WeightedGraph<int, double> GetNextGraph()
        {
            if (TimeIndex >= Graphs.Count - 1) 
                return null;
            return Graphs.ElementAt(++TimeIndex).Value;
        }

        public static DynamicGraph FromStream(Stream stream, bool undirected)
        {
            var edges = new List<EdgeRecord>();
            using (var reader = new StreamReader(stream, Encoding.UTF8, true)) 
            {
                while (!reader.EndOfStream)
                {
                    var item = reader.ReadLine().Split(' ').ToArray();
                    if (item.Length == 4)
                    {
                        var source = int.Parse(item[0]);
                        var dest = int.Parse(item[1]);
                        var cost = double.Parse(item[2]);
                        var timestamp = int.Parse(item[3]);
                        edges.Add(new EdgeRecord(source, dest, cost, timestamp));
                        if(undirected)
                            edges.Add(new EdgeRecord(dest, source, cost, timestamp));
                    }
                    else if (item.Length == 3)
                    {
                        var source = int.Parse(item[0]);
                        var dest = int.Parse(item[1]);
                        var cost = 1;
                        var timestamp = int.Parse(item[2]);
                        edges.Add(new EdgeRecord(source, dest, cost, timestamp));
                        if (undirected)
                            edges.Add(new EdgeRecord(dest, source, cost, timestamp));
                    }
                }
            }
            return new DynamicGraph(edges);
        }

        public static DynamicGraph FromBytes(byte[] bytes, bool undirected = true)
        {
            using(var stream = new MemoryStream(bytes))
            {
                return FromStream(stream, undirected);
            }
        }

        public WeightedGraph<int, double> CurrentGraph =>
            (TimeIndex >= Graphs.Count) ? null : Graphs.ElementAt(TimeIndex).Value;

        public bool HasNextGraph => TimeIndex < Graphs.Count - 1;

        public int NodesCount { get; }
    }
}
