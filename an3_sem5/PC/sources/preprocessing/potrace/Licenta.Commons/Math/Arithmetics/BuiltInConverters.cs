namespace Licenta.Commons.Math.Arithmetics
{
    [OperativeConverter]
    public static class BuiltInConverters
    {
        public static DoubleNumber ConvertInt2Double(IntNumber x) => new DoubleNumber(x.Value);
        public static IntNumber ConvertDouble2Int(DoubleNumber x) => new IntNumber((int)x.Value);


    }
}
