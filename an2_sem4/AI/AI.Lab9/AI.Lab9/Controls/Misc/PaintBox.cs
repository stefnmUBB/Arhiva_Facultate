using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab9.Controls.Misc
{
    public partial class PaintBox : UserControl
    {
        public PaintBox()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                | BindingFlags.Instance | BindingFlags.NonPublic, null,
                Grid, new object[] { true });
        }

        public byte[] Image { get; private set; } = new byte[32 * 32];

        private void Grid_Paint(object sender, PaintEventArgs e)
        {
            var s = Grid.Width / 32;
            for(int y=0;y<32;y++)
            {
                for(int x=0;x<32;x++)
                {
                    if (Image[32 * y + x] == 1)
                        e.Graphics.FillRectangle(Brushes.Black, s * x, s * y, s, s);
                }
            }
        }

        private void PaintBox_Resize(object sender, EventArgs e)
        {
            Grid.Width = Grid.Height = Math.Min(Width, Height);
        }


        void __PutPoint(int x, int y, byte value)
        {
            if (x >= 0 && x < 32 && y >= 0 && y < 32)
                Image[32 * y + x] = value;
        }

        void PutPoint(int x, int y, byte value)
        {
            for(int xi=-1;xi<=1;xi++)
            {
                for (int yi = -1; yi <= 1; yi++)
                    __PutPoint(x + xi, y + yi, value);
            }            
        }

        bool msDown = false;
        private void Grid_MouseDown(object sender, MouseEventArgs e)
        {
            if (Form.ModifierKeys == Keys.Control)
            {
                for (int i = 0; i < 1024; i++) Image[i] = 0;
                Grid.Invalidate();
                return;
            }

            msDown = true;
            int s = Grid.Width / 32;
            int x = Grid.PointToClient(Cursor.Position).X / s;
            int y = Grid.PointToClient(Cursor.Position).Y / s;

            if(e.Button==MouseButtons.Left)
            {
                PutPoint(x, y, 1);
                Grid.Invalidate();
            }
            else if(e.Button==MouseButtons.Right)
            {
                PutPoint(x, y, 0);
                Grid.Invalidate();
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (!msDown) return;

            int s = Grid.Width / 32;
            int x = Grid.PointToClient(Cursor.Position).X / s;
            int y = Grid.PointToClient(Cursor.Position).Y / s;

            if (e.Button == MouseButtons.Left)
            {
                PutPoint(x, y, 1);
                Grid.Invalidate();
            }
            else if (e.Button == MouseButtons.Right)
            {
                PutPoint(x, y, 0);
                Grid.Invalidate();
            }

        }

        private void Grid_MouseUp(object sender, MouseEventArgs e)
        {
            if (!msDown) return;
            msDown = false;
            ImageChanged?.Invoke();

        }

        private void Grid_MouseLeave(object sender, EventArgs e)
        {
            if (!msDown) return;
            msDown = false;
            ImageChanged?.Invoke();
        }

        public delegate void OnImageChanged();
        public event OnImageChanged ImageChanged;

        private void Grid_MouseDoubleClick(object sender, MouseEventArgs e)
        {            
        }
    }
}
