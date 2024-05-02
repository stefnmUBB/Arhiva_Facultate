using System.Collections.Generic;
using static System.Math;

namespace AI.Lab1.Solvers
{
    internal class Problem7 : Problem<(List<int> list, int k), int>
    {
        /*
         * Să se determine al k-lea cel mai mare element al unui șir de numere cu n elemente (k < n). 
         * De ex. al 2-lea cel mai mare element din șirul [7,4,6,3,9,1] este 7.
         * 
         * IO : Parametri
         * Date de intrare : Lista de numere si o pozitie k
         * Date de iesire : Al k-lea cel mai mare element din lista data
         * 
         * Complexitate: Θ(N*log(N)), N = lungimea sirului         
         * 
         * Θ(N) memorie
         */
        public override void Solve()
        {
            Sort(Input.list, 0, Input.list.Count - 1);            
            Output = Input.list[Input.k - 1];
        }

        void Sort(List<int> l, int i, int j)
        {
            if (i >= j) return;

            if (i + 1 == j)
            {
                (l[i], l[j]) = (Max(l[i], l[j]), Min(l[i], l[j]));
                return;
            }

            int m = (i + j) / 2;

            Sort(l, i, m);
            Sort(l, m + 1, j);
            
            int[] tmp = new int[j - i + 1];
            int k = 0;

            int p = i;
            int q = m + 1;

            while(p<=m && q<=j)
            {
                if (l[p] > l[q])
                {
                    tmp[k++] = l[p++];
                }
                else
                {
                    tmp[k++] = l[q++];
                }
            }

            while (p <= m) tmp[k++] = l[p++];
            while (q <= j) tmp[k++] = l[q++];

            for (k = 0; k < j - i + 1; k++)
                l[i + k] = tmp[k];
        }
    }
}
