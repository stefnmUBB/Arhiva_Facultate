using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Commons.AI.Perceptrons
{
    [HiddenLayer]
    internal class ParameterizedRelu : Perceptron
    {
        [Parameter]
        public double A { get; set; }

        public override double Activate(double value) => value > 0 ? value : A * value;

        public override double Derivative(double value) => value > 0 ? 1 : A;
    }
}
