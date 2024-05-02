using AI.Commons.Data;
using System.Collections.Generic;

namespace AI.Commons.IO
{
    public static class GraphReader
    {
        public static Graph<Dictionary<string, string>> FromGML(string text) => GmlParser.ParseText(text);
        public static Graph<Dictionary<string, string>> FromGML(byte[] bytes) => GmlParser.ParseBytes(bytes);
        public static Graph<Dictionary<string, string>> FromGMLFile(string filename) => GmlParser.ParseFile(filename);
    }
}
