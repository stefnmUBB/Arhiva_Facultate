using System.Collections.Generic;

namespace Lab3.Lexic
{
    public class ParsePosition
    {
        public List<Token> Tokens { get; }
        public int Position { get; }

        public ParsePosition(List<Token> tokens, int position)
        {
            Tokens = tokens;
            Position = position;
        }
    }
}
