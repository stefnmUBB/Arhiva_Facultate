using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Parallelization;
using Licenta.Commons.Utils;
using System;
using System.Diagnostics;
using System.Linq;

namespace Licenta.Commons.Math
{
    public static class Matrices
    {
        public static bool HaveSameShape(IMatrix a, IMatrix b) => a.ColumnsCount == b.ColumnsCount && a.RowsCount == b.RowsCount;
        public static Matrix<TO> DoItemByItem<TO, TI1, TI2>(IReadMatrix<TI1> a, IReadMatrix<TI2> b, Func<TI1,TI2,TO> f)
        {
            if (!HaveSameShape(a, b))
                throw new InvalidOperationException("The two matrices must have same shape for this operation");
            return new Matrix<TO>(a.RowsCount, a.ColumnsCount, a.Items.Zip(b.Items, f).ToArray());                
        }

        public static Matrix<TO> DoEachItem<TI,TO>(IReadMatrix<TI> a, Func<TI, TO> f)
        {
            var m = new Matrix<TO>(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.Items.Length; i++) m.Items[i] = f(a.Items[i]);
            return m;            
        }

        public static Matrix<TO> DoEachItem<TI, TO>(IReadMatrix<TI> a, Func<TI, int, int, TO> f)
        {
            var m = new Matrix<TO>(a.RowsCount, a.ColumnsCount);
            for (int i = 0, k = 0; i < a.RowsCount; i++) 
                for (int j = 0; j < a.ColumnsCount; j++, k++) 
                    m.Items[k] = f(a.Items[k], i,j);                        
            return m;            
        }

        public static void ForEachItem<TI>(IReadMatrix<TI> a, Func<TI, int, int, bool> f)
        {           
            for (int i = 0, k = 0; i < a.RowsCount; i++)
                for (int j = 0; j < a.ColumnsCount; j++, k++)
                    if (!f(a.Items[k], i, j)) return;            
        }

        public static Matrix<T> Add<T>(Matrix<T> a, Matrix<T> b) where T : ISetAdditive<T>
            => DoItemByItem(a, b, (x, y) => x.Add(y));
        public static Matrix<T> Subtract<T>(Matrix<T> a, Matrix<T> b) where T : ISetSubtractive<T>
            => DoItemByItem(a, b, (x, y) => x.Subtract(y));

        public static Matrix<T> Multiply<T, S>(Matrix<T> a, S scalar) where T : ISetMultiplicative<S, T> where S:IOperative
            => DoEachItem(a, x => x.Multiply(scalar));
        public static Matrix<T> Divide<T, S>(Matrix<T> a, S scalar) where T : ISetDivisive<S, T> where S : IOperative
            => DoEachItem(a, x => x.Divide(scalar));

        public static T ItemsSum<T>(IReadMatrix<T> m) where T:ISetAdditive<T>
        {
            return m.Items.Aggregate((x, y) => x.Add(y));
        }

        public static double ItemsSum(IReadMatrix<double> m)
        {
            return m.Items.Aggregate((x, y) => x + y);
        }

        public static Matrix<T> ApplyDoubleThreshold<T>(IReadMatrix<T> m, T low, T high, T tlow, T thigh) where T:IComparable
        {
            return DoEachItem(m, x => x.CompareTo(tlow) <= 0 ? low : x.CompareTo(thigh) >= 0 ? high : x);
        }

        public static Matrix<T> Convolve<T>(IReadMatrix<T> a, IReadMatrix<T> b, ConvolutionBorder borderFilter = ConvolutionBorder.Extend)
            where T: ISetAdditive<T>, ISetMultiplicative<T>
            => Convolve<T, T, T>(a, b, borderFilter);

        public static Matrix<double> Convolve(IReadMatrix<double> a, IReadMatrix<DoubleNumber> b, ConvolutionBorder borderFilter = ConvolutionBorder.Extend)
            => Convolve(a, Matrices.DoEachItem(b, x => x.Value), borderFilter);

