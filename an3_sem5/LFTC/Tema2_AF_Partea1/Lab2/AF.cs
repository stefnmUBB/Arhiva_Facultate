using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Lab2
{
    internal class AF
    {
        class State
        {
            public string Name;
            public Dictionary<string, HashSet<State>> Next = new Dictionary<string, HashSet<State>>();
            public bool IsFinal;
            public State(string name)
            {
                Name = name;
            }

            public void SetNextState(string key, State state)
            {
                GetNextStates(key).Add(state);
            }

            public HashSet<State> GetNextStates(string key)
                => Next.TryGetValue(key, out var result) ? result : (Next[key] = new HashSet<State>());            
        }

        private Dictionary<string, State> States = new Dictionary<string, State>();

        private State InitialState;

        public AF() { }
        
        public AF(TextReader tr)
        {
            Load(this, tr);
        }

        public AF(Stream s, bool closeAfterReading = false)
        {
            using (var tr = new StreamReader(s))
                Load(this, tr);
            if (closeAfterReading)
                s.Close();
        }

        public AF(string s)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(s ?? "")))
            using (var tr = new StreamReader(ms))
                Load(this, tr);
        }


        private State GetOrCreateState(string name)
            => States.TryGetValue(name, out var result) ? result : (States[name] = new State(name));


        private void Clear()
        {
            States.Clear();
            InitialState = null;
        }

        private static void Load(AF af, TextReader tr)
        {
            af.Clear();
            string line;
            while ((line = tr.ReadLine()) != null)
            {
                var items = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);                
                if (items.Length == 0) continue;
                if (items.Length >= 1)
                {
                    if (items[0].ToUpper() == "#S")
                    {
                        if (items.Length < 2)
                            throw new InvalidDataException("Syntax error: #S <initial_state>");
                        af.InitialState = af.GetOrCreateState(items[1]);
                        continue;
                    }
                    else if (items[0].ToUpper() == "#F")
                    {
                        foreach (var it in items.Skip(1))
                            af.GetOrCreateState(it).IsFinal = true;
                        continue;
                    }
                    else if (items[0].ToUpper() == "#DONE")
                        break;
                }

                af.GetOrCreateState(items[0]);

                if (items.Length == 3)
                {
                    var s = af.GetOrCreateState(items[0]);
                    var d = af.GetOrCreateState(items[2]);
                    var a = items[1];
                    s.SetNextState(a, d);
                }
            }
            af.IsDeterministic = !af.States.Values.Any(s => s.Next.Values.Any(l => l.Count > 1));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"AF deterministic = {IsDeterministic}");
            sb.AppendLine($"Q = {{ {string.Join(", ", States.Keys)} }}");
            sb.AppendLine($"Σ = {{ {string.Join(", ", States.Values.SelectMany(_ => _.Next.Keys).Distinct().OrderBy(_ => _))} }}");
            var transitions = States
                    .SelectMany(s => s.Value.Next.SelectMany(n =>
                        n.Value.Select(d => $"({s.Key},{n.Key})->{d.Name}")));
            sb.AppendLine($"δ = {{ {string.Join(", ", transitions)} }}");
            sb.AppendLine($"q0 = {InitialState?.Name ?? "(null)"}");
            sb.AppendLine($"F = {{ {string.Join(", ", States.Values.Where(_ => _.IsFinal).Select(_ => _.Name))} }}");
            return sb.ToString();
        }


        public bool IsDeterministic { get; private set; }

        public string LongestMatch(string input)
        {
            if (!IsDeterministic)
                throw new InvalidOperationException("AF must be deterministic");
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var state = InitialState;
            var result = "";

            //if (input == "" && state.IsFinal) result = "";

            void nextMatch(ref State s, string str, ref int p)
            {
                str = str.Substring(p);
                foreach (var a in s.Next.Keys)
                {
                    if (str.StartsWith(a))
                    {
                        s = s.Next[a].First();
                        p = p + a.Length;
                        Console.WriteLine($"Chose {a}");
                        return;
                    }
                }
                Console.WriteLine($"Can't continue");
                s = null;
                p = 0;
            }

            int i = 0;
            while (state != null && i < input.Length)
            {
                nextMatch(ref state, input, ref i);
                if (state != null) result = input.Substring(0, i);

            }
            return result;
            //return i == input.Length && (state?.IsFinal ?? false);
        }

        public bool IsMatch(string input)
        {
            if (!IsDeterministic)
                throw new InvalidOperationException("AF must be deterministic");
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var state = InitialState;            
            if (input == "" && state.IsFinal) return true;

            void nextMatch(ref State s, string str, ref int p)
            {
                str = str.Substring(p);
                foreach(var a in s.Next.Keys)
                {
                    if(str.StartsWith(a))
                    {
                        s = s.Next[a].First();
                        p = p + a.Length;
                        return;
                    }
                }
                s = null;
                p = 0;
            }

            

            int i = 0;
            while (state != null && i < input.Length) 
            {
                nextMatch(ref state, input, ref i);                
            }

            return i == input.Length && (state?.IsFinal ?? false);
        }
    }
}
