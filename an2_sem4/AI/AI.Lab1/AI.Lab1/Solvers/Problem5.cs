using System.Collections.Generic;

namespace AI.Lab1.Solvers
{
    internal class Problem5 : Problem<List<int>, int>
    {
        /*Pentru un șir cu n elemente care conține valori din mulțimea {1, 2, ..., n - 1} 
         * astfel încât o singură valoare se repetă de două ori, să se identifice acea
         * valoare care se repetă. De ex. în șirul [1,2,3,4,2] valoarea 2 apare de două ori.
         * IO : Parametrie
         * Date de intrare : O lista de numere 1,2,...,n-1 + un k duplicat
         * Date de iesire : valoarea elementului k duplicat
         * 
         * Complexitate: O(N) timp, Θ(N) memorie         
         */
        public override void Solve()
        {
            int n = Input.Count;
            bool[] exists = new bool[n + 1];

            for(int i=0;i<n;i++)
            {
                if (exists[Input[i]])
                {
                    Output = Input[i];
                    return;
                }
                exists[Input[i]] = true;
            }
        }
        // O(n) timp, Θ(n) memorie de lucru
    }
}
