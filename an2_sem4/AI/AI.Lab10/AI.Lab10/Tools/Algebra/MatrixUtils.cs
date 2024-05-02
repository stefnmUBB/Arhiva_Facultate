using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Lab10.Algebra
{
    internal static class MatrixUtils
    {
        public static Matrix MatrixFromList(IEnumerable<double> list)
        {
            var m = new Matrix(list.Count(), 1);

            int i = 0;
            foreach (var t in list)
            {
                m[i++, 0] = t;                
            }

            return m;
        }


        public static Matrix ToMatrix(this IEnumerable<double[]> _rows)
        {
            var rows = _rows.ToArray();
            var rowsCount = rows.Count();
            var colsCount = rows.First().Count();

            var m = new Matrix(rowsCount, colsCount);

            int i = 0;
            foreach(var row in rows.ToArray())
            {
                int j = 0;
                foreach (var data in row.ToArray()) 
                {                                       
                    m[i, j] = data;
                    j++;
                }
                i++;
            }
            return m;
        }

        public static Matrix MatrixFromTuples<T>(IEnumerable<T> tuples)
        {
            if (!IsTuple(typeof(T))) 
            {
                throw new ArgumentException("Items type is not a tuple");
            }

            if (typeof(T).GetGenericArguments().Any(t => t != typeof(double))) 
            {
                throw new ArgumentException("All parameters must be double");
            }            

            var properties = typeof(T).GetProperties().ToList();            

            var m = new Matrix(tuples.Count(), properties.Count);

            int i = 0;
            foreach(var t in tuples)
            {
                for(int j = 0; j < properties.Count;j++)
                {
                    m[i, j] = (double)properties[j].GetValue(t);
                }
                i++;
            }

            return m;
        }

        static bool IsTuple(Type tuple)
        {
            if (!tuple.IsGenericType)
                return false;
            var openType = tuple.GetGenericTypeDefinition();
            return openType == typeof(Tuple<>)
                || openType == typeof(Tuple<,>)
                || openType == typeof(Tuple<,,>)
                || openType == typeof(Tuple<,,,>)
                || openType == typeof(Tuple<,,,,>)
                || openType == typeof(Tuple<,,,,,>)
                || openType == typeof(Tuple<,,,,,,>)
                || (openType == typeof(Tuple<,,,,,,,>) && IsTuple(tuple.GetGenericArguments()[7]));
        }       
    }
}
