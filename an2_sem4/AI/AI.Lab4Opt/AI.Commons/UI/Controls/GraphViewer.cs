using AI.Commons.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AI.Commons.UI.Controls
{
    public partial class GraphViewer<T> : UserControl
    {
        public GraphViewer()
        {            
            InitializeComponent();

            EnableDoubleBuffer(Canvas);
            EnableDoubleBuffer(this);            
        }        

        private Bitmap RenderedBitmap = new Bitmap(1000, 1000);

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(RenderedBitmap, 0, 0, RenderedBitmap.Width / Zoom, RenderedBitmap.Height / Zoom);
        }

        int Zoom = 1;     

        [Browsable(false)]
        public IGraphRenderer<T> GraphRenderer { get; set; } = new CircularGraphRenderer<T>();

        public void RenderGraph(Graph<T> graph, List<List<int>> colors=null)
        {
            if(RenderedBitmap!=null)
            {
                RenderedBitmap.Dispose();
            }
            RenderedBitmap = GraphRenderer.Render(graph, colors);
            Canvas.Size = new Size(RenderedBitmap.Width, RenderedBitmap.Height);
            Canvas.Invalidate();
        }

        private void GraphViewer_Resize(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;

            if (Canvas.Width < Width * Zoom) 
            {
                x = (Width - Canvas.Width/Zoom) / 2;
            }

            if (Canvas.Height < Height * Zoom) 
            {
                y = (Height - Canvas.Height/Zoom) / 2;
            }

            Canvas.Location = new Point(x, y);
        }

        private void EnableDoubleBuffer(Control control)
        {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
            | BindingFlags.Instance | BindingFlags.NonPublic, null,
            control, new object[] { true });
        }

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            if (Zoom == 1) return;
            Zoom--;
            Canvas.Size = new Size(RenderedBitmap.Width / Zoom, RenderedBitmap.Height / Zoom);
            Canvas.Invalidate();
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            if (Zoom == 8) return;
            Zoom++;
            Canvas.Size = new Size(RenderedBitmap.Width / Zoom, RenderedBitmap.Height / Zoom);
            Canvas.Invalidate();
        }

        internal void ResetZoom()
        {
            Zoom = 3;
            Canvas.Size = new Size(RenderedBitmap.Width / Zoom, RenderedBitmap.Height / Zoom);
            Canvas.Invalidate();
        }
    }
}
