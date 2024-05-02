using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Licenta.Commons.AI
{
    public class AnnModel
    {
        public List<AnnLayerModel> HiddenLayers { get; private set; } = new List<AnnLayerModel>();
        public int InputLength { get; set; }
        public int OutputLength { get; set; }

        public RuntimeAnn Compile() => RuntimeAnn.CompileFromModel(this);

        public byte[] ToBytes()
        {
            using(var ms=new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                    WriteToBinaryWriter(bw);
                return ms.ToArray();
            }
        }

        public void WriteToBinaryWriter(BinaryWriter bw)
        {            
            bw.Write(InputLength);
            bw.Write(OutputLength);
            bw.Write(HiddenLayers.Count);

            HiddenLayers.ForEach(_ => _.WriteToBinaryWriter(bw));
        }

        public static AnnModel ReadFromBinaryReader(BinaryReader br)
        {
            var model = new AnnModel();

            model.InputLength = br.ReadInt32();
            model.OutputLength = br.ReadInt32();
            int layersCount = br.ReadInt32();
            for (int i = 0; i < layersCount; i++)
                model.HiddenLayers.Add(AnnLayerModel.ReadFromBinaryReader(br));
            return model;
        }

        public AnnModel Clone()
        {
            return new AnnModel
            {
                InputLength = InputLength,
                OutputLength = OutputLength,
                HiddenLayers = HiddenLayers.Select(_ => _.Clone()).ToList()
            };
        }

        public override string ToString()
        {
            var str = "";
            str += $"{InputLength} x Input\n";
            HiddenLayers.ForEach(_ => str += _.ToString() + "\n");
            str += $"{OutputLength} x Output";
            return str;
        }
    }
}
