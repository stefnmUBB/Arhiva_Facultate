using System.Collections.Generic;

namespace AI.Lab1.Solvers
{
    internal class Problem6 : Problem<List<int>, int>
    {
        /*
         * Pentru un șir cu n numere întregi care conține și duplicate, să se determine elementul 
         * majoritar (care apare de mai mult de n / 2 ori). De ex. 2 este elementul majoritar în 
         * șirul [2,8,7,2,2,5,2,3,1,2,2].
         * 
         * IO : Parametri
         * Date de intrare : o lista de numere care contine un elment majoritar
         * Date de iesire : valoarea elementului majoritar
         * 
         * Complexitate: Θ(N*K) = Θ(N), N = lungimea sirului, K = complexitatea inserarii (~O(1))              
         * worst case: O(N^2)
         * 
         * Θ(N) memorie
         */
        public override void Solve()
        {            
            Dictionary<int, int> map = new Dictionary<int, int>();

            for(int i=0;i<Input.Count;i++)
            {
                if (!map.ContainsKey(Input[i]))
                    map[Input[i]] = 1;
                else
                    map[Input[i]]++;
            }

            Output = -1;
            int cmax = 0;

            foreach(var kv in map)
            {
                if(kv.Value>cmax)
                {
                    cmax = kv.Value;
                    Output = kv.Key;
                }
            }
        }
    }
}
