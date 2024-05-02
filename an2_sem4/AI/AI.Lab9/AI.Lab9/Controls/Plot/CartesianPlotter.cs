using AI.Lab9.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AI.Lab9.Controls.Plot
{
    public partial class CartesianPlotter : UserControl, IPlotter
    {
        public CartesianPlotter()
        {
            InitializeComponent();
            BackColor = Color.White;
        }

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
            return Width / 2 + (x + ViewPoint.X) * Width / ViewSize.Width;
        }

        int TranslateY(int y)
        {
            return Height / 2 - (y + ViewPoint.Y) * Height / ViewSize.Height;
        }

        public Point Translate(Point realPoint)
        {
            return new Point(TranslateX(realPoint.X), TranslateY(realPoint.Y));
        }

        int UntranslateX(int x)
        {
            return (x - Width / 2) * ViewSize.Width / Width - ViewPoint.X;
        }

        int UntranslateY(int y)
        {
            return (Height / 2 - y) * ViewSize.Height / Height - ViewPoint.Y;
        }     

        public Point Untranslate(Point screenPoint)
        {
            return new Point(UntranslateX(screenPoint.X), UntranslateY(screenPoint.Y));
        }

        protected override void OnPaint(PaintEventArgs e)                
        {
            int lum = (int)(8 - Math.Log(Zoom)) * 40;
            var gridPen = new Pen(Color.FromArgb(lum, lum, lum));

            {
                int x0 = UntranslateX(0) / 20;
                int x1 = UntranslateX(Width) / 20;

                for (int x = x0; x <= x1; x++)
                {
                    int tx = TranslateX(x * 20);

                    var pen = x == 0 ? new Pen(Brushes.Black, 2) : gridPen;

                    e.Graphics.DrawLine(pen, tx, 0, tx, Height);
                }

                int y0 = UntranslateY(0) / 20;
                int y1 = UntranslateY(Height) / 20;

                for (int y = y0; y >= y1; y--) 
                {
                    int ty = TranslateY(y * 20);                    
                    var pen = y == 0 ? new Pen(Brushes.Black, 2) : gridPen;
                    e.Graphics.DrawLine(pen, 0, ty, Width, ty);
                }
            }

            foreach (var l in Rectangles.Values)
            {
                foreach (var r in l)
                {
                    var pt1 = Translate(r.P1);
                    var pt2 = Translate(r.P2);
                    //Console.WriteLine($"{pt1}, {pt2}");
                    e.Graphics.FillRectangle(new SolidBrush(r.C), pt1.X, pt1.Y, pt2.X - pt1.X + 2, pt1.Y - pt2.Y + 2);
                }
            }

            foreach (var l in Segments.Values) 
            {
                foreach (var s in l)
                {
                    var p1 = Translate(s.P1);
                    var p2 = Translate(s.P2);
                    e.Graphics.DrawLine(new Pen(new SolidBrush(s.C), 2.5f), p1, p2);
                }
            }

            foreach(var l in Points.Values)
            {
                foreach(var p in l)
                {
                    var pt = Translate(p.P);
                    e.Graphics.FillEllipse(new SolidBrush(p.C), pt.X - 5, pt.Y - 5, 10, 10);
                }
            }            
        }

        bool msDown = false;
        bool msMoved = false;
        Point msPoint;
        Point msViewPoint;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
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

            if (!msMoved)
            {
                /*var p = PointToClient(Cursor.Position);
                p = new Point(UntranslateX(p.X), UntranslateY(p.Y));                
                Invalidate();*/
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!msDown) return;
            msMoved = true;

            var p = PointToClient(Cursor.Position);

            ViewPoint = new Point(
                UntranslateX(TranslateX(msViewPoint.X) + (p.X - msPoint.X)),
                UntranslateY(TranslateY(msViewPoint.Y) + (p.Y - msPoint.Y)));

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

        protected override void OnResize(EventArgs e)
        {
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Zoom = (Zoom + Math.Sign(e.Delta) * (int)Math.Log(Zoom)).Clamp(10, 1000);
        }

        Dictionary<IPlottable, List<(Point P, Color C)>> Points = new Dictionary<IPlottable, List<(Point P, Color C)>>();
        Dictionary<IPlottable, List<(Point P1, Point P2, Color C)>> Segments = new Dictionary<IPlottable, List<(Point P1, Point P2, Color C)>>();
        Dictionary<IPlottable, List<(Point P1, Point P2, Color C)>> Rectangles = new Dictionary<IPlottable, List<(Point P1, Point P2, Color C)>>();

        public void Clear(IPlottable plottable)
        {
            if(plottable == null)
            {
                Points.Clear();
                Segments.Clear();
                Rectangles.Clear();
                Invalidate();
                return;
            }

            if(Points.ContainsKey(plottable))
                Points[plottable].Clear();
            if (Segments.ContainsKey(plottable))
                Segments[plottable].Clear();
            if (Rectangles.ContainsKey(plottable))
                Rectangles[plottable].Clear();
        }

        void IPlotter.DrawPoint(IPlottable plottable, double x, double y, Color color)
        {
            if (!Points.ContainsKey(plottable)) 
                Points[plottable] = new List<(Point P, Color C)>();
            Points[plottable].Add((new Point((int)(20 * x), (int)(20 * y)), color));
        }

        void IPlotter.DrawSegment(IPlottable plottable, double x1, double y1, double x2, double y2, Color color)
        {
            if (!Segments.ContainsKey(plottable))
                Segments[plottable] = new List<(Point P1, Point P2, Color C)>();
            Segments[plottable].Add((new Point((int)(20*x1), (int)(20 * y1)), new Point((int)(20 * x2), (int)(20 * y2)), color));
        }

        void IPlotter.DrawRectangle(IPlottable plottable, double x1, double y1, double x2, double y2, Color color)
        {
            if(!Rectangles.ContainsKey(plottable))
            {
                Rectangles[plottable] = new List<(Point P1, Point P2, Color C)>();
            }

            var rx1 = Math.Min(x1, x2);
            var ry1 = Math.Min(y1, y2);

            var rx2 = Math.Max(x1, x2);
            var ry2 = Math.Max(y1, y2);

            Rectangles[plottable].Add((new Point((int)(20 * rx1), (int)(20 * ry1)), new Point((int)(20 * rx2), (int)(20 * ry2)), color));
        }

        public void Deploy()
        {
            Invalidate();
        }
    }
}
