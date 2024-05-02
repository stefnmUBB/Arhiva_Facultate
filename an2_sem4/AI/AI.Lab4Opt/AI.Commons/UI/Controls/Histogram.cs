using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Commons.UI.Controls
{
    public class Histogram : Panel
    {
        public Histogram() : base() 
        {
            
        }        

        Bitmap Bitmap = new Bitmap(10, 10);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);            
            e.Graphics.DrawImage(Bitmap, 0, 0, Width, Height);
            e.Graphics.DrawLine(Pens.Black, 0, Height / 2, Width, Height / 2);
            if (ValuesCount > 0)
            {
                int x1 = SelectedX * Width / ValuesCount;
                int x2 = (SelectedX+1) * Width / ValuesCount;
                e.Graphics.DrawRectangle(Pens.Red, x1, -1, x2-x1, Height);
            }

            e.Graphics.DrawString($"Amplitude = {QMax}", Font, Brushes.Brown, 0, Height - 2 * Font.Height);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var x = PointToClient(Cursor.Position).X;
            SelectedX = x * ValuesCount / Width;
            base.OnMouseDown(e);
        }

        private int _SelectedX=0;
        public int SelectedX
        {
            get => _SelectedX;
            set
            {
                _SelectedX = value;
                Invalidate();
            }
        }

        private double QMax = 0;

        public int ValuesCount { get; private set; } = 0;

        public void SetValues(List<double> values)
        {
            ValuesCount = values.Count;            
            if (values.Count == 0)
            {
                Bitmap = new Bitmap(10, 10);
                Invalidate();
                return;
            }
            int stride = Width / values.Count;
            int height = Height;
            Bitmap = new Bitmap(stride * values.Count, 2* height);
            Point pt1 = new Point(0, height);
            Point pt2 = new Point(0, height);

            var max = QMax = values.Max(Math.Abs);

            using (var g = Graphics.FromImage(Bitmap))
            {
                var pen = new Pen(Brushes.Black, 2);
                var posBrush = new SolidBrush(Color.FromArgb(128, Color.Blue));
                var negBrush = new SolidBrush(Color.FromArgb(128, Color.Red));

                for (int i = 0; i < values.Count; i++)
                {
                    int h = (int)(values[i] / max * height);
                    if(h>0)
                    {
                        g.FillRectangle(posBrush, stride * i, height - h, stride, h);                        
                    }
                    else
                    {
                        g.FillRectangle(negBrush, stride * i, height, stride, -h);                        
                    }
                    pt2 = new Point(stride * i + stride / 2, height - h);
                    g.DrawLine(pen, pt1, pt2);
                    pt1 = pt2;
                }
            }
            SelectedX = -1;
            Invalidate();
        }
    }
}
