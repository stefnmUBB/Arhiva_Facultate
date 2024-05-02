using AI.Commons.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Commons.UI.Controls
{
    public partial class RandomGraphBuilder : UserControl
    {
        public RandomGraphBuilder()
        {
            InitializeComponent();
        }

        internal Graph<Dictionary<string, string>> Generate()
        {
            int N = (int)NodesCountBox.Value;
            int C = (int)ExpectedComsBox.Value;
            int Pin = (int)(PInsideBox.Value * 100);
            int Pout = (int)(POutsideBox.Value * 100);


            var g = new Graph<Dictionary<string, string>>();

            List<int>[] communities = new List<int>[C];
            for (int i = 0; i < C; i++) communities[i] = new List<int>();

            var rand = new Random();

            for (int i = 0; i < N; i++)
            {
                int p = rand.Next() % C;
                g.Nodes.Add(new Dictionary<string, string> { { "id", i.ToString() } });
                communities[p].Add(i);
            }

            for (int c = 0; c < C; c++)
            {
                foreach (var i in communities[c])
                {
                    foreach (var j in communities[c])
                    {
                        if (i == j) continue;
                        int p = rand.Next() % 100;
                        if (p < Pin)
                        {
                            g.Edges.Add((i, j));
                            g.Edges.Add((j, i));
                        }
                    }
                }
            }

            communities = communities.ToList().Where(c => c.Count != 0).ToArray();

            for (int c1 = 0; c1 < communities.Length; c1++) 
            {
                for (int c2 = c1 + 1; c2 < communities.Length; c2++) 
                {
                    for(int i = 0; i < communities[c1].Count;i++)
                    {
                        for(int j = 0; j < communities[c2].Count;j++)
                        {
                            int p = rand.Next() % 100;
                            if (p < Pout)
                            {
                                int u = communities[c1][i];
                                int v = communities[c2][j];

                                g.Edges.Add((u, v));
                                g.Edges.Add((v, u));
                            }
                        }
                    }                   
                }
            }            

            return g;
        }
    }
}
