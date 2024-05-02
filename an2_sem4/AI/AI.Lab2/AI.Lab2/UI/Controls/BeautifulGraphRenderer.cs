using AI.Lab2.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab2.UI.Controls
{
    internal class ForceBasedGraphRenderer<T> : AbstractGraphRenderer<T>
    {
        const int NodeSide = 30;
        const int IntimacyRadius = 4 * NodeSide / 3;

        static Random Random = new Random();

        class Node
        {
            public string Label;
            public double X;
            public double Y;

            public double VX = 0;
            public double VY = 0;            

            public Node(string label, int i, int sz_factor)
            {
                Label = label;

                double a = 2 * Math.PI * i / sz_factor - Math.PI / 2;
                X = (int)(sz_factor * IntimacyRadius * Math.Cos(a));
                Y = (int)(sz_factor * IntimacyRadius * Math.Sin(a));                
            }
        };

        public ForceBasedGraphRenderer(string displayMember = null)
        {
            DisplayMember = displayMember;
        }


        static double dt = 0.0005;

        void RepulseNodes(Node n1, Node n2)
        {
            var dx = n2.X - n1.X;
            var dy = n2.Y - n1.Y;

            var d = (dx * dx + dy * dy);

            var f = 150; // * n1.m * n2.m;

            dx = dx * f / d;
            dy = dy * f / d;

            n1.VX -= dx * dt;
            n1.VY -= dy * dt;

            n2.VX += dx * dt;
            n2.VY += dy * dt;

        }

        void AttractNodes(Node n1, Node n2)
        {
            var dx = n2.X - n1.X;
            var dy = n2.Y - n1.Y;

            var d = Math.Sqrt(dx * dx + dy * dy);

            var f = 0.2;

            dx = dx * f * (d - IntimacyRadius) / 2;
            dy = dy * f * (d - IntimacyRadius) / 2;

            n1.VX += dx * dt;
            n1.VY += dy * dt;

            n2.VX -= dx * dt;
            n2.VY -= dy * dt;
        }

        public override Bitmap Render(Graph<T> graph, List<List<int>> colors = null)
        {            
            int[] clid = new int[graph.Nodes.Count];
            Color[] ncolors;

            if (colors != null)
            {
                for (int c = 0; c < colors.Count; c++)
                {
                    colors[c].ForEach(i => clid[i] = c);
                }
                ncolors = new Color[colors.Count];
                
                for(int i=0;i<ncolors.Length;i++)
                {
                    ncolors[i] = (Color)(typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public)
                        .ToList()
                        .Where(p => p.PropertyType == typeof(Color))
                        .ElementAt(1 + i)
                        .GetValue(null));
                }
            }
            else
            {
                ncolors = new Color[1];
                ncolors[0] = Color.White;
            } 
                
           

            var nodes = graph.Nodes.Select((n, i) => new Node(GetNodeLabel(n), i, graph.Nodes.Count)).ToList();

            for (int k = 0; k < 200; k++) 
            {
                for(int i=0;i<graph.Nodes.Count;i++)
                {
                    for (int j = i + 1; j < graph.Nodes.Count; j++)
                        RepulseNodes(nodes[i], nodes[j]);
                }
                foreach (var e in graph.Edges) 
                {
                    if (e.Source < e.Target)
                        AttractNodes(nodes[e.Source], nodes[e.Target]);
                }

                for (int i = 0; i < graph.Nodes.Count; i++) 
                {
                    nodes[i].X += nodes[i].VX * dt;
                    nodes[i].Y += nodes[i].VY * dt;
                }
            }

            var minX = nodes.Min(n => n.X);
            var minY = nodes.Min(n => n.Y);

            nodes.ForEach(n =>
            {
                n.X -= minX;
                n.Y -= minY;

                //n.X *= 3;
                //n.Y *= 3;

                n.X += 2*NodeSide;
                n.Y += 2*NodeSide;
            });            

            int maxX = (int)nodes.Max(n => n.X) + 2*NodeSide;
            int maxY = (int)nodes.Max(n => n.Y) + 2*NodeSide;

            var bmp = new Bitmap(maxX, maxY);

            using (var g = Graphics.FromImage(bmp))
            {

                Point get_point(int i)
                {
                    return new Point((int)nodes[i].X, (int)nodes[i].Y);
                }

                foreach (var e in graph.Edges)
                {
                    g.DrawLine(Pens.Black, get_point(e.Source), get_point(e.Target));
                }

                Font font = new Font("Arial", 8);

                for (int i = 0; i < graph.Nodes.Count; i++)
                {
                    string label = GetNodeLabel(graph.Nodes[i]);
                    var point = get_point(i);
                    g.FillEllipse(new SolidBrush(ncolors[clid[i]]), point.X - 16, point.Y - 16, 32, 32);
                    g.DrawEllipse(Pens.Black, point.X - 16, point.Y - 16, 32, 32);
                    var format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    point.Y -= 4;
                    g.DrawString(label, font, Brushes.Black, point, format);

                }
            }


            return bmp;
        }
    }
}
