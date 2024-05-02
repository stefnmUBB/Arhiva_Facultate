namespace AI.Commons.UI.Controls
{
    partial class PathSelector
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
            this.DisplayBox = new System.Windows.Forms.TextBox();
            this.Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DisplayBox
            // 
            this.DisplayBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayBox.Location = new System.Drawing.Point(0, 0);
            this.DisplayBox.Name = "DisplayBox";
            this.DisplayBox.Size = new System.Drawing.Size(117, 20);
            this.DisplayBox.TabIndex = 2;
            // 
            // Button
            // 
            this.Button.Dock = System.Windows.Forms.DockStyle.Right;
            this.Button.FlatAppearance.BorderSize = 0;
            this.Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button.Location = new System.Drawing.Point(117, 0);
            this.Button.Name = "Button";
            this.Button.Size = new System.Drawing.Size(33, 20);
            this.Button.TabIndex = 3;
            this.Button.Text = "...";
            this.Button.UseVisualStyleBackColor = true;
            this.Button.Click += new System.EventHandler(this.Button_Click);
            // 
            // PathSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DisplayBox);
            this.Controls.Add(this.Button);
            this.Name = "PathSelector";
            this.Size = new System.Drawing.Size(150, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox DisplayBox;
        private System.Windows.Forms.Button Button;
    }
}
