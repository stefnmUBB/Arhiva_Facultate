using System;

namespace AI.ML.Data
{
    public class SportData
    {
        public SportData(int weight, int waist, int pulse)
        {
            Weight = weight;
            Waist = waist;
            Pulse = pulse;
        }

        public int Weight { get; set; }
        public int Waist { get; set; }
        public int Pulse { get; set; }

        public override string ToString() => $"{Weight}, {Waist}, {Pulse}";
        
        public static double DefaultDifference(SportData s1, SportData s2)
        {
            var we = s1.Weight - s2.Weight;
            var wa = s1.Waist - s2.Waist;
            var p = s1.Pulse - s2.Pulse;            
            return Math.Sqrt(we * we + wa * wa + p * p);
        }
    }
}
