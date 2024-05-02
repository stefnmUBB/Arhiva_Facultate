using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab10.Tools.Text
{
    public class W2VecReader
    {
        private Dictionary<string, double[]> Cache = new Dictionary<string, double[]>();
        private Dictionary<string, string> Redirects = new Dictionary<string, string>();

        private Dictionary<string, int> Indices = new Dictionary<string, int>();

        public W2VecReader()
        {
            Console.WriteLine("W2VecR - loading indices");
            Indices = File.ReadAllLines(@"Input\sorted\words.dat")
                    .Select(l => l.Split(' '))
                    .Select(p => (p[0], int.Parse(p[1])))
                    .Distinct(new eqComp())
                    .ToDictionary(p => p.Item1, p => p.Item2);
            Console.WriteLine("W2VecR - loaded indices");
        }

        
        public double[] GetVector(string key)
        {
            //Console.WriteLine("Getting vector : " + key);
            key = key.ToLowerInvariant();
            if (Redirects.ContainsKey(key))
                key = Redirects[key];
            if (Cache.ContainsKey(key))
                return Cache[key];
            if (!Indices.ContainsKey(key)) 
            {
                var nearKey = Indices.Keys.Where(k => k.StartsWith(key) || k.Contains(key)).FirstOrDefault();
                if (nearKey == null)
                    return null;
                Redirects[key] = nearKey;
                key = nearKey;                
            }
            int page = Indices[key];

            var bytes = File.ReadAllBytes($@"Input\sorted\w2vec{page}.dat");

            int stride = 2528;
            int l = 0;
            int r = 10000;
            //Console.WriteLine("BS vector : " + key + " in page " + page);
            while (l<r)
            {
                int m = (l + r) / 2;

                int pos = stride * m;

                var mword = Encoding.ASCII.GetString(bytes.Skip(pos).Take(128).ToArray()).TrimEnd('\0');                
                if(mword==key)
                {
                    var vec = new double[300];
                    using (var ms = new MemoryStream(bytes.Skip(pos + 128).Take(2400).ToArray()))
                    {
                        using (var br = new BinaryReader(ms))
                        {
                            for (int i = 0; i < 300; i++)
                                vec[i] = br.ReadDouble();
                        }                        
                    }
                    Cache[key] = vec;
                    return vec;
                }

                var cmp = string.Compare(mword, key, comparisonType: StringComparison.OrdinalIgnoreCase);

                if (cmp<0)
                {
                    l = m + 1;
                }
                else
                {
                    r = m;
                }
            }
            return null;
            throw new ArgumentException($"W2Vec key not found: {key}");
        }



        private class eqComp : IEqualityComparer<(string, int)>
        {
            public bool Equals((string, int) x, (string, int) y)
            {
                return x.Item1 == y.Item1;
            }

            public int GetHashCode((string, int) obj)
            {
                return obj.Item1.GetHashCode();
            }
        }

        public static W2VecReader Global = new W2VecReader();

    }
}
