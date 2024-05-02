using Lab3;
using Lab3.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3.Lexic
{    
    public class AtomExtractor
    {
        private readonly Dictionary<string, AF> AFs = new Dictionary<string, AF>();

        public AtomExtractor()
        {
            AFs.Add("CONST", new AF(Resources.af_const));
            AFs.Add("ID", new AF(Resources.af_id));
            AFs.Add("SPACES", new AF(Resources.af_spaces));
            AFs.Add("SYM", new AF(Resources.af_sym));
            AFs.Add("INCLUDE", new AF(Resources.af_include));
        }

        private Token FindNextToken(string input, ref int pos)
        {
            var matches = new Dictionary<string, string>();
            foreach(var kv in AFs)
            {
                var key = kv.Key;
                var af = kv.Value;
                var match = af.LongestMatch(input.Substring(pos), true);
                if (match != null)
                    matches.Add(key, match);

            }
            if (matches.Count == 0)
                throw new LexicalErrorException($"Error at {pos}:({input.GetLineAndColumn(pos)}): Invalid token");
            var result = matches.OrderBy(_ => _.Value.Length).First();

            if (result.Key == "ID" && result.Value.Length > 250)
                throw new LexicalErrorException($"Error at {pos}:({input.GetLineAndColumn(pos)}): Identifier too long");

            var tk = new Token(result.Value, pos, input.GetLineAndColumn(pos), result.Key);
            pos += result.Value.Length;
            return tk;
        }

        public List<Token> SplitToTokens(string input, out LexicalErrorException ex)
        {
            ex = null;
            var list = new List<Token>();
            for (int pos = 0; pos < input.Length;)
            {
                try
                {
                    list.Add(FindNextToken(input, ref pos));
                }
                catch(LexicalErrorException e)
                {
                    ex = e;
                    break;
                }
            }
            return list;            
        }
    }
}
