using System;
using System.Collections.Generic;
using System.Linq;

namespace Licenta.Commons.Parallelization
{
    public class ParallelGraphModel
    {
        private readonly List<ParallelGraphNode> Outputs = new List<ParallelGraphNode>();
        private readonly List<InputGraphNode> Inputs = new List<InputGraphNode>();

        public ParallelGraphNode CreateNestedModelNode(ParallelGraphModel model, ParallelGraphNode input1)
            => CreateNode<object>(in1 => model.RunSync(new[] { in1 }), input1);

        public ParallelGraphNode CreateNestedModelNode(ParallelGraphModel model, ParallelGraphNode input1, int resultIndex)
            => CreateNode<object>(in1 => model.RunSync(new[] { in1 })[resultIndex], input1);        

        public ParallelGraphNode CreateIndexAccessNode(int index, ParallelGraphNode input)
        {
            return CreateNode((object arr) => (arr as object[])[index], input);
        }

        public ParallelGraphNode CreateNode(Delegate del) => new ParallelGraphNode(del);
        public ParallelGraphNode CreateNode(Func<object> func) => new ParallelGraphNode(func);        

        public ParallelGraphNode CreateNode<T>(Func<T, object> func, ParallelGraphNode input1)
        {
            var node = CreateNode(func);
            node.Dependencies.Add(input1);
            return node;
        }

        public ParallelGraphNode CreateNode<T1, T2>(Func<T1, T2, object> func, ParallelGraphNode input1, ParallelGraphNode input2)
        {
            var node = CreateNode(func);
            node.Dependencies.Add(input1);
            node.Dependencies.Add(input2);
            return node;
        }

        public ParallelGraphNode CreateOutput(Delegate del)
        {
            var node = CreateNode(del);
            Outputs.Add(node);
            return node;
        }

        public ParallelGraphNode CreateOutput(ParallelGraphNode input1) => CreateOutput<object>(_ => _, input1);

        public ParallelGraphNode CreateOutput<T>(Func<T, object> func, ParallelGraphNode input1)
        {
            var node = CreateOutput(func);
            node.Dependencies.Add(input1);
            return node;
        }

        public ParallelGraphNode CreateOutput<T1, T2>(Func<T1, T2, object> func, ParallelGraphNode input1, ParallelGraphNode input2)
        {
            var node = CreateOutput(func);
            node.Dependencies.Add(input1);
            node.Dependencies.Add(input2);
            return node;
        }

        public ParallelGraphNode CreateInput<T>()
        {
            var node = new InputGraphNode(Inputs.Count, typeof(T));            
            Inputs.Add(node);
            return node;
        }              

        private readonly List<List<ParallelGraphNode>> Schedule = new List<List<ParallelGraphNode>>();

        public void Build()
        {
            Schedule.Clear();

            var visited = new HashSet<ParallelGraphNode>();

            var row = new HashSet<ParallelGraphNode>(Outputs);
            foreach (var n in row) visited.Add(n);

            while (row.Count > 0)
            {
                var nrow = new HashSet<ParallelGraphNode>();
                foreach (var n in row.SelectMany(_ => _.Dependencies).Distinct())
                    if (!visited.Contains(n))
                    {
                        visited.Add(n);
                        nrow.Add(n);
                    }

                Schedule.Add(row.ToList());
                row = nrow;
            }
        }

        public List<ParallelGraphNode> GetOutputs() => Outputs.ToList();
        public List<InputGraphNode> GetInputs() => Inputs.ToList();

        public List<ParallelGraphNode> GetAllNodes()
        {
            var nodes = new HashSet<ParallelGraphNode>();
            var s = new Stack<ParallelGraphNode>(Outputs);
            while(s.Count>0)
            {
                var n = s.Pop();
                nodes.Add(n);
                foreach (var d in n.Dependencies)
                    s.Push(d);
            }
            return nodes.ToList();
        }

        public object[] Run(TaskManager tm, object[] inputs)
        {
            bool ownsTm = false;
            if(tm==null)
            {
                ownsTm = true;
                tm = new TaskManager(1);
                tm.RunAsync();
            }
            var result = ParallelRuntimeGraph.FromModel(this).Execute(tm, inputs);

            if (ownsTm)
                tm.Stop();            

            return result;
        }

        public object[] RunSync(object[] inputs)
        {           
            return ParallelRuntimeGraph.FromModel(this).ExecuteSync(inputs);
        }
    }
}
