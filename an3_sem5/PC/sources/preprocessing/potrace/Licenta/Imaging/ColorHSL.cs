using System;

namespace Licenta.Imaging
{
    public class ColorHSL
    {
        public float H { get; }
        public float S { get; }
        public float L { get; }

        public ColorHSL(float h, float s, float l)
        {
            H = h;
            S = s;
            L = l;
        }

        public static ColorHSL FromRGB(ColorRGB color) => FromRGB(color.R.Value, color.G.Value, color.B.Value);

        public static ColorHSL FromRGB(double R, double G, double B)
        {
            float _R = (float)R; // (R / 255f);
            float _G = (float)G; // (G / 255f);
            float _B = (float)B; // (B / 255f);

            float _Min = Math.Min(Math.Min(_R, _G), _B);
            float _Max = Math.Max(Math.Max(_R, _G), _B);
            float _Delta = _Max - _Min;

            float H = 0;
            float S = 0;
            float L = (float)((_Max + _Min) / 2.0f);

            if (_Delta != 0)
            {
                if (L < 0.5f)
                    S = (float)(_Delta / (_Max + _Min));
                else
                    S = (float)(_Delta / (2.0f - _Max - _Min));

                if (_R == _Max)                
                    H = (_G - _B) / _Delta;                
                else if (_G == _Max)                
                    H = 2f + (_B - _R) / _Delta;                
                else if (_B == _Max)                
                    H = 4f + (_R - _G) / _Delta;                
            }
            H /= 6;
            while (H < 0) H += 1;
            return new ColorHSL(H, S, L);
        }

        public ColorRGB ToRGB()
        {
            byte r, g, b;
            if (S == 0)
            {
                r = (byte)Math.Round(L * 255d);
                g = (byte)Math.Round(L * 255d);
                b = (byte)Math.Round(L * 255d);
            }
            else
            {
                double t1, t2;
                double th = H;

                if (L < 0.5d)                
                    t2 = L * (1d + S);                
                else               
                    t2 = (L + S) - (L * S);                
                t1 = 2d * L - t2;

                double tr, tg, tb;
                tr = th + (1.0d / 3.0d);
                tg = th;
                tb = th - (1.0d / 3.0d);

                tr = ColorCalc(tr, t1, t2);
                tg = ColorCalc(tg, t1, t2);
                tb = ColorCalc(tb, t1, t2);
                r = (byte)Math.Round(tr * 255d);
                g = (byte)Math.Round(tg * 255d);
                b = (byte)Math.Round(tb * 255d);
            }
            return new ColorRGB(r, g, b);
        }
        private static double ColorCalc(double c, double t1, double t2)
        {

            if (c < 0) c += 1d;
            if (c > 1) c -= 1d;
            if (6.0d * c < 1.0d) return t1 + (t2 - t1) * 6.0d * c;
            if (2.0d * c < 1.0d) return t2;
            if (3.0d * c < 2.0d) return t1 + (t2 - t1) * (2.0d / 3.0d - c) * 6.0d;
            return t1;
        }
    }
}
