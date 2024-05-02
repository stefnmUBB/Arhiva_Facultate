using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Lab9.Controls.Tabs
{
    internal class ANNStatusBar : System.Windows.Forms.StatusStrip, IANNNotifier
    {
        private System.Windows.Forms.ToolStripStatusLabel NetworkTrainStatus;

        public ANNStatusBar()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {            
            NetworkTrainStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.NetworkTrainStatus });
            this.Location = new System.Drawing.Point(0, 249);
            this.Name = "ANNStatusBar";
            this.Size = new System.Drawing.Size(552, 22);
            this.TabIndex = 0;
            this.Text = "ANNStatusBar";
            // 
            // NetworkTrainStatus
            // 
            this.NetworkTrainStatus.Name = "NetworkTrainStatus";
            this.NetworkTrainStatus.Size = new System.Drawing.Size(107, 17);
            this.NetworkTrainStatus.Text = "    Neural Network not loaded";
            this.NetworkTrainStatus.Paint += NetworkTrainStatus_Paint;

            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Brush NetworkTrainStatusBrush = Brushes.Red;

        private void NetworkTrainStatus_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillEllipse(NetworkTrainStatusBrush, NetworkTrainStatus.Height / 3 - 1, NetworkTrainStatus.Height / 3 - 1, NetworkTrainStatus.Height / 3 + 2, NetworkTrainStatus.Height / 3 + 2);
        }

        private void SetNetworkTrainStatus(string message, Brush brush)
        {
            Invoke(new Action(() => 
            {
                NetworkTrainStatusBrush = brush;
                NetworkTrainStatus.Text = "    " + message;                
            }));                        
        }

        public void OnANNLoaded()
        {
            SetNetworkTrainStatus("Loaded network", Brushes.Orange);
        }

        public void OnANNTrainStart()
        {
            SetNetworkTrainStatus("Training Network", Brushes.Orange);
        }

        public void OnANNTrainFinished()
        {
            SetNetworkTrainStatus("Network Trained", Brushes.LimeGreen);
        }

        public void OnANNTesting()
        {
            SetNetworkTrainStatus("Testing Network", Brushes.Yellow);
        }

        public void OnANNIdle()
        {
            SetNetworkTrainStatus("Done", Brushes.Gray);
        }

        public void OnANNError()
        {
            SetNetworkTrainStatus("Error", Brushes.Red);
        }
    }
}
