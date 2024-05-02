using AI.Commons.Data;
using System.Collections.Generic;

namespace AI.Commons.Algorithms.Community.Algorithms
{
    internal interface ICommunityFinder
    {
        List<List<int>> Find<T>(Graph<T> graph);
    }
}
