using LFTC.Lexic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LFTC
{
    public static class Extensions
    {
        public static bool IsBetween(this int x, int a, int b) => a <= x && a <= b;

        public static (int Line, int Column) GetLineAndColumn(this string str, int position)
        {
            int lineNumber = str.ToCharArray(0, position).Count(chr => chr == '\n') + 1;
            int fis = str.LastIndexOf("\n", position);
            int posi = position - fis;
            return (lineNumber, posi);
        }

        public static IEnumerable<Match> RemoveRedundantWhitespace(this string input)
        {
            return new Regex(@"[^\s]+")
                .Matches(input)
                .Cast<Match>();
        }        

        internal static IEnumerable<Token> ToTokens(this string[] splittedValues, string originalInput, int tokenStartIndex)
        {
            int offset = 0;
            foreach (var value in splittedValues)
            {
                yield return new Token(value, tokenStartIndex + offset, originalInput.GetLineAndColumn(tokenStartIndex + offset));
                offset += value.Length;
            }
            yield break;
        }

        public static IEnumerable<T> Peek<T, _>(this IEnumerable<T> items, Func<T, _> action)
        {
            foreach (var item in items)
            {
                action(item);
                yield return item;
            }            
        }

        public static string AsTable<T1, T2>(this IEnumerable<(T1, T2)> items, string header1 = "col1", string header2 = "col2")
        {
            var col1 = new List<string>();
            var col2 = new List<string>();

            int rowsCount = 0;
            foreach(var item in items)
            {
                col1.Add(item.Item1.ToString());
                col2.Add(item.Item2.ToString());
                rowsCount++;
            }

            var col1Len = Math.Max(header1.Length, col1.Count > 0 ? col1.Max(_ => _.Length) : 0);
            var col2Len = Math.Max(header2.Length, col2.Count > 0 ? col2.Max(_ => _.Length) : 0);

            string table = "";
            table += $" {header1.PadLeft(col1Len)} | {header2.PadRight(col2Len)}\n";
            table += $"{new string('-',col1Len+2)}|{new string('-', col2Len + 2)}\n";
            for (int i = 0; i < rowsCount; i++)
                table += $" {col1[i].PadLeft(col1Len)} | {col2[i].PadRight(col2Len)}\n";
            return table;
        }
    }
}
