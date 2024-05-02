namespace Licenta.Commons.AI.Perceptrons
{
    [HiddenLayer]
    internal class LeakyRelu : Perceptron
    {
        public override double Activate(double value)
            => value > 0 ? value : 0.01 * value;

        public override double Derivative(double value) => value >= 0 ? 1 : 0.01;
    }
}
