using AI.Lab2.Data;
using AI.Lab2.Utils;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AI.Lab2.IO
{
    internal class GmlParser
    {       
        private static Dictionary<string, string> ReadNode(Queue<string> tokens)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string token = tokens.Dequeue();
            if (token != "[")
                throw new ArgumentException("'[' expected after 'node'");

            while (tokens.Peek() != "]") 
            {
                var prop_name = tokens.Dequeue();
                var val_name = tokens.Dequeue();
                result[prop_name] = val_name;
            }
            tokens.Dequeue(); // "]"

            return result;
        }

        private static (string Source, string Target) ReadEdge(Queue<string> tokens)
        {
            (string Source, string Target) result = (null, null);

            string token = tokens.Dequeue();
            if (token != "[")
                throw new ArgumentException("'[' expected after 'edge'");

            while (tokens.Peek() != "]")
            {
                var prop_name = tokens.Dequeue();
                var val_name = tokens.Dequeue();

                if (prop_name == "source")
                    result.Source = val_name;
                else if (prop_name == "target")
                    result.Target = val_name;                
            }
            tokens.Dequeue(); // "]"          

            return result;
        }


        private static Graph<Dictionary<string, string>> BuildGraph(Queue<string> tokens)
        {
            var graph = new Graph<Dictionary<string, string>>();

            string token = tokens.Dequeue();
            if (token != "[")
                throw new ArgumentException("'[' expected after 'graph'");

            Dictionary<string, int> IdMapper = new Dictionary<string, int>();

            bool directed = false;

            while (tokens.Peek() != "]") 
            {
                token = tokens.Dequeue();

                if(token=="directed")
                {
                    var value = int.Parse(tokens.Dequeue());
                    directed = (value == 1);
                }

                if (token == "node")
                {
                    var node = ReadNode(tokens);
                    graph.Nodes.Add(node);
                    IdMapper[node["id"]] = graph.Nodes.Count - 1;
                }
                if (token == "edge")
                {
                    var edge = ReadEdge(tokens);

                    int from = IdMapper[edge.Source];
                    int to = IdMapper[edge.Target];

                    graph.Edges.Add((from, to));
                    if(!directed)
                    {
                        graph.Edges.Add((to, from));
                    }                    
                }
            }

            graph.Edges = graph.Edges.Distinct().ToList();

            return graph;
        }

        public static Graph<Dictionary<string, string>> ParseText(string input)
        {
            var tokens = new Queue<string>(input.SplitToTokens());

            while(tokens.Count > 0)
            {
                string token = tokens.Dequeue();
                if(token=="creator")
                {
                    tokens.Dequeue(); // skip author name
                    continue;
                }
                else if(token=="graph")
                {
                    return BuildGraph(tokens);
                }
            }

            throw new ArgumentException("Invalid GML Input");

            //return new Graph<Dictionary<string, string>>();
        }

        public static Graph<Dictionary<string, string>> ParseBytes(byte[] bytes)
        {
            return ParseText(Encoding.UTF8.GetString(bytes));
        }

        public static Graph<Dictionary<string, string>> ParseFile(string filename)
        {
            return ParseText(File.ReadAllText(filename));
        }

    }
}
