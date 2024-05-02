namespace AI.Lab10.Controls.Misc
{
    partial class OutputView
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AccuracyLabel = new System.Windows.Forms.Label();
            this.PrecisionLabel = new System.Windows.Forms.Label();
            this.RecallLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConfusionMatrix = new AI.Lab10.Controls.Misc.ConfusionMatrix();
            this.LossPlotter = new AI.Lab10.Controls.Plot.CartesianPlotter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(395, 215);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LossPlotter);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.RecallLabel);
            this.groupBox1.Controls.Add(this.PrecisionLabel);
            this.groupBox1.Controls.Add(this.AccuracyLabel);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 215);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stats";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ConfusionMatrix);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 215);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Confustion matrix";
            this.groupBox2.Resize += new System.EventHandler(this.groupBox2_Resize);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Accuracy : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Precision : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Recall : ";
            // 
            // AccuracyLabel
            // 
            this.AccuracyLabel.AutoSize = true;
            this.AccuracyLabel.Location = new System.Drawing.Point(73, 16);
            this.AccuracyLabel.Name = "AccuracyLabel";
            this.AccuracyLabel.Size = new System.Drawing.Size(13, 13);
            this.AccuracyLabel.TabIndex = 3;
            this.AccuracyLabel.Text = "0";
            // 
            // PrecisionLabel
            // 
            this.PrecisionLabel.AutoSize = true;
            this.PrecisionLabel.Location = new System.Drawing.Point(73, 31);
            this.PrecisionLabel.Name = "PrecisionLabel";
            this.PrecisionLabel.Size = new System.Drawing.Size(13, 13);
            this.PrecisionLabel.TabIndex = 4;
            this.PrecisionLabel.Text = "0";
            // 
            // RecallLabel
            // 
            this.RecallLabel.AutoSize = true;
            this.RecallLabel.Location = new System.Drawing.Point(73, 46);
            this.RecallLabel.Name = "RecallLabel";
            this.RecallLabel.Size = new System.Drawing.Size(13, 13);
            this.RecallLabel.TabIndex = 5;
            this.RecallLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Loss :";
            // 
            // ConfusionMatrix
            // 
            this.ConfusionMatrix.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ConfusionMatrix.Location = new System.Drawing.Point(3, 59);
            this.ConfusionMatrix.Name = "ConfusionMatrix";
            this.ConfusionMatrix.Size = new System.Drawing.Size(124, 97);
            this.ConfusionMatrix.TabIndex = 8;
            // 
            // LossPlotter
            // 
            this.LossPlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LossPlotter.BackColor = System.Drawing.Color.White;
            this.LossPlotter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LossPlotter.Location = new System.Drawing.Point(9, 75);
            this.LossPlotter.Name = "LossPlotter";
            this.LossPlotter.Size = new System.Drawing.Size(246, 134);
            this.LossPlotter.TabIndex = 7;
            this.LossPlotter.ViewPoint = new System.Drawing.Point(0, 0);
            this.LossPlotter.Zoom = 100;
            // 
            // OutputView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "OutputView";
            this.Size = new System.Drawing.Size(395, 215);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Plot.CartesianPlotter LossPlotter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label RecallLabel;
        private System.Windows.Forms.Label PrecisionLabel;
        private System.Windows.Forms.Label AccuracyLabel;
        private ConfusionMatrix ConfusionMatrix;
    }
}
