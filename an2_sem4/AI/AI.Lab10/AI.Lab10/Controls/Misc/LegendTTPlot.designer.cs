namespace AI.Lab10.Controls.Tabs
{
    partial class LegendTTPlot
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
            this.ShowPredictedCheckBox = new System.Windows.Forms.CheckBox();
            this.LegendLabel = new System.Windows.Forms.Label();
            this.Plotter = new AI.Lab10.Controls.Plot.CartesianPlotter();
            this.SuspendLayout();
            // 
            // ShowPredictedCheckBox
            // 
            this.ShowPredictedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowPredictedCheckBox.AutoSize = true;
            this.ShowPredictedCheckBox.BackColor = System.Drawing.Color.White;
            this.ShowPredictedCheckBox.Location = new System.Drawing.Point(190, 3);
            this.ShowPredictedCheckBox.Name = "ShowPredictedCheckBox";
            this.ShowPredictedCheckBox.Size = new System.Drawing.Size(127, 17);
            this.ShowPredictedCheckBox.TabIndex = 1;
            this.ShowPredictedCheckBox.Text = "Show Predicted Data";
            this.ShowPredictedCheckBox.UseVisualStyleBackColor = false;
            this.ShowPredictedCheckBox.CheckedChanged += new System.EventHandler(this.ShowPredictedCheckBox_CheckedChanged);
            // 
            // LegendLabel
            // 
            this.LegendLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LegendLabel.AutoSize = true;
            this.LegendLabel.BackColor = System.Drawing.Color.White;
            this.LegendLabel.Location = new System.Drawing.Point(187, 23);
            this.LegendLabel.Name = "LegendLabel";
            this.LegendLabel.Size = new System.Drawing.Size(43, 13);
            this.LegendLabel.TabIndex = 2;
            this.LegendLabel.Text = "Legend";
            this.LegendLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.LegendLabel_Paint);
            // 
            // Plotter
            // 
            this.Plotter.BackColor = System.Drawing.Color.White;
            this.Plotter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Plotter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Plotter.Location = new System.Drawing.Point(0, 0);
            this.Plotter.Name = "Plotter";
            this.Plotter.Size = new System.Drawing.Size(320, 300);
            this.Plotter.TabIndex = 0;
            this.Plotter.ViewPoint = new System.Drawing.Point(0, 0);
            this.Plotter.Zoom = 100;
            // 
            // LegendTTPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LegendLabel);
            this.Controls.Add(this.ShowPredictedCheckBox);
            this.Controls.Add(this.Plotter);
            this.Name = "LegendTTPlot";
            this.Size = new System.Drawing.Size(320, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Plot.CartesianPlotter Plotter;
        private System.Windows.Forms.CheckBox ShowPredictedCheckBox;
        private System.Windows.Forms.Label LegendLabel;
    }
}
