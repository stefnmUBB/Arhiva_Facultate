namespace AI.Lab9.Controls.Tabs
{
    partial class SepiaSimTab
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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HSPlot = new AI.Lab9.Controls.Plot.CartesianPlotter();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HBPlot = new AI.Lab9.Controls.Plot.CartesianPlotter();
            this.GenerateTrainDataButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.TestViewer = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ConfMatrixStats = new System.Windows.Forms.Label();
            this.ConfusionMatrix = new AI.Lab9.Controls.Misc.ConfusionMatrix();
            this.ANNStatusBar = new AI.Lab9.Controls.Tabs.ANNStatusBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.LossPlot = new AI.Lab9.Controls.Plot.CartesianPlotter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TestViewer)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer2);
            this.groupBox1.Location = new System.Drawing.Point(3, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Color Distribution (HSB)";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.HSPlot);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label5);
            this.splitContainer2.Panel2.Controls.Add(this.label6);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Panel2.Controls.Add(this.HBPlot);
            this.splitContainer2.Size = new System.Drawing.Size(320, 168);
            this.splitContainer2.SplitterDistance = 160;
            this.splitContainer2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Green;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(122, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Sepia";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(105, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "No Sepia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hue/Saturation";
            // 
            // HSPlot
            // 
            this.HSPlot.BackColor = System.Drawing.Color.White;
            this.HSPlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HSPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HSPlot.Location = new System.Drawing.Point(0, 0);
            this.HSPlot.Name = "HSPlot";
            this.HSPlot.Size = new System.Drawing.Size(160, 168);
            this.HSPlot.TabIndex = 0;
            this.HSPlot.ViewPoint = new System.Drawing.Point(0, 0);
            this.HSPlot.Zoom = 100;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Green;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(119, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Sepia";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Red;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(102, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "No Sepia";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hue/Brightness";
            // 
            // HBPlot
            // 
            this.HBPlot.BackColor = System.Drawing.Color.White;
            this.HBPlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HBPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HBPlot.Location = new System.Drawing.Point(0, 0);
            this.HBPlot.Name = "HBPlot";
            this.HBPlot.Size = new System.Drawing.Size(156, 168);
            this.HBPlot.TabIndex = 1;
            this.HBPlot.ViewPoint = new System.Drawing.Point(0, 0);
            this.HBPlot.Zoom = 100;
            // 
            // GenerateTrainDataButton
            // 
            this.GenerateTrainDataButton.Location = new System.Drawing.Point(3, 3);
            this.GenerateTrainDataButton.Name = "GenerateTrainDataButton";
            this.GenerateTrainDataButton.Size = new System.Drawing.Size(142, 23);
            this.GenerateTrainDataButton.TabIndex = 2;
            this.GenerateTrainDataButton.Text = "Generate Data && Train";
            this.GenerateTrainDataButton.UseVisualStyleBackColor = true;
            this.GenerateTrainDataButton.Click += new System.EventHandler(this.GenerateTrainDataButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ExecuteButton);
            this.splitContainer1.Panel1.Controls.Add(this.TestButton);
            this.splitContainer1.Panel1.Controls.Add(this.TestViewer);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.GenerateTrainDataButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(670, 420);
            this.splitContainer1.SplitterDistance = 332;
            this.splitContainer1.TabIndex = 3;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(242, 3);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteButton.TabIndex = 6;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(151, 3);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(85, 23);
            this.TestButton.TabIndex = 5;
            this.TestButton.Text = "Run Tests";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // TestViewer
            // 
            this.TestViewer.AllowUserToAddRows = false;
            this.TestViewer.AllowUserToDeleteRows = false;
            this.TestViewer.AllowUserToResizeRows = false;
            this.TestViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TestViewer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TestViewer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.TestViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TestViewer.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.TestViewer.Location = new System.Drawing.Point(6, 231);
            this.TestViewer.MultiSelect = false;
            this.TestViewer.Name = "TestViewer";
            this.TestViewer.ReadOnly = true;
            this.TestViewer.RowHeadersVisible = false;
            this.TestViewer.RowHeadersWidth = 20;
            this.TestViewer.Size = new System.Drawing.Size(320, 186);
            this.TestViewer.TabIndex = 4;
            this.TestViewer.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.TestViewer_RowPrePaint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.ConfMatrixStats);
            this.groupBox2.Controls.Add(this.ConfusionMatrix);
            this.groupBox2.Location = new System.Drawing.Point(59, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 303);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Confusion Matrix";
            // 
            // ConfMatrixStats
            // 
            this.ConfMatrixStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfMatrixStats.Location = new System.Drawing.Point(6, 238);
            this.ConfMatrixStats.Name = "ConfMatrixStats";
            this.ConfMatrixStats.Size = new System.Drawing.Size(208, 61);
            this.ConfMatrixStats.TabIndex = 1;
            this.ConfMatrixStats.Text = "  ";
            // 
            // ConfusionMatrix
            // 
            this.ConfusionMatrix.Location = new System.Drawing.Point(6, 19);
            this.ConfusionMatrix.Name = "ConfusionMatrix";
            this.ConfusionMatrix.Size = new System.Drawing.Size(209, 216);
            this.ConfusionMatrix.TabIndex = 0;
            // 
            // ANNStatusBar
            // 
            this.ANNStatusBar.Location = new System.Drawing.Point(0, 420);
            this.ANNStatusBar.Name = "ANNStatusBar";
            this.ANNStatusBar.Size = new System.Drawing.Size(670, 22);
            this.ANNStatusBar.TabIndex = 0;
            this.ANNStatusBar.Text = "annStatusBar1";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.LossPlot);
            this.groupBox3.Location = new System.Drawing.Point(3, 312);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 144);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Loss";
            // 
            // LossPlot
            // 
            this.LossPlot.BackColor = System.Drawing.Color.White;
            this.LossPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LossPlot.Location = new System.Drawing.Point(3, 16);
            this.LossPlot.Name = "LossPlot";
            this.LossPlot.Size = new System.Drawing.Size(322, 125);
            this.LossPlot.TabIndex = 0;
            this.LossPlot.ViewPoint = new System.Drawing.Point(-300, -150);
            this.LossPlot.Zoom = 30;
            // 
            // SepiaSimTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ANNStatusBar);
            this.Name = "SepiaSimTab";
            this.Size = new System.Drawing.Size(670, 442);
            this.Load += new System.EventHandler(this.IrisSimTab_Load);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TestViewer)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ANNStatusBar ANNStatusBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button GenerateTrainDataButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView TestViewer;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button ExecuteButton;
        private Plot.CartesianPlotter HSPlot;
        private Plot.CartesianPlotter HBPlot;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label ConfMatrixStats;
        private Misc.ConfusionMatrix ConfusionMatrix;
        private System.Windows.Forms.GroupBox groupBox3;
        private Plot.CartesianPlotter LossPlot;
    }
}
