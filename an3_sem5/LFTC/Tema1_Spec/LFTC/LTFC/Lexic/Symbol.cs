namespace LFTC.Lexic
{
    public class Symbol
    {
        public string Text { get; }
        public int Id { get; set; }

        public Symbol(string text)
        {
            Text = text;
        }
    }
}
