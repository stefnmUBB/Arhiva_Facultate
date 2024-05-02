namespace Licenta.Commons.Math.Arithmetics
{
    public struct IntNumber : INumber, IAddSubMulDivOperative<IntNumber>
    {
        public int Value { get; }
        public IntNumber(int value)
        {
            Value = value;
        }

        public static explicit operator int(IntNumber x) => x.Value;
        public static implicit operator IntNumber(int x) => new IntNumber(x);  

        public IntNumber Add(INumber x)=>Add(OperativeConverter.Convert<IntNumber>(x));        
        public IntNumber Divide(INumber x)=>Divide(OperativeConverter.Convert<IntNumber>(x));
        public IntNumber Multiply(INumber x) => Multiply(OperativeConverter.Convert<IntNumber>(x));
        public IntNumber Subtract(INumber x) => Subtract(OperativeConverter.Convert<IntNumber>(x));        

        IOperative IAdditive<IntNumber>.Add(IntNumber x) => Add(x);
        IOperative ISubtrative<IntNumber>.Subtract(IntNumber x) => Subtract(x);
        IOperative IMultiplicative<IntNumber>.Multiply(IntNumber x) => Multiply(x);
        IOperative IDivisive<IntNumber>.Divide(IntNumber x) => Divide(x);      
        IOperative IAdditive<INumber>.Add(INumber x) => Add(x);
        IOperative IDivisive<INumber>.Divide(INumber x) => Subtract(x);
        IOperative IMultiplicative<INumber>.Multiply(INumber x) => Multiply(x);
        IOperative ISubtrative<INumber>.Subtract(INumber x) => Subtract(x);

        public IOperative Clone() => new IntNumber(Value);

        INumber ISetAdditive<INumber, INumber>.Add(INumber x) => Add(x);
        INumber ISetSubtractive<INumber, INumber>.Subtract(INumber x) => Subtract(x);
        INumber ISetMultiplicative<INumber, INumber>.Multiply(INumber x) => Multiply(x);
        INumber ISetDivisive<INumber, INumber>.Divide(INumber x) => Divide(x);

        public IntNumber Subtract(IntNumber x) => new IntNumber(Value - x.Value);
        public IntNumber Add(IntNumber x) => new IntNumber(Value + x.Value);
        public IntNumber Multiply(IntNumber x) => new IntNumber(Value * x.Value);
        public IntNumber Divide(IntNumber x) => new IntNumber(Value / x.Value);        
    }
}
