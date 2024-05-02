namespace AI.Lab1.Solvers
{
    internal class Problem10 : FileIOProblem
    {
        public override void ReadInput() { }

        /* Considerându-se o matrice cu n x m elemente binare(0 sau 1) sortate crescător pe linii, 
         * să se identifice indexul liniei care conține cele mai multe elemente de 1.
         * 
         * IO : Fisier
         * Date de intrare : 
         * n m
         * [matrice binara de m linii si n coloane]
         * Date de iesire : indexul liniei care conține cele mai multe elemente de 1
         * 
         * Complexitate: O(N*log2(M))
         *  Determinarea numarului de biti de 1 dintr-o linie
         *  => cautare binara tip lower bound pe valoarea 1
         *  => nr de biti de 1 = [lungimea liniei] - [pozitia primului 1 identificat]
         *  => O(1)
         *  
         *  O(N) memorie
         */
        public override void Run()
        {
            var fin = new IntFileInput(Input);
            int n = fin.ReadInt();
            int m = fin.ReadInt();

            int[] row = new int[m];

            int max_ones = 0;
            int result = -1;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    row[j] = fin.ReadInt();

                int ones = CountOnes(row);
                if (ones > max_ones)
                {
                    max_ones = ones;
                    result = i;
                }
            }

            Writer.WriteLine(result);
        }        

        public int CountOnes(int[] v)
        {
            // lower bound binary search
            int i = 0, j = v.Length;
            while(i<j)
            {                
                int m = (i + j) / 2;
                if (v[m] == 0)
                    i = m + 1;
                else
                    j = m;                
            }
            return v.Length - i;
        }       
    }
}
