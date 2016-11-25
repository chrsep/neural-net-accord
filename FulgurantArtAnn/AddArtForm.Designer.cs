namespace FulgurantArtAnn
{
    partial class AddArtForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.AddNewArt = new System.Windows.Forms.Button();
            this.SubmitArtBtn = new System.Windows.Forms.Button();
            this.BackLblLink = new System.Windows.Forms.LinkLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ArtStudioTxtBox = new System.Windows.Forms.TextBox();
            this.ViewArt = new System.Windows.Forms.ListView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.dialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Add New Art";
            // 
            // AddNewArt
            // 
            this.AddNewArt.BackColor = System.Drawing.Color.PaleTurquoise;
            this.AddNewArt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewArt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.AddNewArt.Location = new System.Drawing.Point(12, 192);
            this.AddNewArt.Name = "AddNewArt";
            this.AddNewArt.Size = new System.Drawing.Size(120, 23);
            this.AddNewArt.TabIndex = 1;
            this.AddNewArt.Text = "Add New";
            this.AddNewArt.UseVisualStyleBackColor = false;
            this.AddNewArt.Click += new System.EventHandler(this.AddNewArt_Click);
            // 
            // SubmitArtBtn
            // 
            this.SubmitArtBtn.BackColor = System.Drawing.Color.PaleTurquoise;
            this.SubmitArtBtn.ForeColor = System.Drawing.Color.DarkOrange;
            this.SubmitArtBtn.Location = new System.Drawing.Point(12, 246);
            this.SubmitArtBtn.Name = "SubmitArtBtn";
            this.SubmitArtBtn.Size = new System.Drawing.Size(308, 50);
            this.SubmitArtBtn.TabIndex = 2;
            this.SubmitArtBtn.Text = "Submit Art";
            this.SubmitArtBtn.UseVisualStyleBackColor = false;
            this.SubmitArtBtn.Visible = false;
            this.SubmitArtBtn.Click += new System.EventHandler(this.SubmitArtBtn_Click);
            // 
            // BackLblLink
            // 
            this.BackLblLink.AutoSize = true;
            this.BackLblLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.BackLblLink.Location = new System.Drawing.Point(288, 323);
            this.BackLblLink.Name = "BackLblLink";
            this.BackLblLink.Size = new System.Drawing.Size(32, 13);
            this.BackLblLink.TabIndex = 3;
            this.BackLblLink.TabStop = true;
            this.BackLblLink.Text = "Back";
            this.BackLblLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.BackLblLink_LinkClicked);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.PaleTurquoise;
            this.comboBox1.DataSource = this.bindingSource1;
            this.comboBox1.ForeColor = System.Drawing.Color.DarkOrange;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(144, 194);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(176, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // ArtStudioTxtBox
            // 
            this.ArtStudioTxtBox.ForeColor = System.Drawing.Color.DarkOrange;
            this.ArtStudioTxtBox.Location = new System.Drawing.Point(12, 221);
            this.ArtStudioTxtBox.Name = "ArtStudioTxtBox";
            this.ArtStudioTxtBox.Size = new System.Drawing.Size(308, 20);
            this.ArtStudioTxtBox.TabIndex = 5;
            this.ArtStudioTxtBox.Text = "Art Studio";
            this.ArtStudioTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ArtStudioTxtBox.Visible = false;
            this.ArtStudioTxtBox.TextChanged += new System.EventHandler(this.SubmitButtonCheck);
            // 
            // ViewArt
            // 
            this.ViewArt.Location = new System.Drawing.Point(12, 37);
            this.ViewArt.Name = "ViewArt";
            this.ViewArt.Size = new System.Drawing.Size(308, 149);
            this.ViewArt.TabIndex = 6;
            this.ViewArt.UseCompatibleStateImageBehavior = false;
            // 
            // imageList
            // 
            this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // dialog
            // 
            this.dialog.Filter = "Image Files (*.jpg,*.jpeg,*.png) | *.jpg; *.jpeg; *.png";
            this.dialog.Multiselect = true;
            // 
            // AddArtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumAquamarine;
            this.ClientSize = new System.Drawing.Size(332, 345);
            this.Controls.Add(this.ViewArt);
            this.Controls.Add(this.ArtStudioTxtBox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.BackLblLink);
            this.Controls.Add(this.SubmitArtBtn);
            this.Controls.Add(this.AddNewArt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Name = "AddArtForm";
            this.Text = "AddArtForm";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddNewArt;
        private System.Windows.Forms.Button SubmitArtBtn;
        private System.Windows.Forms.LinkLabel BackLblLink;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox ArtStudioTxtBox;
        private System.Windows.Forms.ListView ViewArt;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.OpenFileDialog dialog;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}