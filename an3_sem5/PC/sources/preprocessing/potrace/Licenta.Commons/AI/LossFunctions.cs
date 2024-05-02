using System;
using System.Linq;

namespace Licenta.Commons.AI
{
    public static class LossFunctions
    {
        public static double MeanError(double[] r, double[] p)
        {
            if (r.Length == 0) return 0;
            double s = 0;
            for (int i = 0; i < r.Length; i++)
            {                
                s += System.Math.Abs(r[i] - p[i]);
            }
            return s / r.Length;
        }

        public static double MeanSquaredError(double[] r, double[] p)
        {
            if (r.Length == 0) return 0;
            double s = 0;
            for(int i=0;i<r.Length;i++)
            {
                var d = r[i] - p[i];
                s += d * d;
            }
            return s / r.Length;            
        }

        public static double SquaredError(double[] r, double[] p)        
        {            
            double s = 0;
            for (int i = 0; i < r.Length; i++)
            {
                var d = r[i] - p[i];
                s += d * d;
            }
            return s;
        }

        public static double LogLoss(double[] r, double[] p)        
        {
            double loss = 0;
            for (int k = 0; k < r.Length; k++)
            {
                if (r[k] != 0)
                    loss -= r[k] * System.Math.Log(p[k]);
            }
            return loss;
        }
    }
}
