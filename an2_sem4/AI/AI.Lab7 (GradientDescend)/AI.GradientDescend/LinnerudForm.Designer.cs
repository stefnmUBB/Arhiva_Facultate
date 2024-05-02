namespace AI.GradientDescend
{
    partial class LinnerudForm
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
            this.W0Label = new System.Windows.Forms.Label();
            this.W1Label = new System.Windows.Forms.Label();
            this.W2Label = new System.Windows.Forms.Label();
            this.W3Label = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // W0Label
            // 
            this.W0Label.AutoSize = true;
            this.W0Label.Location = new System.Drawing.Point(12, 9);
            this.W0Label.Name = "W0Label";
            this.W0Label.Size = new System.Drawing.Size(27, 13);
            this.W0Label.TabIndex = 0;
            this.W0Label.Text = "w0=";
            // 
            // W1Label
            // 
            this.W1Label.AutoSize = true;
            this.W1Label.Location = new System.Drawing.Point(12, 39);
            this.W1Label.Name = "W1Label";
            this.W1Label.Size = new System.Drawing.Size(27, 13);
            this.W1Label.TabIndex = 1;
            this.W1Label.Text = "w1=";
            // 
            // W2Label
            // 
            this.W2Label.AutoSize = true;
            this.W2Label.Location = new System.Drawing.Point(12, 74);
            this.W2Label.Name = "W2Label";
            this.W2Label.Size = new System.Drawing.Size(27, 13);
            this.W2Label.TabIndex = 2;
            this.W2Label.Text = "w2=";
            // 
            // W3Label
            // 
            this.W3Label.AutoSize = true;
            this.W3Label.Location = new System.Drawing.Point(12, 107);
            this.W3Label.Name = "W3Label";
            this.W3Label.Size = new System.Drawing.Size(27, 13);
            this.W3Label.TabIndex = 3;
            this.W3Label.Text = "w3=";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(12, 143);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(37, 13);
            this.ErrorLabel.TabIndex = 4;
            this.ErrorLabel.Text = "error =";
            // 
            // LinnerudForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.W3Label);
            this.Controls.Add(this.W2Label);
            this.Controls.Add(this.W1Label);
            this.Controls.Add(this.W0Label);
            this.Name = "LinnerudForm";
            this.Text = "LinnerudForm";
            this.Load += new System.EventHandler(this.LinnerudForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label W0Label;
        private System.Windows.Forms.Label W1Label;
        private System.Windows.Forms.Label W2Label;
        private System.Windows.Forms.Label W3Label;
        private System.Windows.Forms.Label ErrorLabel;
    }
}