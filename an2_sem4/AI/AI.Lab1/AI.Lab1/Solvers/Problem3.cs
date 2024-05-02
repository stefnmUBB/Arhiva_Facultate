using System;
using System.Collections.Generic;

namespace AI.Lab1.Solvers
{
    /* Să se determine produsul scalar a doi vectori rari care conțin numere reale. 
     * Un vector este rar atunci când conține multe elemente nule. Vectorii pot avea 
     * oricâte dimensiuni. De ex. produsul scalar a 2 vectori unisimensionali 
     * [1,0,2,0,3] și [1,2,0,3,1] este 4.
     * 
     * IO : Fisier
     * Date de intrare : 
     * d
     * n
     * x1_1 x1_2 ... x1_d u1
     * ...
     * xn_1 xn_2 ... xn_d un
     * m
     * y1_1 y1_2 ... y1_d v1
     * ...
     * ym_1 ym_2 ... ym_d vm
     * 
     * d = numarul de dimensiuni (1=vector, 2=matrice etc)
     * n = numarul de elemente nenule ale vectorului X
     * m = numarul de elemente nenule ale vectorului Y
     * Se citesc elementele vectorilor X si Y pe fiecare linie in felul urmator: 
     * la pozitia (x1, x2,..., xd) se afla valoarea nenula "u"     
     * 
     * Date de iesire : Un numar reprezentand produsul scalar al celor doi vectori.
     * 
     * Complexitate: O(D*(M+N)*I) ==> Θ(D*(M+N)), 
     *  D   = nr de dimensiuni
     *  M,N = nr de elemente nenule din vectori
     *  I   = complexitatea creare/selectare nod (folosim Dictionary aka hash table => Θ(1) avg)
     *  
     *  Θ(M+N) memorie 
     */
    internal class Problem3 : FileIOProblem
    {
        int Dimension;

        TreeNode root = new TreeNode();

        public override void ReadInput()
        {
            var fin = new IntFileInput(Input);
            Dimension = fin.ReadInt();
            
            int v1cnt = fin.ReadInt();
            for(int i=0;i<v1cnt;i++)
            {
                var node = root;
                for (int d=0;d<Dimension;d++)
                {
                    int c = fin.ReadInt();                    
                    node = node.GetChild(c);                    
                }
                node.Value1 = fin.ReadInt();
            }            

            int v2cnt = fin.ReadInt();
            for (int i = 0; i < v2cnt; i++)
            {
                var node = root;
                for (int d = 0; d < Dimension; d++)
                {
                    int c = fin.ReadInt();                    
                    node = node.GetChild(c);
                }
                node.Value2 = fin.ReadInt();
            }            
        }

        public override void Run()
        {
            int cdot = 0;
            root.ParseLeaves(v => cdot += v);
            Writer.WriteLine(cdot);            
        }

        class TreeNode
        {
            public int Value1 = 0;
            public int Value2 = 0;

            public Dictionary<int, TreeNode> Children = new Dictionary<int, TreeNode>();

            public TreeNode GetChild(int x)
            {
                if (!Children.ContainsKey(x))
                    Children[x] = new TreeNode();
                return Children[x];
            }

            public void ParseLeaves(Action<int> callback)
            {
                if (Children.Count == 0)
                {
                    callback(Value1 * Value2);
                    return;
                }
                foreach(var kv in Children)
                {
                    kv.Value.ParseLeaves(callback);
                }
            }
        }
    }

}
