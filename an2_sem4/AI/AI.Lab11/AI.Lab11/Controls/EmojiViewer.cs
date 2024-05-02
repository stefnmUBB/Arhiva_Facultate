using AI.Lab11.Data;
using AI.Lab11.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab11.Controls
{
    public partial class EmojiViewer : UserControl
    {
        public EmojiViewer()
        {
            InitializeComponent();
        }

        private void VScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        private void HScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            Invalidate();
        }

        private void EmojiViewer_Paint(object sender, PaintEventArgs e)
        {
            int y0 = VScrollBar.Value / 17;
            int x0 = HScrollBar.Value / 17;
            int yo = VScrollBar.Value % 17;
            int xo = HScrollBar.Value % 17;

            for (int y = y0; y < 61; y++)
            {
                if (y < 0 || y >= 61) continue;
                for (int x = x0; x < 60; x++)
                {
                    if (x < 0 || x >= 60) continue;
                    e.Graphics.FillRectangle(ClassBrushes[Classes[y, x]], -xo + 17 * (x - x0), -yo + 17 * (y - y0), 17, 17);
                }
            }

            e.Graphics.DrawImageUnscaled(Resources.emoji16_img, -HScrollBar.Value, -VScrollBar.Value);

            var tBrush = new SolidBrush(Color.FromArgb(128, 255, 255, 255));
            for (int y = y0; y < 61; y++)
            {
                if (y < 0 || y >= 61) continue;
                for (int x = x0; x < 60; x++)
                {
                    if (x < 0 || x >= 60) continue;
                    if (Classes[y, x] == 0)
                        e.Graphics.FillRectangle(tBrush, -xo + 17 * (x - x0), -yo + 17 * (y - y0), 17, 17);
                }
            }

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

        Brush[] ClassBrushes = new Brush[] { Brushes.White, Brushes.Red, Brushes.Green, Brushes.Blue };

        int[,] Classes = new int[61, 60];

        private void EmojiViewer_Resize(object sender, EventArgs e)
        {
            HScrollBar.Maximum = 1020 - Width + 2*HScrollBar.Height;
            VScrollBar.Maximum = 1037 - Height + 2*VScrollBar.Width;
            Invalidate();
        }

        public void SetEmojis((int x, int y, int c)[] data)
        {            
            for (int y = 0; y < 61; y++)
                for (int x = 0; x < 60; x++)
                    Classes[y, x] = 0;            
            foreach (var (x, y, c) in data)
                Classes[y, x] = c;
            BeginInvoke(new Action(Invalidate));
        }
    }
}
