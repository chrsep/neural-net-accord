namespace FulgurantArtAnn
{
    partial class BrowseForm
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
            this.components = new System.ComponentModel.Container();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelLinkBack = new System.Windows.Forms.LinkLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(134, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Fulgurant Art - Get Fulgent!";
            // 
            // labelLinkBack
            // 
            this.labelLinkBack.AutoSize = true;
            this.labelLinkBack.Location = new System.Drawing.Point(390, 203);
            this.labelLinkBack.Name = "labelLinkBack";
            this.labelLinkBack.Size = new System.Drawing.Size(31, 13);
            this.labelLinkBack.TabIndex = 1;
            this.labelLinkBack.TabStop = true;
            this.labelLinkBack.Text = "back";
            this.labelLinkBack.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelLinkBack_LinkClicked);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 25);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(409, 170);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // BrowseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 225);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.labelLinkBack);
            this.Controls.Add(this.labelTitle);
            this.Name = "BrowseForm";
            this.Text = "BrowseForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BrowseForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.LinkLabel labelLinkBack;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView1;
    }
}