namespace AI.Lab4Opt
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DynamicGraphsButton = new System.Windows.Forms.Button();
            this.RhoBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.BetaBox = new System.Windows.Forms.NumericUpDown();
            this.AlphaBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.GraphEditor = new AI.Lab4Opt.UI.Controls.CartesianGraphEditor();
            this.AllNodesCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RhoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BetaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AllNodesCheckbox);
            this.groupBox1.Controls.Add(this.DynamicGraphsButton);
            this.groupBox1.Controls.Add(this.RhoBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BetaBox);
            this.groupBox1.Controls.Add(this.AlphaBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ResetButton);
            this.groupBox1.Controls.Add(this.StartStopButton);
            this.groupBox1.Location = new System.Drawing.Point(428, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 223);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Static Graph";
            // 
            // DynamicGraphsButton
            // 
            this.DynamicGraphsButton.Location = new System.Drawing.Point(6, 194);
            this.DynamicGraphsButton.Name = "DynamicGraphsButton";
            this.DynamicGraphsButton.Size = new System.Drawing.Size(116, 23);
            this.DynamicGraphsButton.TabIndex = 8;
            this.DynamicGraphsButton.Text = "Dynamic Graphs";
            this.DynamicGraphsButton.UseVisualStyleBackColor = true;
            this.DynamicGraphsButton.Click += new System.EventHandler(this.DynamicGraphsButton_Click);
            // 
            // RhoBox
            // 
            this.RhoBox.DecimalPlaces = 2;
            this.RhoBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RhoBox.Location = new System.Drawing.Point(65, 124);
            this.RhoBox.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RhoBox.Name = "RhoBox";
            this.RhoBox.Size = new System.Drawing.Size(57, 20);
            this.RhoBox.TabIndex = 7;
            this.RhoBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RhoBox.ValueChanged += new System.EventHandler(this.RhoBox_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ρ";
            // 
            // BetaBox
            // 
            this.BetaBox.DecimalPlaces = 2;
            this.BetaBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.BetaBox.Location = new System.Drawing.Point(159, 74);
            this.BetaBox.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.BetaBox.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.BetaBox.Name = "BetaBox";
            this.BetaBox.Size = new System.Drawing.Size(57, 20);
            this.BetaBox.TabIndex = 5;
            this.BetaBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BetaBox.ValueChanged += new System.EventHandler(this.BetaBox_ValueChanged);
            // 
            // AlphaBox
            // 
            this.AlphaBox.DecimalPlaces = 2;
            this.AlphaBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.AlphaBox.Location = new System.Drawing.Point(65, 74);
            this.AlphaBox.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.AlphaBox.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.AlphaBox.Name = "AlphaBox";
            this.AlphaBox.Size = new System.Drawing.Size(57, 20);
            this.AlphaBox.TabIndex = 4;
            this.AlphaBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AlphaBox.ValueChanged += new System.EventHandler(this.AlphaBox_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "β";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "α";
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(87, 19);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 1;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // StartStopButton
            // 
            this.StartStopButton.Location = new System.Drawing.Point(6, 19);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(75, 23);
            this.StartStopButton.TabIndex = 0;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // GraphEditor
            // 
            this.GraphEditor.Location = new System.Drawing.Point(12, 12);
            this.GraphEditor.Name = "GraphEditor";
            this.GraphEditor.Size = new System.Drawing.Size(410, 426);
            this.GraphEditor.TabIndex = 0;
            // 
            // AllNodesCheckbox
            // 
            this.AllNodesCheckbox.AutoSize = true;
            this.AllNodesCheckbox.Location = new System.Drawing.Point(23, 163);
            this.AllNodesCheckbox.Name = "AllNodesCheckbox";
            this.AllNodesCheckbox.Size = new System.Drawing.Size(68, 17);
            this.AllNodesCheckbox.TabIndex = 9;
            this.AllNodesCheckbox.Text = "AllNodes";
            this.AllNodesCheckbox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GraphEditor);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RhoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BetaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Controls.CartesianGraphEditor GraphEditor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button StartStopButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.NumericUpDown BetaBox;
        private System.Windows.Forms.NumericUpDown AlphaBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown RhoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button DynamicGraphsButton;
        private System.Windows.Forms.CheckBox AllNodesCheckbox;
    }
}

