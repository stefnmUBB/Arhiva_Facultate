using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AI.Commons.Utils
{
    public static class StringUtils
    {
        public static string RemoveRedundantWhitespace(this string input)
        {
            return string.Join(" ", new Regex(@"(?:([""])(.*?)(?<!\\)(?>\\\\)*\1|([^""\s]+))")
                .Matches(input)
                .Cast<Match>()
                .Select(m => m.Value));
        }

        public static List<string> SplitToTokens(this string input)
        {
            return new Regex(@"(?:([""])(.*?)(?<!\\)(?>\\\\)*\1|([^""\s]+))")
                .Matches(RemoveRedundantWhitespace(input))
                .Cast<Match>()
                .Select(m => m.Value)
                .Where(s => s != "")
                .Select(s =>
                {
                    if (s[0] == '"') return new List<string> { s };
                    return Regex.Split(s, @"([\[\]])|(\d*\.\d*)").ToList();
                })
                .SelectMany(x => x)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
        }

        public static string Tab(this string input)
        {
            return string.Join("\n", input.Split('\n').Select(x => "\t" + x).ToArray());
        }
    }
}
