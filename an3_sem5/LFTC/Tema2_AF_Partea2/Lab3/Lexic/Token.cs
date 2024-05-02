namespace Lab3.Lexic
{
    public class Token
    {
        public string Text { get; }
        public int Position { get; }
        public int Line { get; }
        public int Column { get; }
        public string Type { get; }

        public ParsePosition ParsePosition { get; }

        public int Length => Text.Length;

        public bool IncludesIndex(int pos) => pos.IsBetween(Position, Position + Length - 1);

        public override string ToString() => $"Token \"{Text}\" at position {Position}, {Line}:{Column}";

        public Token(string text, int position, (int Line, int Column) lineCol, string type, ParsePosition tokenPos = null) : this(text, position, lineCol.Line, lineCol.Column, type, tokenPos) { }

        public Token(string text, int position, int line, int column, string type, ParsePosition tokenPos = null)
        {
            Text = text;
            Position = position;
            Line = line;
            Column = column;
            Type = type;
            ParsePosition = tokenPos;
        }

        public Token Reposition(ParsePosition tokenPosition) => new Token(Text, Position, Line, Column, Type, tokenPosition);
    }
}
