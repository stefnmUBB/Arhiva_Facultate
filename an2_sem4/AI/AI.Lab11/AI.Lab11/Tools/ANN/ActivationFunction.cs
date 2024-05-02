using System;

namespace AI.Lab11.Tools.ANN
{
    public class ActivationFunction
    {
        public Func<double, double> Function;
        public Func<double, double> Derivative;

        public ActivationFunction(Func<double, double> function, Func<double, double> derivative)
        {
            Function = function;
            Derivative = derivative;
        }

        public static ActivationFunction Self => new ActivationFunction(new Func<double, double>(x => x), new Func<double, double>(x => 1));

        public static ActivationFunction Sign => Prag(-1, 1, 0);

        public static ActivationFunction Sigmoid => new ActivationFunction(
            x => 1 / (1 + Math.Exp(-x)),
            x =>
            {
                var ex = Math.Exp(-x);
                return ex / ((1 + ex) * (1 + ex));
            }
            );

        public static ActivationFunction Constant(double value) => new ActivationFunction(_ => value, _ => 0);
        public static ActivationFunction Prag(double a, double b, double c)
            => new ActivationFunction(x => x < c ? a : b, _ => 0);

        public static ActivationFunction Rampa(double a, double b, double c, double d)
            => new ActivationFunction(new Func<double, double>(x =>
            {
                if (x < c) return a;
                if (x > d) return b;
                return a + (x - c) * (b - a) / (d - c);
            }), 
            x=>
            {
                if (x < c) return 0;
                if (x > d) return 0;
                return (b - a) / (d - c);
            });

        public static ActivationFunction Linear(double _a, double _b)
        {
            var a = _a;
            var b = _b;            
            return new ActivationFunction(x => { return a * x + b; }, x => a);
        }

        public static ActivationFunction Quad(double a, double b, double c)
        {
            return new ActivationFunction(
                x => a * x * x + b * x + c,
                x => 2 * a * x + b);

        }

        public static ActivationFunction Cubic(double a, double b, double c, double d)
        {
            return new ActivationFunction(
                x => a * x * x * x + b * x * x + c * x + d,
                x => 3 * a * x * x + 2 * b * x + c);
        }

        private static double Sqr(double x) => x * x;

        public static ActivationFunction Gauss(double _m, double _v)
        {
            var m = _m;
            var v = _v;
            return new ActivationFunction(
                new Func<double, double>(x => Math.Exp(-0.5 * Sqr((x - m) / v)) / (Math.Sqrt(2 * Math.PI) * v)),
                new Func<double, double>(x => -(x - m) * Math.Exp(-0.5 * Sqr((x - m) / v)) / (Math.Sqrt(2 * Math.PI) * v * v * v))
                );
        }

        public static ActivationFunction ReLU
            => new ActivationFunction(x => Math.Max(x, 0), x => x < 0 ? 0 : 1);

        public static ActivationFunction LocalizedSigmoid(double lmin, double lmax, double skew, double centerx)
        {
            return new ActivationFunction(
                x => lmin + (lmax - lmin) / (1 + Math.Exp(-skew*x+skew*centerx)),
                x =>
                {
                    var sig = Tools.Algebra.Functions.Sigmoid(-skew * x + skew * centerx);
                    var sigd = sig * (1 - sig);
                    return -(lmax - lmin) * skew * sigd;
                }
                );
        }
    }


}
