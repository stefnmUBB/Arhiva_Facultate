namespace Licenta.Commons.AI.Perceptrons
{
    internal class Input : Perceptron
    {
        public double Value { get; set; }

        public override double Activate(double value) => Value;

        public override double Derivative(double value) => 0;        
    }
}
