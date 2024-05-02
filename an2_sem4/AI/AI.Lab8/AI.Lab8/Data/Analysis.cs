namespace AI.Lab8.Data
{
    internal class Analysis
    {
        public double Radius { get; set; }
        public double Texture { get; set; }
        public string Type { get; set; }

        public Analysis(double radius, double texture, string type)
        {
            Radius = radius;
            Texture = texture;
            Type = type;
        }

        public override string ToString() => $"{Radius};{Texture};{Type}";
    }
}
