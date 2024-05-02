namespace AI.Lab4Opt
{
    partial class DynamicGraphForm
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
            this.AllNodesCheckbox = new System.Windows.Forms.CheckBox();
            this.NodesCountLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GraphSelector = new System.Windows.Forms.ComboBox();
            this.NextGraphStateButton = new System.Windows.Forms.Button();
            this.RhoBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.BetaBox = new System.Windows.Forms.NumericUpDown();
            this.AlphaBox = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ResetButton = new System.Windows.Forms.Button();
            this.StartStopButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.EdgesView = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SolutionsView = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.StartNodeBox = new System.Windows.Forms.NumericUpDown();
            this.EndNodeBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RhoBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BetaBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EdgesView)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SolutionsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartNodeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndNodeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EndNodeBox);
            this.groupBox1.Controls.Add(this.StartNodeBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.AllNodesCheckbox);
            this.groupBox1.Controls.Add(this.NodesCountLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.GraphSelector);
            this.groupBox1.Controls.Add(this.NextGraphStateButton);
            this.groupBox1.Controls.Add(this.RhoBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.BetaBox);
            this.groupBox1.Controls.Add(this.AlphaBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ResetButton);
            this.groupBox1.Controls.Add(this.StartStopButton);
            this.groupBox1.Location = new System.Drawing.Point(397, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 280);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control Panel";
            // 
            // AllNodesCheckbox
            // 
            this.AllNodesCheckbox.AutoSize = true;
            this.AllNodesCheckbox.Location = new System.Drawing.Point(13, 199);
            this.AllNodesCheckbox.Name = "AllNodesCheckbox";
            this.AllNodesCheckbox.Size = new System.Drawing.Size(68, 17);
            this.AllNodesCheckbox.TabIndex = 10;
            this.AllNodesCheckbox.Text = "AllNodes";
            this.AllNodesCheckbox.UseVisualStyleBackColor = true;
            // 
            // NodesCountLabel
            // 
            this.NodesCountLabel.AutoSize = true;
            this.NodesCountLabel.Location = new System.Drawing.Point(70, 164);
            this.NodesCountLabel.Name = "NodesCountLabel";
            this.NodesCountLabel.Size = new System.Drawing.Size(13, 13);
            this.NodesCountLabel.TabIndex = 11;
            this.NodesCountLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "NodesCount";
            // 
            // GraphSelector
            // 
            this.GraphSelector.FormattingEnabled = true;
            this.GraphSelector.Items.AddRange(new object[] {
            "aves_sparrow_social",
            "insecta_ant_trophallaxis_colony2",
            "mammalia_raccoon_proximity",
            "reptilia_tortoise_network_hw"});
            this.GraphSelector.Location = new System.Drawing.Point(10, 19);
            this.GraphSelector.Name = "GraphSelector";
            this.GraphSelector.Size = new System.Drawing.Size(207, 21);
            this.GraphSelector.TabIndex = 9;
            this.GraphSelector.SelectedIndexChanged += new System.EventHandler(this.GraphSelector_SelectedIndexChanged);
            // 
            // NextGraphStateButton
            // 
            this.NextGraphStateButton.Location = new System.Drawing.Point(6, 251);
            this.NextGraphStateButton.Name = "NextGraphStateButton";
            this.NextGraphStateButton.Size = new System.Drawing.Size(116, 23);
            this.NextGraphStateButton.TabIndex = 8;
            this.NextGraphStateButton.Text = "Next Graph State";
            this.NextGraphStateButton.UseVisualStyleBackColor = true;
            this.NextGraphStateButton.Click += new System.EventHandler(this.NextGraphStateButton_Click);
            // 
            // RhoBox
            // 
            this.RhoBox.DecimalPlaces = 2;
            this.RhoBox.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.RhoBox.Location = new System.Drawing.Point(26, 118);
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
            this.label3.Location = new System.Drawing.Point(7, 120);
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
            this.BetaBox.Location = new System.Drawing.Point(120, 68);
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
            this.AlphaBox.Location = new System.Drawing.Point(26, 68);
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
            this.label2.Location = new System.Drawing.Point(101, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "β";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "α";
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(87, 222);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 1;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // StartStopButton
            // 
            this.StartStopButton.Location = new System.Drawing.Point(6, 222);
            this.StartStopButton.Name = "StartStopButton";
            this.StartStopButton.Size = new System.Drawing.Size(75, 23);
            this.StartStopButton.TabIndex = 0;
            this.StartStopButton.Text = "Start";
            this.StartStopButton.UseVisualStyleBackColor = true;
            this.StartStopButton.Click += new System.EventHandler(this.StartStopButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EdgesView);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 165);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Edges";
            // 
            // EdgesView
            // 
            this.EdgesView.AllowUserToAddRows = false;
            this.EdgesView.AllowUserToDeleteRows = false;
            this.EdgesView.AllowUserToResizeRows = false;
            this.EdgesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EdgesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EdgesView.Location = new System.Drawing.Point(3, 16);
            this.EdgesView.Name = "EdgesView";
            this.EdgesView.Size = new System.Drawing.Size(343, 146);
            this.EdgesView.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SolutionsView);
            this.groupBox3.Location = new System.Drawing.Point(12, 220);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 165);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Solutions";
            // 
            // SolutionsView
            // 
            this.SolutionsView.AllowUserToAddRows = false;
            this.SolutionsView.AllowUserToDeleteRows = false;
            this.SolutionsView.AllowUserToResizeRows = false;
            this.SolutionsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SolutionsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SolutionsView.Location = new System.Drawing.Point(3, 16);
            this.SolutionsView.Name = "SolutionsView";
            this.SolutionsView.Size = new System.Drawing.Size(373, 146);
            this.SolutionsView.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(130, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Start Node";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(133, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "FinishNode";
            // 
            // StartNodeBox
            // 
            this.StartNodeBox.Location = new System.Drawing.Point(199, 118);
            this.StartNodeBox.Name = "StartNodeBox";
            this.StartNodeBox.Size = new System.Drawing.Size(120, 20);
            this.StartNodeBox.TabIndex = 14;
            // 
            // EndNodeBox
            // 
            this.EndNodeBox.Location = new System.Drawing.Point(199, 149);
            this.EndNodeBox.Name = "EndNodeBox";
            this.EndNodeBox.Size = new System.Drawing.Size(120, 20);
            this.EndNodeBox.TabIndex = 15;
            // 
            // DynamicGraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DynamicGraphForm";
            this.Text = "DynamicGraphForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RhoBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BetaBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AlphaBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EdgesView)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SolutionsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartNodeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndNodeBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown RhoBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown BetaBox;
        private System.Windows.Forms.NumericUpDown AlphaBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button StartStopButton;
        private System.Windows.Forms.Button NextGraphStateButton;
        private System.Windows.Forms.ComboBox GraphSelector;
        private System.Windows.Forms.Label NodesCountLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView EdgesView;
        private System.Windows.Forms.DataGridView SolutionsView;
        private System.Windows.Forms.CheckBox AllNodesCheckbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown EndNodeBox;
        private System.Windows.Forms.NumericUpDown StartNodeBox;
    }
}