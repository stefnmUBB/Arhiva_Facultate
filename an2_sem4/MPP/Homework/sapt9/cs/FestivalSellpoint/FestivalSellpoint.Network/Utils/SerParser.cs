using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FestivalSellpoint.Network.Utils
{
    public class SerParser
    {
        class Build
        {
            public static object ClassEmptyProps(object[] idt)
            {
                var type = Reflection.GetClassBySimpleName(idt[0] as string);
                var result = FormatterServices.GetUninitializedObject(type);
                return result;
            }

            public static object Class(object[] idt) 
            {
                var type = Reflection.GetClassBySimpleName(idt[0] as string);               
                var vprops = (idt[1] as object[]).Cast<PropertyValue>().ToList();
                var result = FormatterServices.GetUninitializedObject(type);

                foreach (var pv in vprops) 
                {
                    var field = Reflection.GetField(type, pv.Name);
                    
                    if (pv.Value == null)
                        field.SetValue(result, null);
                    else if (!field.FieldType.IsArray)
                    {
                        field.SetValue(result, pv.Value[0]);
                    }
                    else
                    {
                        var arr = Array.CreateInstance(field.FieldType.GetElementType(), pv.Value.Length);
                        Array.Copy(pv.Value, arr, pv.Value.Length);
                        field.SetValue(result, arr);
                    }
                }
                return result;
            }
            public static object Self(object[] idt) { return idt[0]; }
            public static object NullValue(object[] idt) { return null; }
            public static object Prepend(object[] idt)
            {
                object first = idt[0];
                object[] arr = idt[1] as object[];
                return arr.Prepend(first).ToArray();
            }
            public static object ElemToList(object[] idt) => new object[] { idt[0] };

            public static object Property(object[] idt)
            {
                if (idt[1] == null)                
                    return new PropertyValue(idt[0] as string, null);
                if (!idt[1].GetType().IsArray)
                    return new PropertyValue(idt[0] as string, new object[] { idt[1] });
                return new PropertyValue(idt[0] as string, idt[1] as object[]);
            }
            public static object EmptyArrayProperty(object[] idt)
                => new PropertyValue(idt[0] as string, new object[0]);
        }


        public SerParser()
        {
            RegisterRule("@E", "NAME { @PROPS }", Build.Class);
            RegisterRule("@E", "NAME { }", Build.ClassEmptyProps);
            RegisterRule("@E", "STRING", Build.Self);
            RegisterRule("@E", "NUMBER", Build.Self);
            RegisterRule("@E", "null", Build.NullValue);
            RegisterRule("@ELIST", "@E , @ELIST", Build.Prepend);
            RegisterRule("@ELIST", "@E", Build.ElemToList);
            RegisterRule("@PROPS", "@PROP ; @PROPS", Build.Prepend);
            RegisterRule("@PROPS", "@PROP", Build.ElemToList);
            RegisterRule("@PROP", "NAME = @ELIST", Build.Property);
            RegisterRule("@PROP", "NAME = empty", Build.EmptyArrayProperty);
        }

        #region Parse
        public object Parse(string input) => LookFor("@E", SplitToTokens(input), 0)?.Value;        

        private Record LookFor(ParseRule rule, List<string> tokens, int pos)
        {
            var pattern = rule.ParsePattern;

            var wildcardMatches = new List<Record>();

            int originalPos = pos;
            foreach (var wildcard in pattern)
            {
                if (pos >= tokens.Count) return null;

                if (wildcard.StartsWith("@"))
                {
                    var rec = LookFor(wildcard, tokens, pos);
                    if (rec == null) return null;
                    pos += rec.Size;
                    wildcardMatches.Add(rec);
                }
                else if (wildcard == "NUMBER")
                {
                    if (!int.TryParse(tokens[pos], out int number))
                        return null;
                    var rec = new Record(wildcard, number, pos, 1);
                    pos++;
                    AddToCache(rec);
                    wildcardMatches.Add(rec);
                }
                else if (wildcard == "STRING")
                {
                    var token = tokens[pos];
                    if (!(token.StartsWith("\"") && token.EndsWith("\""))) 
                        return null;
                    string str = token.FromLiteral();
                    var rec = new Record(wildcard, str, pos, 1);
                    pos++;
                    AddToCache(rec);
                    wildcardMatches.Add(rec);
                }
                else if (wildcard == "NAME")
                {
                    if (!CodeDomProvider.CreateProvider("C#").IsValidIdentifier(tokens[pos]))
                        return null;
                    var rec = new Record(wildcard, tokens[pos], pos, 1);
                    pos++;
                    AddToCache(rec);
                    wildcardMatches.Add(rec);
                }
                else
                {
                    if (wildcard != tokens[pos])
                        return null;
                    pos++;
                }
            }
            var value = rule.BuildMethod(wildcardMatches.Select(r => r.Value).ToArray());
            var _rec = new Record(rule.Key, value, originalPos, pos - originalPos);
            AddToCache(_rec);
            return _rec;
        }

        private Record LookFor(string ruleKey, List<string> tokens, int pos)
        {
            Record fromCache;
            if ((fromCache = GetFromCache(ruleKey, pos)) != null) 
                return fromCache;
            
            foreach(var rule in GetRules(ruleKey))
            {
                var rec = LookFor(rule, tokens, pos);
                if(rec!=null)
                {
                    AddToCache(rec);
                    return rec;
                }
            }
            return null;
        }
        #endregion

        #region ParseRules
        private List<ParseRule> ParseRules = new List<ParseRule>();

        private IEnumerable<ParseRule> GetRules(string ruleKey)
            => ParseRules.Where(r => r.Key == ruleKey);

        private void RegisterRule(string key, string rule, Func<object[], object> buildMethod)
            => ParseRules.Add(new ParseRule(key, rule.Split(' '), buildMethod));
        #endregion

        #region ParseCache
        private List<Record> ParseCache = new List<Record>();
        private void AddToCache(Record r)
        {
            if (GetFromCache(r.RuleKey, r.Position) == null)
                ParseCache.Add(r);
        }

        private Record GetFromCache(string ruleKey, int pos)        
            => ParseCache.Where(r => r.RuleKey == ruleKey && r.Position == pos).FirstOrDefault();
        #endregion

        #region Classes
        class Record
        {
            public string RuleKey { get; }
            public object Value { get; }
            public int Position { get; }
            public int Size { get; }
            public Record(string ruleKey, object value, int position, int size)
            {
                RuleKey = ruleKey;
                Value = value;
                Position = position;
                Size = size;
            }
        }

        class PropertyValue
        {
            public string Name { get; }
            public object[] Value { get; }

            public PropertyValue(string name, object[] value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return $"PV(Name={Name}; Values={string.Join(", ", Value)})";
            }
        }

        class ParseRule
        {
            public string Key { get; }
            public string[] ParsePattern { get; }
            public Func<object[], object> BuildMethod;

            public ParseRule(string key, string[] parsePattern, Func<object[], object> buildMethod)
            {
                Key = key;
                ParsePattern = parsePattern;
                BuildMethod = buildMethod;
            }
        }
        #endregion

        #region SplitToTokens
        public static List<string> SplitToTokens(string input)
        {
            return Regex.Matches(input, "([\"])(.*?)(?<!\\\\)(?>\\\\\\\\)*\\1|([^\"\\s]+)")
                .Cast<Match>()
                .Select(m => m.Value)
                 .Select(s =>
                 {
                     if (s[0] == '"') return new List<string> { s };
                     return Regex.Split(s, "((?=[{};=,])|(?<=[{};=,]))").ToList();
                 })
                 .SelectMany(x => x)
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
        }
        #endregion
    }
}