        public static Matrix<double> Convolve(IReadMatrix<double> a, IReadMatrix<double> b, ConvolutionBorder borderFilter = ConvolutionBorder.Extend)
        {
            if (b.RowsCount % 2 == 0 || b.ColumnsCount % 2 == 0)
                throw new ArgumentException("Convolution matrix must have an odd number of rows and column");
            int dr = b.RowsCount / 2;
            int dc = b.ColumnsCount / 2;

            var items = new double[a.RowsCount * a.ColumnsCount];

            ParallelForLoop.Run(r =>
            {
                for (int c = 0; c < a.ColumnsCount; c++)
                {
                    int ix = r * a.ColumnsCount + c;

                    for (int ir = 0; ir < b.RowsCount; ir++)
                    {
                        int r2 = r + ir - dr;
                        for (int ic = 0; ic < b.ColumnsCount; ic++)
                        {
                            int c2 = c + ic - dc;
                            double t = GetItem(a, r2, c2, borderFilter);
                            items[ix] = items[ix]+(t*(b[ir, ic]));
                        }
                    }
                }
            }, 0, a.RowsCount);

            return new Matrix<double>(a.RowsCount, a.ColumnsCount, items);
        }

        public static Matrix<TO> Convolve<TI1, TI2, TO>(IReadMatrix<TI1> a, IReadMatrix<TI2> b, ConvolutionBorder borderFilter = ConvolutionBorder.Crop)
            where TI1 : ISetMultiplicative<TI2, TO>
            where TI2 : IOperative
            where TO : IOperative, ISetAdditive<TO>
        {
            if (b.RowsCount % 2 == 0 || b.ColumnsCount % 2 == 0)
                throw new ArgumentException("Convolution matrix must have an odd number of rows and column");
            int dr = b.RowsCount / 2;
            int dc = b.ColumnsCount / 2;

            var items = new TO[a.RowsCount * a.ColumnsCount];

            ParallelForLoop.Run(r =>
            {
                for (int c = 0; c < a.ColumnsCount; c++)
                {
                    int ix = r * a.ColumnsCount + c;

                    for (int ir = 0; ir < b.RowsCount; ir++)
                    {
                        int r2 = r + ir - dr;
                        for (int ic = 0; ic < b.ColumnsCount; ic++)
                        {
                            int c2 = c + ic - dc;
                            TI1 t = GetItem(a, r2, c2, borderFilter);
                            items[ix] = items[ix].Add(t.Multiply(b[ir, ic]));
                        }
                    }
                }
            }, 0, a.RowsCount);

            return new Matrix<TO>(a.RowsCount, a.ColumnsCount, items);
        }

        private static int Mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        private static T GetItem<T>(IReadMatrix<T> m, int i, int j, ConvolutionBorder filter)
        {
            if (0 <= i && i < m.RowsCount && 0 <= j && j < m.ColumnsCount) return m[i, j];
            switch(filter)
            {
                case ConvolutionBorder.Crop: return default(T);
                case ConvolutionBorder.Extend: return m[i.Clamp(0, m.RowsCount - 1), j.Clamp(0, m.ColumnsCount - 1)];
                case ConvolutionBorder.Wrap: return m[Mod(i, m.RowsCount), Mod(j, m.ColumnsCount)];
                default: throw new ArgumentException("Invalid border filter");
            }        
        }        

        public enum ConvolutionBorder
        {
            Crop,
            Extend,
            Wrap,            
        }

        public static T MaxElement<T>(IReadMatrix<T> mat) where T:IComparable
        {
            return mat.Items.Max();                      
        }

        public static Matrix<double> Subtract(Matrix<double> a, Matrix<double> b)
            => DoItemByItem(a, b, (x, y) => x - y);

        public static Matrix<double> Multiply(IReadMatrix<double> a, IReadMatrix<double> b)
        {
            if (a.ColumnsCount != b.RowsCount)
                throw new InvalidOperationException($"Cannot multiply matrices of dimensions ({a.RowsCount},{a.ColumnsCount}) and ({b.RowsCount},{b.ColumnsCount})");
            var p = new Matrix<double>(a.RowsCount, b.ColumnsCount);
            ParallelForLoop.Run(r =>
            {
                for (int c = 0; c < p.ColumnsCount; c++)
                {
                    for (int k = 0; k < a.ColumnsCount; k++)
                        p[r, c] += a[r, k] * b[k, c];
                }
            }, 0, p.RowsCount);
            return p;
        }
    }
}
