using System.Collections.Generic;

namespace AI.Lab1.Solvers
{
    internal class Problem5Boost : Problem<List<int>, int>
    {

        /* Optimizare problema 5  */
        // 1, 2, ... , n-1 + un k duplicat
        // invariant : n = lungimea sirului
        // sum(input) = 1+2+...+n-1 + k
        // k = sum(input) - sum(1..n-1)
        // Θ(n) timp, Θ(1) memorie de lucru (fara inputs)
        public override void Solve()
        {
            int sum = 0;
            int n = Input.Count;
            for (int i = 0; i < n; i++) sum += Input[i];

            Output = sum - n * (n - 1) / 2;
        }
       
    }
}
