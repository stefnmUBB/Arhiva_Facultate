using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab1.Solvers
{
    internal class IntFileInput
    {
        List<int> tokens;
        int pos = 0;

        public IntFileInput(string filename)
        {
            tokens = File.ReadAllText(filename).Split().Where(s => s != "").Select(int.Parse).ToList();
        }

        public int ReadInt()
        {
            return tokens[pos++];
        }

        public bool Ready => pos == tokens.Count;

        public Matrix ReadMatrix(int rows, int cols)
        {
            Matrix mat = new Matrix(rows, cols);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    mat[i, j] = ReadInt();
                }
            }
            return mat;
        }

        public Matrix ReadMatrix()
        {
            int n = ReadInt();
            int m = ReadInt();
            return ReadMatrix(n, m);
            
        }
    }
}
