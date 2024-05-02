using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Licenta.Commons.Parallelization
{
    internal class ParallelRuntimeNode
    {


        public List<ParallelRuntimeNode> InputNodes { get; } = new List<ParallelRuntimeNode>();
        public List<ParallelRuntimeNode> NextNodes { get; } = new List<ParallelRuntimeNode>();
        private Delegate Delegate { get; }


        private bool _IsReady = false;
        public bool IsReady => _IsReady;

        private object _Result = null;

        public ParallelRuntimeNode(Delegate @delegate)
        {            
            Delegate = @delegate;
        }

        public object Result => _Result;

        public bool IsInputReady => InputNodes.All(_ => _.IsReady);

        public void Execute()
        {
            //Debug.WriteLine($"[Parallel] Execute {Delegate.Method.Name}");
            try
            {
                _Result = Delegate.DynamicInvoke(InputNodes.Select(_ => _.Result).ToArray());
            }
            catch(Exception e)
            {
                throw;
                Debug.WriteLine(e.Message);                
                Environment.Exit(-1);
            }
            Debug.WriteLine($"[Parallel] SetReady {Delegate.Method.Name}");
            _IsReady = true;
        }

        public override string ToString()
        {
            return $"Node:{Delegate.Method.Name}; In:({string.Join(", ", InputNodes.Select(_ => _.Delegate.Method.Name))});" +
                $"Out:({string.Join(", ", NextNodes.Select(_ => _.Delegate.Method.Name))})";
        }
    }
}
