using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab4
{
    public partial class CoordsLoaderForm : Form
    {
        public CoordsLoaderForm()
        {
            InitializeComponent();
        }

        public List<Point> Points { get; } = new List<Point>();

        private void button1_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            var lines = textBox1.Lines;
            foreach(var line in lines)
            {
                var vals = line.Split(' ').Where(x => x != "").Select(x => (int)double.Parse(x)).ToArray();
                Points.Add(new Point(vals[1], vals[2]));
            }
            DialogResult = DialogResult.OK;
        }
    }
}
