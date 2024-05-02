using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FestivalSellpoint.Network.Utils
{
    public class Stringifier
    {
        public static string Encode(object obj)
        {
            if (obj is int n)
                return n.ToString();

            if (obj is string str)
                return str.ToLiteral();

            if (obj == null)
                return "null";

            if (obj is IStringifiable stringifiable)
            {
                var fields = Reflection.GetAllFields(obj.GetType());
                var propsStr = new List<string>();

                foreach(var f in fields)
                {
                    var value = Encode(f.GetValue(stringifiable));
                    propsStr.Add($"{Reflection.GetSimpleFieldName(f)}={value}");
                }
                return $"{obj.GetType().Name}{{{string.Join(";", propsStr)}}}";                    
            }                

            if(obj.GetType().IsArray)
            {
                var arr = obj as object[];
                if (arr.Length == 0)
                    return "empty";
                return string.Join(",", arr.Select(Encode));
            }

            throw new ArgumentException($"Cannot stringify object of type {obj.GetType()}");
        }


        public static T Decode<T>(string input) => (T)new SerParser().Parse(input);

    }
}
