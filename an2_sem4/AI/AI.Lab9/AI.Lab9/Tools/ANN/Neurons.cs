using System;

namespace AI.Lab9.Tools.ANN
{
    public class Neurons
    {
        public static Func<Neuron> Input => () => new Neuron();

        public static Func<Neuron> Hidden(ActivationFunction activate) => () => new Neuron(activate);
        public static Func<Neuron> Output(ActivationFunction activate) => () => new Neuron(activate);

    }
}
