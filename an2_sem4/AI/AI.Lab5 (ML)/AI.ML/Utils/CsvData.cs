using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AI.ML.Utils
{
    public class CsvData : IEnumerable<CsvRow>
    {
        public int ColumnsCount { get; }
        public List<CsvRow> Items = new List<CsvRow>();

        internal Dictionary<string, int> ColumnIndices = new Dictionary<string, int>();

        public string[] Columns;

        public CsvData(string filename)
        {
            var lines = File.ReadAllLines(filename);
            Columns = lines[0].Split(',');
            ColumnIndices = Columns.Select((c, i) => (c, i)).ToDictionary(x => x.c, x => x.i);
            ColumnsCount = ColumnIndices.Count;
            Items = lines.Skip(1).Select(l => new CsvRow(this, l.Split(','))).ToList();            
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
                foreach(var colName in Columns)
                {
                    var prop = typeof(T).GetProperty(colName);
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

        internal CsvRow(CsvData parentData, string[] values)
        {
            ParentData = parentData;
            Values = values;
        }

        public string this[int index] => Values[index];
        public string this[string columnName] => Values[ParentData.ColumnIndices[columnName]];

        public T Get<T>(int index) => (T)Convert.ChangeType(this[index], typeof(T));
        public T Get<T>(string columnName) => (T)Convert.ChangeType(this[columnName], typeof(T));

        public override string ToString()
        {
            return string.Join(", ", Values);
        }
    }
}
