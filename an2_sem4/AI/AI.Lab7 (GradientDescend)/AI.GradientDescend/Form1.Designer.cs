namespace AI.GradientDescend
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LinnerudButton = new System.Windows.Forms.Button();
            this.UniOutputLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UniBatchBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UniRunButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MultiOutputLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.MultiBatchBox = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MultiRunButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.UniHappinessNormSelector = new AI.GradientDescend.Controls.NormalizationMethodSelector();
            this.UniGdpNormSelector = new AI.GradientDescend.Controls.NormalizationMethodSelector();
            this.UniPlotter = new AI.GradientDescend.Controls.CartesianPlotter();
            this.MultiFreedomNormSelector = new AI.GradientDescend.Controls.NormalizationMethodSelector();
            this.MultiHappinessNormSelector = new AI.GradientDescend.Controls.NormalizationMethodSelector();
            this.MultiGdpNormSelector = new AI.GradientDescend.Controls.NormalizationMethodSelector();
            this.MultiPlotter = new AI.GradientDescend.Controls.CartesianPlotter();
            this.UniErrorLabel = new System.Windows.Forms.Label();
            this.MultiErrorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.UniErrorLabel);
            this.splitContainer1.Panel1.Controls.Add(this.LinnerudButton);
            this.splitContainer1.Panel1.Controls.Add(this.UniOutputLabel);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.UniPlotter);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.MultiErrorLabel);
            this.splitContainer1.Panel2.Controls.Add(this.MultiOutputLabel);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.MultiPlotter);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 408;
            this.splitContainer1.TabIndex = 3;
            // 
            // LinnerudButton
            // 
            this.LinnerudButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LinnerudButton.Location = new System.Drawing.Point(327, 415);
            this.LinnerudButton.Name = "LinnerudButton";
            this.LinnerudButton.Size = new System.Drawing.Size(75, 23);
            this.LinnerudButton.TabIndex = 3;
            this.LinnerudButton.Text = "Linnerud";
            this.LinnerudButton.UseVisualStyleBackColor = true;
            this.LinnerudButton.Click += new System.EventHandler(this.LinnerudButton_Click);
            // 
            // UniOutputLabel
            // 
            this.UniOutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UniOutputLabel.AutoSize = true;
            this.UniOutputLabel.Location = new System.Drawing.Point(18, 388);
            this.UniOutputLabel.Name = "UniOutputLabel";
            this.UniOutputLabel.Size = new System.Drawing.Size(35, 13);
            this.UniOutputLabel.TabIndex = 2;
            this.UniOutputLabel.Text = "label6";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.UniBatchBox);
            this.panel1.Controls.Add(this.UniHappinessNormSelector);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.UniRunButton);
            this.panel1.Controls.Add(this.UniGdpNormSelector);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(393, 100);
            this.panel1.TabIndex = 1;
            // 
            // UniBatchBox
            // 
            this.UniBatchBox.AutoSize = true;
            this.UniBatchBox.Location = new System.Drawing.Point(229, 78);
            this.UniBatchBox.Name = "UniBatchBox";
            this.UniBatchBox.Size = new System.Drawing.Size(54, 17);
            this.UniBatchBox.TabIndex = 5;
            this.UniBatchBox.Text = "Batch";
            this.UniBatchBox.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Happiness Normalization";
            // 
            // UniRunButton
            // 
            this.UniRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UniRunButton.Location = new System.Drawing.Point(315, 74);
            this.UniRunButton.Name = "UniRunButton";
            this.UniRunButton.Size = new System.Drawing.Size(75, 23);
            this.UniRunButton.TabIndex = 2;
            this.UniRunButton.Text = "Run";
            this.UniRunButton.UseVisualStyleBackColor = true;
            this.UniRunButton.Click += new System.EventHandler(this.UniRunButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GDP Normalization";
            // 
            // MultiOutputLabel
            // 
            this.MultiOutputLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MultiOutputLabel.AutoSize = true;
            this.MultiOutputLabel.Location = new System.Drawing.Point(12, 388);
            this.MultiOutputLabel.Name = "MultiOutputLabel";
            this.MultiOutputLabel.Size = new System.Drawing.Size(35, 13);
            this.MultiOutputLabel.TabIndex = 3;
            this.MultiOutputLabel.Text = "label7";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.MultiBatchBox);
            this.panel2.Controls.Add(this.MultiFreedomNormSelector);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.MultiHappinessNormSelector);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.MultiRunButton);
            this.panel2.Controls.Add(this.MultiGdpNormSelector);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(3, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(382, 121);
            this.panel2.TabIndex = 5;
            // 
            // MultiBatchBox
            // 
            this.MultiBatchBox.AutoSize = true;
            this.MultiBatchBox.Location = new System.Drawing.Point(218, 99);
            this.MultiBatchBox.Name = "MultiBatchBox";
            this.MultiBatchBox.Size = new System.Drawing.Size(54, 17);
            this.MultiBatchBox.TabIndex = 7;
            this.MultiBatchBox.Text = "Batch";
            this.MultiBatchBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Freedom Normalization";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Happiness Normalization";
            // 
            // MultiRunButton
            // 
            this.MultiRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MultiRunButton.Location = new System.Drawing.Point(304, 95);
            this.MultiRunButton.Name = "MultiRunButton";
            this.MultiRunButton.Size = new System.Drawing.Size(75, 23);
            this.MultiRunButton.TabIndex = 2;
            this.MultiRunButton.Text = "Run";
            this.MultiRunButton.UseVisualStyleBackColor = true;
            this.MultiRunButton.Click += new System.EventHandler(this.MultiRunButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "GDP Normalization";
            // 
            // UniHappinessNormSelector
            // 
            this.UniHappinessNormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UniHappinessNormSelector.AutoSize = true;
            this.UniHappinessNormSelector.Location = new System.Drawing.Point(135, 30);
            this.UniHappinessNormSelector.Name = "UniHappinessNormSelector";
            this.UniHappinessNormSelector.Size = new System.Drawing.Size(255, 21);
            this.UniHappinessNormSelector.TabIndex = 4;
            // 
            // UniGdpNormSelector
            // 
            this.UniGdpNormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UniGdpNormSelector.AutoSize = true;
            this.UniGdpNormSelector.Location = new System.Drawing.Point(135, 3);
            this.UniGdpNormSelector.Name = "UniGdpNormSelector";
            this.UniGdpNormSelector.Size = new System.Drawing.Size(255, 21);
            this.UniGdpNormSelector.TabIndex = 1;
            // 
            // UniPlotter
            // 
            this.UniPlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UniPlotter.BackColor = System.Drawing.Color.White;
            this.UniPlotter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.UniPlotter.Location = new System.Drawing.Point(12, 139);
            this.UniPlotter.Name = "UniPlotter";
            this.UniPlotter.Size = new System.Drawing.Size(390, 246);
            this.UniPlotter.TabIndex = 0;
            this.UniPlotter.ViewPoint = new System.Drawing.Point(0, 0);
            this.UniPlotter.Zoom = 100;
            // 
            // MultiFreedomNormSelector
            // 
            this.MultiFreedomNormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MultiFreedomNormSelector.AutoSize = true;
            this.MultiFreedomNormSelector.Location = new System.Drawing.Point(135, 30);
            this.MultiFreedomNormSelector.Name = "MultiFreedomNormSelector";
            this.MultiFreedomNormSelector.Size = new System.Drawing.Size(244, 21);
            this.MultiFreedomNormSelector.TabIndex = 6;
            // 
            // MultiHappinessNormSelector
            // 
            this.MultiHappinessNormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MultiHappinessNormSelector.AutoSize = true;
            this.MultiHappinessNormSelector.Location = new System.Drawing.Point(138, 57);
            this.MultiHappinessNormSelector.Name = "MultiHappinessNormSelector";
            this.MultiHappinessNormSelector.Size = new System.Drawing.Size(241, 21);
            this.MultiHappinessNormSelector.TabIndex = 4;
            // 
            // MultiGdpNormSelector
            // 
            this.MultiGdpNormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MultiGdpNormSelector.AutoSize = true;
            this.MultiGdpNormSelector.Location = new System.Drawing.Point(135, 3);
            this.MultiGdpNormSelector.Name = "MultiGdpNormSelector";
            this.MultiGdpNormSelector.Size = new System.Drawing.Size(244, 21);
            this.MultiGdpNormSelector.TabIndex = 1;
            // 
            // MultiPlotter
            // 
            this.MultiPlotter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MultiPlotter.BackColor = System.Drawing.Color.White;
            this.MultiPlotter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MultiPlotter.Location = new System.Drawing.Point(3, 139);
            this.MultiPlotter.Name = "MultiPlotter";
            this.MultiPlotter.Size = new System.Drawing.Size(379, 246);
            this.MultiPlotter.TabIndex = 1;
            this.MultiPlotter.ViewPoint = new System.Drawing.Point(0, 0);
            this.MultiPlotter.Zoom = 100;
            // 
            // UniErrorLabel
            // 
            this.UniErrorLabel.AutoSize = true;
            this.UniErrorLabel.Location = new System.Drawing.Point(18, 406);
            this.UniErrorLabel.Name = "UniErrorLabel";
            this.UniErrorLabel.Size = new System.Drawing.Size(35, 13);
            this.UniErrorLabel.TabIndex = 4;
            this.UniErrorLabel.Text = "label6";
            // 
            // MultiErrorLabel
            // 
            this.MultiErrorLabel.AutoSize = true;
            this.MultiErrorLabel.Location = new System.Drawing.Point(12, 406);
            this.MultiErrorLabel.Name = "MultiErrorLabel";
            this.MultiErrorLabel.Size = new System.Drawing.Size(35, 13);
            this.MultiErrorLabel.TabIndex = 5;
            this.MultiErrorLabel.Text = "label7";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CartesianPlotter UniPlotter;
        private Controls.CartesianPlotter MultiPlotter;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private Controls.NormalizationMethodSelector UniGdpNormSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button UniRunButton;
        private Controls.NormalizationMethodSelector UniHappinessNormSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private Controls.NormalizationMethodSelector MultiHappinessNormSelector;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button MultiRunButton;
        private Controls.NormalizationMethodSelector MultiGdpNormSelector;
        private System.Windows.Forms.Label label4;
        private Controls.NormalizationMethodSelector MultiFreedomNormSelector;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label UniOutputLabel;
        private System.Windows.Forms.Label MultiOutputLabel;
        private System.Windows.Forms.Button LinnerudButton;
        private System.Windows.Forms.CheckBox UniBatchBox;
        private System.Windows.Forms.CheckBox MultiBatchBox;
        private System.Windows.Forms.Label UniErrorLabel;
        private System.Windows.Forms.Label MultiErrorLabel;
    }
}

