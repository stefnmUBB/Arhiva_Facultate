namespace Licenta.Utils.Forms
{
    partial class ImageRGBDisplayForm
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
            this.ContentPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LumBox = new System.Windows.Forms.CheckBox();
            this.SatBox = new System.Windows.Forms.CheckBox();
            this.HueBox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Location = new System.Drawing.Point(0, 0);
            this.ContentPanel.Name = "ContentPanel";
            this.ContentPanel.Size = new System.Drawing.Size(200, 100);
            this.ContentPanel.TabIndex = 0;
            this.ContentPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ContentPanel_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LumBox);
            this.panel2.Controls.Add(this.SatBox);
            this.panel2.Controls.Add(this.HueBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 36);
            this.panel2.TabIndex = 1;
            // 
            // LumBox
            // 
            this.LumBox.AutoSize = true;
            this.LumBox.Checked = true;
            this.LumBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LumBox.Location = new System.Drawing.Point(112, 12);
            this.LumBox.Name = "LumBox";
            this.LumBox.Size = new System.Drawing.Size(46, 17);
            this.LumBox.TabIndex = 2;
            this.LumBox.Text = "Lum";
            this.LumBox.UseVisualStyleBackColor = true;
            this.LumBox.CheckedChanged += new System.EventHandler(this.ComponentBox_CheckedChanged);
            // 
            // SatBox
            // 
            this.SatBox.AutoSize = true;
            this.SatBox.Checked = true;
            this.SatBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.SatBox.Location = new System.Drawing.Point(64, 12);
            this.SatBox.Name = "SatBox";
            this.SatBox.Size = new System.Drawing.Size(42, 17);
            this.SatBox.TabIndex = 1;
            this.SatBox.Text = "Sat";
            this.SatBox.UseVisualStyleBackColor = true;
            this.SatBox.CheckedChanged += new System.EventHandler(this.ComponentBox_CheckedChanged);
            // 
            // HueBox
            // 
            this.HueBox.AutoSize = true;
            this.HueBox.Checked = true;
            this.HueBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HueBox.Location = new System.Drawing.Point(12, 12);
            this.HueBox.Name = "HueBox";
            this.HueBox.Size = new System.Drawing.Size(46, 17);
            this.HueBox.TabIndex = 0;
            this.HueBox.Text = "Hue";
            this.HueBox.UseVisualStyleBackColor = true;
            this.HueBox.CheckedChanged += new System.EventHandler(this.ComponentBox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.ContentPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 414);
            this.panel1.TabIndex = 2;
            // 
            // ImageRGBDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ImageRGBDisplayForm";
            this.Text = "ImageRGB";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ContentPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox HueBox;
        private System.Windows.Forms.CheckBox LumBox;
        private System.Windows.Forms.CheckBox SatBox;
        private System.Windows.Forms.Panel panel1;
    }
}