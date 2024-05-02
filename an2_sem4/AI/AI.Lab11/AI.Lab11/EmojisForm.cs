using AI.Lab11.Solvers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI.Lab11
{
    public partial class EmojisForm : Form
    {        

        public EmojisForm()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void EmojisForm_Load(object sender, EventArgs e)
        {
            Task.Run(Solve);
        }

        private void Solve()
        {
            BeginInvoke(new Action(() => Text = "Emojis - Running..."));
            BeginInvoke(new Action(() => RetryButton.Enabled = false));
            var solver = new EmojiSolver();
            solver.Solve();
            var data = solver.GetResults().Select(_ => (_.Emoji.SheetX, _.Emoji.SheetY, _.LabelIndex + 1)).ToArray();

            EmojiViewer.SetEmojis(data);
            Invoke(new Action(() => RetryButton.Enabled = true));
            BeginInvoke(new Action(() => Text = "Emojis - Done."));
        }

        private void RetryButton_Click(object sender, EventArgs e)
        {
            Task.Run(Solve);
        }
    }
}
