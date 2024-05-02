using Licenta.Commons.Math;
using Licenta.Commons.Utils;
using System.Collections.Generic;
using System.IO;

namespace Licenta.Commons.AI
{
    public class SavedStateAnn
    {
        public AnnModel Model { get; }
        public List<Matrix<double>> Weights { get; }

        public SavedStateAnn(AnnModel model, List<Matrix<double>> weights)
        {
            Model = model;
            Weights = weights;
        }

        public void WriteToBinaryWriter(BinaryWriter bw)
        {
            bw.Write(Model);
            bw.Write(Weights.Count);
            Weights.ForEach(bw.Write);
        }

        public static SavedStateAnn ReadFromBinaryReader(BinaryReader br)
        {
            var model = br.ReadAnnModel();
            int weightsCount = br.ReadInt32();
            var weights = new List<Matrix<double>>();
            for (int i = 0; i < weightsCount; i++)
                weights.Add(br.ReadDoubleMatrix());
            return new SavedStateAnn(model, weights);
        }

        public void Save(string filename)
        {
            using (var f = new FileStream(filename, FileMode.Create))
            {
                using (var bw = new BinaryWriter(f))
                {
                    WriteToBinaryWriter(bw);
                }
            }
        }
    }
}
