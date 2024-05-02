namespace AI.Lab8
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.D1GdSettings = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.D1BatchSize = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.D1GDMaxEpochs = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.D1GDLearningRate = new System.Windows.Forms.NumericUpDown();
            this.D1LsSettings = new System.Windows.Forms.Panel();
            this.D1GD_Selector = new System.Windows.Forms.RadioButton();
            this.D1LSR_Selector = new System.Windows.Forms.RadioButton();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.D1OutputsView = new System.Windows.Forms.DataGridView();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.D1Logs = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.D1ShowPredicted = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.D1ConfMatrix = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.D1AccuracyLabel = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.D1WeightsLabel = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.D1AllZButton = new System.Windows.Forms.Button();
            this.D1AllMinMaxButton = new System.Windows.Forms.Button();
            this.D1AllNoNormButton = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.D1InputSelector = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.D1PartitionsBox = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.D1CrossValidationItersBox = new System.Windows.Forms.NumericUpDown();
            this.D1CrossValidation = new System.Windows.Forms.CheckBox();
            this.D1Run = new System.Windows.Forms.Button();
            this.ThresholdBar = new System.Windows.Forms.TrackBar();
            this.ThresholdLabel = new System.Windows.Forms.Label();
            this.D2RunButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.D2GdSettings = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.D2BatchSize = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.D2GDMaxEpochs = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.D2GDLearningRate = new System.Windows.Forms.NumericUpDown();
            this.D2LsSettings = new System.Windows.Forms.Panel();
            this.D2GD_Selector = new System.Windows.Forms.RadioButton();
            this.D2LSR_Selector = new System.Windows.Forms.RadioButton();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.D2OutputsView = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.D2Logs = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.D2ShowPredicted = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.D2PlotYSelector = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.D2PlotXSelector = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.D2AllZButton = new System.Windows.Forms.Button();
            this.D2AllMinMaxButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.D2AllNoNormButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.D2ConfMatrixLabel = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.D2AccuracyLabel = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.D2WeightsLabel = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.D1PlotSpace = new AI.Lab8.Controls.CartesianPlotter();
            this.D1LossPlotter = new AI.Lab8.Controls.CartesianPlotter();
            this.D1OutNormSelector = new AI.Lab8.Controls.NormalizationMethodSelector();
            this.D1F0NormSelector = new AI.Lab8.Controls.NormalizationMethodSelector();
            this.D1F1NormSelector = new AI.Lab8.Controls.NormalizationMethodSelector();
            this.D2PetalLSepalLPlot = new AI.Lab8.Controls.CartesianPlotter();
            this.D2F0NormSelector = new AI.Lab8.Controls.NormalizationMethodSelector();
            this.D2F3NormSelector = new AI.Lab8.Controls.NormalizationMethodSelector();
            this.D2OutNormSelector = new AI.Lab8.Controls.NormalizationMethodSelector();
            this.D2F2NormSelector = new AI.Lab8.Controls.NormalizationMethodSelector();
            this.D2F1NormSelector = new AI.Lab8.Controls.NormalizationMethodSelector();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.D2Partitions = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.D2CVIterations = new System.Windows.Forms.NumericUpDown();
            this.D2CrossValidation = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.D1GdSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D1BatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D1GDMaxEpochs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D1GDLearningRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D1OutputsView)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D1PartitionsBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D1CrossValidationItersBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).BeginInit();
            this.panel1.SuspendLayout();
            this.D2GdSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D2BatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D2GDMaxEpochs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D2GDLearningRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D2OutputsView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D2Partitions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.D2CVIterations)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.D1InputSelector);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 450);
            this.splitContainer1.SplitterDistance = 591;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.D1GdSettings);
            this.panel4.Controls.Add(this.D1LsSettings);
            this.panel4.Controls.Add(this.D1GD_Selector);
            this.panel4.Controls.Add(this.D1LSR_Selector);
            this.panel4.Location = new System.Drawing.Point(272, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(178, 170);
            this.panel4.TabIndex = 17;
            // 
            // D1GdSettings
            // 
            this.D1GdSettings.Controls.Add(this.label15);
            this.D1GdSettings.Controls.Add(this.D1BatchSize);
            this.D1GdSettings.Controls.Add(this.label12);
            this.D1GdSettings.Controls.Add(this.D1GDMaxEpochs);
            this.D1GdSettings.Controls.Add(this.label14);
            this.D1GdSettings.Controls.Add(this.D1GDLearningRate);
            this.D1GdSettings.Enabled = false;
            this.D1GdSettings.Location = new System.Drawing.Point(3, 70);
            this.D1GdSettings.Name = "D1GdSettings";
            this.D1GdSettings.Size = new System.Drawing.Size(171, 87);
            this.D1GdSettings.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1, 55);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Batch Size";
            // 
            // D1BatchSize
            // 
            this.D1BatchSize.Location = new System.Drawing.Point(76, 53);
            this.D1BatchSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.D1BatchSize.Name = "D1BatchSize";
            this.D1BatchSize.Size = new System.Drawing.Size(95, 20);
            this.D1BatchSize.TabIndex = 4;
            this.D1BatchSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(1, 31);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Max Epochs";
            // 
            // D1GDMaxEpochs
            // 
            this.D1GDMaxEpochs.Location = new System.Drawing.Point(76, 29);
            this.D1GDMaxEpochs.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.D1GDMaxEpochs.Name = "D1GDMaxEpochs";
            this.D1GDMaxEpochs.Size = new System.Drawing.Size(95, 20);
            this.D1GDMaxEpochs.TabIndex = 2;
            this.D1GDMaxEpochs.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1, 5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Learning rate";
            // 
            // D1GDLearningRate
            // 
            this.D1GDLearningRate.DecimalPlaces = 5;
            this.D1GDLearningRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.D1GDLearningRate.Location = new System.Drawing.Point(76, 3);
            this.D1GDLearningRate.Name = "D1GDLearningRate";
            this.D1GDLearningRate.Size = new System.Drawing.Size(92, 20);
            this.D1GDLearningRate.TabIndex = 0;
            this.D1GDLearningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // D1LsSettings
            // 
            this.D1LsSettings.Location = new System.Drawing.Point(3, 26);
            this.D1LsSettings.Name = "D1LsSettings";
            this.D1LsSettings.Size = new System.Drawing.Size(171, 15);
            this.D1LsSettings.TabIndex = 1;
            // 
            // D1GD_Selector
            // 
            this.D1GD_Selector.AutoSize = true;
            this.D1GD_Selector.Location = new System.Drawing.Point(3, 47);
            this.D1GD_Selector.Name = "D1GD_Selector";
            this.D1GD_Selector.Size = new System.Drawing.Size(92, 17);
            this.D1GD_Selector.TabIndex = 2;
            this.D1GD_Selector.TabStop = true;
            this.D1GD_Selector.Text = "GD Regressor";
            this.D1GD_Selector.UseVisualStyleBackColor = true;
            this.D1GD_Selector.CheckedChanged += new System.EventHandler(this.D1GDR_Selector_CheckedChanged);
            // 
            // D1LSR_Selector
            // 
            this.D1LSR_Selector.AutoSize = true;
            this.D1LSR_Selector.Location = new System.Drawing.Point(3, 3);
            this.D1LSR_Selector.Name = "D1LSR_Selector";
            this.D1LSR_Selector.Size = new System.Drawing.Size(89, 17);
            this.D1LSR_Selector.TabIndex = 0;
            this.D1LSR_Selector.TabStop = true;
            this.D1LSR_Selector.Text = "LS Regressor";
            this.D1LSR_Selector.UseVisualStyleBackColor = true;
            this.D1LSR_Selector.CheckedChanged += new System.EventHandler(this.D1LSR_Selector_CheckedChanged);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer3.Location = new System.Drawing.Point(12, 188);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.D1OutputsView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer3.Size = new System.Drawing.Size(576, 250);
            this.splitContainer3.SplitterDistance = 191;
            this.splitContainer3.TabIndex = 16;
            // 
            // D1OutputsView
            // 
            this.D1OutputsView.AllowUserToAddRows = false;
            this.D1OutputsView.AllowUserToDeleteRows = false;
            this.D1OutputsView.AllowUserToResizeColumns = false;
            this.D1OutputsView.AllowUserToResizeRows = false;
            this.D1OutputsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D1OutputsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.D1OutputsView.Location = new System.Drawing.Point(0, 0);
            this.D1OutputsView.MultiSelect = false;
            this.D1OutputsView.Name = "D1OutputsView";
            this.D1OutputsView.ReadOnly = true;
            this.D1OutputsView.RowHeadersVisible = false;
            this.D1OutputsView.Size = new System.Drawing.Size(191, 250);
            this.D1OutputsView.TabIndex = 12;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(381, 250);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.D1Logs);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(373, 224);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Logs";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // D1Logs
            // 
            this.D1Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.D1Logs.Location = new System.Drawing.Point(3, 3);
            this.D1Logs.Multiline = true;
            this.D1Logs.Name = "D1Logs";
            this.D1Logs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.D1Logs.Size = new System.Drawing.Size(367, 218);
            this.D1Logs.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.D1ShowPredicted);
            this.tabPage4.Controls.Add(this.D1PlotSpace);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(373, 224);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Plot";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // D1ShowPredicted
            // 
            this.D1ShowPredicted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.D1ShowPredicted.AutoSize = true;
            this.D1ShowPredicted.Location = new System.Drawing.Point(266, 7);
            this.D1ShowPredicted.Name = "D1ShowPredicted";
            this.D1ShowPredicted.Size = new System.Drawing.Size(101, 17);
            this.D1ShowPredicted.TabIndex = 3;
            this.D1ShowPredicted.Text = "Show Predicted";
            this.D1ShowPredicted.UseVisualStyleBackColor = true;
            this.D1ShowPredicted.CheckedChanged += new System.EventHandler(this.D1ShowPredicted_CheckedChanged);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label20);
            this.tabPage5.Controls.Add(this.label19);
            this.tabPage5.Controls.Add(this.D1LossPlotter);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(373, 224);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Loss";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Blue;
            this.label20.Location = new System.Drawing.Point(326, 29);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(25, 13);
            this.label20.TabIndex = 2;
            this.label20.Text = "Log";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(326, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(30, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "MSE";
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.D1ConfMatrix);
            this.tabPage6.Controls.Add(this.label23);
            this.tabPage6.Controls.Add(this.D1AccuracyLabel);
            this.tabPage6.Controls.Add(this.label22);
            this.tabPage6.Controls.Add(this.D1WeightsLabel);
            this.tabPage6.Controls.Add(this.label21);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(373, 224);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Stats";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // D1ConfMatrix
            // 
            this.D1ConfMatrix.AutoSize = true;
            this.D1ConfMatrix.Location = new System.Drawing.Point(98, 75);
            this.D1ConfMatrix.Name = "D1ConfMatrix";
            this.D1ConfMatrix.Size = new System.Drawing.Size(10, 13);
            this.D1ConfMatrix.TabIndex = 5;
            this.D1ConfMatrix.Text = "-";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 76);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(85, 13);
            this.label23.TabIndex = 4;
            this.label23.Text = "Confusion Matrix";
            // 
            // D1AccuracyLabel
            // 
            this.D1AccuracyLabel.AutoSize = true;
            this.D1AccuracyLabel.Location = new System.Drawing.Point(65, 46);
            this.D1AccuracyLabel.Name = "D1AccuracyLabel";
            this.D1AccuracyLabel.Size = new System.Drawing.Size(10, 13);
            this.D1AccuracyLabel.TabIndex = 3;
            this.D1AccuracyLabel.Text = "-";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(6, 46);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(52, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "Accuracy";
            // 
            // D1WeightsLabel
            // 
            this.D1WeightsLabel.AutoSize = true;
            this.D1WeightsLabel.Location = new System.Drawing.Point(65, 16);
            this.D1WeightsLabel.Name = "D1WeightsLabel";
            this.D1WeightsLabel.Size = new System.Drawing.Size(10, 13);
            this.D1WeightsLabel.TabIndex = 1;
            this.D1WeightsLabel.Text = "-";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 16);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(46, 13);
            this.label21.TabIndex = 0;
            this.label21.Text = "Weights";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.D1OutNormSelector);
            this.groupBox2.Controls.Add(this.D1AllZButton);
            this.groupBox2.Controls.Add(this.D1F0NormSelector);
            this.groupBox2.Controls.Add(this.D1AllMinMaxButton);
            this.groupBox2.Controls.Add(this.D1AllNoNormButton);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.D1F1NormSelector);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(12, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(254, 140);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Normalizations";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Output";
            // 
            // D1AllZButton
            // 
            this.D1AllZButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.D1AllZButton.Location = new System.Drawing.Point(11, 104);
            this.D1AllZButton.Name = "D1AllZButton";
            this.D1AllZButton.Size = new System.Drawing.Size(75, 23);
            this.D1AllZButton.TabIndex = 13;
            this.D1AllZButton.Text = "All ZNorm";
            this.D1AllZButton.UseVisualStyleBackColor = true;
            this.D1AllZButton.Click += new System.EventHandler(this.D1AllZButton_Click);
            // 
            // D1AllMinMaxButton
            // 
            this.D1AllMinMaxButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.D1AllMinMaxButton.Location = new System.Drawing.Point(92, 104);
            this.D1AllMinMaxButton.Name = "D1AllMinMaxButton";
            this.D1AllMinMaxButton.Size = new System.Drawing.Size(75, 23);
            this.D1AllMinMaxButton.TabIndex = 12;
            this.D1AllMinMaxButton.Text = "All MinMax";
            this.D1AllMinMaxButton.UseVisualStyleBackColor = true;
            this.D1AllMinMaxButton.Click += new System.EventHandler(this.D1AllMinMaxButton_Click);
            // 
            // D1AllNoNormButton
            // 
            this.D1AllNoNormButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.D1AllNoNormButton.Location = new System.Drawing.Point(173, 104);
            this.D1AllNoNormButton.Name = "D1AllNoNormButton";
            this.D1AllNoNormButton.Size = new System.Drawing.Size(75, 23);
            this.D1AllNoNormButton.TabIndex = 11;
            this.D1AllNoNormButton.Text = "All NoNorm";
            this.D1AllNoNormButton.UseVisualStyleBackColor = true;
            this.D1AllNoNormButton.Click += new System.EventHandler(this.D1AllNoNormButton_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 52);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "R";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "T";
            // 
            // D1InputSelector
            // 
            this.D1InputSelector.FormattingEnabled = true;
            this.D1InputSelector.Items.AddRange(new object[] {
            "wdbc.data",
            "wpbc.data"});
            this.D1InputSelector.Location = new System.Drawing.Point(12, 15);
            this.D1InputSelector.Name = "D1InputSelector";
            this.D1InputSelector.Size = new System.Drawing.Size(248, 21);
            this.D1InputSelector.TabIndex = 15;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.label18);
            this.panel3.Controls.Add(this.D1PartitionsBox);
            this.panel3.Controls.Add(this.label17);
            this.panel3.Controls.Add(this.D1CrossValidationItersBox);
            this.panel3.Controls.Add(this.D1CrossValidation);
            this.panel3.Controls.Add(this.D1Run);
            this.panel3.Controls.Add(this.ThresholdBar);
            this.panel3.Controls.Add(this.ThresholdLabel);
            this.panel3.Location = new System.Drawing.Point(456, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(128, 170);
            this.panel3.TabIndex = 14;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(15, 86);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 7;
            this.label18.Text = "Partitions";
            // 
            // D1PartitionsBox
            // 
            this.D1PartitionsBox.Location = new System.Drawing.Point(88, 84);
            this.D1PartitionsBox.Name = "D1PartitionsBox";
            this.D1PartitionsBox.Size = new System.Drawing.Size(37, 20);
            this.D1PartitionsBox.TabIndex = 6;
            this.D1PartitionsBox.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 111);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "CV Iterations";
            // 
            // D1CrossValidationItersBox
            // 
            this.D1CrossValidationItersBox.Location = new System.Drawing.Point(88, 109);
            this.D1CrossValidationItersBox.Name = "D1CrossValidationItersBox";
            this.D1CrossValidationItersBox.Size = new System.Drawing.Size(37, 20);
            this.D1CrossValidationItersBox.TabIndex = 4;
            this.D1CrossValidationItersBox.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // D1CrossValidation
            // 
            this.D1CrossValidation.AutoSize = true;
            this.D1CrossValidation.Location = new System.Drawing.Point(8, 66);
            this.D1CrossValidation.Name = "D1CrossValidation";
            this.D1CrossValidation.Size = new System.Drawing.Size(101, 17);
            this.D1CrossValidation.TabIndex = 3;
            this.D1CrossValidation.Text = "Cross Validation";
            this.D1CrossValidation.UseVisualStyleBackColor = true;
            // 
            // D1Run
            // 
            this.D1Run.Location = new System.Drawing.Point(7, 147);
            this.D1Run.Name = "D1Run";
            this.D1Run.Size = new System.Drawing.Size(75, 23);
            this.D1Run.TabIndex = 2;
            this.D1Run.Text = "Run";
            this.D1Run.UseVisualStyleBackColor = true;
            this.D1Run.Click += new System.EventHandler(this.D1Run_Click);
            // 
            // ThresholdBar
            // 
            this.ThresholdBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ThresholdBar.Location = new System.Drawing.Point(7, 26);
            this.ThresholdBar.Name = "ThresholdBar";
            this.ThresholdBar.Size = new System.Drawing.Size(118, 45);
            this.ThresholdBar.TabIndex = 1;
            this.ThresholdBar.Value = 5;
            // 
            // ThresholdLabel
            // 
            this.ThresholdLabel.AutoSize = true;
            this.ThresholdLabel.Location = new System.Drawing.Point(4, 4);
            this.ThresholdLabel.Name = "ThresholdLabel";
            this.ThresholdLabel.Size = new System.Drawing.Size(54, 13);
            this.ThresholdLabel.TabIndex = 0;
            this.ThresholdLabel.Text = "Threshold";
            // 
            // D2RunButton
            // 
            this.D2RunButton.Location = new System.Drawing.Point(7, 144);
            this.D2RunButton.Name = "D2RunButton";
            this.D2RunButton.Size = new System.Drawing.Size(75, 23);
            this.D2RunButton.TabIndex = 10;
            this.D2RunButton.Text = "Run";
            this.D2RunButton.UseVisualStyleBackColor = true;
            this.D2RunButton.Click += new System.EventHandler(this.D2RunButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.D2GdSettings);
            this.panel1.Controls.Add(this.D2LsSettings);
            this.panel1.Controls.Add(this.D2GD_Selector);
            this.panel1.Controls.Add(this.D2LSR_Selector);
            this.panel1.Location = new System.Drawing.Point(263, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(178, 170);
            this.panel1.TabIndex = 13;
            // 
            // D2GdSettings
            // 
            this.D2GdSettings.Controls.Add(this.label16);
            this.D2GdSettings.Controls.Add(this.D2BatchSize);
            this.D2GdSettings.Controls.Add(this.label9);
            this.D2GdSettings.Controls.Add(this.D2GDMaxEpochs);
            this.D2GdSettings.Controls.Add(this.label8);
            this.D2GdSettings.Controls.Add(this.D2GDLearningRate);
            this.D2GdSettings.Enabled = false;
            this.D2GdSettings.Location = new System.Drawing.Point(3, 70);
            this.D2GdSettings.Name = "D2GdSettings";
            this.D2GdSettings.Size = new System.Drawing.Size(171, 87);
            this.D2GdSettings.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(58, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "Batch Size";
            // 
            // D2BatchSize
            // 
            this.D2BatchSize.Location = new System.Drawing.Point(76, 55);
            this.D2BatchSize.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.D2BatchSize.Name = "D2BatchSize";
            this.D2BatchSize.Size = new System.Drawing.Size(95, 20);
            this.D2BatchSize.TabIndex = 6;
            this.D2BatchSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Max Epochs";
            // 
            // D2GDMaxEpochs
            // 
            this.D2GDMaxEpochs.Location = new System.Drawing.Point(76, 29);
            this.D2GDMaxEpochs.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.D2GDMaxEpochs.Name = "D2GDMaxEpochs";
            this.D2GDMaxEpochs.Size = new System.Drawing.Size(95, 20);
            this.D2GDMaxEpochs.TabIndex = 2;
            this.D2GDMaxEpochs.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(69, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Learning rate";
            // 
            // D2GDLearningRate
            // 
            this.D2GDLearningRate.DecimalPlaces = 5;
            this.D2GDLearningRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.D2GDLearningRate.Location = new System.Drawing.Point(76, 3);
            this.D2GDLearningRate.Name = "D2GDLearningRate";
            this.D2GDLearningRate.Size = new System.Drawing.Size(92, 20);
            this.D2GDLearningRate.TabIndex = 0;
            this.D2GDLearningRate.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // D2LsSettings
            // 
            this.D2LsSettings.Location = new System.Drawing.Point(3, 26);
            this.D2LsSettings.Name = "D2LsSettings";
            this.D2LsSettings.Size = new System.Drawing.Size(171, 15);
            this.D2LsSettings.TabIndex = 1;
            // 
            // D2GD_Selector
            // 
            this.D2GD_Selector.AutoSize = true;
            this.D2GD_Selector.Location = new System.Drawing.Point(3, 47);
            this.D2GD_Selector.Name = "D2GD_Selector";
            this.D2GD_Selector.Size = new System.Drawing.Size(92, 17);
            this.D2GD_Selector.TabIndex = 2;
            this.D2GD_Selector.TabStop = true;
            this.D2GD_Selector.Text = "GD Regressor";
            this.D2GD_Selector.UseVisualStyleBackColor = true;
            this.D2GD_Selector.CheckedChanged += new System.EventHandler(this.D2GD_Selector_CheckedChanged);
            // 
            // D2LSR_Selector
            // 
            this.D2LSR_Selector.AutoSize = true;
            this.D2LSR_Selector.Location = new System.Drawing.Point(3, 3);
            this.D2LSR_Selector.Name = "D2LSR_Selector";
            this.D2LSR_Selector.Size = new System.Drawing.Size(89, 17);
            this.D2LSR_Selector.TabIndex = 0;
            this.D2LSR_Selector.TabStop = true;
            this.D2LSR_Selector.Text = "LS Regressor";
            this.D2LSR_Selector.UseVisualStyleBackColor = true;
            this.D2LSR_Selector.CheckedChanged += new System.EventHandler(this.D2LSR_Selector_CheckedChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.Location = new System.Drawing.Point(3, 217);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.D2OutputsView);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(574, 221);
            this.splitContainer2.SplitterDistance = 195;
            this.splitContainer2.TabIndex = 12;
            // 
            // D2OutputsView
            // 
            this.D2OutputsView.AllowUserToAddRows = false;
            this.D2OutputsView.AllowUserToDeleteRows = false;
            this.D2OutputsView.AllowUserToResizeColumns = false;
            this.D2OutputsView.AllowUserToResizeRows = false;
            this.D2OutputsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.D2OutputsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.D2OutputsView.Location = new System.Drawing.Point(0, 0);
            this.D2OutputsView.MultiSelect = false;
            this.D2OutputsView.Name = "D2OutputsView";
            this.D2OutputsView.ReadOnly = true;
            this.D2OutputsView.RowHeadersVisible = false;
            this.D2OutputsView.Size = new System.Drawing.Size(195, 221);
            this.D2OutputsView.TabIndex = 11;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(375, 221);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.D2Logs);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(367, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logs";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // D2Logs
            // 
            this.D2Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.D2Logs.Location = new System.Drawing.Point(3, 3);
            this.D2Logs.Multiline = true;
            this.D2Logs.Name = "D2Logs";
            this.D2Logs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.D2Logs.Size = new System.Drawing.Size(361, 189);
            this.D2Logs.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.D2ShowPredicted);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.D2PetalLSepalLPlot);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(367, 195);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plot";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // D2ShowPredicted
            // 
            this.D2ShowPredicted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.D2ShowPredicted.AutoSize = true;
            this.D2ShowPredicted.Location = new System.Drawing.Point(250, 54);
            this.D2ShowPredicted.Name = "D2ShowPredicted";
            this.D2ShowPredicted.Size = new System.Drawing.Size(101, 17);
            this.D2ShowPredicted.TabIndex = 2;
            this.D2ShowPredicted.Text = "Show Predicted";
            this.D2ShowPredicted.UseVisualStyleBackColor = true;
            this.D2ShowPredicted.CheckedChanged += new System.EventHandler(this.D2ShowPredicted_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.Controls.Add(this.D2PlotYSelector);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.D2PlotXSelector);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(81, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(222, 31);
            this.panel2.TabIndex = 1;
            // 
            // D2PlotYSelector
            // 
            this.D2PlotYSelector.FormattingEnabled = true;
            this.D2PlotYSelector.Items.AddRange(new object[] {
            "PetalLength",
            "PetalWidth",
            "SepalLength",
            "SepalWidth"});
            this.D2PlotYSelector.Location = new System.Drawing.Point(130, 3);
            this.D2PlotYSelector.Name = "D2PlotYSelector";
            this.D2PlotYSelector.Size = new System.Drawing.Size(78, 21);
            this.D2PlotYSelector.TabIndex = 3;
            this.D2PlotYSelector.SelectedIndexChanged += new System.EventHandler(this.D2PlotYSelector_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(110, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Y";
            // 
            // D2PlotXSelector
            // 
            this.D2PlotXSelector.FormattingEnabled = true;
            this.D2PlotXSelector.Items.AddRange(new object[] {
            "PetalLength",
            "PetalWidth",
            "SepalLength",
            "SepalWidth"});
            this.D2PlotXSelector.Location = new System.Drawing.Point(23, 3);
            this.D2PlotXSelector.Name = "D2PlotXSelector";
            this.D2PlotXSelector.Size = new System.Drawing.Size(78, 21);
            this.D2PlotXSelector.TabIndex = 1;
            this.D2PlotXSelector.SelectedIndexChanged += new System.EventHandler(this.D2PlotXSelector_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "X";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.D2AllZButton);
            this.groupBox1.Controls.Add(this.D2F0NormSelector);
            this.groupBox1.Controls.Add(this.D2AllMinMaxButton);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.D2F3NormSelector);
            this.groupBox1.Controls.Add(this.D2AllNoNormButton);
            this.groupBox1.Controls.Add(this.D2OutNormSelector);
            this.groupBox1.Controls.Add(this.D2F2NormSelector);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.D2F1NormSelector);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(3, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 199);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Normalizations";
            // 
            // D2AllZButton
            // 
            this.D2AllZButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.D2AllZButton.Location = new System.Drawing.Point(11, 159);
            this.D2AllZButton.Name = "D2AllZButton";
            this.D2AllZButton.Size = new System.Drawing.Size(75, 23);
            this.D2AllZButton.TabIndex = 13;
            this.D2AllZButton.Text = "All ZNorm";
            this.D2AllZButton.UseVisualStyleBackColor = true;
            this.D2AllZButton.Click += new System.EventHandler(this.D2AllZButton_Click);
            // 
            // D2AllMinMaxButton
            // 
            this.D2AllMinMaxButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.D2AllMinMaxButton.Location = new System.Drawing.Point(92, 159);
            this.D2AllMinMaxButton.Name = "D2AllMinMaxButton";
            this.D2AllMinMaxButton.Size = new System.Drawing.Size(75, 23);
            this.D2AllMinMaxButton.TabIndex = 12;
            this.D2AllMinMaxButton.Text = "All MinMax";
            this.D2AllMinMaxButton.UseVisualStyleBackColor = true;
            this.D2AllMinMaxButton.Click += new System.EventHandler(this.D2AllMinMaxButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Output";
            // 
            // D2AllNoNormButton
            // 
            this.D2AllNoNormButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.D2AllNoNormButton.Location = new System.Drawing.Point(173, 159);
            this.D2AllNoNormButton.Name = "D2AllNoNormButton";
            this.D2AllNoNormButton.Size = new System.Drawing.Size(75, 23);
            this.D2AllNoNormButton.TabIndex = 11;
            this.D2AllNoNormButton.Text = "All NoNorm";
            this.D2AllNoNormButton.UseVisualStyleBackColor = true;
            this.D2AllNoNormButton.Click += new System.EventHandler(this.D2AllNoNormButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "SepalWidth";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "PetalWidth";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SepalLength";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "PetalLength";
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.D2ConfMatrixLabel);
            this.tabPage7.Controls.Add(this.label25);
            this.tabPage7.Controls.Add(this.D2AccuracyLabel);
            this.tabPage7.Controls.Add(this.label27);
            this.tabPage7.Controls.Add(this.D2WeightsLabel);
            this.tabPage7.Controls.Add(this.label29);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(367, 195);
            this.tabPage7.TabIndex = 2;
            this.tabPage7.Text = "Stats";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // D2ConfMatrixLabel
            // 
            this.D2ConfMatrixLabel.AutoSize = true;
            this.D2ConfMatrixLabel.Location = new System.Drawing.Point(98, 81);
            this.D2ConfMatrixLabel.Name = "D2ConfMatrixLabel";
            this.D2ConfMatrixLabel.Size = new System.Drawing.Size(10, 13);
            this.D2ConfMatrixLabel.TabIndex = 11;
            this.D2ConfMatrixLabel.Text = "-";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(6, 82);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(85, 13);
            this.label25.TabIndex = 10;
            this.label25.Text = "Confusion Matrix";
            // 
            // D2AccuracyLabel
            // 
            this.D2AccuracyLabel.AutoSize = true;
            this.D2AccuracyLabel.Location = new System.Drawing.Point(65, 52);
            this.D2AccuracyLabel.Name = "D2AccuracyLabel";
            this.D2AccuracyLabel.Size = new System.Drawing.Size(10, 13);
            this.D2AccuracyLabel.TabIndex = 9;
            this.D2AccuracyLabel.Text = "-";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 52);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(52, 13);
            this.label27.TabIndex = 8;
            this.label27.Text = "Accuracy";
            // 
            // D2WeightsLabel
            // 
            this.D2WeightsLabel.AutoSize = true;
            this.D2WeightsLabel.Location = new System.Drawing.Point(65, 3);
            this.D2WeightsLabel.Name = "D2WeightsLabel";
            this.D2WeightsLabel.Size = new System.Drawing.Size(10, 13);
            this.D2WeightsLabel.TabIndex = 7;
            this.D2WeightsLabel.Text = "-";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(6, 3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(46, 13);
            this.label29.TabIndex = 6;
            this.label29.Text = "Weights";
            // 
            // D1PlotSpace
            // 
            this.D1PlotSpace.BackColor = System.Drawing.Color.White;
            this.D1PlotSpace.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.D1PlotSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.D1PlotSpace.Location = new System.Drawing.Point(3, 3);
            this.D1PlotSpace.Name = "D1PlotSpace";
            this.D1PlotSpace.Size = new System.Drawing.Size(367, 218);
            this.D1PlotSpace.TabIndex = 1;
            this.D1PlotSpace.ViewPoint = new System.Drawing.Point(0, 0);
            this.D1PlotSpace.Zoom = 100;
            // 
            // D1LossPlotter
            // 
            this.D1LossPlotter.BackColor = System.Drawing.Color.White;
            this.D1LossPlotter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.D1LossPlotter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.D1LossPlotter.Location = new System.Drawing.Point(3, 3);
            this.D1LossPlotter.Name = "D1LossPlotter";
            this.D1LossPlotter.Size = new System.Drawing.Size(367, 218);
            this.D1LossPlotter.TabIndex = 0;
            this.D1LossPlotter.ViewPoint = new System.Drawing.Point(0, 0);
            this.D1LossPlotter.Zoom = 100;
            // 
            // D1OutNormSelector
            // 
            this.D1OutNormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D1OutNormSelector.AutoSize = true;
            this.D1OutNormSelector.Location = new System.Drawing.Point(96, 76);
            this.D1OutNormSelector.Name = "D1OutNormSelector";
            this.D1OutNormSelector.Size = new System.Drawing.Size(152, 22);
            this.D1OutNormSelector.TabIndex = 14;
            this.D1OutNormSelector.Value = typeof(AI.Lab8.Normalization._NoNorm);
            // 
            // D1F0NormSelector
            // 
            this.D1F0NormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D1F0NormSelector.AutoSize = true;
            this.D1F0NormSelector.Location = new System.Drawing.Point(96, 19);
            this.D1F0NormSelector.Name = "D1F0NormSelector";
            this.D1F0NormSelector.Size = new System.Drawing.Size(152, 22);
            this.D1F0NormSelector.TabIndex = 0;
            this.D1F0NormSelector.Value = typeof(AI.Lab8.Normalization._NoNorm);
            // 
            // D1F1NormSelector
            // 
            this.D1F1NormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D1F1NormSelector.AutoSize = true;
            this.D1F1NormSelector.Location = new System.Drawing.Point(96, 47);
            this.D1F1NormSelector.Name = "D1F1NormSelector";
            this.D1F1NormSelector.Size = new System.Drawing.Size(152, 22);
            this.D1F1NormSelector.TabIndex = 2;
            this.D1F1NormSelector.Value = typeof(AI.Lab8.Normalization._NoNorm);
            // 
            // D2PetalLSepalLPlot
            // 
            this.D2PetalLSepalLPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D2PetalLSepalLPlot.BackColor = System.Drawing.Color.White;
            this.D2PetalLSepalLPlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.D2PetalLSepalLPlot.Location = new System.Drawing.Point(3, 43);
            this.D2PetalLSepalLPlot.Name = "D2PetalLSepalLPlot";
            this.D2PetalLSepalLPlot.Size = new System.Drawing.Size(358, 146);
            this.D2PetalLSepalLPlot.TabIndex = 0;
            this.D2PetalLSepalLPlot.ViewPoint = new System.Drawing.Point(0, 0);
            this.D2PetalLSepalLPlot.Zoom = 100;
            // 
            // D2F0NormSelector
            // 
            this.D2F0NormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D2F0NormSelector.AutoSize = true;
            this.D2F0NormSelector.Location = new System.Drawing.Point(96, 19);
            this.D2F0NormSelector.Name = "D2F0NormSelector";
            this.D2F0NormSelector.Size = new System.Drawing.Size(152, 22);
            this.D2F0NormSelector.TabIndex = 0;
            this.D2F0NormSelector.Value = typeof(AI.Lab8.Normalization._NoNorm);
            // 
            // D2F3NormSelector
            // 
            this.D2F3NormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D2F3NormSelector.AutoSize = true;
            this.D2F3NormSelector.Location = new System.Drawing.Point(96, 103);
            this.D2F3NormSelector.Name = "D2F3NormSelector";
            this.D2F3NormSelector.Size = new System.Drawing.Size(152, 22);
            this.D2F3NormSelector.TabIndex = 4;
            this.D2F3NormSelector.Value = typeof(AI.Lab8.Normalization._NoNorm);
            // 
            // D2OutNormSelector
            // 
            this.D2OutNormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D2OutNormSelector.AutoSize = true;
            this.D2OutNormSelector.Location = new System.Drawing.Point(96, 131);
            this.D2OutNormSelector.Name = "D2OutNormSelector";
            this.D2OutNormSelector.Size = new System.Drawing.Size(152, 22);
            this.D2OutNormSelector.TabIndex = 8;
            this.D2OutNormSelector.Value = typeof(AI.Lab8.Normalization._NoNorm);
            // 
            // D2F2NormSelector
            // 
            this.D2F2NormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D2F2NormSelector.AutoSize = true;
            this.D2F2NormSelector.Location = new System.Drawing.Point(96, 75);
            this.D2F2NormSelector.Name = "D2F2NormSelector";
            this.D2F2NormSelector.Size = new System.Drawing.Size(152, 22);
            this.D2F2NormSelector.TabIndex = 3;
            this.D2F2NormSelector.Value = typeof(AI.Lab8.Normalization._NoNorm);
            // 
            // D2F1NormSelector
            // 
            this.D2F1NormSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.D2F1NormSelector.AutoSize = true;
            this.D2F1NormSelector.Location = new System.Drawing.Point(96, 47);
            this.D2F1NormSelector.Name = "D2F1NormSelector";
            this.D2F1NormSelector.Size = new System.Drawing.Size(152, 22);
            this.D2F1NormSelector.TabIndex = 2;
            this.D2F1NormSelector.Value = typeof(AI.Lab8.Normalization._NoNorm);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.label24);
            this.panel5.Controls.Add(this.D2RunButton);
            this.panel5.Controls.Add(this.D2Partitions);
            this.panel5.Controls.Add(this.label26);
            this.panel5.Controls.Add(this.D2CVIterations);
            this.panel5.Controls.Add(this.D2CrossValidation);
            this.panel5.Location = new System.Drawing.Point(449, 15);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(124, 170);
            this.panel5.TabIndex = 15;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(14, 64);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(50, 13);
            this.label24.TabIndex = 7;
            this.label24.Text = "Partitions";
            // 
            // D2Partitions
            // 
            this.D2Partitions.Location = new System.Drawing.Point(83, 62);
            this.D2Partitions.Name = "D2Partitions";
            this.D2Partitions.Size = new System.Drawing.Size(37, 20);
            this.D2Partitions.TabIndex = 6;
            this.D2Partitions.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(14, 89);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 13);
            this.label26.TabIndex = 5;
            this.label26.Text = "CV Iterations";
            // 
            // D2CVIterations
            // 
            this.D2CVIterations.Location = new System.Drawing.Point(83, 87);
            this.D2CVIterations.Name = "D2CVIterations";
            this.D2CVIterations.Size = new System.Drawing.Size(37, 20);
            this.D2CVIterations.TabIndex = 4;
            this.D2CVIterations.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // D2CrossValidation
            // 
            this.D2CrossValidation.AutoSize = true;
            this.D2CrossValidation.Location = new System.Drawing.Point(7, 44);
            this.D2CrossValidation.Name = "D2CrossValidation";
            this.D2CrossValidation.Size = new System.Drawing.Size(101, 17);
            this.D2CrossValidation.TabIndex = 3;
            this.D2CrossValidation.Text = "Cross Validation";
            this.D2CrossValidation.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.D1GdSettings.ResumeLayout(false);
            this.D1GdSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D1BatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D1GDMaxEpochs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D1GDLearningRate)).EndInit();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.D1OutputsView)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D1PartitionsBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D1CrossValidationItersBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThresholdBar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.D2GdSettings.ResumeLayout(false);
            this.D2GdSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D2BatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D2GDMaxEpochs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D2GDLearningRate)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.D2OutputsView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.D2Partitions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.D2CVIterations)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.NormalizationMethodSelector D2F0NormSelector;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private Controls.NormalizationMethodSelector D2F3NormSelector;
        private Controls.NormalizationMethodSelector D2OutNormSelector;
        private Controls.NormalizationMethodSelector D2F2NormSelector;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Controls.NormalizationMethodSelector D2F1NormSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button D2RunButton;
        private System.Windows.Forms.DataGridView D2OutputsView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox D2Logs;
        private System.Windows.Forms.Button D2AllNoNormButton;
        private System.Windows.Forms.Button D2AllMinMaxButton;
        private System.Windows.Forms.Button D2AllZButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel D2LsSettings;
        private System.Windows.Forms.RadioButton D2LSR_Selector;
        private System.Windows.Forms.Panel D2GdSettings;
        private System.Windows.Forms.RadioButton D2GD_Selector;
        private Controls.CartesianPlotter D2PetalLSepalLPlot;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox D2PlotYSelector;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox D2PlotXSelector;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox D2ShowPredicted;
        private System.Windows.Forms.NumericUpDown D2GDLearningRate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown D2GDMaxEpochs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label ThresholdLabel;
        private System.Windows.Forms.TrackBar ThresholdBar;
        private System.Windows.Forms.ComboBox D1InputSelector;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private Controls.NormalizationMethodSelector D1OutNormSelector;
        private System.Windows.Forms.Button D1AllZButton;
        private Controls.NormalizationMethodSelector D1F0NormSelector;
        private System.Windows.Forms.Button D1AllMinMaxButton;
        private System.Windows.Forms.Button D1AllNoNormButton;
        private System.Windows.Forms.Label label11;
        private Controls.NormalizationMethodSelector D1F1NormSelector;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView D1OutputsView;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel D1GdSettings;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown D1GDMaxEpochs;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown D1GDLearningRate;
        private System.Windows.Forms.RadioButton D1GD_Selector;
        private System.Windows.Forms.RadioButton D1LSR_Selector;
        private System.Windows.Forms.TextBox D1Logs;
        private System.Windows.Forms.Button D1Run;
        private Controls.CartesianPlotter D1PlotSpace;
        private System.Windows.Forms.CheckBox D1ShowPredicted;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown D1BatchSize;
        private System.Windows.Forms.Panel D1LsSettings;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown D2BatchSize;
        private System.Windows.Forms.CheckBox D1CrossValidation;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown D1CrossValidationItersBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown D1PartitionsBox;
        private System.Windows.Forms.TabPage tabPage5;
        private Controls.CartesianPlotter D1LossPlotter;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label D1WeightsLabel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label D1AccuracyLabel;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label D1ConfMatrix;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Label D2ConfMatrixLabel;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label D2AccuracyLabel;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label D2WeightsLabel;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown D2Partitions;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown D2CVIterations;
        private System.Windows.Forms.CheckBox D2CrossValidation;
    }
}

