using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab9.Controls.Tabs
{
    internal interface IANNNotifier
    {
        void OnANNLoaded();
        void OnANNTrainStart();
        void OnANNTrainFinished();
        void OnANNTesting();
        void OnANNIdle();
        void OnANNError();
    }
}
