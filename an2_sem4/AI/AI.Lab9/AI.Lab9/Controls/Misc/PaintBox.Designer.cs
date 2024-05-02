namespace AI.Lab9.Controls.Misc
{
    partial class PaintBox
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
            this.Grid = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // Grid
            // 
            this.Grid.BackColor = System.Drawing.Color.White;
            this.Grid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Grid.Dock = System.Windows.Forms.DockStyle.Right;
            this.Grid.Location = new System.Drawing.Point(56, 0);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(240, 244);
            this.Grid.TabIndex = 0;
            this.Grid.Paint += new System.Windows.Forms.PaintEventHandler(this.Grid_Paint);
            this.Grid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseDoubleClick);
            this.Grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseDown);
            this.Grid.MouseLeave += new System.EventHandler(this.Grid_MouseLeave);
            this.Grid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseMove);
            this.Grid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseUp);
            // 
            // PaintBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Grid);
            this.Name = "PaintBox";
            this.Size = new System.Drawing.Size(296, 244);
            this.Resize += new System.EventHandler(this.PaintBox_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel Grid;
    }
}
