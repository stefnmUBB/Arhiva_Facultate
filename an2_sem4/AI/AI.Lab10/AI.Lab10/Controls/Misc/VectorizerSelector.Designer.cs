namespace AI.Lab10.Controls.Misc
{
    partial class VectorizerSelector
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
            this.Combobox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Combobox
            // 
            this.Combobox.Dock = System.Windows.Forms.DockStyle.Top;
            this.Combobox.FormattingEnabled = true;
            this.Combobox.Items.AddRange(new object[] {
            "BagOfWords",
            "NGram2",
            "NGram3",
            "NGram4",
            "TFIDF",
            "Word2Vec"});
            this.Combobox.Location = new System.Drawing.Point(0, 0);
            this.Combobox.Name = "Combobox";
            this.Combobox.Size = new System.Drawing.Size(150, 21);
            this.Combobox.TabIndex = 0;
            this.Combobox.SelectedIndexChanged += new System.EventHandler(this.Combobox_SelectedIndexChanged);
            // 
            // VectorizerSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Combobox);
            this.Name = "VectorizerSelector";
            this.Size = new System.Drawing.Size(150, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox Combobox;
    }
}
