namespace SGBD.ExamenPractic
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
            this.ArticoleView = new System.Windows.Forms.DataGridView();
            this.TipuriArticoleView = new System.Windows.Forms.DataGridView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NrAutoriBox = new System.Windows.Forms.NumericUpDown();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.NrPaginiBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TitluBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AnPublicareBox = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ArticoleView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TipuriArticoleView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NrAutoriBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NrPaginiBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnPublicareBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ArticoleView
            // 
            this.ArticoleView.AllowUserToAddRows = false;
            this.ArticoleView.AllowUserToDeleteRows = false;
            this.ArticoleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ArticoleView.Dock = System.Windows.Forms.DockStyle.Top;
            this.ArticoleView.Location = new System.Drawing.Point(0, 0);
            this.ArticoleView.Name = "ArticoleView";
            this.ArticoleView.Size = new System.Drawing.Size(382, 233);
            this.ArticoleView.TabIndex = 1;
            this.ArticoleView.SelectionChanged += new System.EventHandler(this.ArticoleView_SelectionChanged);
            // 
            // TipuriArticoleView
            // 
            this.TipuriArticoleView.AllowUserToAddRows = false;
            this.TipuriArticoleView.AllowUserToDeleteRows = false;
            this.TipuriArticoleView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TipuriArticoleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TipuriArticoleView.Location = new System.Drawing.Point(0, 0);
            this.TipuriArticoleView.Name = "TipuriArticoleView";
            this.TipuriArticoleView.Size = new System.Drawing.Size(414, 450);
            this.TipuriArticoleView.TabIndex = 0;
            this.TipuriArticoleView.SelectionChanged += new System.EventHandler(this.TipuriArticoleView_SelectionChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TipuriArticoleView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.ArticoleView);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.AnPublicareBox);
            this.groupBox1.Controls.Add(this.NrAutoriBox);
            this.groupBox1.Controls.Add(this.DeleteButton);
            this.groupBox1.Controls.Add(this.UpdateButton);
            this.groupBox1.Controls.Add(this.AddButton);
            this.groupBox1.Controls.Add(this.NrPaginiBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TitluBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 208);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // NrAutoriBox
            // 
            this.NrAutoriBox.Location = new System.Drawing.Point(92, 52);
            this.NrAutoriBox.Name = "NrAutoriBox";
            this.NrAutoriBox.Size = new System.Drawing.Size(100, 20);
            this.NrAutoriBox.TabIndex = 9;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(202, 75);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 8;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(202, 49);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 7;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(202, 25);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 6;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // NrPaginiBox
            // 
            this.NrPaginiBox.Location = new System.Drawing.Point(92, 78);
            this.NrPaginiBox.Name = "NrPaginiBox";
            this.NrPaginiBox.Size = new System.Drawing.Size(100, 20);
            this.NrPaginiBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "NrPagini";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "NrAutori";
            // 
            // TitluBox
            // 
            this.TitluBox.Location = new System.Drawing.Point(92, 25);
            this.TitluBox.Name = "TitluBox";
            this.TitluBox.Size = new System.Drawing.Size(100, 20);
            this.TitluBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nume";
            // 
            // AnPublicareBox
            // 
            this.AnPublicareBox.Location = new System.Drawing.Point(92, 104);
            this.AnPublicareBox.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.AnPublicareBox.Minimum = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            this.AnPublicareBox.Name = "AnPublicareBox";
            this.AnPublicareBox.Size = new System.Drawing.Size(100, 20);
            this.AnPublicareBox.TabIndex = 10;
            this.AnPublicareBox.Value = new decimal(new int[] {
            1800,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "An Publicare";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ArticoleView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TipuriArticoleView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NrAutoriBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NrPaginiBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnPublicareBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView ArticoleView;
        private System.Windows.Forms.DataGridView TipuriArticoleView;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.NumericUpDown NrPaginiBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TitluBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown NrAutoriBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown AnPublicareBox;
    }
}

