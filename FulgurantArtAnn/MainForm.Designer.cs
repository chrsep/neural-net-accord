namespace FulgurantArtAnn
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.buttonAddArt = new System.Windows.Forms.Button();
            this.buttonCheckCategory = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.linkExit = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(107, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(67, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Fulgurant Art";
            // 
            // buttonAddArt
            // 
            this.buttonAddArt.Location = new System.Drawing.Point(99, 56);
            this.buttonAddArt.Name = "buttonAddArt";
            this.buttonAddArt.Size = new System.Drawing.Size(75, 23);
            this.buttonAddArt.TabIndex = 1;
            this.buttonAddArt.Text = "Add Art";
            this.buttonAddArt.UseVisualStyleBackColor = true;
            // 
            // buttonCheckCategory
            // 
            this.buttonCheckCategory.Location = new System.Drawing.Point(99, 85);
            this.buttonCheckCategory.Name = "buttonCheckCategory";
            this.buttonCheckCategory.Size = new System.Drawing.Size(75, 23);
            this.buttonCheckCategory.TabIndex = 2;
            this.buttonCheckCategory.Text = "Check Category";
            this.buttonCheckCategory.UseVisualStyleBackColor = true;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(99, 114);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 3;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            // 
            // linkExit
            // 
            this.linkExit.AutoSize = true;
            this.linkExit.Location = new System.Drawing.Point(123, 140);
            this.linkExit.Name = "linkExit";
            this.linkExit.Size = new System.Drawing.Size(24, 13);
            this.linkExit.TabIndex = 4;
            this.linkExit.TabStop = true;
            this.linkExit.Text = "Exit";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.linkExit);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.buttonCheckCategory);
            this.Controls.Add(this.buttonAddArt);
            this.Controls.Add(this.labelTitle);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button buttonAddArt;
        private System.Windows.Forms.Button buttonCheckCategory;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.LinkLabel linkExit;
    }
}

