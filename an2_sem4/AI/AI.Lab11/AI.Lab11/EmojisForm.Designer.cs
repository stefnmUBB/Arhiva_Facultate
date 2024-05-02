namespace AI.Lab11
{
    partial class EmojisForm
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
            this.EmojiViewer = new AI.Lab11.Controls.EmojiViewer();
            this.RetryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EmojiViewer
            // 
            this.EmojiViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmojiViewer.Location = new System.Drawing.Point(12, 12);
            this.EmojiViewer.Name = "EmojiViewer";
            this.EmojiViewer.Size = new System.Drawing.Size(567, 426);
            this.EmojiViewer.TabIndex = 0;
            // 
            // RetryButton
            // 
            this.RetryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RetryButton.Location = new System.Drawing.Point(585, 12);
            this.RetryButton.Name = "RetryButton";
            this.RetryButton.Size = new System.Drawing.Size(75, 23);
            this.RetryButton.TabIndex = 1;
            this.RetryButton.Text = "Retry";
            this.RetryButton.UseVisualStyleBackColor = true;
            this.RetryButton.Click += new System.EventHandler(this.RetryButton_Click);
            // 
            // EmojisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.RetryButton);
            this.Controls.Add(this.EmojiViewer);
            this.Name = "EmojisForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.EmojisForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.EmojiViewer EmojiViewer;
        private System.Windows.Forms.Button RetryButton;
    }
}

