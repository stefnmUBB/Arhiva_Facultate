using AI.Commons.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AI.Commons.UI.Controls
{
    public class CommunityGraphRenderer<T> : AbstractGraphRenderer<T>
    {
        static readonly int NodeSide = 32;

        public List<List<int>> NodeCommunity = new List<List<int>>();

        class Node 
        {
            public int Id;
            public string Label;
            public int X; 
            public int Y;

            public Node(int id, string label, int x = 0, int y = 0)
            {
                Id = id;
                Label = label;
                X = x;
                Y = y;
            }

            public Point Position => new Point(X, Y);
        }

        class Community
        {
            public List<Node> Nodes = new List<Node>();            
            public Community(List<Node> nodes)
            {
                Nodes = nodes;

                int rad = Nodes.Count * 2 * NodeSide;

                Point get_point(int i)
                {
                    double a = 2 * Math.PI * i / Nodes.Count - Math.PI / 2;
                    return new Point((int)(rad * Math.Cos(a)), (int)(rad * Math.Sin(a)));
                }

                for(int i=0;i<Nodes.Count;i++)
                {
                    var p = get_point(i);
                    Nodes[i].X = p.X;
                    Nodes[i].Y = p.Y;
                }               
            }

            public void Move(int dx, int dy) => Nodes.ForEach(n => { n.X += dx; n.Y += dy; });

            public int Size => 2 * Nodes.Count * NodeSide + 2 * NodeSide;
        }

        public CommunityGraphRenderer(string displayName, List<List<int>> nodesCom) : base(displayName)
        {
            NodeCommunity = nodesCom;
        }

        public override Bitmap Render(Graph<T> graph, List<List<int>> colors = null)
        {
            LoadNodeColors(colors, graph.Nodes.Count);


            var comms = NodeCommunity
                .Select(l => new Community(l.Select(n => new Node(n, GetNodeLabel(graph.Nodes[n]))).ToList()))
                .ToList();

            int mxsize = comms.Max(c => c.Size);

            int minX = comms.Min(c => c.Nodes.Min(n => n.X));
            int minY = comms.Min(c => c.Nodes.Min(n => n.Y));

            comms.ForEach(c => c.Move(-minX, -minY));

            //int r = mxsize * comms.Count / 2;
            int x = 0;
            
            for(int i=0;i<comms.Count;i++)
            {
                //double a = 2 * Math.PI * i / comms.Count;
                //comms[i].Move((int)(r * Math.Cos(a)), (int)(r * Math.Sin(a)));
                comms[i].Move(x + NodeSide / 2, NodeSide / 2);
                x += 2 * comms[i].Size;
            }

            Node[] nodes = new Node[graph.Nodes.Count];            


            foreach(var com in comms)
            {
                foreach(var n in com.Nodes)
                {
                    nodes[n.Id] = n;
                    //Debug.WriteLine(n.Id);
                }
            }

            for (int i = 0; i < graph.Nodes.Count; i++)
                if (nodes[i] == null)
                {
                    Debug.WriteLine($"NULLLL Node={i}");
                }

            //int s = 2 * r + 2 * mxsize;

            var bmp = new Bitmap(x + 2 * mxsize + NodeSide, 2 * mxsize + NodeSide);

            using (var g = Graphics.FromImage(bmp))
            {                

                foreach (var e in graph.Edges)
                {
                    g.DrawLine(Pens.Black, nodes[e.Source].Position, nodes[e.Target].Position);
                    //Debug.WriteLine($"{nodes[e.Source].Position}; {nodes[e.Target].Position}");
                }

                Font font = new Font("Arial", 8);

                for (int i = 0; i < graph.Nodes.Count; i++)
                {
                    var point = nodes[i].Position;
                    string label = GetNodeLabel(graph.Nodes[i]);                    
                    g.FillEllipse(new SolidBrush(GetNodeColor(i)), point.X - 16, point.Y - 16, 32, 32);
                    g.DrawEllipse(Pens.Black, point.X - 16, point.Y - 16, 32, 32);
                    var format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    point.Y -= 4;
                    point.Y -= 4;
                    g.DrawString(label, font, Brushes.Black, point, format);

                }
            }

            return bmp;
        }
    }
}
