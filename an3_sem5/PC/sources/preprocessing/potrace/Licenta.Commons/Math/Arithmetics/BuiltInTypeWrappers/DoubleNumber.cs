using System;

namespace Licenta.Commons.Math.Arithmetics
{
    public struct DoubleNumber : INumber, IAddSubMulDivOperative<DoubleNumber>, IComparable
    {
        public double Value { get; }
        public DoubleNumber(double value)
        {
            Value = value;
        }

        public static explicit operator double(DoubleNumber x) => x.Value;

        public static implicit operator DoubleNumber(double x) => new DoubleNumber(x);

        public DoubleNumber Add(DoubleNumber x) => new DoubleNumber(Value + x.Value);
        public DoubleNumber Subtract(DoubleNumber x) => new DoubleNumber(Value - x.Value);
        public DoubleNumber Multiply(DoubleNumber x) => new DoubleNumber(Value * x.Value);
        public DoubleNumber Divide(DoubleNumber x) => new DoubleNumber(Value / x.Value);

        public DoubleNumber Add(INumber x) => Add(OperativeConverter.Convert<DoubleNumber>(x));        

        public DoubleNumber Divide(INumber x) => Divide(OperativeConverter.Convert<DoubleNumber>(x));                

        public DoubleNumber Multiply(INumber x) => Multiply(OperativeConverter.Convert<DoubleNumber>(x));                
        public DoubleNumber Subtract(INumber x) => Subtract(OperativeConverter.Convert<DoubleNumber>(x));                
        
        IOperative IAdditive<INumber>.Add(INumber x) => Add(x);
        IOperative IDivisive<INumber>.Divide(INumber x) => Subtract(x);
        IOperative IMultiplicative<INumber>.Multiply(INumber x) => Multiply(x);
        IOperative ISubtrative<INumber>.Subtract(INumber x) => Subtract(x);

        public IOperative Clone() => new DoubleNumber(Value);

        INumber ISetAdditive<INumber, INumber>.Add(INumber x) => Add(x);
        INumber ISetSubtractive<INumber, INumber>.Subtract(INumber x) => Subtract(x);
        INumber ISetMultiplicative<INumber, INumber>.Multiply(INumber x) => Multiply(x);
        INumber ISetDivisive<INumber, INumber>.Divide(INumber x) => Divide(x);        

        IOperative IAdditive<DoubleNumber>.Add(DoubleNumber x) => Add(x);                

        IOperative ISubtrative<DoubleNumber>.Subtract(DoubleNumber x) => Subtract(x);        

        IOperative IMultiplicative<DoubleNumber>.Multiply(DoubleNumber x) => Multiply(x);        

        IOperative IDivisive<DoubleNumber>.Divide(DoubleNumber x) => Divide(x);

        public override string ToString() => Value.ToString();

        public int CompareTo(DoubleNumber d) => Value.CompareTo(d.Value);

        public int CompareTo(object obj)
        {
            if (!(obj is DoubleNumber d)) throw new ArgumentException("Object must be of type DoubleNumber");
            return CompareTo(d);
        }
    }
}
