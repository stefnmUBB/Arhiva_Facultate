using Licenta.Commons.Math;
using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Utils;

namespace Licenta.Imaging
{
    using ColorComponentType = DoubleNumber;
    public struct ColorRGB : ISetAddSubOperative<ColorRGB>, ISetMultiplicative<ColorComponentType, ColorRGB>
        , ISetDivisive<ColorComponentType, ColorRGB>, ISetDivisive<IntNumber, ColorRGB>
    {
        public ColorComponentType R { get; }
        public ColorComponentType G { get; }
        public ColorComponentType B { get; }

        public ColorRGB(byte r, byte g, byte b)
        {
            R = r / 255.0;
            G = g / 255.0;
            B = b / 255.0;
        }

        public ColorRGB(ColorComponentType r, ColorComponentType g, ColorComponentType b)
        {
            R = r;
            G = g;
            B = b;
        }

        public ColorRGB(int color)
        {
            B = (color & 0xFF) / 255.0;
            G = ((color >> 8) & 0xFF) / 255.0;
            R = ((color >> 16) & 0xFF) / 255.0;
        }

        public IOperative Clone() => new ColorRGB(R, G, B);

        public ColorRGB Clamp() => new ColorRGB(R.Value.Clamp(0, 1), G.Value.Clamp(0, 1), B.Value.Clamp(0, 1));

        public static ColorRGB FromRGB(int color) => new ColorRGB(color);

        public ColorRGB Add(ColorRGB x) => new ColorRGB(R.Add(x.R), G.Add(x.G), B.Add(x.B));          
        public ColorRGB Subtract(ColorRGB x) => new ColorRGB(R.Subtract(x.R), G.Subtract(x.G), B.Subtract(x.B));
        public ColorRGB Multiply(ColorComponentType x) => new ColorRGB(R.Multiply(x), G.Multiply(x), B.Multiply(x));
        public ColorRGB Divide(ColorComponentType x) => new ColorRGB(R.Divide(x), G.Divide(x), B.Divide(x));        

        ColorRGB ISetDivisive<ColorComponentType, ColorRGB>.Divide(ColorComponentType x) => Divide(x);
        ColorRGB ISetMultiplicative<ColorComponentType, ColorRGB>.Multiply(ColorComponentType x) => Multiply(x);
        IOperative IDivisive<ColorComponentType>.Divide(ColorComponentType x) => Divide(x);
        IOperative IMultiplicative<ColorComponentType>.Multiply(ColorComponentType x) => Multiply(x);

        IOperative IAdditive<ColorRGB>.Add(ColorRGB x) => Add(x);
        IOperative ISubtrative<ColorRGB>.Subtract(ColorRGB x) => Subtract(x);

        public ColorRGB Divide(IntNumber x)=> new ColorRGB(R.Divide(x), G.Divide(x), B.Divide(x));
        IOperative IDivisive<IntNumber>.Divide(IntNumber x) => Divide(x);

        public override string ToString() => $"(R={R.Value};G={G.Value};B={B.Value})";
    }
}
