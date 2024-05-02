using AI.Lab9.Tools.ANN;

namespace AI.Lab9.Solvers
{
    internal static class Networks
    {
        public static NeuralNetwork IrisANN() => new NeuralNetwork()
                .AddLayer(Neurons.Input, 4)
                .AddLayer(Neurons.Hidden(ActivationFunction.Linear(0.1, 0)), 2)
                .AddLayer(Neurons.Hidden(ActivationFunction.Self), 2)
                .AddLayer(Neurons.Hidden(ActivationFunction.ReLU), 3)
                .Create();

        public static NeuralNetwork DigitsANN()=> new NeuralNetwork()
        {
            IterationsCount = 100,
            LearningRate = 0.11504956607942,
        }
                .AddLayer(Neurons.Input, 64)
                .AddLayer(Neurons.Hidden(ActivationFunction.Linear(0.011300861375081, -0.0100627213297704)), 6)
                .AddLayer(Neurons.Output(ActivationFunction.Linear(0.76314877614526, 0.074567726382319)), 10)
                .Create();

        public static NeuralNetwork SepiaANN() => new NeuralNetwork()
        {
            IterationsCount = 200,
            LearningRate = 0.088669556350759
        }
                .AddLayer(Neurons.Input, 64)
                .AddLayer(Neurons.Hidden(ActivationFunction.Gauss(-3.04926678060403, -0.543287212398503)), 23)
                .AddLayer(Neurons.Hidden(ActivationFunction.Sign), 30)
                .AddLayer(Neurons.Output(ActivationFunction.LocalizedSigmoid(0.159845345611612, -0.923969837714206, 1.95574179464287, -0.194074992442771)), 14)
                .AddLayer(Neurons.Output(ActivationFunction.Sigmoid), 2)
                .Create();
    }
}
