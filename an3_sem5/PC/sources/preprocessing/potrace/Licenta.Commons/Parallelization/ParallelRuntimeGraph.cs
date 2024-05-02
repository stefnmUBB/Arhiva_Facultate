using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Licenta.Commons.Parallelization
{
    internal class ParallelRuntimeGraph
    {
        public List<ParallelRuntimeNode> Nodes { get; } = new List<ParallelRuntimeNode>();
        public List<ParallelRuntimeNode> Outputs { get; } = new List<ParallelRuntimeNode>();
        public List<ParallelRuntimeNode> Inputs { get; } = new List<ParallelRuntimeNode>();

        private object[] InputValues;

        public object[] ExecuteSync(object[] inputs)
        {
            InputValues = inputs;

            var pendingNodes = new HashSet<ParallelRuntimeNode>(Nodes);            
            while (pendingNodes.Count > 0)
            {
                Thread.Sleep(10);
                foreach (var node in pendingNodes.ToArray())
                {
                    if (node.IsInputReady)
                    {
                        pendingNodes.Remove(node);
                        node.Execute();                        
                    }
                }
            }            
            return Outputs.Select(_ => _.Result).ToArray();
        }


        public object[] Execute(TaskManager tm, object[] inputs)
        {            
            InputValues = inputs;

            var pendingNodes = new HashSet<ParallelRuntimeNode>(Nodes);            
            var tg = tm.CreateTaskGroup();
            while (pendingNodes.Count > 0)
            {
                Thread.Sleep(2);
                foreach (var node in pendingNodes.ToArray()) 
                {                    
                    if (node.IsInputReady) 
                    {
                        pendingNodes.Remove(node);                        
                        tg.AddTask(node.Execute);
                    }
                }
            }
            tg.WaitAll();
            return Outputs.Select(_ => _.Result).ToArray();
        }

        public static ParallelRuntimeGraph FromModel(ParallelGraphModel model)
        {            
            var g = new ParallelRuntimeGraph();
            var allModelNodes = model.GetAllNodes();

            var allNodes = allModelNodes.Select(m => (model: m, runtime: m is InputGraphNode input 
                    ? new ParallelRuntimeNode(GetInputMethod(g, input.Id))
                    : new ParallelRuntimeNode(m.Delegate)))
                .ToDictionary(p => p.model, p => p.runtime);

            foreach(var kv in allNodes)
            {
                var m = kv.Key;
                var r = kv.Value;
                foreach(var d in m.Dependencies)
                {
                    r.InputNodes.Add(allNodes[d]);
                    allNodes[d].NextNodes.Add(r);
                }
            }

            var runtimeOutputs = model.GetOutputs().Select(_ => allNodes[_]).ToList();
            var runtimeInputs = model.GetInputs().Select(_ => allNodes[_]).ToList();            

            g.Nodes.AddRange(allNodes.Values);
            g.Inputs.AddRange(runtimeInputs);
            g.Outputs.AddRange(runtimeOutputs);

            g.InputValues = new object[runtimeInputs.Count];

            //g.Nodes.ForEach(Console.WriteLine);

            return g;
        }

        public void SetInput(int index, object obj) => InputValues[index] = obj;

        private static Delegate GetInputMethod(ParallelRuntimeGraph graph, int id) =>
            new Func<object>(() => graph.InputValues[id]);
    }
}
