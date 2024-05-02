namespace AI.Lab10.Controls.Tabs
{
    partial class ReviewKTab
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
            this.VectorizerSelector = new AI.Lab10.Controls.Misc.VectorizerSelector();
            this.label1 = new System.Windows.Forms.Label();
            this.GoodBadHelper = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RunButton.Location = new System.Drawing.Point(340, 3);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 23);
            this.RunButton.TabIndex = 2;
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
            this.OutputView.Size = new System.Drawing.Size(412, 236);
            this.OutputView.TabIndex = 3;
            // 
            // VectorizerSelector
            // 
            this.VectorizerSelector.Location = new System.Drawing.Point(68, 4);
            this.VectorizerSelector.Name = "VectorizerSelector";
            this.VectorizerSelector.Size = new System.Drawing.Size(150, 24);
            this.VectorizerSelector.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Vectorizer";
            // 
            // GoodBadHelper
            // 
            this.GoodBadHelper.AutoSize = true;
            this.GoodBadHelper.Location = new System.Drawing.Point(224, 7);
            this.GoodBadHelper.Name = "GoodBadHelper";
            this.GoodBadHelper.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.GoodBadHelper.Size = new System.Drawing.Size(89, 17);
            this.GoodBadHelper.TabIndex = 6;
            this.GoodBadHelper.Text = "Words helper";
            this.GoodBadHelper.UseVisualStyleBackColor = true;
            // 
            // ReviewKTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GoodBadHelper);
            this.Controls.Add(this.VectorizerSelector);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OutputView);
            this.Controls.Add(this.RunButton);
            this.Name = "ReviewKTab";
            this.Size = new System.Drawing.Size(418, 271);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RunButton;
        private Misc.OutputView OutputView;
        private Misc.VectorizerSelector VectorizerSelector;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox GoodBadHelper;
    }
}
