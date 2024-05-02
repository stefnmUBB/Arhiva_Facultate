using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AI.Lab9.Utils
{
    public class CsvData : IEnumerable<CsvRow>
    {
        public int ColumnsCount { get; }
        public List<CsvRow> Items = new List<CsvRow>();

        internal Dictionary<string, int> ColumnIndices = new Dictionary<string, int>();

        public string[] Columns;

        static List<string> SplitToTokens(string input)
        {
            var list = new Regex(@"(?:([""])(.*?)(?<!\\)(?>\\\\)*\1|([^""\s]+))")
                .Matches(input)
                .Cast<Match>()
                .Select(m => m.Value)
                .Select(s =>
                {
                    if (s[0] == '"') return new List<string> { s.Substring(1, s.Length - 2) };
                    var l = Regex.Split(s, @"([,;])").ToList();
                    var iresult = new List<string>();

                    foreach (var i in l)
                    {
                        if (iresult.Count == 0)
                            iresult.Add(i);
                        else
                        {
                            var last = iresult.Last();
                            if ((i == ";" || i == ",") && (last == ";" || last == ","))
                                iresult.Add("<!NULL>");
                            iresult.Add(i);
                        }
                    }                    
                    return iresult;
                })
                .SelectMany(x => x).ToList();

            var result = new List<string>();

            var token = "";
            foreach(var i in list)
            {
                if(i==";" || i==",")
                {
                    result.Add(token);
                    token = "";
                    continue;
                }
                if (token == "")
                    token = i;
                else
                {                    
                    if (i != "")
                        token = token + " " + i;
                }
            }
            if (token != "" || list.LastOrDefault() == ";" || list.LastOrDefault() == ",") 
                result.Add(token);

            //Console.WriteLine(string.Join(";", result.Select(x => $"{x.Length} {x}")));

            return result;
        }

        private static string[] ReadLines(byte[] bytes)
        {
            var lines = new List<string>();
            using (var stream = new MemoryStream(bytes))
            using (var reader = new StreamReader(stream, Encoding.UTF8)) 
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines.ToArray();
        }

        public CsvData(byte[] bytes, bool hasHeader = true)
        {
            var lines = ReadLines(bytes).Where(l => l != "").ToArray();

            Columns = SplitToTokens(lines[0]).SelectIf(!hasHeader, (_, i) => $"Column{i}").ToArray();
            ColumnIndices = Columns.Select((c, i) => (c, i)).ToDictionary(x => x.c, x => x.i);

            ColumnsCount = ColumnIndices.Count;
            Items = lines.SkipIf(hasHeader, 1).Select(l => new CsvRow(this, SplitToTokens(l).ToArray())).ToList();
        }

        public CsvData(string filename, bool hasHeader = true)
        {
            var lines = File.ReadAllLines(filename).Where(l => l != "").ToArray();

            Columns = SplitToTokens(lines[0]).SelectIf(!hasHeader, (_, i) => $"Column{i}").ToArray();                            
            ColumnIndices = Columns.Select((c, i) => (c, i)).ToDictionary(x => x.c, x => x.i);            

            ColumnsCount = ColumnIndices.Count;
            Items = lines.SkipIf(hasHeader, 1).Select(l => new CsvRow(this, SplitToTokens(l).ToArray())).ToList();
        }
        public CsvData(string filename, params string[] customHeaders)
        {
            var lines = File.ReadAllLines(filename).Where(l => l != "").ToArray();

            Columns = customHeaders.ToArray();            
            ColumnIndices = Columns.Select((c, i) => (c, i)).ToDictionary(x => x.c, x => x.i);            

            ColumnsCount = ColumnIndices.Count;
            Items = lines.Select(l => new CsvRow(this, SplitToTokens(l).ToArray())).ToList();
        }

        public CsvData(byte[] bytes, params string[] customHeaders)
        {
            var lines = ReadLines(bytes).Where(l => l != "").ToArray();            

            Columns = customHeaders.ToArray();
            ColumnIndices = Columns.Select((c, i) => (c, i)).ToDictionary(x => x.c, x => x.i);

            ColumnsCount = ColumnIndices.Count;
            Items = lines.Select(l => new CsvRow(this, SplitToTokens(l).ToArray())).ToList();
        }

        public IEnumerator<CsvRow> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public IEnumerable<T> ToObjects<T>(Func<CsvRow, T> converter)
            => this.Select(converter);

        public IEnumerable<T> ToObjects<T>()
            => ToObjects(row =>
            {
                T result = (T)Activator.CreateInstance(typeof(T));
                foreach (var colName in Columns)
                {
                    var propName = string.Join("", Regex.Matches(colName, @"\b[\w']*\b")
                        .Cast<Match>().Select(m => m.Value).Select(s => s == "" ? "" : char.ToUpper(s[0]) + s.Substring(1)));                    
                    var prop = typeof(T).GetProperty(propName);
                    if (prop == null) continue;
                    prop.SetValue(result, Convert.ChangeType(row[colName], prop.PropertyType));
                }
                return result;
            });
    }

    public class CsvRow
    {
        CsvData ParentData;

        string[] Values;

        public int ColumnsCount => ParentData.ColumnsCount;

        internal CsvRow(CsvData parentData, string[] values)
        {
            ParentData = parentData;
            Values = values;          
        }

        public string this[int index] { get=> Values[index]; set => Values[index]=value; }
        public string this[string columnName]
        {
            get => Values[ParentData.ColumnIndices[columnName]];
            set => Values[ParentData.ColumnIndices[columnName]] = value;
        }


        public T Get<T>(int index) => (T)Convert.ChangeType(this[index], typeof(T));
        public T Get<T>(string columnName) => (T)Convert.ChangeType(this[columnName], typeof(T));

        public override string ToString()
        {
            return string.Join(", ", Values);
        }
    }
}
