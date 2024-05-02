using System;
using System.Windows.Forms;

namespace AI.Lab9
{
    public partial class MainForm : Form
    {        

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(TabPage page in Body.TabPages)
            {
                page.Invalidate();
            }
        }
    }
}
