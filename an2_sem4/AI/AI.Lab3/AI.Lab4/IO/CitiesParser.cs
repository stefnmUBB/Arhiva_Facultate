using AI.Commons.Data;
using AI.Commons.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AI.Lab4.IO
{
    public static class CitiesParser
    {
        public static WeightedGraph<int, int> FromCitiesLines(string[] lines)
        {
            int ncount = int.Parse(lines[0]);
            var graph = new WeightedGraph<int, int>(Enumerable.Range(0, ncount).ToList());

            for (int i = 0;i<ncount;i++)
            {
                lines[1 + i].Split(',').Select(x => int.Parse(x.Trim()))
                    .Select((c, j) => (c, j)).Where(x => x.c != 0).ToList()
                    .ForEach(r => graph.Edges.Add((i, r.j, r.c)));
            }            

            return graph;
        }                
    }
}
