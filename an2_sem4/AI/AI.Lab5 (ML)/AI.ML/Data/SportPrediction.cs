using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.ML.Data
{
    public class SportPrediction
    {
        public int Weight { get; set; }
        public int Waist { get; set; }
        public int Pulse { get; set; }
        public int PredictedWeight { get; set; } 
        public int PredictedWaist { get; set; }
        public int PredictedPulse { get; set; }

        public override string ToString()
        {
            return $"Real=({Weight}, {Waist}, {Pulse}), Predicted=({PredictedWeight}, {PredictedWaist}, {PredictedPulse})";
        }

        public PredictedData<SportData> ToPredictedData()
            => new PredictedData<SportData>(new SportData(Weight, Waist, Pulse),
                new SportData(PredictedWeight, PredictedWaist, PredictedPulse));                         
    }
}
