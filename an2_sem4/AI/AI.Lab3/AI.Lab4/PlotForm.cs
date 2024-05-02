using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab4
{
    public partial class PlotForm : Form
    {
        List<long> Values;
        long Max;       
        public PlotForm(List<long> list)
        {
            InitializeComponent();
            Values = list;
            Max = Values.Max() + 1;
        }

        private void PlotForm_Load(object sender, EventArgs e)
        {
            
        }

        private void PlotForm_Paint(object sender, PaintEventArgs e)
        {

            var points = Values.Count < Width ? Values.Select(x=>(double)x).ToList() : Values
                .Select((entry, index) => new { index, entry })
                .GroupBy(entry => entry.index / Width)
                .Select(group => group.Select(grouping => grouping.entry).Average()).ToList();

            Debug.WriteLine(string.Join(", ", points));
            for (int i = 0; i < points.Count; i++)
            {
                int x = i;
                int y = Height - (int)(points[i] * Height / Max);
                e.Graphics.FillEllipse(Brushes.Red, x - 2, y - 2, 4, 4);
            }
        }
    }
}
