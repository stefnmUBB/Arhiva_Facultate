using AI.Commons.Data;
using AI.Commons.UI.Controls;
using AI.Commons.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab4.UI.Controls
{
    public partial class CartesianGraphEditor : UserControl
    {
        public CartesianGraphEditor()
        {
            InitializeComponent();            
        }

        public WeightedGraph<int, int> GetGraph()
            => GraphViewer.GetGraph();

        public void HighlightPath(int[] path, bool cycle = false)
            => GraphViewer.HighlightPath(path, cycle);

        private void ClearButton_Click(object sender, EventArgs e)
        {
            GraphViewer.Points.Clear();
            GraphViewer.Segments.Clear();
            GraphViewer.HighlightedPath.Clear();
            GraphViewer.Invalidate();
        }

        private void RandomSegmentsButton_Click(object sender, EventArgs e)
        {
            GraphViewer.HighlightedPath.Clear();
            GraphViewer.Segments.Clear();
            int segcount = Commons.Utils.Random.FromRange(
                 GraphViewer.Points.Count,
                 GraphViewer.Points.Count * GraphViewer.Points.Count
                 );
            for (int i = 0; i < segcount; i++) 
            {
                int p1 = Commons.Utils.Random.IndexFromContainer(GraphViewer.Points);
                int p2 = Commons.Utils.Random.IndexFromContainer(GraphViewer.Points);
                if (p1 == p2)
                    continue;
                GraphViewer.Segments.Add((p1, p2));
            }
            GraphViewer.Segments = GraphViewer.Segments.Distinct().ToList();
            GraphViewer.Invalidate();
        }

        private void ExportAsMatrixButton_Click(object sender, EventArgs e)
        {
            if(SaveFileDialog.ShowDialog()==DialogResult.OK)
            {
                var graph = GetGraph();
                var matrix = graph.GetAdjacencyMatrixEdgeData(int.MaxValue);

                for (int i = 0; i < graph.Nodes.Count; i++)
                    matrix[i, i] = 0;

                using (var file = new FileStream(SaveFileDialog.FileName, FileMode.CreateNew)) 
                {
                    using (var writer = new StreamWriter(file)) 
                    {
                        writer.WriteLine(graph.Nodes.Count.ToString());
                        for(int i=0; i < graph.Nodes.Count; i++)
                        {
                            writer.Write(matrix[i, 0]);
                            for (int j = 1; j < graph.Nodes.Count; j++)
                                writer.Write("," + matrix[i, j].ToString());
                            writer.WriteLine();
                        }
                        writer.WriteLine(1);
                        writer.WriteLine(graph.Nodes.Count);
                    }
                }
            }
        }

        public void LoadFromResource(string res)
        {
            var points = new List<Point>();
            var lines = res.Split('\n').Where(s => s != "").ToList();            
            double x = 0;
            double y = 0;
            foreach(var line in lines)
            {
                var vals = line.Split(' ');                
                x = double.Parse(vals[1]) * 0.005;
                y = double.Parse(vals[0]) * 0.005;                
                points.Add(new Point((int)x, (int)-y));
            }

            while(points.Count>50)
            {
                points.RemoveAt(Commons.Utils.Random.IndexFromContainer(points));
            }

            var minX = points.Min(p => p.X);
            var minY = points.Min(p => p.Y);
            var maxX = points.Max(p => p.X);
            var maxY = points.Max(p => p.Y);
            var w = maxX - minX;
            var h = maxY - minY;

            points = points.Select(p => new Point(p.X - minX - w / 2, p.Y - minY - h / 2)).ToList();

            GraphViewer.Points.Clear();
            GraphViewer.Points.AddRange(points);
            GraphViewer.Segments.Clear();
            GraphViewer.HighlightedPath.Clear();
            GraphViewer.Invalidate();            
        }

        public CartesianGraphViewer Viewer => GraphViewer;
    }
}
