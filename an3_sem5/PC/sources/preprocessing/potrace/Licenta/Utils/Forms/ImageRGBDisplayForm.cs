using Licenta.Commons.Math;
using Licenta.Imaging;
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

namespace Licenta.Utils.Forms
{
    public partial class ImageRGBDisplayForm : Form
    {
        public ImageRGBDisplayForm()
        {
            InitializeComponent();

            var doubleBufferedProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);

            doubleBufferedProp.SetValue(this, true);
            doubleBufferedProp.SetValue(ContentPanel, true);

        }

        private Matrix<ColorHSL> pImage;

        //private ImageRGB pImage;
        public ImageRGB Image
        {
            set
            {
                if(value==null)
                {
                    pImage = null;
                }
                else
                {
                    pImage = value.ToHSL();
                    ContentPanel.Size = new Size(value.Width, value.Height);
                }                
                ReloadImage();
                ContentPanel.Invalidate();
            }
        }

        private Bitmap pBitmap;
        void ReloadImage()
        {
            pBitmap?.Dispose();
            if (pImage == null) { pBitmap = null; return; }

            if(HueBox.Checked)
            {
                pBitmap = new ImageRGB(Matrices.DoEachItem(pImage, c =>
                {
                    return new ColorHSL(c.H, SatBox.Checked ? c.S : 1.0f, LumBox.Checked ? c.L : 0.5f).ToRGB();
                })).ToBitmap();
            }
            else
            {
                pBitmap = new ImageRGB(Matrices.DoEachItem(pImage, c =>
                {
                    return new ColorHSL(0.5f, SatBox.Checked ? c.S : 1.0f, LumBox.Checked ? c.L : 0.5f).ToRGB();
                })).ToBitmap();
                /*pBitmap = new ImageRGB(Matrices.DoEachItem(pImage, c => (double)(SatBox.Checked ? c.S : 1) * (LumBox.Checked ? c.L : 1)))
                    .ToBitmap();*/
            }          
        }

        private void ContentPanel_Paint(object sender, PaintEventArgs e)
        {
            if (pBitmap == null) return;
            e.Graphics.DrawImageUnscaled(pBitmap, Point.Empty);
        }

        private void ComponentBox_CheckedChanged(object sender, EventArgs e)
        {
            ReloadImage();
            ContentPanel.Invalidate();
        }
    }
}
