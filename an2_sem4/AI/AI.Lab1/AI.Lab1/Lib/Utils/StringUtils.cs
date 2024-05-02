using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab1.Lib.Utils
{
    public static class StringUtils
    {
        public static void SplitByChar(this string input, char c, Action<string> callback)
        {
            input += c;

            string token = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == c)
                {
                    if (token == "") continue;
                    callback?.Invoke(token);                   
                    token = "";
                }
                else token += input[i];
            }            
        }

        public static List<string> SplitByChar(this string input, char c)
        {
            List<string> result = new List<string>();

            input += c;

            string token = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == c)
                {
                    result.Add(token);
                    token = "";
                }
                else token += input[i];
            }            

            return result;
        }

        public static string ToLowerCase(this string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].IsBetween('A', 'Z'))
                    result += Convert.ToChar(input[i] - 'A' + 'a');
                else
                    result += input[i];
            }            
            return result;
        }
    }
}
