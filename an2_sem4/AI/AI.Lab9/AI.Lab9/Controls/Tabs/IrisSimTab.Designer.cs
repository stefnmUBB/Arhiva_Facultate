namespace AI.Lab9.Controls.Tabs
{
    partial class IrisSimTab
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
            this.GenerateTrainDataButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.TestViewer = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ConfMatrixStats = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.DistributionPlot = new AI.Lab9.Controls.Tabs.LegendTTPlot();
            this.LossPlot = new AI.Lab9.Controls.Plot.CartesianPlotter();
            this.ConfusionMatrix = new AI.Lab9.Controls.Misc.ConfusionMatrix();
            this.ANNStatusBar = new AI.Lab9.Controls.Tabs.ANNStatusBar();
            this.groupBox1.SuspendLayout();
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
            this.groupBox1.Controls.Add(this.DistributionPlot);
            this.groupBox1.Location = new System.Drawing.Point(3, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 187);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Distribution (Petal Width/ Sepal Length)";
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
            this.splitContainer1.Size = new System.Drawing.Size(670, 558);
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
            this.TestViewer.Size = new System.Drawing.Size(320, 324);
            this.TestViewer.TabIndex = 4;
            this.TestViewer.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.TestViewer_RowPrePaint);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox2.Controls.Add(this.ConfMatrixStats);
            this.groupBox2.Controls.Add(this.ConfusionMatrix);
            this.groupBox2.Location = new System.Drawing.Point(24, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 371);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Confusion Matrix";
            // 
            // ConfMatrixStats
            // 
            this.ConfMatrixStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfMatrixStats.Location = new System.Drawing.Point(7, 282);
            this.ConfMatrixStats.Name = "ConfMatrixStats";
            this.ConfMatrixStats.Size = new System.Drawing.Size(273, 86);
            this.ConfMatrixStats.TabIndex = 1;
            this.ConfMatrixStats.Text = "  ";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.LossPlot);
            this.groupBox3.Location = new System.Drawing.Point(3, 389);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 144);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Loss";
            // 
            // DistributionPlot
            // 
            this.DistributionPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DistributionPlot.Location = new System.Drawing.Point(3, 16);
            this.DistributionPlot.Name = "DistributionPlot";
            this.DistributionPlot.Size = new System.Drawing.Size(320, 168);
            this.DistributionPlot.TabIndex = 0;
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
            // ConfusionMatrix
            // 
            this.ConfusionMatrix.Location = new System.Drawing.Point(6, 19);
            this.ConfusionMatrix.Name = "ConfusionMatrix";
            this.ConfusionMatrix.Size = new System.Drawing.Size(274, 260);
            this.ConfusionMatrix.TabIndex = 0;
            // 
            // ANNStatusBar
            // 
            this.ANNStatusBar.Location = new System.Drawing.Point(0, 558);
            this.ANNStatusBar.Name = "ANNStatusBar";
            this.ANNStatusBar.Size = new System.Drawing.Size(670, 22);
            this.ANNStatusBar.TabIndex = 0;
            this.ANNStatusBar.Text = "annStatusBar1";
            // 
            // IrisSimTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ANNStatusBar);
            this.Name = "IrisSimTab";
            this.Size = new System.Drawing.Size(670, 580);
            this.Load += new System.EventHandler(this.IrisSimTab_Load);
            this.groupBox1.ResumeLayout(false);
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
        private LegendTTPlot DistributionPlot;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView TestViewer;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button ExecuteButton;
        private Misc.ConfusionMatrix ConfusionMatrix;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label ConfMatrixStats;
        private System.Windows.Forms.GroupBox groupBox3;
        private Plot.CartesianPlotter LossPlot;
    }
}
