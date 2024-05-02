using Licenta.Commons.Math;
using Licenta.Commons.Utils;
using Licenta.Commons.AI;
using System;
using System.IO;
using System.Security.Policy;

namespace Licenta.Commons.Utils
{    
    internal static class BinaryRW
    {
        public static AnnModel ReadAnnModel(this BinaryReader br) => AnnModel.ReadFromBinaryReader(br);
        public static void Write(this BinaryWriter bw, AnnModel model) => model.WriteToBinaryWriter(bw);

        public static Matrix<double> ReadDoubleMatrix(this BinaryReader br)
        {
            var rows = br.ReadInt32();
            var cols = br.ReadInt32();
            var bytes = br.ReadBytes(sizeof(double) * rows * cols); 
            double[] values = new double[rows * cols];          
            for (int i = 0; i < values.Length; i++) 
                values[i] = BitConverter.ToDouble(bytes, i * sizeof(double));
            var m = new Matrix<double>(rows, cols, values);
            return m;
        }

        public static void Write(this BinaryWriter bw, Matrix<double> m)
        {
            bw.Write(m.RowsCount);
            bw.Write(m.ColumnsCount);
            m.Items.ForEach(bw.Write);
        }

    }
}
