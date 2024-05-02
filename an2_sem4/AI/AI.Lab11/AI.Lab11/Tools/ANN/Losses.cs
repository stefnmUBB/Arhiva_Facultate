using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab11.Tools.ANN
{
    public static class Losses
    {
        public static double LogLoss(double[][] real, double[][] pred)
        {
            double loss = 0;
            for (int i = 0; i < real.Length; i++) 
            {                
                for (int k = 0; k < real[i].Length; k++) 
                {
                    if (real[i][k] != 0) 
                    {
                        loss -= real[i][k] * Math.Log10(pred[i][k]);
                    }
                }
            }
            return loss;
        }

        public static double MSE(double[][] real, double[][] pred)
        {
            double loss = 0;
            for (int i = 0; i < real.Length; i++)
            {
                for (int k = 0; k < real[i].Length; k++)
                {                    
                    var d = real[i][k] - pred[i][k];                    
                    loss += d * d;                    
                }
            }
            return loss;
        }


    }
}
