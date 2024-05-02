namespace AI.Lab9.Data
{
    internal class Flower
    {
        public double SepalLength { get; set; }
        public double SepalWidth { get; set; }
        public double PetalLength { get; set; }
        public double PetalWidth { get; set; }
        public string Class { get; set; }
        public override string ToString() => $"{SepalLength};{SepalWidth};{PetalLength};{PetalWidth};{Class}";
    }
}
