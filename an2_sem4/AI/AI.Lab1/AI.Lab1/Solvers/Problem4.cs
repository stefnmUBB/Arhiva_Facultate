using AI.Lab1.Lib.Utils;
using System;
using System.IO;

namespace AI.Lab1.Solvers
{
    /*Să se determine cuvintele unui text care apar exact o singură dată în acel text. 
     * De ex. cuvintele care apar o singură dată în ”ana are ana are mere rosii ana" 
     * sunt: 'mere' și 'rosii'.
     * 
     * IO : Fisier
     * Date de intrare : un text format din cuvinte separate prin spatiu
     * Date de iesire : Cuvintele care apar o singura data, fiecare pe cate o linie
     * 
     * Complexitate: Θ(N*K), N = lungimea sirului, K = lungimea medie a unui cuvant     
     *  construire trie : Θ(N * K)
     *  parcurgere trie : Θ(LUNGIME_ALPFABET * K) = Θ(K)
     *  
     *  O(N) memorie
     */
    internal class Problem4 : FileIOProblem
    {
        string Text = "";
        public override void ReadInput() => Text = File.ReadAllText(Input);        

        public override void Run()
        {
            TrieNode trie = new TrieNode();

            Text.SplitByChar(' ', trie.Add);

            trie.ForEach((w, c) =>
            {
                //Console.WriteLine($"{w} : {c} ");
                if (c == 1)
                    Writer.WriteLine(w);
            });
        }

        class TrieNode
        {
            public TrieNode[] Children = new TrieNode[26];
            public int Value = 0;

            public void Add(string word)
            {
                if (word == "") return;
                if (word.Length == 1)
                {
                    GetChild(word[0]).Value++;
                    return;
                }
                GetChild(word[0]).Add(word.Substring(1));
            }

            private TrieNode GetChild(char c)
            {
                TrieNode node = Children[c - 'a'];
                if (node == null)
                {
                    Children[c - 'a'] = node = new TrieNode();                  
                }
                return node;
            }

            private void ForEach(Action<string, int> callback, string prefix)
            {
                for(int i=0;i<26;i++)
                {
                    if (Children[i] == null)
                        continue;                    
                    var new_prefix = prefix + Convert.ToChar('a' + i);

                    if (Children[i].Value > 0) // word exists
                    {
                        callback(new_prefix, Children[i].Value);
                    }

                    Children[i].ForEach(callback, new_prefix);
                }
            }

            public void ForEach(Action<string, int> callback)
            {
                ForEach(callback, "");
            }
        }
    }
}
