using System;

namespace AI.Lab11.Tools.ANN
{
    public class Neuron
    {
        public Neuron(ActivationFunction activate = null)
        {
            Activate = activate ?? ActivationFunction.Constant(0);            
        }
        public double Value { get; set; }
        public ActivationFunction Activate { get; set; }        
    }
}
