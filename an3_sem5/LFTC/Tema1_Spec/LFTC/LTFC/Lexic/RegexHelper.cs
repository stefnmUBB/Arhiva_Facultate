using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LFTC.Lexic
{
    internal static class RegexHelper
    {
        private static string EscapeChars(string s)
        {
            return Regex.Escape(s).Replace("/", "\\/").Replace("-", "\\/");
        }

        internal static string GetBlockCommentsRegex((string Begin, string End)[] styles)
        {
            return string.Join("|", styles.Select(s => $"({EscapeChars(s.Begin)}.*?{EscapeChars(s.End)})"));
        }

        internal static string GetInlineCommentsRegex(string[] styles)
        {
            return string.Join("|", styles.Select(s => $"({EscapeChars(s)}.*$)"));
        }

        internal static string BuildSplitBreakingRegex(string characters)
        {
            if (characters.Length == 0 || characters.Length > 2)
                throw new ArgumentException("Invalid split breaking characters sequence");
            if (characters.Length == 1)
            {
                var c = Regex.Escape(characters[0].ToString());
                return $@"([{c}]([^{c}\\\n]|\\.|\\\n)*[{c}])";
            }
            else
            {
                var cStart = Regex.Escape(characters[0].ToString());
                var cEnd = Regex.Escape(characters[1].ToString());
                return $@"([{cStart}]([^{cStart}{cEnd}\\\n]|\\.|\\\n)*[{cEnd}])";
            }
        }

        internal static string GetSplitBreakingRegexStringsOnly(string[] splitBreakingCharacters)
        {
            return string.Join("|", splitBreakingCharacters.Select(BuildSplitBreakingRegex));
        }

        internal static string GetSplitBreakingRegex(string[] splitBreakingCharacters)
        {
            var splitChars = splitBreakingCharacters.SelectMany(s => s).Select(c => Regex.Escape(c.ToString()));

            return string.Join("|", splitBreakingCharacters.Select(BuildSplitBreakingRegex)
                .Concat(new List<string> { 
                    // tokens that don't contain any delimiters
                    "[^"
                    + string.Join("", splitChars)
                    + "\\s]+",

                    // individual "unpaired" delimiters (e.g. "abc' => " , abc, ')
                    (splitChars.Count() == 0 ? "" :
                    "["
                    + string.Join("", splitChars)
                    + "]"
                    )})
                .Where(_ => !string.IsNullOrEmpty(_)));
        }

        internal static bool IsSplitBreakingString(this string input, string splitBreakingCharacters)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            if (input.Length == 1)
                return false;
            return input.First() == splitBreakingCharacters.First() && input.Last() == splitBreakingCharacters.Last();
        }

        internal static bool IsSplitBreakingString(this string input, string[] splitBreakingCharacters)
        {
            return splitBreakingCharacters.Any(chs => input.IsSplitBreakingString(chs));
        }

        internal static string GetAtomsSplitRegex(this string[] atoms)
        {
            List<string> groups = new List<string>();
            groups.AddRange(atoms.Where(a => a.Length >= 2).Select(a => a.StartsWith("(") && a.EndsWith(")") ? a : $"({a})"));
            if (atoms.Where(a => a.Length == 1).Count() > 0)
                groups.Add("["
                    + string.Join("", atoms
                        .Where(a => a.Length == 1)
                        .Select(a => Regex.Escape(a).Replace("-", @"\-").Replace("/", @"\/")))
                    + "]");
            return string.Join("|", groups);
        }
    }
}
