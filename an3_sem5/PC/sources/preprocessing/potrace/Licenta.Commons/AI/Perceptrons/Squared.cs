using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Commons.AI.Perceptrons
{
    [HiddenLayer]
    public class Squared : Perceptron
    {
        public override double Activate(double value) => value * value;

        public override double Derivative(double value) => 2 * value;        
    }
}
