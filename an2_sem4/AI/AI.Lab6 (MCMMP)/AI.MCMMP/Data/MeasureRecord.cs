using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.MCMMP.Data
{
    public class MeasureRecord
    {
        public string Country { get; set; }
        public int HappinessRank { get; set; }
        public double HappinessScore { get; set; }
        public double WhiskerHigh { get; set; }
        public double WhiskerLow { get; set; }        
        public double EconomyGDPPerCapita { get; set; }
        public double Family { get; set; }
        public double HealthLifeExpectancy { get; set; }
        public double Freedom { get; set; }
        public double Generosity { get; set; }
        public double TrustGovernmentCorruption { get; set; }
        public double DystopiaResidual { get; set; }

        public MeasureRecord() { }

        public override string ToString()
        {
            return $"{Country} {HappinessRank} {HappinessScore} {WhiskerHigh} {WhiskerLow} {EconomyGDPPerCapita} {Family} " +
                $"{HealthLifeExpectancy} {Freedom} {Generosity} {TrustGovernmentCorruption} {DystopiaResidual}";
        }
    }
}
