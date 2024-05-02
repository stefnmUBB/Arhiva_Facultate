namespace AI.Commons.UI.Controls
{
    partial class GraphViewer<T>
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
            this.Canvas = new System.Windows.Forms.Panel();
            this.ZoomInButton = new System.Windows.Forms.PictureBox();
            this.ZoomOutButton = new System.Windows.Forms.PictureBox();
            this.Body = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomInButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomOutButton)).BeginInit();
            this.Body.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.Location = new System.Drawing.Point(3, 3);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(100, 100);
            this.Canvas.TabIndex = 0;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            // 
            // ZoomInButton
            // 
            this.ZoomInButton.Image = global::AI.Commons.Properties.Resources.zoom_in;
            this.ZoomInButton.Location = new System.Drawing.Point(3, 3);
            this.ZoomInButton.Name = "ZoomInButton";
            this.ZoomInButton.Size = new System.Drawing.Size(24, 24);
            this.ZoomInButton.TabIndex = 1;
            this.ZoomInButton.TabStop = false;
            this.ZoomInButton.Click += new System.EventHandler(this.ZoomInButton_Click);
            // 
            // ZoomOutButton
            // 
            this.ZoomOutButton.Image = global::AI.Commons.Properties.Resources.zoom_out;
            this.ZoomOutButton.Location = new System.Drawing.Point(33, 3);
            this.ZoomOutButton.Name = "ZoomOutButton";
            this.ZoomOutButton.Size = new System.Drawing.Size(24, 24);
            this.ZoomOutButton.TabIndex = 2;
            this.ZoomOutButton.TabStop = false;
            this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
            // 
            // Body
            // 
            this.Body.AutoScroll = true;
            this.Body.Controls.Add(this.Canvas);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 0);
            this.Body.Name = "Body";
            this.Body.Size = new System.Drawing.Size(503, 257);
            this.Body.TabIndex = 3;
            // 
            // GraphViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ZoomOutButton);
            this.Controls.Add(this.ZoomInButton);
            this.Controls.Add(this.Body);
            this.Name = "GraphViewer";
            this.Size = new System.Drawing.Size(503, 257);
            this.Resize += new System.EventHandler(this.GraphViewer_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ZoomInButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ZoomOutButton)).EndInit();
            this.Body.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.PictureBox ZoomInButton;
        private System.Windows.Forms.PictureBox ZoomOutButton;
        private System.Windows.Forms.Panel Body;
    }
}
