using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AI.Lab9
{
    public partial class DigitViewMockForm : Form
    {
        public byte[] Data { get; }        
        bool Reduced { get; }
        public DigitViewMockForm(byte[] data, int value, bool reduced=false)
        {
            Data = data;
            Reduced = reduced;
            InitializeComponent();
            Text = value.ToString();
        }        

        private void DigitViewMockForm_Paint(object sender, PaintEventArgs e)
        {
            if (!Reduced)
            {
                for (int y = 0; y < 32; y++)
                {
                    for (int x = 0; x < 32; x++)
                    {
                        if (Data[32 * y + x] == 1)
                            e.Graphics.FillRectangle(Brushes.Black, 8 * x, 8 * y, 8, 8);
                    }
                }
            }
            else
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        int s = Data[8 * y + x];
                        var b = 255 - 15 * s;
                        var color = Color.FromArgb(b, b, b);
                        e.Graphics.FillRectangle(new SolidBrush(color), 8 * x, 8 * y, 8, 8);
                    }
                }
            }
        }
    }
}
