using System.Linq;

namespace AI.Lab1.Solvers
{
    internal class Problem8 : FileIOProblem
    {
        int N;
        public override void ReadInput()
        {
            N = new IntFileInput(Input).ReadInt();
        }

        /*
         * Să se genereze toate numerele (în reprezentare binară) cuprinse între 1 și n. 
         * De ex. dacă n = 4, numerele sunt: 1, 10, 11, 100.        
         * 
         * IO : Fisier
         * Date de intrare : Un numar N
         * Date de iesire : Reprezentarile binare ale numerelor 1..n, cate una pe fiecare linie
         * 
         * Complexitate: Θ(N*D) => Θ(N), D=dimensiunea cuvantului de biti (int=32)
         * Daca excludem afisarea:
         * 
         * T(Inc(n)) = 1, daca n e impar; 2, daca n=M2, dar n!=M4 etc
         * <=> T(Inc(n)) = q, unde n%(2^q)==0 && q maxim cu aceasta proprietate
         * 
         * Ex. Daca N=10, se Inc(i) va efectua:
         * ! 1 pas  pentru i=1,3,5,7,9 (xy0 + 1 = xy1)
         * ! 2 pasi pentru i=2,6,10    (x01 + 1 = x10)
         * ! 3 pasi pentru i=4,8       (x011+1 = x100)
         * 
         * in media, Inc(i) executa X pasi pentru N/(2^X) valori ale lui i
         * 
         * Def. k=[log2(N)] + 1
         * T(Run(N)) = sum_(i=1..k) { (N/2^i) * T(Inc(i)) } = N * (sum_(i=1..k) { i/2^i })
         * T(Run(N)) ~= 2*N => Θ(N)
         * 
         * Θ(log2(N)) memorie
         * 
         */
        public override void Run()
        {
            char[] tmp = new string('0', 32).ToArray();
            for (int i = 0; i < N; i++) 
            {
                Inc(tmp);
                Writer.WriteLine(string.Join("", tmp).TrimStart('0'));                
            }
        }        

        void Inc(char[] tmp)
        {
            int k = 31;
            while (k>=0 && tmp[k]=='1')
            {
                tmp[k] = '0';
                k--;
            }
            if (k >= 0) tmp[k] = '1';
        }
    }
}
