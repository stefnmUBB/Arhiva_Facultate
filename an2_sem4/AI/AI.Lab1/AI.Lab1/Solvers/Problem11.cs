using System.Collections.Generic;

namespace AI.Lab1.Solvers
{
    internal class Problem11 : FileIOProblem
    {
        Matrix matrix;
        public override void ReadInput()
        {
            IntFileInput fin = new IntFileInput(Input);
            matrix = fin.ReadMatrix();
        }

        /*
         * Considerându-se o matrice cu n x m elemente binare (0 sau 1), să se înlocuiască cu 1 toate 
         * aparițiile elementelor egale cu 0 care sunt complet înconjurate de 1.
         * 
         * IO : Fisier
         * Date de intrare : 
         * n m
         * [matrice binara de n linii si m coloane]
         * Date de iesire : matricea in care "insulele" de 0 au fost inlocuite cu valori de 1
         * 
         * Complexitate: Θ(N*M), cu algoritmul lui Lee         
         * 
         * O(N*M) memorie
         */
        public override void Run()
        {
            for (int i = 0; i < matrix.RowsCount; i++)
                for (int j = 0; j < matrix.ColsCount; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        Lee(matrix, i, j);
                    }
                }

            for (int i = 0; i < matrix.RowsCount; i++)
                for (int j = 0; j < matrix.ColsCount; j++)
                {
                    if (matrix[i, j] == 2)
                    {
                        matrix[i, j] = 0; // reset "visited" cells
                    }
                }

            Writer.Write(matrix.ToString());    
        }
       
        static readonly int[] dr = new int[] { -1, 0, 1, 0 };
        static readonly int[] dc = new int[] { 0, -1, 0, 1 };

        static void Lee(Matrix m, int si, int sj)
        {            

            Queue<(int r, int c)> Q = new Queue<(int, int)>();
            Q.Enqueue((si, sj));

            List<(int r, int c)> coords = new List<(int r, int c)>();
            int replacement = 1;

            while(Q.Count>0)
            {
                int r, c;
                (r, c) = Q.Dequeue();
                coords.Add((r, c));
                m[r, c] = 1;

                if (r == 0 || c == 0 || r == m.RowsCount - 1 || c == m.ColsCount - 1)
                    replacement = 2;

                for (int i = 0; i < 4; i++) 
                {
                    int nr = r + dr[i];
                    int nc = c + dc[i];

                    if (nr < 0 || nc < 0 || nr >= m.RowsCount || nc >= m.ColsCount)
                        continue;

                    if (m[nr, nc] != 0)
                        continue;

                    Q.Enqueue((nr, nc));
                }
            }

            if (replacement != 1)
            {
                foreach (var (r, c) in coords)
                {
                    m[r, c] = replacement;
                }
            }
        }      
    }
}
