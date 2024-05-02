namespace AI.Lab1.Solvers
{
    internal class Problem9 : FileIOProblem
    {        
        public override void ReadInput() { }


        /* Considerându-se o matrice cu n x m elemente întregi și o listă cu perechi formate din 
         * coordonatele a 2 căsuțe din matrice ((p,q) și (r,s)), să se calculeze suma elementelor 
         * din sub-matricile identificate de fieare pereche.
         * 
         * IO : Fisier
         * Date de intrare : 
         * n m
         * [o matrice cu n linii si m coloane]
         * [o lista de tuple (r1 c1 r2 c2) reprezentand coordonatele submatricelor ale
         *  caror sume vrem sa le calculam]
         * Date de iesire : Suma elementelor fiecarei submatrice, fiecare pe cate o linie
         * 
         * Complexitate: Θ(M*N + K)
         *   K = Numarul de submatrice
         *   
         *   Retinem sumele partiale ale submatricelor (1 1 r c)
         *   => Suma elementelor unei submatrice aleatoare se realizeaza in O(1)
         *   
         *   Θ(M*N) memorie
         * 
         */

        public override void Run()
        {
            var fin = new IntFileInput(Input);

            int N = fin.ReadInt();
            int M = fin.ReadInt();

            Matrix A = new Matrix(N + 1, M + 1);

            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= M; j++)
                {
                    A[i, j] = fin.ReadInt();
                    A[i, j] += A[i, j - 1] + A[i - 1, j] - A[i - 1, j - 1];
                }
            }            

            while (!fin.Ready) 
            {
                int r1 = fin.ReadInt() + 1;
                int c1 = fin.ReadInt() + 1; 
                int r2 = fin.ReadInt() + 1;
                int c2 = fin.ReadInt() + 1;

                //Console.WriteLine($"{r1} {c1} {r2} {c2}");
                int sum = A[r2, c2] - A[r1 - 1, c2] - A[r2, c1 - 1] + A[r1 - 1, c1 - 1];
                //Console.WriteLine(sum);
                Writer.WriteLine(sum);                
            }
        }
    }
}
