namespace AI.Lab2
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
            this.TabControl = new System.Windows.Forms.TabControl();
            this.InputTab = new System.Windows.Forms.TabPage();
            this.InputBody = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.GoButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RandomGraphBuilder = new AI.Lab2.UI.Controls.RandomGraphBuilder();
            this.GmlFileSelector = new AI.Lab2.UI.Controls.PathSelector();
            this.TemplateInputCombobox = new System.Windows.Forms.ComboBox();
            this.FourTestsCombobox = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FourTestsButton = new System.Windows.Forms.RadioButton();
            this.RandomButton = new System.Windows.Forms.RadioButton();
            this.TemplateInputButton = new System.Windows.Forms.RadioButton();
            this.ExternalFileButton = new System.Windows.Forms.RadioButton();
            this.OutputTab = new System.Windows.Forms.TabPage();
            this.ControlsBody = new System.Windows.Forms.Panel();
            this.BackButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.CommunitiesBox = new System.Windows.Forms.GroupBox();
            this.CommunitiesOutput = new System.Windows.Forms.TextBox();
            this.CommunitiesOutputCondensed = new System.Windows.Forms.TextBox();
            this.FindSubComsButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Histogram = new AI.Lab2.UI.Controls.Histogram();
            this.GraphViewCombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GraphViewer = new AI.Lab2.UI.Controls.DictionaryNodeGraphViewer();
            this.TabControl.SuspendLayout();
            this.InputTab.SuspendLayout();
            this.InputBody.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.OutputTab.SuspendLayout();
            this.ControlsBody.SuspendLayout();
            this.CommunitiesBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.InputTab);
            this.TabControl.Controls.Add(this.OutputTab);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(803, 450);
            this.TabControl.TabIndex = 1;
            // 
            // InputTab
            // 
            this.InputTab.BackColor = System.Drawing.SystemColors.Control;
            this.InputTab.Controls.Add(this.InputBody);
            this.InputTab.Location = new System.Drawing.Point(4, 22);
            this.InputTab.Name = "InputTab";
            this.InputTab.Padding = new System.Windows.Forms.Padding(3);
            this.InputTab.Size = new System.Drawing.Size(795, 424);
            this.InputTab.TabIndex = 0;
            this.InputTab.Text = "Input";
            // 
            // InputBody
            // 
            this.InputBody.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.InputBody.Controls.Add(this.panel3);
            this.InputBody.Controls.Add(this.panel2);
            this.InputBody.Controls.Add(this.panel1);
            this.InputBody.Location = new System.Drawing.Point(170, 87);
            this.InputBody.Name = "InputBody";
            this.InputBody.Size = new System.Drawing.Size(462, 270);
            this.InputBody.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.GoButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 233);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(462, 27);
            this.panel3.TabIndex = 8;
            // 
            // GoButton
            // 
            this.GoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GoButton.Location = new System.Drawing.Point(383, 3);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(75, 23);
            this.GoButton.TabIndex = 4;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.RandomGraphBuilder);
            this.panel2.Controls.Add(this.GmlFileSelector);
            this.panel2.Controls.Add(this.TemplateInputCombobox);
            this.panel2.Controls.Add(this.FourTestsCombobox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(462, 212);
            this.panel2.TabIndex = 3;
            // 
            // RandomGraphBuilder
            // 
            this.RandomGraphBuilder.Dock = System.Windows.Forms.DockStyle.Top;
            this.RandomGraphBuilder.Location = new System.Drawing.Point(0, 62);
            this.RandomGraphBuilder.Name = "RandomGraphBuilder";
            this.RandomGraphBuilder.Size = new System.Drawing.Size(462, 150);
            this.RandomGraphBuilder.TabIndex = 3;
            this.RandomGraphBuilder.Visible = false;
            // 
            // GmlFileSelector
            // 
            this.GmlFileSelector.Dock = System.Windows.Forms.DockStyle.Top;
            this.GmlFileSelector.Filter = "GML files(*.gml)|*.gml";
            this.GmlFileSelector.InitialDirectory = "";
            this.GmlFileSelector.Location = new System.Drawing.Point(0, 42);
            this.GmlFileSelector.Name = "GmlFileSelector";
            this.GmlFileSelector.Path = "";
            this.GmlFileSelector.Size = new System.Drawing.Size(462, 20);
            this.GmlFileSelector.TabIndex = 2;
            this.GmlFileSelector.Visible = false;
            this.GmlFileSelector.PathChanged += new AI.Lab2.UI.Controls.PathSelector.OnPathChanged(this.GmlFileSelector_PathChanged);
            // 
            // TemplateInputCombobox
            // 
            this.TemplateInputCombobox.Dock = System.Windows.Forms.DockStyle.Top;
            this.TemplateInputCombobox.FormattingEnabled = true;
            this.TemplateInputCombobox.Location = new System.Drawing.Point(0, 21);
            this.TemplateInputCombobox.Name = "TemplateInputCombobox";
            this.TemplateInputCombobox.Size = new System.Drawing.Size(462, 21);
            this.TemplateInputCombobox.TabIndex = 1;
            this.TemplateInputCombobox.Visible = false;
            this.TemplateInputCombobox.SelectedIndexChanged += new System.EventHandler(this.TemplateInputCombobox_SelectedIndexChanged);
            // 
            // FourTestsCombobox
            // 
            this.FourTestsCombobox.Dock = System.Windows.Forms.DockStyle.Top;
            this.FourTestsCombobox.FormattingEnabled = true;
            this.FourTestsCombobox.Items.AddRange(new object[] {
            "Dolphins",
            "Football",
            "Karate",
            "Krebs"});
            this.FourTestsCombobox.Location = new System.Drawing.Point(0, 0);
            this.FourTestsCombobox.Name = "FourTestsCombobox";
            this.FourTestsCombobox.Size = new System.Drawing.Size(462, 21);
            this.FourTestsCombobox.TabIndex = 0;
            this.FourTestsCombobox.Visible = false;
            this.FourTestsCombobox.SelectedIndexChanged += new System.EventHandler(this.FourTestsCombobox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FourTestsButton);
            this.panel1.Controls.Add(this.RandomButton);
            this.panel1.Controls.Add(this.TemplateInputButton);
            this.panel1.Controls.Add(this.ExternalFileButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(462, 21);
            this.panel1.TabIndex = 7;
            // 
            // FourTestsButton
            // 
            this.FourTestsButton.AutoSize = true;
            this.FourTestsButton.Location = new System.Drawing.Point(3, 3);
            this.FourTestsButton.Name = "FourTestsButton";
            this.FourTestsButton.Size = new System.Drawing.Size(100, 17);
            this.FourTestsButton.TabIndex = 1;
            this.FourTestsButton.TabStop = true;
            this.FourTestsButton.Text = "From the 4 tests";
            this.FourTestsButton.UseVisualStyleBackColor = true;
            this.FourTestsButton.CheckedChanged += new System.EventHandler(this.FourTestsButton_CheckedChanged);
            // 
            // RandomButton
            // 
            this.RandomButton.AutoSize = true;
            this.RandomButton.Location = new System.Drawing.Point(351, 3);
            this.RandomButton.Name = "RandomButton";
            this.RandomButton.Size = new System.Drawing.Size(65, 17);
            this.RandomButton.TabIndex = 6;
            this.RandomButton.TabStop = true;
            this.RandomButton.Text = "Random";
            this.RandomButton.UseVisualStyleBackColor = true;
            this.RandomButton.CheckedChanged += new System.EventHandler(this.RandomButton_CheckedChanged);
            // 
            // TemplateInputButton
            // 
            this.TemplateInputButton.AutoSize = true;
            this.TemplateInputButton.Location = new System.Drawing.Point(109, 3);
            this.TemplateInputButton.Name = "TemplateInputButton";
            this.TemplateInputButton.Size = new System.Drawing.Size(122, 17);
            this.TemplateInputButton.TabIndex = 2;
            this.TemplateInputButton.TabStop = true;
            this.TemplateInputButton.Text = "From Template Input";
            this.TemplateInputButton.UseVisualStyleBackColor = true;
            this.TemplateInputButton.CheckedChanged += new System.EventHandler(this.TemplateInputButton_CheckedChanged);
            // 
            // ExternalFileButton
            // 
            this.ExternalFileButton.AutoSize = true;
            this.ExternalFileButton.Location = new System.Drawing.Point(237, 3);
            this.ExternalFileButton.Name = "ExternalFileButton";
            this.ExternalFileButton.Size = new System.Drawing.Size(108, 17);
            this.ExternalFileButton.TabIndex = 5;
            this.ExternalFileButton.TabStop = true;
            this.ExternalFileButton.Text = "From External File";
            this.ExternalFileButton.UseVisualStyleBackColor = true;
            this.ExternalFileButton.CheckedChanged += new System.EventHandler(this.ExternalFileButton_CheckedChanged);
            // 
            // OutputTab
            // 
            this.OutputTab.BackColor = System.Drawing.SystemColors.Control;
            this.OutputTab.Controls.Add(this.ControlsBody);
            this.OutputTab.Controls.Add(this.GraphViewer);
            this.OutputTab.Location = new System.Drawing.Point(4, 22);
            this.OutputTab.Name = "OutputTab";
            this.OutputTab.Padding = new System.Windows.Forms.Padding(3);
            this.OutputTab.Size = new System.Drawing.Size(795, 424);
            this.OutputTab.TabIndex = 1;
            this.OutputTab.Text = "Output";
            // 
            // ControlsBody
            // 
            this.ControlsBody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlsBody.Controls.Add(this.BackButton);
            this.ControlsBody.Controls.Add(this.ResetButton);
            this.ControlsBody.Controls.Add(this.CommunitiesBox);
            this.ControlsBody.Controls.Add(this.FindSubComsButton);
            this.ControlsBody.Controls.Add(this.groupBox1);
            this.ControlsBody.Controls.Add(this.GraphViewCombo);
            this.ControlsBody.Controls.Add(this.label1);
            this.ControlsBody.Location = new System.Drawing.Point(542, 6);
            this.ControlsBody.Name = "ControlsBody";
            this.ControlsBody.Size = new System.Drawing.Size(245, 412);
            this.ControlsBody.TabIndex = 1;
            // 
            // BackButton
            // 
            this.BackButton.Location = new System.Drawing.Point(184, 174);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(48, 23);
            this.BackButton.TabIndex = 8;
            this.BackButton.Text = "Back";
            this.BackButton.UseVisualStyleBackColor = true;
            this.BackButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(130, 174);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(48, 23);
            this.ResetButton.TabIndex = 7;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // CommunitiesBox
            // 
            this.CommunitiesBox.Controls.Add(this.CommunitiesOutput);
            this.CommunitiesBox.Controls.Add(this.CommunitiesOutputCondensed);
            this.CommunitiesBox.Location = new System.Drawing.Point(9, 203);
            this.CommunitiesBox.Name = "CommunitiesBox";
            this.CommunitiesBox.Size = new System.Drawing.Size(230, 148);
            this.CommunitiesBox.TabIndex = 6;
            this.CommunitiesBox.TabStop = false;
            this.CommunitiesBox.Text = "Found Communities";
            // 
            // CommunitiesOutput
            // 
            this.CommunitiesOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommunitiesOutput.Location = new System.Drawing.Point(3, 57);
            this.CommunitiesOutput.Multiline = true;
            this.CommunitiesOutput.Name = "CommunitiesOutput";
            this.CommunitiesOutput.ReadOnly = true;
            this.CommunitiesOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.CommunitiesOutput.Size = new System.Drawing.Size(224, 88);
            this.CommunitiesOutput.TabIndex = 5;
            this.CommunitiesOutput.WordWrap = false;
            // 
            // CommunitiesOutputCondensed
            // 
            this.CommunitiesOutputCondensed.Dock = System.Windows.Forms.DockStyle.Top;
            this.CommunitiesOutputCondensed.Location = new System.Drawing.Point(3, 16);
            this.CommunitiesOutputCondensed.Multiline = true;
            this.CommunitiesOutputCondensed.Name = "CommunitiesOutputCondensed";
            this.CommunitiesOutputCondensed.ReadOnly = true;
            this.CommunitiesOutputCondensed.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.CommunitiesOutputCondensed.Size = new System.Drawing.Size(224, 41);
            this.CommunitiesOutputCondensed.TabIndex = 6;
            this.CommunitiesOutputCondensed.WordWrap = false;
            // 
            // FindSubComsButton
            // 
            this.FindSubComsButton.Location = new System.Drawing.Point(9, 174);
            this.FindSubComsButton.Name = "FindSubComsButton";
            this.FindSubComsButton.Size = new System.Drawing.Size(115, 23);
            this.FindSubComsButton.TabIndex = 4;
            this.FindSubComsButton.Text = "Find Subcommunities";
            this.FindSubComsButton.UseVisualStyleBackColor = true;
            this.FindSubComsButton.Click += new System.EventHandler(this.FindSubComsButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Histogram);
            this.groupBox1.Location = new System.Drawing.Point(6, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 135);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modularity";
            // 
            // Histogram
            // 
            this.Histogram.BackColor = System.Drawing.Color.White;
            this.Histogram.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Histogram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Histogram.Location = new System.Drawing.Point(3, 16);
            this.Histogram.Name = "Histogram";
            this.Histogram.SelectedX = 0;
            this.Histogram.Size = new System.Drawing.Size(230, 116);
            this.Histogram.TabIndex = 2;
            this.Histogram.Text = "histogram1";
            this.Histogram.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Histogram_MouseDown);
            // 
            // GraphViewCombo
            // 
            this.GraphViewCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphViewCombo.FormattingEnabled = true;
            this.GraphViewCombo.Items.AddRange(new object[] {
            "Circular",
            "Force-Based",
            "Community"});
            this.GraphViewCombo.Location = new System.Drawing.Point(39, 6);
            this.GraphViewCombo.Name = "GraphViewCombo";
            this.GraphViewCombo.Size = new System.Drawing.Size(203, 21);
            this.GraphViewCombo.TabIndex = 1;
            this.GraphViewCombo.SelectedIndexChanged += new System.EventHandler(this.GraphViewCombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "View";
            // 
            // GraphViewer
            // 
            this.GraphViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GraphViewer.AutoScroll = true;
            this.GraphViewer.BackColor = System.Drawing.Color.White;
            this.GraphViewer.Location = new System.Drawing.Point(6, 6);
            this.GraphViewer.Name = "GraphViewer";
            this.GraphViewer.Size = new System.Drawing.Size(530, 410);
            this.GraphViewer.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 450);
            this.Controls.Add(this.TabControl);
            this.Name = "MainForm";
            this.Text = "Community Finder";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TabControl.ResumeLayout(false);
            this.InputTab.ResumeLayout(false);
            this.InputBody.ResumeLayout(false);
            this.InputBody.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.OutputTab.ResumeLayout(false);
            this.ControlsBody.ResumeLayout(false);
            this.ControlsBody.PerformLayout();
            this.CommunitiesBox.ResumeLayout(false);
            this.CommunitiesBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Controls.DictionaryNodeGraphViewer GraphViewer;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage InputTab;
        private System.Windows.Forms.TabPage OutputTab;
        private System.Windows.Forms.RadioButton TemplateInputButton;
        private System.Windows.Forms.RadioButton FourTestsButton;
        private System.Windows.Forms.Panel InputBody;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox FourTestsCombobox;
        private System.Windows.Forms.ComboBox TemplateInputCombobox;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.Panel ControlsBody;
        private System.Windows.Forms.ComboBox GraphViewCombo;
        private System.Windows.Forms.Label label1;
        private UI.Controls.Histogram Histogram;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button FindSubComsButton;
        private System.Windows.Forms.GroupBox CommunitiesBox;
        private System.Windows.Forms.TextBox CommunitiesOutput;
        private System.Windows.Forms.TextBox CommunitiesOutputCondensed;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button BackButton;
        private UI.Controls.PathSelector GmlFileSelector;
        private System.Windows.Forms.RadioButton ExternalFileButton;
        private System.Windows.Forms.RadioButton RandomButton;
        private UI.Controls.RandomGraphBuilder RandomGraphBuilder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
    }
}

