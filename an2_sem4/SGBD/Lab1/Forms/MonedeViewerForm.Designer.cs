namespace SGBD.DepozitColectie.Forms
{
    partial class MonedeViewerForm
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
            this.TipMonedeView = new System.Windows.Forms.DataGridView();
            this.Body = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.StareConversareSelector = new System.Windows.Forms.TrackBar();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.SelectedTipMonedaLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ExemplareMonedeView = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CladireSelector = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CameraBox = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.RaionBox = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.RaftBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.TipMonedeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).BeginInit();
            this.Body.Panel1.SuspendLayout();
            this.Body.Panel2.SuspendLayout();
            this.Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StareConversareSelector)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExemplareMonedeView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RaionBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RaftBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TipMonedeView
            // 
            this.TipMonedeView.AllowUserToAddRows = false;
            this.TipMonedeView.AllowUserToDeleteRows = false;
            this.TipMonedeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TipMonedeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TipMonedeView.Location = new System.Drawing.Point(0, 0);
            this.TipMonedeView.MultiSelect = false;
            this.TipMonedeView.Name = "TipMonedeView";
            this.TipMonedeView.Size = new System.Drawing.Size(389, 450);
            this.TipMonedeView.TabIndex = 0;
            this.TipMonedeView.SelectionChanged += new System.EventHandler(this.TipMonedeView_SelectionChanged);
            // 
            // Body
            // 
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 0);
            this.Body.Name = "Body";
            // 
            // Body.Panel1
            // 
            this.Body.Panel1.Controls.Add(this.TipMonedeView);
            // 
            // Body.Panel2
            // 
            this.Body.Panel2.Controls.Add(this.groupBox2);
            this.Body.Panel2.Controls.Add(this.label2);
            this.Body.Panel2.Controls.Add(this.StareConversareSelector);
            this.Body.Panel2.Controls.Add(this.DeleteButton);
            this.Body.Panel2.Controls.Add(this.UpdateButton);
            this.Body.Panel2.Controls.Add(this.AddButton);
            this.Body.Panel2.Controls.Add(this.SelectedTipMonedaLabel);
            this.Body.Panel2.Controls.Add(this.label1);
            this.Body.Panel2.Controls.Add(this.groupBox1);
            this.Body.Size = new System.Drawing.Size(800, 450);
            this.Body.SplitterDistance = 389;
            this.Body.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Stare conservare";
            // 
            // StareConversareSelector
            // 
            this.StareConversareSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StareConversareSelector.Location = new System.Drawing.Point(116, 84);
            this.StareConversareSelector.Minimum = 1;
            this.StareConversareSelector.Name = "StareConversareSelector";
            this.StareConversareSelector.Size = new System.Drawing.Size(277, 45);
            this.StareConversareSelector.TabIndex = 8;
            this.StareConversareSelector.Value = 1;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.ForeColor = System.Drawing.Color.DarkRed;
            this.DeleteButton.Location = new System.Drawing.Point(317, 240);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 7;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateButton.Location = new System.Drawing.Point(236, 240);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 6;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.Location = new System.Drawing.Point(155, 240);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 5;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // SelectedTipMonedaLabel
            // 
            this.SelectedTipMonedaLabel.AutoSize = true;
            this.SelectedTipMonedaLabel.Location = new System.Drawing.Point(95, 9);
            this.SelectedTipMonedaLabel.Name = "SelectedTipMonedaLabel";
            this.SelectedTipMonedaLabel.Size = new System.Drawing.Size(20, 13);
            this.SelectedTipMonedaLabel.TabIndex = 4;
            this.SelectedTipMonedaLabel.Text = "#0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Tip Moneda : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ExemplareMonedeView);
            this.groupBox1.Location = new System.Drawing.Point(0, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 181);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Exemplare";
            // 
            // ExemplareMonedeView
            // 
            this.ExemplareMonedeView.AllowUserToAddRows = false;
            this.ExemplareMonedeView.AllowUserToDeleteRows = false;
            this.ExemplareMonedeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ExemplareMonedeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExemplareMonedeView.Location = new System.Drawing.Point(3, 16);
            this.ExemplareMonedeView.MultiSelect = false;
            this.ExemplareMonedeView.Name = "ExemplareMonedeView";
            this.ExemplareMonedeView.Size = new System.Drawing.Size(396, 162);
            this.ExemplareMonedeView.TabIndex = 0;
            this.ExemplareMonedeView.SelectionChanged += new System.EventHandler(this.ExamplareMonedeView_SelectionChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.RaftBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.RaionBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.CameraBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.CladireSelector);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(25, 135);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(367, 99);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Depozit";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Cladire";
            // 
            // CladireSelector
            // 
            this.CladireSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CladireSelector.FormattingEnabled = true;
            this.CladireSelector.Location = new System.Drawing.Point(59, 21);
            this.CladireSelector.Name = "CladireSelector";
            this.CladireSelector.Size = new System.Drawing.Size(302, 21);
            this.CladireSelector.TabIndex = 1;
            this.CladireSelector.DropDown += new System.EventHandler(this.CladireSelector_DropDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Camera";
            // 
            // CameraBox
            // 
            this.CameraBox.Location = new System.Drawing.Point(59, 48);
            this.CameraBox.Name = "CameraBox";
            this.CameraBox.Size = new System.Drawing.Size(56, 20);
            this.CameraBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(121, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Raion";
            // 
            // RaionBox
            // 
            this.RaionBox.Location = new System.Drawing.Point(162, 48);
            this.RaionBox.Name = "RaionBox";
            this.RaionBox.Size = new System.Drawing.Size(56, 20);
            this.RaionBox.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Raft";
            // 
            // RaftBox
            // 
            this.RaftBox.Location = new System.Drawing.Point(257, 48);
            this.RaftBox.Name = "RaftBox";
            this.RaftBox.Size = new System.Drawing.Size(56, 20);
            this.RaftBox.TabIndex = 7;
            // 
            // MonedeViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Body);
            this.Name = "MonedeViewerForm";
            this.Text = "MonedeViewerForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MonedeViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TipMonedeView)).EndInit();
            this.Body.Panel1.ResumeLayout(false);
            this.Body.Panel2.ResumeLayout(false);
            this.Body.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).EndInit();
            this.Body.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StareConversareSelector)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExemplareMonedeView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RaionBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RaftBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TipMonedeView;
        private System.Windows.Forms.SplitContainer Body;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView ExemplareMonedeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SelectedTipMonedaLabel;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar StareConversareSelector;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CladireSelector;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown CameraBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown RaionBox;
        private System.Windows.Forms.NumericUpDown RaftBox;
    }
}