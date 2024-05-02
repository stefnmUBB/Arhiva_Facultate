namespace AI.MCMMP
{
    partial class Form1
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
            this.GDPFreedomFunctionLabel = new System.Windows.Forms.Label();
            this.InputSelector = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ParametersBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SingularPlotter = new AI.MCMMP.Controls.CartesianPlotter();
            this.GDPFreedomPlotter = new AI.MCMMP.Controls.CartesianPlotter();
            this.PythonButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PythonButton);
            this.groupBox1.Controls.Add(this.GDPFreedomFunctionLabel);
            this.groupBox1.Controls.Add(this.GDPFreedomPlotter);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 399);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GDP + Freedom";
            // 
            // GDPFreedomFunctionLabel
            // 
            this.GDPFreedomFunctionLabel.AutoSize = true;
            this.GDPFreedomFunctionLabel.Location = new System.Drawing.Point(6, 26);
            this.GDPFreedomFunctionLabel.Name = "GDPFreedomFunctionLabel";
            this.GDPFreedomFunctionLabel.Size = new System.Drawing.Size(21, 13);
            this.GDPFreedomFunctionLabel.TabIndex = 1;
            this.GDPFreedomFunctionLabel.Text = "w=";
            // 
            // InputSelector
            // 
            this.InputSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InputSelector.FormattingEnabled = true;
            this.InputSelector.Items.AddRange(new object[] {
            "v1_world-happiness-report-2017.csv",
            "v2_world-happiness-report-2017.csv",
            "v3_world-happiness-report-2017.csv"});
            this.InputSelector.Location = new System.Drawing.Point(49, 12);
            this.InputSelector.Name = "InputSelector";
            this.InputSelector.Size = new System.Drawing.Size(760, 21);
            this.InputSelector.TabIndex = 2;
            this.InputSelector.SelectedIndexChanged += new System.EventHandler(this.InputSelector_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Input";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(797, 399);
            this.splitContainer1.SplitterDistance = 431;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SingularPlotter);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ParametersBox);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(425, 329);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Uni Regression";
            // 
            // ParametersBox
            // 
            this.ParametersBox.FormattingEnabled = true;
            this.ParametersBox.Location = new System.Drawing.Point(67, 23);
            this.ParametersBox.Name = "ParametersBox";
            this.ParametersBox.Size = new System.Drawing.Size(352, 21);
            this.ParametersBox.TabIndex = 0;
            this.ParametersBox.SelectedIndexChanged += new System.EventHandler(this.ParametersBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Parameter";
            // 
            // SingularPlotter
            // 
            this.SingularPlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SingularPlotter.BackColor = System.Drawing.Color.White;
            this.SingularPlotter.Location = new System.Drawing.Point(6, 86);
            this.SingularPlotter.Name = "SingularPlotter";
            this.SingularPlotter.Size = new System.Drawing.Size(413, 237);
            this.SingularPlotter.TabIndex = 2;
            this.SingularPlotter.ViewPoint = new System.Drawing.Point(0, 0);
            this.SingularPlotter.Zoom = 100;
            // 
            // GDPFreedomPlotter
            // 
            this.GDPFreedomPlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GDPFreedomPlotter.BackColor = System.Drawing.Color.White;
            this.GDPFreedomPlotter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GDPFreedomPlotter.Location = new System.Drawing.Point(6, 58);
            this.GDPFreedomPlotter.Name = "GDPFreedomPlotter";
            this.GDPFreedomPlotter.Size = new System.Drawing.Size(350, 298);
            this.GDPFreedomPlotter.TabIndex = 0;
            this.GDPFreedomPlotter.ViewPoint = new System.Drawing.Point(0, 0);
            this.GDPFreedomPlotter.Zoom = 100;
            // 
            // PythonButton
            // 
            this.PythonButton.Location = new System.Drawing.Point(281, 362);
            this.PythonButton.Name = "PythonButton";
            this.PythonButton.Size = new System.Drawing.Size(75, 23);
            this.PythonButton.TabIndex = 1;
            this.PythonButton.Text = "Python";
            this.PythonButton.UseVisualStyleBackColor = true;
            this.PythonButton.Click += new System.EventHandler(this.PythonButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.InputSelector);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CartesianPlotter GDPFreedomPlotter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox InputSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label GDPFreedomFunctionLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ParametersBox;
        private Controls.CartesianPlotter SingularPlotter;
        private System.Windows.Forms.Button PythonButton;
    }
}

