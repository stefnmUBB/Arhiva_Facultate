using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab1.Solvers
{
    internal abstract class Problem<I,O>
    {
        public I Input { get; set; }
        public O Output { get; protected set; }
        public abstract void Solve();

        private static void Assert(bool condition)
        {
            if (!condition)
                throw new Exception();
        }

        bool CompareOutputTo(O output)
        {
            if(typeof(O) == typeof(string))
            {
                return Equals(output, Output);
            }
            if(typeof(IEnumerable).IsAssignableFrom(typeof(O)))
            {                                
                var elemType = typeof(O).GetGenericArguments()[0];
                return (bool)typeof(Enumerable).GetMethods()
                    .Where(m => m.Name == "SequenceEqual")
                    .Where(m => m.GetParameters().Length == 2)
                    .First()
                    .MakeGenericMethod(elemType)
                    .Invoke(null, new object[] { Output, output });                
            }
            return Equals(output, Output);
        }

        public virtual void TestAssert(I input, O output)
        {
            Input = input;
            Solve();
            Assert(CompareOutputTo(output));
        }
        public bool Test(I input, O output)
        {
            Input = input;
            Solve();
            return CompareOutputTo(output);
        }
    }
}
