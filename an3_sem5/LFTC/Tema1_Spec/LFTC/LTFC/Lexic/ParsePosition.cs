using System.Collections.Generic;

namespace LFTC.Lexic
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
