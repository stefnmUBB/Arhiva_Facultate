namespace Licenta.Commons.AI.Perceptrons
{
    [HiddenLayer]
    public class Self : Perceptron
    {
        public override double Activate(double value) => value;

        public override double Derivative(double value) => 1;        
    }
}
