using AI.Lab9.Algebra;
using AI.Lab9.Tools.ANN;
using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AI.Lab9.Controls.Tabs
{
    internal class ANNController
    {
        private IANNNotifier Notifier;

        private NeuralNetwork _NeuralNetwork = null;
        public NeuralNetwork NeuralNetwork 
        {
            get => _NeuralNetwork;
            set
            {
                if (IsBusy)
                    throw new InvalidOperationException("Network is running");
                _NeuralNetwork = value;
                Notifier.OnANNLoaded();
                IsLoaded = true;
                IsBusy = false;
            }
        }

        public bool IsLoaded { get; private set; } = false;
        public bool IsBusy { get; private set; }        

        public ANNController(IANNNotifier notifier)
        {
            Notifier = notifier;
        }

        public void Train(List<(double[] x, double[] t)> _data, Action<double> lossReport = null)
        {
            if (!IsLoaded)
                throw new InvalidOperationException("Network not loaded");
            if (IsBusy)
                throw new InvalidOperationException("Network is running");

            IsBusy = true;            
            var data = _data.ToList();
            Notifier.OnANNTrainStart();
            Task.Run(() =>
            {
                NeuralNetwork.Train(data, lossReport);
                Thread.Sleep(1000);
                Notifier.OnANNTrainFinished();
                IsBusy = false;
            });
        }

        public void Test(List<(double[] x, double[] t)> _data)
        {
            if (!IsLoaded)
                throw new InvalidOperationException("Network not loaded");
            if (IsBusy)
                throw new InvalidOperationException("Network is running");

            IsBusy = true;
            Notifier.OnANNTesting();
            
            Task.Run(() =>
            {                
                bool softmax = true;                

                var res = _data.Select((_) =>
                {
                    var results = NeuralNetwork.PredictSingle(_.x);
                    var maxIndex = results.ArgMax();                    

                    if (softmax)
                    {
                        var min = results.Min();
                        if (min < 0)
                        {
                            results = results.Select(x => x - min).ToArray();
                        }

                        var sum = results.Sum();
                        if (sum != 0)
                        {
                            for (int i = 0; i < results.Length; i++)
                                results[i] /= sum;
                        }
                    }

                    return results.Concat(new double[] { maxIndex, _.t.ArgMax() }).ToArray();
                }).ToMatrix();
                
                TestFinished?.Invoke(res);                
                IsBusy = false;
                Notifier.OnANNIdle();
            });
        }

        public delegate void OnTestFinished(Matrix results);
        public event OnTestFinished TestFinished;
    }
}
