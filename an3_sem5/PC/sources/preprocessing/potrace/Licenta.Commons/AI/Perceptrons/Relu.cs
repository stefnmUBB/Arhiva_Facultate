using System;

namespace Licenta.Commons.AI.Perceptrons
{
    [HiddenLayer]
    public class Relu : Perceptron
    {
        public override double Activate(double value) => System.Math.Max(0, value);

        public override double Derivative(double value) => value > 0 ? 1 : 0;
    }
}
