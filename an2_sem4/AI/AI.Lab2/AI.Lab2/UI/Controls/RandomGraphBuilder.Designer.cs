namespace AI.Lab2.UI.Controls
{
    partial class RandomGraphBuilder
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.POutsideBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PInsideBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ExpectedComsBox = new System.Windows.Forms.NumericUpDown();
            this.NodesCountBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.POutsideBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PInsideBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpectedComsBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NodesCountBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.POutsideBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.PInsideBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ExpectedComsBox);
            this.groupBox1.Controls.Add(this.NodesCountBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(417, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Random graph parameters";
            // 
            // POutsideBox
            // 
            this.POutsideBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.POutsideBox.DecimalPlaces = 2;
            this.POutsideBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.POutsideBox.Location = new System.Drawing.Point(162, 105);
            this.POutsideBox.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.POutsideBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.POutsideBox.Name = "POutsideBox";
            this.POutsideBox.Size = new System.Drawing.Size(249, 20);
            this.POutsideBox.TabIndex = 7;
            this.POutsideBox.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "P. Edge between communities";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "P. Edge inside community";
            // 
            // PInsideBox
            // 
            this.PInsideBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PInsideBox.DecimalPlaces = 2;
            this.PInsideBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PInsideBox.Location = new System.Drawing.Point(162, 79);
            this.PInsideBox.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PInsideBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.PInsideBox.Name = "PInsideBox";
            this.PInsideBox.Size = new System.Drawing.Size(249, 20);
            this.PInsideBox.TabIndex = 4;
            this.PInsideBox.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Expected Communities Count";
            // 
            // ExpectedComsBox
            // 
            this.ExpectedComsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExpectedComsBox.Location = new System.Drawing.Point(162, 53);
            this.ExpectedComsBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.ExpectedComsBox.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ExpectedComsBox.Name = "ExpectedComsBox";
            this.ExpectedComsBox.Size = new System.Drawing.Size(249, 20);
            this.ExpectedComsBox.TabIndex = 2;
            this.ExpectedComsBox.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // NodesCountBox
            // 
            this.NodesCountBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NodesCountBox.Location = new System.Drawing.Point(162, 27);
            this.NodesCountBox.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.NodesCountBox.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NodesCountBox.Name = "NodesCountBox";
            this.NodesCountBox.Size = new System.Drawing.Size(249, 20);
            this.NodesCountBox.TabIndex = 1;
            this.NodesCountBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nodes Count";
            // 
            // RandomGraphBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "RandomGraphBuilder";
            this.Size = new System.Drawing.Size(417, 150);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.POutsideBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PInsideBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExpectedComsBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NodesCountBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NodesCountBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ExpectedComsBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown PInsideBox;
        private System.Windows.Forms.NumericUpDown POutsideBox;
        private System.Windows.Forms.Label label4;
    }
}
