using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LFTC.Lexic
{    
    public class AtomExtractor
    {        
        private static List<string> SplitAtoms = new List<string>
        { @"\+\+", @"\-\-", @"\+", @"\-", @"\*", @"\/", @"\%", @"\=", @"\;", @"\:", @"\(", @"\)", @"\[", @"\]", @"\.", @"\,",
          @"\<\=", @"\>\=", @"\<", @"\>", @"\=\=", @"\!\=", @"\<\>",
          @"\&", @"\|", @"\&\&", @"\|\|", @"\?", @"\^",
          @"\+\=", @"\-\=", @"\*\=", @"\/\=", @"\%\=", @"\&\=", @"\|\=", @"\^\=", @"\<\<\=", @"\>\>\=",
          @"\<\<", @"\>\>", @"((?<!\w)\d+\.\d*(?!\w))",

        }.OrderByDescending(_ => _.Length).Distinct().ToList();

        TokenSplitOptions TokenSplitOptions = new TokenSplitOptions(SplitAtoms);                        

        public List<Token> TokenizeBetweenTokens(string input, List<Token> tokens)
        {
            List<Token> result = new List<Token>();
            int startIndex = 0;
            foreach (var token in tokens)
            {
                if (startIndex == token.Position)
                {
                    startIndex += token.Length;
                    continue;
                }
                var substr = input.Substring(startIndex, token.Position - startIndex);
                result.Add(new Token(substr, startIndex, input.GetLineAndColumn(startIndex)));
                startIndex = token.Position + token.Length;
            }
            if (startIndex < input.Length)
            {
                var substr = input.Substring(startIndex);
                result.Add(new Token(substr, startIndex, input.GetLineAndColumn(startIndex)));
            }
            return result;
        }

        public List<Token> SplitToTokens(string input)
        {

            var splitBreakingRegex = RegexHelper.GetSplitBreakingRegex(new string[0]);

            var tokens = Regex.Matches(input, splitBreakingRegex).Cast<Match>()
                // remove spaces
                .Select(m => new Token(m.Value, m.Index, input.GetLineAndColumn(m.Index)))            
                // split by atoms
                .Select(token =>
                {
                    if (TokenSplitOptions.Atoms.GetAtomsSplitRegex() == "")
                        return new List<Token> { token };
                    return token.Text.RemoveRedundantWhitespace()
                            .Select(m => new Token(m.Value, token.Position + m.Index, input.GetLineAndColumn(token.Position + m.Index)))
                            .Select(tk => Regex.Split(tk.Text, TokenSplitOptions.Atoms.GetAtomsSplitRegex())
                            .ToTokens(input, tk.Position)).SelectMany(_ => _);
                }).SelectMany(_ => _)
                // cleanup
                .Where(token => !string.IsNullOrWhiteSpace(token.Text))                
                .ToList();

            var list = tokens.OrderBy(_ => _.Position).ToList();
            for (int i = 0; i < list.Count; i++)
                list[i] = list[i].Reposition(new ParsePosition(list, i));
            return list;
        }
    }
}
