namespace SGBD.Lab2
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
            this.ChildrenView = new System.Windows.Forms.DataGridView();
            this.SelectedChildLabel = new System.Windows.Forms.Label();
            this.ChildrenLabel = new System.Windows.Forms.Label();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.ChildrenBox = new System.Windows.Forms.GroupBox();
            this.Body = new System.Windows.Forms.SplitContainer();
            this.ParentsView = new System.Windows.Forms.DataGridView();
            this.PropsPanel = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.monedeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ChildrenView)).BeginInit();
            this.ChildrenBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).BeginInit();
            this.Body.Panel1.SuspendLayout();
            this.Body.Panel2.SuspendLayout();
            this.Body.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ParentsView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChildrenView
            // 
            this.ChildrenView.AllowUserToAddRows = false;
            this.ChildrenView.AllowUserToDeleteRows = false;
            this.ChildrenView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChildrenView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChildrenView.Location = new System.Drawing.Point(3, 16);
            this.ChildrenView.MultiSelect = false;
            this.ChildrenView.Name = "ChildrenView";
            this.ChildrenView.Size = new System.Drawing.Size(350, 48);
            this.ChildrenView.TabIndex = 0;
            this.ChildrenView.SelectionChanged += new System.EventHandler(this.ChildrenView_SelectionChanged);
            // 
            // SelectedChildLabel
            // 
            this.SelectedChildLabel.AutoSize = true;
            this.SelectedChildLabel.Location = new System.Drawing.Point(95, 9);
            this.SelectedChildLabel.Name = "SelectedChildLabel";
            this.SelectedChildLabel.Size = new System.Drawing.Size(20, 13);
            this.SelectedChildLabel.TabIndex = 4;
            this.SelectedChildLabel.Text = "#0";
            // 
            // ChildrenLabel
            // 
            this.ChildrenLabel.AutoSize = true;
            this.ChildrenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChildrenLabel.Location = new System.Drawing.Point(3, 9);
            this.ChildrenLabel.Name = "ChildrenLabel";
            this.ChildrenLabel.Size = new System.Drawing.Size(43, 13);
            this.ChildrenLabel.TabIndex = 3;
            this.ChildrenLabel.Text = "Entity ";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DeleteButton.ForeColor = System.Drawing.Color.DarkRed;
            this.DeleteButton.Location = new System.Drawing.Point(271, 240);
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
            this.UpdateButton.Location = new System.Drawing.Point(190, 240);
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
            this.AddButton.Location = new System.Drawing.Point(109, 240);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(75, 23);
            this.AddButton.TabIndex = 5;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // ChildrenBox
            // 
            this.ChildrenBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChildrenBox.Controls.Add(this.ChildrenView);
            this.ChildrenBox.Location = new System.Drawing.Point(0, 269);
            this.ChildrenBox.Name = "ChildrenBox";
            this.ChildrenBox.Size = new System.Drawing.Size(356, 67);
            this.ChildrenBox.TabIndex = 2;
            this.ChildrenBox.TabStop = false;
            this.ChildrenBox.Text = "Entities";
            // 
            // Body
            // 
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 24);
            this.Body.Name = "Body";
            // 
            // Body.Panel1
            // 
            this.Body.Panel1.Controls.Add(this.ParentsView);
            // 
            // Body.Panel2
            // 
            this.Body.Panel2.Controls.Add(this.PropsPanel);
            this.Body.Panel2.Controls.Add(this.DeleteButton);
            this.Body.Panel2.Controls.Add(this.UpdateButton);
            this.Body.Panel2.Controls.Add(this.AddButton);
            this.Body.Panel2.Controls.Add(this.SelectedChildLabel);
            this.Body.Panel2.Controls.Add(this.ChildrenLabel);
            this.Body.Panel2.Controls.Add(this.ChildrenBox);
            this.Body.Size = new System.Drawing.Size(710, 336);
            this.Body.SplitterDistance = 345;
            this.Body.TabIndex = 2;
            // 
            // ParentsView
            // 
            this.ParentsView.AllowUserToAddRows = false;
            this.ParentsView.AllowUserToDeleteRows = false;
            this.ParentsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ParentsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParentsView.Location = new System.Drawing.Point(0, 0);
            this.ParentsView.MultiSelect = false;
            this.ParentsView.Name = "ParentsView";
            this.ParentsView.ReadOnly = true;
            this.ParentsView.Size = new System.Drawing.Size(345, 336);
            this.ParentsView.TabIndex = 0;
            this.ParentsView.SelectionChanged += new System.EventHandler(this.ParentsView_SelectionChanged);
            // 
            // PropsPanel
            // 
            this.PropsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PropsPanel.Location = new System.Drawing.Point(6, 38);
            this.PropsPanel.Name = "PropsPanel";
            this.PropsPanel.Size = new System.Drawing.Size(343, 196);
            this.PropsPanel.TabIndex = 8;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monedeToolStripMenuItem,
            this.consoleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(710, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // monedeToolStripMenuItem
            // 
            this.monedeToolStripMenuItem.Name = "monedeToolStripMenuItem";
            this.monedeToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.monedeToolStripMenuItem.Text = "Monede";
            this.monedeToolStripMenuItem.Click += new System.EventHandler(this.monedeToolStripMenuItem_Click);
            // 
            // consoleToolStripMenuItem
            // 
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.consoleToolStripMenuItem.Text = "Console";
            this.consoleToolStripMenuItem.Click += new System.EventHandler(this.consoleToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 360);
            this.Controls.Add(this.Body);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChildrenView)).EndInit();
            this.ChildrenBox.ResumeLayout(false);
            this.Body.Panel1.ResumeLayout(false);
            this.Body.Panel2.ResumeLayout(false);
            this.Body.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Body)).EndInit();
            this.Body.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ParentsView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ChildrenView;
        private System.Windows.Forms.Label SelectedChildLabel;
        private System.Windows.Forms.Label ChildrenLabel;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.GroupBox ChildrenBox;
        private System.Windows.Forms.SplitContainer Body;
        private System.Windows.Forms.DataGridView ParentsView;
        private System.Windows.Forms.Panel PropsPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem monedeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
    }
}

