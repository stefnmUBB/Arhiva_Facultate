using AI.Commons.Data;
using AI.Commons.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab4.UI.Controls
{
    public partial class CartesianGraphViewer : UserControl
    {
        public CartesianGraphViewer()
        {
            InitializeComponent();
        }

        public List<Point> Points = new List<Point>();
        public List<(int P1, int P2)> Segments = new List<(int P1, int P2)>();
        public List<int> HighlightedPath = new List<int>();

        Point _ViewPoint = new Point(0, 0);
        Size _ViewSize = new Size(100, 100);

        public Point ViewPoint
        {
            get => _ViewPoint;
            set
            {
                _ViewPoint = value;
                Invalidate();
            }
        }

        int _Zoom = 100;
        public int Zoom
        {
            get => _Zoom;
            set
            {
                _Zoom = value;
                Invalidate();
            }
        }

        public Size ViewSize
        {
            get => new Size(Width * 100 / Zoom, Height * 100 / Zoom);
        }

        int TranslateX(int x)
        {
            return Width / 2 + (x - ViewPoint.X) * Width / ViewSize.Width;
        }

        int TranslateY(int y)
        {
            return Height / 2 + (y - ViewPoint.Y) * Height / ViewSize.Height;
        }

        public Point Translate(Point realPoint)
        {
            return new Point(TranslateX(realPoint.X), TranslateY(realPoint.Y));
        }
      
        int UntranslateX(int x)
        {
            return (x - Width / 2) * ViewSize.Width / Width + ViewPoint.X;
        }

        int UntranslateY(int y)
        {
            return (y - Height / 2) * ViewSize.Height / Height + ViewPoint.Y;
        }

        public void HighlightPath(int[] path, bool cycle = false)
        {
            HighlightedPath = path.ToList();
            if(HighlightedPath.Count>1 && cycle)
            {
                HighlightedPath.Add(HighlightedPath.First());
            }
            Invalidate();
        }

        private void CartesianGraphViewer_Paint(object sender, PaintEventArgs e)
        {
            {
                int x0 = UntranslateX(0) / 20;
                int x1 = UntranslateX(Width) / 20;

                for (int x = x0; x <= x1; x++)
                {
                    int tx = TranslateX(x * 20);

                    var pen = x == 0 ? new Pen(Brushes.Black, 2) : Pens.Black;

                    e.Graphics.DrawLine(pen, tx, 0, tx, Height);
                }

                int y0 = UntranslateY(0) / 20;
                int y1 = UntranslateY(Height) / 20;

                for (int y = y0; y <= y1; y++)
                {
                    int ty = TranslateY(y * 20);
                    var pen = y == 0 ? new Pen(Brushes.Black, 2) : Pens.Black;
                    e.Graphics.DrawLine(pen, 0, ty, Width, ty);
                }
            }

            for (int i = 0; i < Segments.Count; i++)
            {
                var p1 = Translate(Points[Segments[i].P1]);
                var p2 = Translate(Points[Segments[i].P2]);
                e.Graphics.DrawLine(Pens.Blue, p1, p2);
            }

            var highlightPen = new Pen(Brushes.Red, 2);

            if (HighlightedPath.Count > 0 && Points.Count > 0) 
            {
                var p1 = Translate(Points[HighlightedPath[0]]);
                for (int i = 1; i < HighlightedPath.Count; i++)
                {
                    var p2 = Translate(Points[HighlightedPath[i]]);
                    e.Graphics.DrawLine(highlightPen, p1, p2);
                    p1 = p2;
                }
            }           
            

            for (int i = 0; i < Points.Count; i++) 
            {
                var t = Translate(Points[i]);
                e.Graphics.FillEllipse(Brushes.Red, t.X - 10, t.Y - 10, 20, 20);
                e.Graphics.DrawString((i + 1).ToString(), Font, Brushes.White, t.X - 7, t.Y - 7);
            }
            
        }

        bool msDown = false;
        bool msMoved = false;
        Point msPoint;
        Point msViewPoint;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                msDown = true;
                msMoved = false;
                msPoint = PointToClient(Cursor.Position);
                msViewPoint = ViewPoint;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            msDown = false;

            if(!msMoved)
            {
                var p = PointToClient(Cursor.Position);
                p = new Point(UntranslateX(p.X), UntranslateY(p.Y));
                Points.Add(p);
                Invalidate();
                
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!msDown) return;
            msMoved = true;

            var p = PointToClient(Cursor.Position);

            ViewPoint = new Point(msViewPoint.X - p.X + msPoint.X, msViewPoint.Y - p.Y + msPoint.Y);
            
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        int Distance(Point p, Point q)
        {
            int dx = p.X - q.X;
            int dy = p.Y - q.Y;
            return (int)Math.Sqrt(dx * dx + dy * dy);
        }

        public WeightedGraph<int, int> GetGraph()
        {
            var graph = new WeightedGraph<int, int>(Enumerable.Range(0, Points.Count).ToList());
            if(Segments.Count==0)
            {
                for(int i=0;i<Points.Count;i++)
                {
                    for (int j = i + 1; j < Points.Count; j++) 
                    {
                        graph.Edges.Add((i, j, Distance(Points[i], Points[j])));
                        graph.Edges.Add((j, i, Distance(Points[i], Points[j])));
                    }
                }
            }
            else
            {
                foreach(var seg in Segments)
                {
                    int c = Distance(Points[seg.P1], Points[seg.P2]);
                    graph.Edges.Add((seg.P1, seg.P2, c));
                    graph.Edges.Add((seg.P2, seg.P1, c));
                }
            }

            return graph;
        }

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Zoom = (Zoom + Math.Sign(e.Delta) * 2).Clamp(10, 300);
        }        
    }
}
