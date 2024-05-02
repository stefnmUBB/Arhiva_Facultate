using Licenta.Commons.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Reflection.Emit;

namespace Licenta.Commons.AI
{
    public class AnnLayerModel
    {
        public Type PerceptronType { get; }
        public int PerceptronsCount { get; set; }
        public Dictionary<string, double> Parameters { get; }

        public AnnLayerModel(Type perceptronType, int perceptronsCount)
        {
            if (!typeof(Perceptron).IsAssignableFrom(perceptronType))
                throw new ArgumentException($"Type does not describe a perceptron");            

            PerceptronType = perceptronType;
            PerceptronsCount = perceptronsCount;
            Parameters = perceptronType.GetProperties()
                .Where(p => p.GetCustomAttribute<ParameterAttribute>() != null && p.PropertyType == typeof(double))
                .ToDictionary(p => p.Name, p => p.GetCustomAttribute<ParameterAttribute>().DefaultValue);
        }

        public Perceptron[] Compile()
            => Enumerable
                .Range(0, PerceptronsCount)
                .Select(_ => Activator.CreateInstance(PerceptronType) as Perceptron)
                .Peek(_ => Parameters.ForEach(kv => PerceptronType.GetProperty(kv.Key).SetValue(_, kv.Value)))
                .ToArray();


        public void WriteToBinaryWriter(BinaryWriter bw)
        {
            bw.Write(PerceptronsCount);
            bw.Write(PerceptronType.FullName);
            bw.Write(Parameters.Count);
            foreach (var param in Parameters) 
            {
                bw.Write(param.Key);
                bw.Write(param.Value);
            }
        }

        public static AnnLayerModel ReadFromBinaryReader(BinaryReader br)
        {
            var perceptronsCount = br.ReadInt32();
            var typeName = br.ReadString();
            var type = Reflection.GetTypeFromFullName(typeName);
            if (type == null)
                throw new InvalidOperationException($"Could not find type `{typeName}`");
            var model = new AnnLayerModel(type, perceptronsCount);

            var pmsCount = br.ReadInt32();
            for(int i=0;i<pmsCount;i++)
            {
                var key = br.ReadString();
                var value = br.ReadDouble();
                if (model.Parameters.ContainsKey(key))
                    model.Parameters[key] = value;
            }
            return model;
        }

        public AnnLayerModel Clone()
        {
            var model = new AnnLayerModel(PerceptronType, PerceptronsCount);

            model.Parameters.Clear();            
            foreach(var kv in Parameters)            
                model.Parameters.Add(kv.Key, kv.Value);                          

            return model;
        }

        public override string ToString() => $"{PerceptronsCount} x {PerceptronType.FullName} " +
            $"({string.Join(", ", Parameters.Select(kv => $"{kv.Key}={kv.Value}"))})";
    }
}
