
using System;

namespace Licenta.Commons.Parallelization
{
    public class InputGraphNode : ParallelGraphNode
    {
        public int Id { get; }

        public Type Type { get; }

        public InputGraphNode(int id, Type type) : base(null)
        {
            Id = id;
            Type = type;
        }
    }
}
