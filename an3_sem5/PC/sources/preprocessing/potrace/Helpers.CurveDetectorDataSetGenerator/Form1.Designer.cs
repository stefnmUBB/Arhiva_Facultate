namespace Helpers.CurveDetectorDataSetGenerator
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
            this.Canvas = new System.Windows.Forms.Panel();
            this.InputImg = new System.Windows.Forms.Panel();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(73, 68);
            this.Canvas.TabIndex = 0;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            // 
            // InputImg
            // 
            this.InputImg.Location = new System.Drawing.Point(211, 28);
            this.InputImg.Name = "InputImg";
            this.InputImg.Size = new System.Drawing.Size(256, 256);
            this.InputImg.TabIndex = 1;
            this.InputImg.Paint += new System.Windows.Forms.PaintEventHandler(this.InputImg_Paint);
            this.InputImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputImg_MouseDown);
            this.InputImg.MouseLeave += new System.EventHandler(this.InputImg_MouseLeave);
            this.InputImg.MouseMove += new System.Windows.Forms.MouseEventHandler(this.InputImg_MouseMove);
            this.InputImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InputImg_MouseUp);
            // 
            // ProcessButton
            // 
            this.ProcessButton.Location = new System.Drawing.Point(473, 28);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(75, 23);
            this.ProcessButton.TabIndex = 2;
            this.ProcessButton.Text = "Process";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(668, 339);
            this.Controls.Add(this.ProcessButton);
            this.Controls.Add(this.InputImg);
            this.Controls.Add(this.Canvas);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Canvas;
        private System.Windows.Forms.Panel InputImg;
        private System.Windows.Forms.Button ProcessButton;
    }
}

