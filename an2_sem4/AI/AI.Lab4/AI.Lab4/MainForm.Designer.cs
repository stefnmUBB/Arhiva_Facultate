namespace AI.Lab4
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
            this.GodsView = new System.Windows.Forms.DataGridView();
            this.GenerationLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SolutionTypePanel = new System.Windows.Forms.GroupBox();
            this.CycleRadio = new System.Windows.Forms.RadioButton();
            this.AllNodesRadio = new System.Windows.Forms.RadioButton();
            this.ShortestPathRadio = new System.Windows.Forms.RadioButton();
            this.FinishNodeBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.StartNodeBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.MaxAgeBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.MutationBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.IndividualsCountLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GraphHost = new System.Windows.Forms.Panel();
            this.CostMatrixView = new System.Windows.Forms.DataGridView();
            this.GraphDesignerCombo = new System.Windows.Forms.ComboBox();
            this.CityFileLoader = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.MissingNodePenaltyBox = new System.Windows.Forms.NumericUpDown();
            this.DupeNodePenaltyBox = new System.Windows.Forms.NumericUpDown();
            this.GraphEditor = new AI.Lab4.UI.Controls.CartesianGraphEditor();
            this.BestFitnessLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GodsView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SolutionTypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinishNodeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartNodeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAgeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MutationBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.GraphHost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CostMatrixView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MissingNodePenaltyBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DupeNodePenaltyBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GodsView
            // 
            this.GodsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GodsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GodsView.Location = new System.Drawing.Point(6, 26);
            this.GodsView.MultiSelect = false;
            this.GodsView.Name = "GodsView";
            this.GodsView.Size = new System.Drawing.Size(337, 120);
            this.GodsView.TabIndex = 2;
            this.GodsView.SelectionChanged += new System.EventHandler(this.GodsView_SelectionChanged);
            // 
            // GenerationLabel
            // 
            this.GenerationLabel.AutoSize = true;
            this.GenerationLabel.Location = new System.Drawing.Point(129, 10);
            this.GenerationLabel.Name = "GenerationLabel";
            this.GenerationLabel.Size = new System.Drawing.Size(27, 13);
            this.GenerationLabel.TabIndex = 10;
            this.GenerationLabel.Text = "Gen";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DupeNodePenaltyBox);
            this.groupBox1.Controls.Add(this.MissingNodePenaltyBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.SolutionTypePanel);
            this.groupBox1.Controls.Add(this.FinishNodeBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.StartNodeBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.ResetButton);
            this.groupBox1.Controls.Add(this.StartStopButton);
            this.groupBox1.Controls.Add(this.MaxAgeBar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.MutationBar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 152);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 273);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // SolutionTypePanel
            // 
            this.SolutionTypePanel.Controls.Add(this.CycleRadio);
            this.SolutionTypePanel.Controls.Add(this.AllNodesRadio);
            this.SolutionTypePanel.Controls.Add(this.ShortestPathRadio);
            this.SolutionTypePanel.Location = new System.Drawing.Point(18, 195);
            this.SolutionTypePanel.Name = "SolutionTypePanel";
            this.SolutionTypePanel.Size = new System.Drawing.Size(310, 43);
            this.SolutionTypePanel.TabIndex = 12;
            this.SolutionTypePanel.TabStop = false;
            this.SolutionTypePanel.Text = "Solution Type";
            // 
            // CycleRadio
            // 
            this.CycleRadio.AutoSize = true;
            this.CycleRadio.Location = new System.Drawing.Point(177, 19);
            this.CycleRadio.Name = "CycleRadio";
            this.CycleRadio.Size = new System.Drawing.Size(93, 17);
            this.CycleRadio.TabIndex = 2;
            this.CycleRadio.TabStop = true;
            this.CycleRadio.Text = "Shortest Cycle";
            this.CycleRadio.UseVisualStyleBackColor = true;
            this.CycleRadio.CheckedChanged += new System.EventHandler(this.CycleRadio_CheckedChanged);
            // 
            // AllNodesRadio
            // 
            this.AllNodesRadio.AutoSize = true;
            this.AllNodesRadio.Location = new System.Drawing.Point(101, 20);
            this.AllNodesRadio.Name = "AllNodesRadio";
            this.AllNodesRadio.Size = new System.Drawing.Size(70, 17);
            this.AllNodesRadio.TabIndex = 1;
            this.AllNodesRadio.TabStop = true;
            this.AllNodesRadio.Text = "All Nodes";
            this.AllNodesRadio.UseVisualStyleBackColor = true;
            this.AllNodesRadio.CheckedChanged += new System.EventHandler(this.AllNodesRadio_CheckedChanged);
            // 
            // ShortestPathRadio
            // 
            this.ShortestPathRadio.AutoSize = true;
            this.ShortestPathRadio.Location = new System.Drawing.Point(6, 20);
            this.ShortestPathRadio.Name = "ShortestPathRadio";
            this.ShortestPathRadio.Size = new System.Drawing.Size(89, 17);
            this.ShortestPathRadio.TabIndex = 0;
            this.ShortestPathRadio.TabStop = true;
            this.ShortestPathRadio.Text = "Shortest Path";
            this.ShortestPathRadio.UseVisualStyleBackColor = true;
            this.ShortestPathRadio.CheckedChanged += new System.EventHandler(this.ShortestPathRadio_CheckedChanged);
            // 
            // FinishNodeBox
            // 
            this.FinishNodeBox.Location = new System.Drawing.Point(200, 160);
            this.FinishNodeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FinishNodeBox.Name = "FinishNodeBox";
            this.FinishNodeBox.Size = new System.Drawing.Size(46, 20);
            this.FinishNodeBox.TabIndex = 11;
            this.FinishNodeBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FinishNodeBox.ValueChanged += new System.EventHandler(this.FinishNodeBox_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Finish Node";
            // 
            // StartNodeBox
            // 
            this.StartNodeBox.Location = new System.Drawing.Point(79, 160);
            this.StartNodeBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StartNodeBox.Name = "StartNodeBox";
            this.StartNodeBox.Size = new System.Drawing.Size(46, 20);
            this.StartNodeBox.TabIndex = 9;
            this.StartNodeBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StartNodeBox.ValueChanged += new System.EventHandler(this.StartNodeBox_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Start Node";
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(68, 244);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(46, 23);
            this.ResetButton.TabIndex = 7;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // StartStopButton
            // 
            this.StartStopButton.Location = new System.Drawing.Point(18, 244);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(46, 23);
            this.StartStopButton.TabIndex = 6;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // MaxAgeBar
            // 
            this.MaxAgeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxAgeBar.Location = new System.Drawing.Point(7, 114);
            this.MaxAgeBar.Maximum = 500;
            this.MaxAgeBar.Minimum = 50;
            this.MaxAgeBar.Name = "MaxAgeBar";
            this.MaxAgeBar.Size = new System.Drawing.Size(324, 45);
            this.MaxAgeBar.TabIndex = 5;
            this.MaxAgeBar.Value = 100;
            this.MaxAgeBar.Scroll += new System.EventHandler(this.MaxAgeBar_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Max Age";
            // 
            // MutationBar
            // 
            this.MutationBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MutationBar.Location = new System.Drawing.Point(7, 66);
            this.MutationBar.Maximum = 100;
            this.MutationBar.Name = "MutationBar";
            this.MutationBar.Size = new System.Drawing.Size(324, 45);
            this.MutationBar.TabIndex = 2;
            this.MutationBar.Scroll += new System.EventHandler(this.MutationBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mutation Probability";
            // 
            // IndividualsCountLabel
            // 
            this.IndividualsCountLabel.AutoSize = true;
            this.IndividualsCountLabel.Location = new System.Drawing.Point(3, 10);
            this.IndividualsCountLabel.Name = "IndividualsCountLabel";
            this.IndividualsCountLabel.Size = new System.Drawing.Size(85, 13);
            this.IndividualsCountLabel.TabIndex = 8;
            this.IndividualsCountLabel.Text = "IndividualsCount";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GraphHost);
            this.splitContainer1.Panel1.Controls.Add(this.GraphDesignerCombo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.BestFitnessLabel);
            this.splitContainer1.Panel2.Controls.Add(this.IndividualsCountLabel);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.GenerationLabel);
            this.splitContainer1.Panel2.Controls.Add(this.GodsView);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 450;
            this.splitContainer1.TabIndex = 11;
            // 
            // GraphHost
            // 
            this.GraphHost.Controls.Add(this.GraphEditor);
            this.GraphHost.Controls.Add(this.CostMatrixView);
            this.GraphHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphHost.Location = new System.Drawing.Point(0, 21);
            this.GraphHost.Name = "GraphHost";
            this.GraphHost.Size = new System.Drawing.Size(450, 429);
            this.GraphHost.TabIndex = 1;
            // 
            // CostMatrixView
            // 
            this.CostMatrixView.AllowUserToAddRows = false;
            this.CostMatrixView.AllowUserToDeleteRows = false;
            this.CostMatrixView.AllowUserToResizeColumns = false;
            this.CostMatrixView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CostMatrixView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CostMatrixView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CostMatrixView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.CostMatrixView.Enabled = false;
            this.CostMatrixView.Location = new System.Drawing.Point(0, 0);
            this.CostMatrixView.MultiSelect = false;
            this.CostMatrixView.Name = "CostMatrixView";
            this.CostMatrixView.ReadOnly = true;
            this.CostMatrixView.Size = new System.Drawing.Size(450, 429);
            this.CostMatrixView.TabIndex = 0;
            // 
            // GraphDesignerCombo
            // 
            this.GraphDesignerCombo.Dock = System.Windows.Forms.DockStyle.Top;
            this.GraphDesignerCombo.FormattingEnabled = true;
            this.GraphDesignerCombo.Items.AddRange(new object[] {
            "Graph From \"City\" File",
            "Cartesian Designer"});
            this.GraphDesignerCombo.Location = new System.Drawing.Point(0, 0);
            this.GraphDesignerCombo.Name = "GraphDesignerCombo";
            this.GraphDesignerCombo.Size = new System.Drawing.Size(450, 21);
            this.GraphDesignerCombo.TabIndex = 0;
            this.GraphDesignerCombo.SelectedIndexChanged += new System.EventHandler(this.GraphDesignerCombo_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Missing Node Penalty";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Dupe Node Penalty";
            // 
            // MissingNodePenaltyBox
            // 
            this.MissingNodePenaltyBox.Location = new System.Drawing.Point(126, 23);
            this.MissingNodePenaltyBox.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.MissingNodePenaltyBox.Name = "MissingNodePenaltyBox";
            this.MissingNodePenaltyBox.Size = new System.Drawing.Size(47, 20);
            this.MissingNodePenaltyBox.TabIndex = 15;
            this.MissingNodePenaltyBox.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.MissingNodePenaltyBox.ValueChanged += new System.EventHandler(this.MissingNodePenaltyBox_ValueChanged);
            // 
            // DupeNodePenaltyBox
            // 
            this.DupeNodePenaltyBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DupeNodePenaltyBox.Location = new System.Drawing.Point(281, 23);
            this.DupeNodePenaltyBox.Maximum = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.DupeNodePenaltyBox.Name = "DupeNodePenaltyBox";
            this.DupeNodePenaltyBox.Size = new System.Drawing.Size(47, 20);
            this.DupeNodePenaltyBox.TabIndex = 16;
            this.DupeNodePenaltyBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.DupeNodePenaltyBox.ValueChanged += new System.EventHandler(this.DupeNodePenaltyBox_ValueChanged);
            // 
            // GraphEditor
            // 
            this.GraphEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphEditor.Location = new System.Drawing.Point(0, 0);
            this.GraphEditor.Name = "GraphEditor";
            this.GraphEditor.Size = new System.Drawing.Size(450, 429);
            this.GraphEditor.TabIndex = 1;
            // 
            // BestFitnessLabel
            // 
            this.BestFitnessLabel.AutoSize = true;
            this.BestFitnessLabel.Location = new System.Drawing.Point(225, 8);
            this.BestFitnessLabel.Name = "BestFitnessLabel";
            this.BestFitnessLabel.Size = new System.Drawing.Size(62, 13);
            this.BestFitnessLabel.TabIndex = 11;
            this.BestFitnessLabel.Text = "CurrentBest";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GodsView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.SolutionTypePanel.ResumeLayout(false);
            this.SolutionTypePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinishNodeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartNodeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAgeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MutationBar)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.GraphHost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CostMatrixView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MissingNodePenaltyBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DupeNodePenaltyBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GodsView;
        private System.Windows.Forms.Label GenerationLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button StartStopButton;
        private System.Windows.Forms.TrackBar MaxAgeBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar MutationBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label IndividualsCountLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox GraphDesignerCombo;
        private System.Windows.Forms.Panel GraphHost;
        private System.Windows.Forms.OpenFileDialog CityFileLoader;
        private System.Windows.Forms.DataGridView CostMatrixView;
        private UI.Controls.CartesianGraphEditor GraphEditor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown FinishNodeBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown StartNodeBox;
        private System.Windows.Forms.GroupBox SolutionTypePanel;
        private System.Windows.Forms.RadioButton ShortestPathRadio;
        private System.Windows.Forms.RadioButton CycleRadio;
        private System.Windows.Forms.RadioButton AllNodesRadio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown DupeNodePenaltyBox;
        private System.Windows.Forms.NumericUpDown MissingNodePenaltyBox;
        private System.Windows.Forms.Label BestFitnessLabel;
    }
}

