namespace AI.Lab9
{
    partial class MainForm
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
            this.Body = new System.Windows.Forms.TabControl();
            this.IrisTabPage = new System.Windows.Forms.TabPage();
            this.DigitsTabPage = new System.Windows.Forms.TabPage();
            this.SepiaTabPage = new System.Windows.Forms.TabPage();
            this.irisSimTab1 = new AI.Lab9.Controls.Tabs.IrisSimTab();
            this.digitsSimTab1 = new AI.Lab9.Controls.Tabs.DigitsSimTab();
            this.sepiaSimTab1 = new AI.Lab9.Controls.Tabs.SepiaSimTab();
            this.Body.SuspendLayout();
            this.IrisTabPage.SuspendLayout();
            this.DigitsTabPage.SuspendLayout();
            this.SepiaTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Body
            // 
            this.Body.Controls.Add(this.IrisTabPage);
            this.Body.Controls.Add(this.DigitsTabPage);
            this.Body.Controls.Add(this.SepiaTabPage);
            this.Body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Body.Location = new System.Drawing.Point(0, 0);
            this.Body.Name = "Body";
            this.Body.SelectedIndex = 0;
            this.Body.Size = new System.Drawing.Size(800, 561);
            this.Body.TabIndex = 0;
            // 
            // IrisTabPage
            // 
            this.IrisTabPage.Controls.Add(this.irisSimTab1);
            this.IrisTabPage.Location = new System.Drawing.Point(4, 22);
            this.IrisTabPage.Name = "IrisTabPage";
            this.IrisTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.IrisTabPage.Size = new System.Drawing.Size(792, 535);
            this.IrisTabPage.TabIndex = 0;
            this.IrisTabPage.Text = "Iris";
            this.IrisTabPage.UseVisualStyleBackColor = true;
            // 
            // DigitsTabPage
            // 
            this.DigitsTabPage.Controls.Add(this.digitsSimTab1);
            this.DigitsTabPage.Location = new System.Drawing.Point(4, 22);
            this.DigitsTabPage.Name = "DigitsTabPage";
            this.DigitsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.DigitsTabPage.Size = new System.Drawing.Size(792, 424);
            this.DigitsTabPage.TabIndex = 1;
            this.DigitsTabPage.Text = "Digits";
            this.DigitsTabPage.UseVisualStyleBackColor = true;
            // 
            // SepiaTabPage
            // 
            this.SepiaTabPage.Controls.Add(this.sepiaSimTab1);
            this.SepiaTabPage.Location = new System.Drawing.Point(4, 22);
            this.SepiaTabPage.Name = "SepiaTabPage";
            this.SepiaTabPage.Size = new System.Drawing.Size(792, 424);
            this.SepiaTabPage.TabIndex = 2;
            this.SepiaTabPage.Text = "Sepia";
            this.SepiaTabPage.UseVisualStyleBackColor = true;
            // 
            // irisSimTab1
            // 
            this.irisSimTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.irisSimTab1.Location = new System.Drawing.Point(3, 3);
            this.irisSimTab1.Name = "irisSimTab1";
            this.irisSimTab1.Size = new System.Drawing.Size(786, 529);
            this.irisSimTab1.TabIndex = 0;
            // 
            // digitsSimTab1
            // 
            this.digitsSimTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.digitsSimTab1.Location = new System.Drawing.Point(3, 3);
            this.digitsSimTab1.Name = "digitsSimTab1";
            this.digitsSimTab1.Size = new System.Drawing.Size(786, 418);
            this.digitsSimTab1.TabIndex = 0;
            // 
            // sepiaSimTab1
            // 
            this.sepiaSimTab1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sepiaSimTab1.Location = new System.Drawing.Point(0, 0);
            this.sepiaSimTab1.Name = "sepiaSimTab1";
            this.sepiaSimTab1.Size = new System.Drawing.Size(792, 424);
            this.sepiaSimTab1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.Body);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Text = "ANN - Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Body.ResumeLayout(false);
            this.IrisTabPage.ResumeLayout(false);
            this.DigitsTabPage.ResumeLayout(false);
            this.SepiaTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Body;
        private System.Windows.Forms.TabPage IrisTabPage;
        private System.Windows.Forms.TabPage DigitsTabPage;
        private System.Windows.Forms.TabPage SepiaTabPage;
        private Controls.Tabs.IrisSimTab irisSimTab1;
        private Controls.Tabs.DigitsSimTab digitsSimTab1;
        private Controls.Tabs.SepiaSimTab sepiaSimTab1;
    }
}

