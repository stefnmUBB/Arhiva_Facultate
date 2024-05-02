using AI.Lab2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab2.IO
{
    internal static class GraphReader
    {
        public static Graph<Dictionary<string, string>> FromGML(string text) => GmlParser.ParseText(text);
        public static Graph<Dictionary<string, string>> FromGML(byte[] bytes) => GmlParser.ParseBytes(bytes);
        public static Graph<Dictionary<string, string>> FromGMLFile(string filename) => GmlParser.ParseFile(filename);
    }
}
