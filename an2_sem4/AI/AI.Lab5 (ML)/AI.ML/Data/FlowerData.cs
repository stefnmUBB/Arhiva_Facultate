using System.Collections.Generic;

namespace AI.ML.Data
{
    internal class FlowerData
    {
        public string Type { get; set; }

        public FlowerData(string type)
        {
            Type = type;
        }

        public override string ToString()
        {
            return $"{Type}";
        }

        public override bool Equals(object obj)
        {
            return obj is FlowerData data &&
                   Type == data.Type;
        }

        public override int GetHashCode()
        {
            return 2049151605 + EqualityComparer<string>.Default.GetHashCode(Type);
        }
    }
}
