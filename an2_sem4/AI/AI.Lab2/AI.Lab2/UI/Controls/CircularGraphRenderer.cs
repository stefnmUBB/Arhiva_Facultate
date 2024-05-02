using AI.Lab2.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab2.UI.Controls
{
    internal class CircularGraphRenderer<T> : AbstractGraphRenderer<T>
    {
        public CircularGraphRenderer(string displayMember = null) : base()
        {
            DisplayMember = displayMember;
        }


        public override Bitmap Render(Graph<T> graph, List<List<int>> colors = null)
        {
            const int node_side = 30;
            int nodes_count = graph.Nodes.Count;

            int side = node_side * (nodes_count / 2 + 2);
            int mid = side / 2;
            int rad = side / 2 - node_side;

            Bitmap bitmap = new Bitmap(side, side);
            using (var g = Graphics.FromImage(bitmap))
            {

                Point get_point(int i)
                {
                    double a = 2 * Math.PI * i / nodes_count - Math.PI / 2;
                    return new Point(mid + (int)(rad * Math.Cos(a)), mid + (int)(rad * Math.Sin(a)));
                }

                foreach (var e in graph.Edges)
                {
                    g.DrawLine(Pens.Black, get_point(e.Source), get_point(e.Target));
                }

                Font font = new Font("Arial", 8);

                for (int i=0;i<graph.Nodes.Count;i++)
                {
                    string label = GetNodeLabel(graph.Nodes[i]);                    
                    var point = get_point(i);
                    g.FillEllipse(Brushes.White, point.X - 16, point.Y - 16, 32, 32);
                    g.DrawEllipse(Pens.Black, point.X - 16, point.Y - 16, 32, 32);
                    var format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    point.Y -= 4;
                    g.DrawString(label, font, Brushes.Black, point, format);                    
                    
                }
            }
            return bitmap;
        }
    }
}
