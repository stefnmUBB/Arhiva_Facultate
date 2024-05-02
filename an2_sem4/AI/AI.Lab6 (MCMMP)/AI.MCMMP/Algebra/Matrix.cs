using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MCMMP.Algebra
{
    public class Matrix
    {
        private double[,] Data { get; }

        public Matrix(int n, int m)
        {
            Data = new double[n, m];
        }

        public Matrix(int n, int m, params double[] values)
        {
            Data = new double[n, m];

            for (int i = 0; i < values.Length; i++)
                Data[i / m, i % m] = values[i];
        }

        public int RowsCount { get => Data == null ? 0 : Data.GetLength(0); }
        public int ColsCount { get => Data == null ? 0 : Data.GetLength(1); }

        public double this[int i, int j]
        {
            get => Data[i, j];
            set => Data[i, j] = value;
        }

        // R0*=f
        private void MulRow(int r0, double f)
        {
            for (int j = 0; j < ColsCount; j++)
                Data[r0, j] *= f;
        }

        // R0 += f*R
        private void AddRowTo(int r0, int r, double f = 1)
        {
            for (int j = 0; j < ColsCount; j++)
                Data[r0, j] += f * Data[r, j];
        }

        private void SwapRows(int r1, int r2)
        {
            for (int j = 0; j < ColsCount; j++)
            {
                var aux = Data[r1, j];
                Data[r1, j] = Data[r2, j];
                Data[r2, j] = aux;
            }
        }

        private void SwapCols(int c1, int c2)
        {
            for (int i = 0; i < RowsCount; i++)
            {
                var aux = Data[i, c1];
                Data[i, c1] = Data[i, c2];
                Data[i, c2] = aux;
            }
        }

        
        public IEnumerable<double> GetRow(int i)
        {
            for (int j = 0; j < ColsCount; j++)
                yield return Data[i, j];
            yield break;
        }

        public IEnumerable<double> GetColumn(int j)
        {
            for (int i = 0; i < RowsCount; j++) 
                yield return Data[i, j];
            yield break;
        }

        public Matrix Transpose 
        {
            get
            {
                var matrix = new Matrix(ColsCount, RowsCount);

                for (int i = 0; i < RowsCount; i++)
                    for (int j = 0; j < ColsCount; j++)
                        matrix[j, i] = Data[i, j];          
                return matrix;
            }
        }

        public static Matrix operator + (Matrix a, Matrix b)
        {
            if (a.RowsCount != b.RowsCount || a.ColsCount != b.ColsCount)
                throw new ArgumentException("Cannot add matrices of different dimensions");
            var c = new Matrix(a.RowsCount, a.ColsCount);
            for(int i=0;i<c.RowsCount;i++)
            {
                for (int j = 0; j < c.ColsCount; j++)
                    c[i, j] = a[i, j] + b[i, j];
            }
            return c;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.RowsCount != b.RowsCount || a.ColsCount != b.ColsCount)
                throw new ArgumentException("Cannot add matrices of different dimensions");
            var c = new Matrix(a.RowsCount, a.ColsCount);
            for (int i = 0; i < c.RowsCount; i++)
            {
                for (int j = 0; j < c.ColsCount; j++)
                    c[i, j] = a[i, j] - b[i, j];
            }
            return c;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.ColsCount != b.RowsCount)
                throw new ArgumentException("Cannot multiply matrices");
            var c = new Matrix(a.RowsCount, b.ColsCount);

            for (int i = 0; i < a.RowsCount; i++) 
            {
                for (int j = 0; j < b.ColsCount; j++) 
                {
                    for (int k = 0; k < a.ColsCount; k++)
                        c[i, j] += a[i, k] * b[k, j];
                }
            }
            return c;
        }

        public (Matrix L, Matrix U) LUDecompose()
        {
            if (RowsCount != ColsCount)
                throw new ArgumentException("Matrix must be square");
            int n = RowsCount;

            var L = new Matrix(n, n);
            var U = new Matrix(n, n);

            for(int i=0;i<n;i++)
            {
                for (int k = i; k < n; k++) 
                {
                    double sum = 0;
                    for (int j = 0; j < i; j++)
                        sum += L[i, j] * U[j, k];
                    U[i, k] = Data[i, k] - sum;
                }

                if (U[i, i] == 0) return (null, null);
                for(int k=i;k<n;k++)
                {
                    if (i == k)
                        L[i, i] = 1;
                    else
                    {
                        double sum = 0;
                        for (int j = 0; j < i; j++)
                            sum += L[k, j] * U[j, i];
                        L[k, i] = (Data[k, i] - sum) / U[i, i];
                    }
                }
            }
            return (L, U);
        }

        public double Determinant => LUDecompose().U?.DiagonalProduct ?? 0;

        public static Matrix Identity(int n)
        {
            var m = new Matrix(n, n);
            for (int i = 0; i < n; i++) m[i, i] = 1;
            return m;
        }

        private static Matrix UInverse(Matrix U)
        {
            int n = U.RowsCount;
            var r = Identity(n);

            for(int i=0;i<n;i++)
            {
                var f = 1 / U[i, i];
                U.MulRow(i, f);
                r.MulRow(i, f);

                for (int j = i + 1; j < n; j++) 
                {
                    var f2 = -U[i, j] / U[j, j];
                    U.AddRowTo(i, j, f2);
                    r.AddRowTo(i, j, f2);
                }
            }

            return r;
        }

        public Matrix Inverse
        {
            get
            {
                var d = LUDecompose();
                if (d.U == null)
                    return null;

                var ui = UInverse(d.U);
                var li = UInverse(d.L.Transpose).Transpose;

                return ui * li;
            }
        }

        private double DiagonalProduct
        {
            get
            {
                double p = 1;
                for (int i = 0; i < Math.Min(RowsCount, ColsCount); i++)
                    p *= Data[i, i];
                return p;
            }
        }               

        public override string ToString()
        {
            var s = "";
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColsCount; j++)
                {
                    if (j > 0) s += " ";
                    s += Data[i, j].ToString().PadLeft(6);
                }
                s += "\n";
            }
            return s;
        }

        public IEnumerable<double[]> Rows
        {
            get
            {
                for (int i = 0; i < RowsCount; i++)
                {
                    var row = new double[ColsCount];
                    for (int j = 0; j <ColsCount;j++)
                    {
                        row[j] = Data[i, j];
                    }
                    yield return row;
                }
                yield break;
            }
        }
    }
}
