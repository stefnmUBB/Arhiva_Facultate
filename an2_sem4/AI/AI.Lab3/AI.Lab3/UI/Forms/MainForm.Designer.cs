namespace AI.Lab3
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
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromThe4TestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GraphViewer = new AI.Commons.UI.Controls.IntNodeGraphViewer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CommunitiesBox = new System.Windows.Forms.TextBox();
            this.LabelsBox = new System.Windows.Forms.TextBox();
            this.GenerationLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ViewComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AlphaBox = new System.Windows.Forms.NumericUpDown();
            this.AlphaLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ChromosomeSelector = new System.Windows.Forms.ComboBox();
            this.ResetButton = new System.Windows.Forms.Button();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.MaxAgeBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.MutationBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.GodsView = new System.Windows.Forms.DataGridView();
            this.IndividualsCountLabel = new System.Windows.Forms.Label();
            this.loadGMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAgeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MutationBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GodsView)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(826, 24);
            this.MainMenu.TabIndex = 0;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadGraphToolStripMenuItem,
            this.loadGMLToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadGraphToolStripMenuItem
            // 
            this.loadGraphToolStripMenuItem.Name = "loadGraphToolStripMenuItem";
            this.loadGraphToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadGraphToolStripMenuItem.Text = "Load Graph...";
            // 
            // fromThe4TestsToolStripMenuItem
            // 
            this.fromThe4TestsToolStripMenuItem.Name = "fromThe4TestsToolStripMenuItem";
            this.fromThe4TestsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fromThe4TestsToolStripMenuItem.Text = "From the 4 tests...";
            // 
            // fromFileToolStripMenuItem
            // 
            this.fromFileToolStripMenuItem.Name = "fromFileToolStripMenuItem";
            this.fromFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fromFileToolStripMenuItem.Text = "From file...";
            // 
            // fromTemplateToolStripMenuItem
            // 
            this.fromTemplateToolStripMenuItem.Name = "fromTemplateToolStripMenuItem";
            this.fromTemplateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fromTemplateToolStripMenuItem.Text = "From template...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.GraphViewer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel2.Controls.Add(this.GenerationLabel);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.ViewComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.GodsView);
            this.splitContainer1.Panel2.Controls.Add(this.IndividualsCountLabel);
            this.splitContainer1.Size = new System.Drawing.Size(826, 561);
            this.splitContainer1.SplitterDistance = 475;
            this.splitContainer1.TabIndex = 1;
            // 
            // GraphViewer
            // 
            this.GraphViewer.BackColor = System.Drawing.Color.White;
            this.GraphViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphViewer.Location = new System.Drawing.Point(0, 0);
            this.GraphViewer.Name = "GraphViewer";
            this.GraphViewer.Size = new System.Drawing.Size(475, 561);
            this.GraphViewer.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.CommunitiesBox);
            this.groupBox2.Controls.Add(this.LabelsBox);
            this.groupBox2.Location = new System.Drawing.Point(6, 420);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(332, 129);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Communities";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Labels";
            // 
            // CommunitiesBox
            // 
            this.CommunitiesBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommunitiesBox.Location = new System.Drawing.Point(78, 45);
            this.CommunitiesBox.Multiline = true;
            this.CommunitiesBox.Name = "CommunitiesBox";
            this.CommunitiesBox.ReadOnly = true;
            this.CommunitiesBox.Size = new System.Drawing.Size(247, 78);
            this.CommunitiesBox.TabIndex = 1;
            this.CommunitiesBox.WordWrap = false;
            // 
            // LabelsBox
            // 
            this.LabelsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelsBox.Location = new System.Drawing.Point(78, 19);
            this.LabelsBox.Name = "LabelsBox";
            this.LabelsBox.ReadOnly = true;
            this.LabelsBox.Size = new System.Drawing.Size(247, 20);
            this.LabelsBox.TabIndex = 0;
            // 
            // GenerationLabel
            // 
            this.GenerationLabel.AutoSize = true;
            this.GenerationLabel.Location = new System.Drawing.Point(147, 33);
            this.GenerationLabel.Name = "GenerationLabel";
            this.GenerationLabel.Size = new System.Drawing.Size(27, 13);
            this.GenerationLabel.TabIndex = 7;
            this.GenerationLabel.Text = "Gen";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "View";
            // 
            // ViewComboBox
            // 
            this.ViewComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewComboBox.FormattingEnabled = true;
            this.ViewComboBox.Items.AddRange(new object[] {
            "Circular",
            "Force-Based",
            "Community"});
            this.ViewComboBox.Location = new System.Drawing.Point(38, 9);
            this.ViewComboBox.Name = "ViewComboBox";
            this.ViewComboBox.Size = new System.Drawing.Size(300, 21);
            this.ViewComboBox.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.AlphaBox);
            this.groupBox1.Controls.Add(this.AlphaLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.ChromosomeSelector);
            this.groupBox1.Controls.Add(this.ResetButton);
            this.groupBox1.Controls.Add(this.StartStopButton);
            this.groupBox1.Controls.Add(this.MaxAgeBar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.MutationBar);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 205);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(338, 209);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // AlphaBox
            // 
            this.AlphaBox.DecimalPlaces = 2;
            this.AlphaBox.Location = new System.Drawing.Point(263, 20);
            this.AlphaBox.Name = "AlphaBox";
            this.AlphaBox.Size = new System.Drawing.Size(62, 20);
            this.AlphaBox.TabIndex = 11;
            this.AlphaBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // AlphaLabel
            // 
            this.AlphaLabel.AutoSize = true;
            this.AlphaLabel.Location = new System.Drawing.Point(243, 22);
            this.AlphaLabel.Name = "AlphaLabel";
            this.AlphaLabel.Size = new System.Drawing.Size(14, 13);
            this.AlphaLabel.TabIndex = 10;
            this.AlphaLabel.Text = "α";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Chromosome";
            // 
            // ChromosomeSelector
            // 
            this.ChromosomeSelector.FormattingEnabled = true;
            this.ChromosomeSelector.Items.AddRange(new object[] {
            "Modularity",
            "Density",
            "CommunityScore",
            "WrongModularity"});
            this.ChromosomeSelector.Location = new System.Drawing.Point(88, 19);
            this.ChromosomeSelector.Name = "ChromosomeSelector";
            this.ChromosomeSelector.Size = new System.Drawing.Size(149, 21);
            this.ChromosomeSelector.TabIndex = 8;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(68, 165);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(46, 23);
            this.ResetButton.TabIndex = 7;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // StartStopButton
            // 
            this.StartStopButton.Location = new System.Drawing.Point(18, 165);
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
            this.MaxAgeBar.Size = new System.Drawing.Size(325, 45);
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
            this.MutationBar.Size = new System.Drawing.Size(325, 45);
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
            // GodsView
            // 
            this.GodsView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GodsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GodsView.Location = new System.Drawing.Point(6, 49);
            this.GodsView.Name = "GodsView";
            this.GodsView.Size = new System.Drawing.Size(338, 150);
            this.GodsView.TabIndex = 1;
            this.GodsView.SelectionChanged += new System.EventHandler(this.GodsView_SelectionChanged);
            // 
            // IndividualsCountLabel
            // 
            this.IndividualsCountLabel.AutoSize = true;
            this.IndividualsCountLabel.Location = new System.Drawing.Point(3, 33);
            this.IndividualsCountLabel.Name = "IndividualsCountLabel";
            this.IndividualsCountLabel.Size = new System.Drawing.Size(85, 13);
            this.IndividualsCountLabel.TabIndex = 0;
            this.IndividualsCountLabel.Text = "IndividualsCount";
            // 
            // loadGMLToolStripMenuItem
            // 
            this.loadGMLToolStripMenuItem.Name = "loadGMLToolStripMenuItem";
            this.loadGMLToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadGMLToolStripMenuItem.Text = "Load GML...";
            this.loadGMLToolStripMenuItem.Click += new System.EventHandler(this.loadGMLToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 585);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAgeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MutationBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GodsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromThe4TestsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromTemplateToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Commons.UI.Controls.IntNodeGraphViewer GraphViewer;
        private System.Windows.Forms.Label IndividualsCountLabel;
        private System.Windows.Forms.DataGridView GodsView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar MutationBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar MaxAgeBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ViewComboBox;
        private System.Windows.Forms.Button StartStopButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.ComboBox ChromosomeSelector;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown AlphaBox;
        private System.Windows.Forms.Label AlphaLabel;
        private System.Windows.Forms.Label GenerationLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox CommunitiesBox;
        private System.Windows.Forms.TextBox LabelsBox;
        private System.Windows.Forms.ToolStripMenuItem loadGMLToolStripMenuItem;
    }
}

