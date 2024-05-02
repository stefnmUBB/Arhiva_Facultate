using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab1.Solvers
{
    internal abstract class FileIOProblem : Problem<string, string>
    {
        private MemoryStream Stream = new MemoryStream();
        public StreamWriter Writer;

        public FileIOProblem()
        {
            Writer = new StreamWriter(Stream);
        }

        private void ResetStream()
        {
            Stream.Close();
            Stream.Dispose();

            Writer.Close();
            Writer.Dispose();

            Stream = new MemoryStream();
            Writer = new StreamWriter(Stream);
        }        

        public sealed override void Solve()
        {            
            ReadInput();
            ResetStream();
            Run();
            Writer.Flush();
            Output = Encoding.ASCII.GetString(Stream.ToArray()).Replace("\r\n", "\n");
        }

        public abstract void ReadInput();
        public abstract void Run();

        public override void TestAssert(string input, string output)
        {
            output = File.ReadAllText(output).Replace("\r\n", "\n");
            base.TestAssert(input, output);
        }
    }
}
