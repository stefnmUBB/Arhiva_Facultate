namespace Licenta.Utils.ParallelModels
{
    public class CannyEdgeDetectionOptions
    {
        public double Threshold0 { get; }
        public double Threshold1 { get; }

        public CannyEdgeDetectionOptions(double threshold0 = 0.2, double threshold1 = 0.8)
        {
            Threshold0 = threshold0;
            Threshold1 = threshold1;
        }

        public override string ToString() => $"{{DoubleThreshold=({Threshold0};{Threshold1})}}";
    }
}
