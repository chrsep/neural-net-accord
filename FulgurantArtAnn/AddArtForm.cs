using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FulgurantArtAnn
{
    public partial class AddArtForm : Form
    {
        private readonly NeuralEngine _engine;

        private readonly Form _parentForm;
        private string[] _paths;
        private string[] _fileNames;

        public AddArtForm(Form parent)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            _parentForm = parent;
            _engine = NeuralEngine.Instance;
            _paths = new string[0];
            var availableCategories = Directory.GetDirectories("pictures").ToList();
            availableCategories = availableCategories.Select(paths => new DirectoryInfo(paths).Name).ToList();
            availableCategories.Add("Add new categories");
            availableCategories.Reverse();
            bindingSource1.DataSource = availableCategories;
        }

        private void AddNewArt_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var initial = 0;
                _paths = dialog.FileNames;
                _fileNames = dialog.SafeFileNames;
                for (var i = 0; i < _paths.Length; i++)
                    imageList.Images.Add(_fileNames[i], Image.FromFile(_paths[i]));
                imageList.ImageSize = new Size(60, 60);
                ViewArt.LargeImageList = imageList;

                for (var i = 0; i < imageList.Images.Count; i++)
                {
                    var item = new ListViewItem {ImageIndex = i};
                    ViewArt.Items.Add(item);
                }

                if (comboBox1.SelectedItem.Equals("Add new categories"))
                    ArtStudioTxtBox.Visible = true;
                SubmitButtonCheck();
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ArtStudioTxtBox.Visible = comboBox1.SelectedItem.Equals("Add new categories");
            SubmitButtonCheck();
        }

        private void SubmitButtonCheck(object sender = null, EventArgs e = null)
        {
            var visibility = false;
            if (comboBox1.SelectedItem.Equals("Add new categories"))
            {
                if (!ArtStudioTxtBox.Text.Equals("")) visibility = true;
            }
            else visibility = true;

            SubmitArtBtn.Visible = visibility;
        }

        private void BackLblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _parentForm.Show();
            Hide();
        }

        private void SubmitArtBtn_Click(object sender, EventArgs e)
        {
            string directory = "pictures/";
            if (comboBox1.SelectedItem.Equals("Add new categories"))
            {
                directory += ArtStudioTxtBox.Text;
            }
            else
            {
                directory += comboBox1.SelectedText;
            }
            Directory.CreateDirectory(directory);
            for (int i = 0; i < _paths.Length; i++)
            {
                try
                {
                    File.Copy(_paths[i], directory + "/" + _fileNames[i]);
                }
                catch (Exception)
                {
                    MessageBox.Show(_fileNames[i] + " already Exist!!");
                }
                
            }
        }
    }
}