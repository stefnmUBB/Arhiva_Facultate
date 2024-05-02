using Licenta.Commons.Math.Arithmetics;
using Licenta.Commons.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Licenta.Commons.Math
{
    public interface IMatrix
    {
        int RowsCount { get; }
        int ColumnsCount { get; }
        IReadMatrix<S> Cast<S>();        
    }

    public interface IReadMatrix<out T> : IMatrix
    {
        T[] Items { get; }
        T this[int row, int column] { get; }
        IReadMatrix<T> GetRow(int index);
        IReadMatrix<T> GetColumn(int index);
        IReadMatrix<S> Select<S>(Func<T, S> selector);
    }

    public interface IWriteMatrix<in T> : IMatrix
    {      
        T this[int row, int column] { set; }        
    }

    public interface IMatrix<T> : IReadMatrix<T>, IWriteMatrix<T> { }

    public class Matrix<T> : IMatrix<T>
    {
        public int RowsCount { get; }
        public int ColumnsCount { get; }
        public T[] Items { get; }

        public Matrix(IReadMatrix<T> m) : this(m.RowsCount, m.ColumnsCount, m.Items.ToArray()) { }
        public Matrix(int rowsCount, int columnsCount)
        {
            RowsCount = rowsCount;
            ColumnsCount = columnsCount;
            Items = new T[RowsCount * ColumnsCount];
        }
        public Matrix(int rowsCount, int columnsCount, params T[] items) : this(rowsCount, columnsCount)
        {
            Array.Copy(items, Items, System.Math.Min(items.Length, Items.Length));
        }

        public T this[int row, int column]
        {
            get => Items[row * ColumnsCount + column];
            set => Items[row * ColumnsCount + column] = value;
        }

        public IEnumerable<T> EnumerateRow(int index)
        {
            if (index < 0 || index >= RowsCount)
                throw new IndexOutOfRangeException($"Invalid row index: {index}");
            return Items.Skip(index * ColumnsCount).Take(ColumnsCount);
        }

        public T[] GetRowArray(int index)
        {
            if (index < 0 || index >= RowsCount)
                throw new IndexOutOfRangeException($"Invalid row index: {index}");
            return Items.Skip(index * ColumnsCount).Take(ColumnsCount).ToArray();
        }

        public Matrix<T> GetRow(int index)
        {
            if (index < 0 || index >= RowsCount)
                throw new IndexOutOfRangeException($"Invalid row index: {index}");
            return new Matrix<T>(1, ColumnsCount, Items.Skip(index * ColumnsCount).Take(ColumnsCount).ToArray());
        }
        public Matrix<T> GetColumn(int index)
        {
            if (index < 0 || index >= ColumnsCount)
                throw new IndexOutOfRangeException($"Invalid column index: {index}");
            var col = new T[RowsCount];
            for (int i = 0; i < RowsCount; i++)
                col[i] = Items[i * ColumnsCount + index];
            return new Matrix<T>(RowsCount, 1, col);
        }

        IReadMatrix<T> IReadMatrix<T>.GetRow(int index) => GetRow(index);
        IReadMatrix<T> IReadMatrix<T>.GetColumn(int index) => GetColumn(index);
        public IReadMatrix<S> Cast<S>() => new Matrix<S>(RowsCount, ColumnsCount, Items.Cast<S>().ToArray());

        public IReadMatrix<S> Select<S>(Func<T, S> selector) => new Matrix<S>(RowsCount, ColumnsCount, Items.Select(selector).ToArray());

        public static Matrix<T> Fill(int rowsCount, int colsCount, T value)
        {
            return new Matrix<T>(rowsCount, colsCount, Enumerable.Repeat(value, rowsCount * colsCount).ToArray());
        }

        public static Matrix<T> CreateByRule(int rowsCount, int colsCount, Func<int,int, T> f)
        {
            var items = new T[rowsCount * colsCount];
            for (int i = 0; i < rowsCount; i++)
                for (int j = 0; j < colsCount; j++)
                    items[i * rowsCount + j] = f(i, j);
            return new Matrix<T>(rowsCount, colsCount, items);
        }

        public override string ToString()
        {
            var itemsstr = Items.Select(_ => _.ToString()).ToArray();
            var maxLen = itemsstr.Max(_ => _.Length);
            itemsstr = itemsstr.Select(_ => _.PadRight(maxLen)).ToArray();
            return itemsstr.GroupChunks(ColumnsCount).Select(r => r.JoinToString(" ")).JoinToString("\n");
        }

        public Matrix<T> Transpose
        {
            get 
            {
                var matrix = new Matrix<T>(ColumnsCount, RowsCount);
                for(int i=0;i<RowsCount;i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                        matrix[j, i] = this[i, j];
                }
                return matrix;
            }
        }
    }    
}
