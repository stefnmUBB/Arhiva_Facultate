using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AI.ML.Utils
{
    public static class Functions
    {        
        // sum (yi-f(xi))^2
        public static double RegressionLoss<T>(double[] y, double[,] x, double[] beta)
        {
            double loss = 0;
            for(int i=0;i<y.Length;i++)
            {
                double t = y[i];
                for(int j=0;j<beta.Length;j++)
                {
                    t -= x[i, j] * beta[j];
                }
                loss += t * t;
            }
            return loss;
        }

        // - sum (yi*log(xi))
        public static double LogLoss<T>(double[] x, double[] y)
        {
            double loss = 0;
            for (int i = 0; i < x.Length; i++)
                loss -= y[i] * Math.Log(x[i]);
            return loss;
        }


        public static double CrossEntropyLoss(double[,] x, double[,] y)
        {
            // ????
            double loss = 0;
            for (int i = 0; i < x.GetLength(0); i++) 
            {
                for (int j = 0; j < x.GetLength(1); j++) 
                    loss -= y[i, j] * Math.Log(x[i, j]);
            }
            return loss / x.GetLength(0);
        }
    }
}
