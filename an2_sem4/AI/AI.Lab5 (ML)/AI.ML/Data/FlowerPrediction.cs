using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.ML.Data
{
    internal class FlowerPrediction
    {
        public string Type { get; set; }
        public string PredictedType { get; set; }

        public PredictedData<FlowerData> ToPredictedData()
        => new PredictedData<FlowerData>(new FlowerData(Type),
                new FlowerData(PredictedType));

        public override string ToString()
        {
            return $"{Type} {PredictedType}";
        }
    }
}
