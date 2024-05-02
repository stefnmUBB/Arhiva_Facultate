using System;
using System.Collections.Generic;
using System.Reflection;

namespace Licenta.Commons.Parallelization
{
    public class ParallelGraphNode
    {
        public List<ParallelGraphNode> Dependencies { get; } = new List<ParallelGraphNode>();       

        public Delegate Delegate { get; }

        public ParallelGraphNode(Delegate @delegate)
        {
            Delegate = @delegate;
        }
    }
}
