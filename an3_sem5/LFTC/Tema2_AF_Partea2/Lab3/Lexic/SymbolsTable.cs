using Lab3.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lab3.Lexic
{
    public class SymbolsTable
    {
        public static List<string> ReservedAtomsList =
            new List<string> { "%ID%", "%CONST%" }
            .Concat(Resources.rKeyWords.Split('\n'))
            .Concat(Resources.rSymbols.Split('\n'))
            .ToList();

        public readonly Symbol[] TS;
        public readonly (int Cod, int TSPos)[] FIP;

        public string Error { get; private set; }

        public SymbolsTable(List<Token> tokens)
        {            
            var symbols = new SortedDictionary<string, Symbol>();
            FIP = new (int, int)[tokens.Count];

            var solveQ = new List<(int fipPos, Symbol s)>();                        

            for(int i=0;i<tokens.Count;i++)
            {
                if (i > 0)
                {                    
                    if (tokens[i - 1].Type == "CONST" && tokens[i].Type == "ID") 
                    {
                        if (tokens[i - 1].Position + tokens[i - 1].Length == tokens[i].Position) 
                        {
                            Error = $"Two identifiers next to each other {tokens[i].Position} ({tokens[i].Line}:{tokens[i].Column})";
                            break;
                        }
                    }
                }

                FIP[i].TSPos = -1;
                
                var index = ReservedAtomsList.IndexOf(tokens[i].Text);                

                if (index >= 2)
                    FIP[i].Cod = index;
                else
                {
                    if (index == 0 || index == 1) throw new InvalidOperationException();

                    var s = symbols.TryGetValue(tokens[i].Text, out var sym) ? sym : symbols[tokens[i].Text] = new Symbol(tokens[i].Text);

                    if (IsNumberConstLiteral(tokens[i]))
                        FIP[i].Cod = 1;
                    else if (IsIdentifierName(tokens[i]))
                        FIP[i].Cod = 0;
                    else
                    {
                        Error = $"Invalid token: '{tokens[i]}'";
                        break;
                    }
                    
                    solveQ.Add((i, s));
                }                
            }
            int k = 0;            

            symbols.Values.ToList().ForEach(_ => _.Id = k++);


            solveQ.ForEach(_ => FIP[_.fipPos].TSPos = _.s.Id);
            TS = symbols.Values.ToArray();
        }


        static bool IsNumberConstLiteral(Token x) => x.Type == "CONST";
        static bool IsIdentifierName(Token x) => x.Type == "ID";        
    }
}
