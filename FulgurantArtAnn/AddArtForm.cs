using System;
using System.Collections.Generic;
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
        private List<string> _paths;
        private List<string> _fileNames;

        public AddArtForm(Form parent)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            _parentForm = parent;
            _engine = NeuralEngine.Instance;
            _paths = new List<string>();
            _fileNames = new List<string>();
            var availableCategories = Directory.GetDirectories("pictures").ToList();
            availableCategories = availableCategories.Select(paths => new DirectoryInfo(paths).Name).ToList();
            availableCategories.Add("Add new categories");
            availableCategories.Reverse();
            bindingSource1.DataSource = availableCategories;
        }

        private void AddNewArt_Click(object sender, EventArgs e)
        {
            if (dialog.ShowDialog() != DialogResult.OK) return;
            _paths.AddRange(dialog.FileNames.ToList());  
            _fileNames.AddRange(dialog.SafeFileNames);
            
            imageList.Images.Clear();
            foreach (var path in _paths)
                imageList.Images.Add(Image.FromFile(path));

            ViewArt.Clear();
            for (var i = 0; i < imageList.Images.Count; i++)
            {
                var item = new ListViewItem(_fileNames[i],i);
                ViewArt.Items.Add(item);
            }

            if (comboBox1.SelectedItem.Equals("Add new categories"))
                ArtStudioTxtBox.Visible = true;
            SubmitButtonCheck();
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
                directory += comboBox1.SelectedItem;
            }
            Directory.CreateDirectory(directory);
            for (int i = 0; i < _paths.Count; i++)
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
            _engine.TrainClasificationNetwork();
        }
    }
}