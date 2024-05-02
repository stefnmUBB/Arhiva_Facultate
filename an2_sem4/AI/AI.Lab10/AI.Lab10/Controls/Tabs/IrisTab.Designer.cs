namespace AI.Lab10.Controls.Tabs
{
    partial class IrisTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.legendTTPlot1 = new AI.Lab10.Controls.Tabs.LegendTTPlot();
            this.RetryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // legendTTPlot1
            // 
            this.legendTTPlot1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.legendTTPlot1.Location = new System.Drawing.Point(3, 3);
            this.legendTTPlot1.Name = "legendTTPlot1";
            this.legendTTPlot1.Size = new System.Drawing.Size(496, 306);
            this.legendTTPlot1.TabIndex = 1;
            // 
            // RetryButton
            // 
            this.RetryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RetryButton.Location = new System.Drawing.Point(505, 3);
            this.RetryButton.Name = "RetryButton";
            this.RetryButton.Size = new System.Drawing.Size(75, 23);
            this.RetryButton.TabIndex = 2;
            this.RetryButton.Text = "Retry";
            this.RetryButton.UseVisualStyleBackColor = true;
            this.RetryButton.Click += new System.EventHandler(this.RetryButton_Click);
            // 
            // IrisTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RetryButton);
            this.Controls.Add(this.legendTTPlot1);
            this.Name = "IrisTab";
            this.Size = new System.Drawing.Size(583, 312);
            this.Load += new System.EventHandler(this.IrisTab_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LegendTTPlot legendTTPlot1;
        private System.Windows.Forms.Button RetryButton;
    }
}
