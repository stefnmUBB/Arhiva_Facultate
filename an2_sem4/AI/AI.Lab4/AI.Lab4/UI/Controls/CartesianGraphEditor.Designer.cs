namespace AI.Lab4.UI.Controls
{
    partial class CartesianGraphEditor
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.RandomSegmentsButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.GraphViewer = new AI.Lab4.UI.Controls.CartesianGraphViewer();
            this.ExportAsMatrixButton = new System.Windows.Forms.Button();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ExportAsMatrixButton);
            this.panel1.Controls.Add(this.RandomSegmentsButton);
            this.panel1.Controls.Add(this.ClearButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(594, 26);
            this.panel1.TabIndex = 1;
            // 
            // RandomSegmentsButton
            // 
            this.RandomSegmentsButton.Location = new System.Drawing.Point(85, 2);
            this.RandomSegmentsButton.Name = "RandomSegmentsButton";
            this.RandomSegmentsButton.Size = new System.Drawing.Size(112, 23);
            this.RandomSegmentsButton.TabIndex = 1;
            this.RandomSegmentsButton.Text = "Generate Segments";
            this.RandomSegmentsButton.UseVisualStyleBackColor = true;
            this.RandomSegmentsButton.Click += new System.EventHandler(this.RandomSegmentsButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(2, 2);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(79, 23);
            this.ClearButton.TabIndex = 0;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // GraphViewer
            // 
            this.GraphViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GraphViewer.Location = new System.Drawing.Point(0, 0);
            this.GraphViewer.Name = "GraphViewer";
            this.GraphViewer.Size = new System.Drawing.Size(594, 301);
            this.GraphViewer.TabIndex = 0;
            this.GraphViewer.ViewPoint = new System.Drawing.Point(0, 0);
            this.GraphViewer.Zoom = 100;
            // 
            // ExportAsMatrixButton
            // 
            this.ExportAsMatrixButton.Location = new System.Drawing.Point(203, 2);
            this.ExportAsMatrixButton.Name = "ExportAsMatrixButton";
            this.ExportAsMatrixButton.Size = new System.Drawing.Size(97, 23);
            this.ExportAsMatrixButton.TabIndex = 2;
            this.ExportAsMatrixButton.Text = "Export As Matrix";
            this.ExportAsMatrixButton.UseVisualStyleBackColor = true;
            this.ExportAsMatrixButton.Click += new System.EventHandler(this.ExportAsMatrixButton_Click);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "Text File (*.txt) |*.txt";
            this.SaveFileDialog.Title = "Save Costs Matrix";
            // 
            // CartesianGraphEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GraphViewer);
            this.Controls.Add(this.panel1);
            this.Name = "CartesianGraphEditor";
            this.Size = new System.Drawing.Size(594, 327);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CartesianGraphViewer GraphViewer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button RandomSegmentsButton;
        private System.Windows.Forms.Button ExportAsMatrixButton;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
    }
}
