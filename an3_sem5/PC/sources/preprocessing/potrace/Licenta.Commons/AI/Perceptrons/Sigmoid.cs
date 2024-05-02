using System;

namespace Licenta.Commons.AI.Perceptrons
{
    [HiddenLayer]
    public class Sigmoid : Perceptron
    {
        public override double Activate(double value) => 1 / (1 + System.Math.Exp(-value));

        public override double Derivative(double value)
        {
            var s = 1 / (1 + System.Math.Exp(-value));
            return s * (1 - s);
        }
    }
}
