namespace AI.Lab10.Controls.Tabs
{
    partial class EmailTab
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
            this.RunButton = new System.Windows.Forms.Button();
            this.OutputView = new AI.Lab10.Controls.Misc.OutputView();
            this.label1 = new System.Windows.Forms.Label();
            this.VectorizerSelector = new AI.Lab10.Controls.Misc.VectorizerSelector();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RunButton.Location = new System.Drawing.Point(480, 3);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 1;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // OutputView
            // 
            this.OutputView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputView.Location = new System.Drawing.Point(3, 32);
            this.OutputView.Name = "OutputView";
            this.OutputView.Size = new System.Drawing.Size(552, 289);
            this.OutputView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Vectorizer";
            // 
            // VectorizerSelector
            // 
            this.VectorizerSelector.Location = new System.Drawing.Point(63, 2);
            this.VectorizerSelector.Name = "VectorizerSelector";
            this.VectorizerSelector.Size = new System.Drawing.Size(150, 24);
            this.VectorizerSelector.TabIndex = 3;
            // 
            // EmailTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VectorizerSelector);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.OutputView);
            this.Name = "EmailTab";
            this.Size = new System.Drawing.Size(558, 324);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Misc.OutputView OutputView;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label label1;
        private Misc.VectorizerSelector VectorizerSelector;
    }
}
