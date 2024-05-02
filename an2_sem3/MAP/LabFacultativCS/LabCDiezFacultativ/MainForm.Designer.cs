namespace LabCDiezFacultativ
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
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.T1JucatoriView = new System.Windows.Forms.DataGridView();
            this.T1EchipaBox = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.T2Echipa1View = new System.Windows.Forms.DataGridView();
            this.T2Echipa2View = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.T2Echipa1Box = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.T2Echipa2Box = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.T3MeciuriView = new System.Windows.Forms.DataGridView();
            this.T3Timeline = new LabCDiezFacultativ.UI.Controls.Timeline();
            this.TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.T1JucatoriView)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.T2Echipa1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.T2Echipa2View)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.T3MeciuriView)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tabPage1);
            this.TabControl.Controls.Add(this.tabPage2);
            this.TabControl.Controls.Add(this.tabPage3);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(784, 411);
            this.TabControl.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.T1JucatoriView);
            this.tabPage1.Controls.Add(this.T1EchipaBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 385);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Echipe";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Echipa";
            // 
            // T1JucatoriView
            // 
            this.T1JucatoriView.AllowUserToAddRows = false;
            this.T1JucatoriView.AllowUserToDeleteRows = false;
            this.T1JucatoriView.AllowUserToResizeRows = false;
            this.T1JucatoriView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T1JucatoriView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.T1JucatoriView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.T1JucatoriView.Location = new System.Drawing.Point(6, 33);
            this.T1JucatoriView.Name = "T1JucatoriView";
            this.T1JucatoriView.ReadOnly = true;
            this.T1JucatoriView.Size = new System.Drawing.Size(762, 346);
            this.T1JucatoriView.TabIndex = 1;
            // 
            // T1EchipaBox
            // 
            this.T1EchipaBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T1EchipaBox.FormattingEnabled = true;
            this.T1EchipaBox.Location = new System.Drawing.Point(54, 6);
            this.T1EchipaBox.Name = "T1EchipaBox";
            this.T1EchipaBox.Size = new System.Drawing.Size(714, 21);
            this.T1EchipaBox.TabIndex = 0;
            this.T1EchipaBox.SelectedIndexChanged += new System.EventHandler(this.T1EchipaBox_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(829, 294);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Meciuri";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.ScoreLabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(352, 122);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(118, 100);
            this.panel1.TabIndex = 7;
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.Location = new System.Drawing.Point(3, 47);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(112, 23);
            this.ScoreLabel.TabIndex = 1;
            this.ScoreLabel.Text = "0-0";
            this.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Score";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(8, 63);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.T2Echipa1View);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.T2Echipa2View);
            this.splitContainer1.Size = new System.Drawing.Size(813, 223);
            this.splitContainer1.SplitterDistance = 403;
            this.splitContainer1.TabIndex = 8;
            // 
            // T2Echipa1View
            // 
            this.T2Echipa1View.AllowUserToAddRows = false;
            this.T2Echipa1View.AllowUserToDeleteRows = false;
            this.T2Echipa1View.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T2Echipa1View.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.T2Echipa1View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.T2Echipa1View.Location = new System.Drawing.Point(3, 3);
            this.T2Echipa1View.Name = "T2Echipa1View";
            this.T2Echipa1View.Size = new System.Drawing.Size(318, 217);
            this.T2Echipa1View.TabIndex = 6;
            // 
            // T2Echipa2View
            // 
            this.T2Echipa2View.AllowUserToAddRows = false;
            this.T2Echipa2View.AllowUserToDeleteRows = false;
            this.T2Echipa2View.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T2Echipa2View.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.T2Echipa2View.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.T2Echipa2View.Location = new System.Drawing.Point(80, 3);
            this.T2Echipa2View.Name = "T2Echipa2View";
            this.T2Echipa2View.Size = new System.Drawing.Size(325, 217);
            this.T2Echipa2View.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.T2Echipa1Box);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.T2Echipa2Box);
            this.groupBox1.Location = new System.Drawing.Point(205, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(421, 51);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vizualizare meci";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Echipa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(197, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "vs.";
            // 
            // T2Echipa1Box
            // 
            this.T2Echipa1Box.FormattingEnabled = true;
            this.T2Echipa1Box.Location = new System.Drawing.Point(47, 23);
            this.T2Echipa1Box.Name = "T2Echipa1Box";
            this.T2Echipa1Box.Size = new System.Drawing.Size(121, 21);
            this.T2Echipa1Box.TabIndex = 0;
            this.T2Echipa1Box.SelectedIndexChanged += new System.EventHandler(this.T2Echipa1Box_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Echipa";
            // 
            // T2Echipa2Box
            // 
            this.T2Echipa2Box.FormattingEnabled = true;
            this.T2Echipa2Box.Location = new System.Drawing.Point(290, 23);
            this.T2Echipa2Box.Name = "T2Echipa2Box";
            this.T2Echipa2Box.Size = new System.Drawing.Size(121, 21);
            this.T2Echipa2Box.TabIndex = 1;
            this.T2Echipa2Box.SelectedIndexChanged += new System.EventHandler(this.T2Echipa2Box_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.T3Timeline);
            this.tabPage3.Controls.Add(this.T3MeciuriView);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(829, 294);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Timeline";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // T3MeciuriView
            // 
            this.T3MeciuriView.AllowUserToAddRows = false;
            this.T3MeciuriView.AllowUserToDeleteRows = false;
            this.T3MeciuriView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T3MeciuriView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.T3MeciuriView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.T3MeciuriView.Location = new System.Drawing.Point(8, 63);
            this.T3MeciuriView.Name = "T3MeciuriView";
            this.T3MeciuriView.Size = new System.Drawing.Size(813, 223);
            this.T3MeciuriView.TabIndex = 0;
            // 
            // T3Timeline
            // 
            this.T3Timeline.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.T3Timeline.EndDate = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.T3Timeline.Location = new System.Drawing.Point(8, 4);
            this.T3Timeline.Name = "T3Timeline";
            this.T3Timeline.Size = new System.Drawing.Size(813, 35);
            this.T3Timeline.StartDate = new System.DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.T3Timeline.TabIndex = 1;
            this.T3Timeline.SelectedIntervalChanged += new LabCDiezFacultativ.UI.Controls.Timeline.OnSelectedIntervalChanged(this.T3Timeline_SelectedIntervalChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.TabControl);
            this.Name = "MainForm";
            this.Text = "NBA";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.T1JucatoriView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.T2Echipa1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.T2Echipa2View)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.T3MeciuriView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView T1JucatoriView;
        private System.Windows.Forms.ComboBox T1EchipaBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox T2Echipa1Box;
        private System.Windows.Forms.ComboBox T2Echipa2Box;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView T2Echipa2View;
        private System.Windows.Forms.DataGridView T2Echipa1View;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView T3MeciuriView;
        private UI.Controls.Timeline T3Timeline;
    }
}

