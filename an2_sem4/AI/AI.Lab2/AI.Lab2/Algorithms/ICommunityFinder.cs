using AI.Lab2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab2.Algorithms
{
    internal interface ICommunityFinder
    {
        List<List<int>> Find<T>(Graph<T> graph);
    }
}
