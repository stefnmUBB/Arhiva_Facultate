using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FestivalSellpoint.Network.Utils
{
    public static class Reflection
    {
        public static List<FieldInfo> GetAllFields(Type type)
        {
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).ToList();
            if (type.BaseType != null)
                fields.AddRange(GetAllFields(type.BaseType));
            return fields;
        }

        public static FieldInfo GetField(Type type, string name)
            => GetAllFields(type).Where(f => f.Name == name.FirstUppercase()
                || f.Name == $"<{name.FirstUppercase()}>k__BackingField"
                || f.Name == $"_{name.FirstUppercase()}")
                .FirstOrDefault();

        public static Type GetClassBySimpleName(string name)
            => AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Name == name).FirstOrDefault();

        private static string FirstLowercase(this string input)
        {
            if (string.IsNullOrEmpty(input)) 
                return input;
            return char.ToLower(input[0]) + input.Substring(1);
        }

        private static string FirstUppercase(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }

        public static string GetSimpleFieldName(FieldInfo field)
        {    
            if(field.Name.StartsWith("_"))
            {
                return field.Name.Substring(1).FirstLowercase();
            }
            if(field.Name.EndsWith("k__BackingField"))
            {
                return field.Name.Substring(1, field.Name.Length - "k__BackingField".Length - 2).FirstLowercase();
            }
            return field.Name.FirstLowercase();
        }

        public static string ToLiteral(this string input)
        {
            using (var writer = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                    return writer.ToString();
                }
            }
        }

        public static string FromLiteral(this string input)
        {
            input = input.Substring(1, input.Length - 2);
            string str = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '\\')
                {
                    i++;
                    if (input[i] == '"') str += "\"";
                    else if (input[i] == '\\') str += "\\";
                    else if (input[i] == '0') str += "\0";
                    else if (input[i] == 'a') str += "\a";
                    else if (input[i] == 'b') str += "\b";
                    else if (input[i] == 'f') str += "\f";
                    else if (input[i] == 'n') str += "\n";
                    else if (input[i] == 'r') str += "\r";
                    else if (input[i] == 't') str += "\t";
                    else if (input[i] == 'v') str += "\v";
                    else str += input[i + 1];
                    // \u...
                }
                else str += input[i];
            }
            return str;
        }
    }
}
