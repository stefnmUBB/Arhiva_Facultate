using AI.Lab1.Lib.Utils;
using System.IO;

namespace AI.Lab1.Solvers
{
    /*
     * Să se determine ultimul (din punct de vedere alfabetic) cuvânt care poate apărea într-un text 
     * care conține mai multe cuvinte separate prin ” ” (spațiu). De ex. ultimul (dpdv alfabetic) cuvânt 
     * din ”Ana are mere rosii si galbene” este cuvântul "si".
     * 
     * IO : Fisier
     * Date de intrare : un text format din cuvinte separate prin spatiu
     * Date de iesire : ultimul cuvant in ordine lexicografica
     * 
     * Complexitate: Θ(N), N = lungimea sirului, pp. Θ(1) compararea stringurilor
     *               Θ(N*K), K=lungimea medie a unui cuvant
     *               O(N) memorie
     */
    internal class Problem1 : FileIOProblem
    {
        string Text = "";
        public override void ReadInput()
        {
            Text = File.ReadAllText(Input);
        }
        
        public override void Run()
        {
            Text += ' ';
            string result = "";

            string token = "";
            for (int i = 0; i < Text.Length; i++) 
            {
                if (Text[i] == ' ')
                {
                    if (token == "") continue;
                    if (result == "")
                    {
                        result = token;
                    }
                    else if (result.ToLowerCase().CompareTo(token.ToLowerCase()) < 0)
                        result = token;
                    token = "";
                }
                else token += Text[i];
            }

            Writer.Write(result);            
        }        
    }
}
