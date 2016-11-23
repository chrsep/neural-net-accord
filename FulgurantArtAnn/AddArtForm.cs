using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FulgurantArtAnn
{
    public partial class AddArtForm : Form
    {

        private readonly Form _parentForm;
        private readonly NeuralEngine _engine;
        private OpenFileDialog imageDialog;

        public AddArtForm(Form parent)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            _parentForm = parent;
            _engine = NeuralEngine.Instance;
            imageDialog = new OpenFileDialog();
            SubmitArtBtn.Visible = false;
            ArtStudioTxtBox.Visible = false;

        }

        private void AddNewArt_Click(object sender, EventArgs e)
        {
            
            imageDialog.Filter = "Image Files (*.jpg,*.jpeg,*.png) | *.jpg; *.jpeg; *.png";
            imageDialog.Multiselect = true;
            imageDialog.InitialDirectory = @"C:\..\Pictures";
            imageDialog.Title = "Open";
            imageDialog.ShowDialog();

            int initial = 0;

            foreach (string fileName in imageDialog.FileNames)
            {
                Image img = Image.FromFile(fileName);
                string name = initial.ToString();
                imageList.Images.Add(name, img);
            }

            this.ViewArt.View = View.LargeIcon;
            this.imageList.ImageSize = new Size(60, 60);
            this.ViewArt.LargeImageList = this.imageList;

            for (int i = 0; i < this.imageList.Images.Count; i++) {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                this.ViewArt.Items.Add(item);
            }

            //ArtStudioTxtBox.Visible = true;
        }

        private void BackLblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _parentForm.Show();
            Hide();
        }

        private void ViewArt_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void AddArtForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
