using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FulgurantArtAnn
{
    public partial class BrowseForm : Form
    {
        private readonly Form _parentForm;

        public BrowseForm(Form parent)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            _parentForm = parent;
        }

        private void BrowseForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentForm.Show();
        }

        private void labelLinkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void BrowseForm_Load(object sender, EventArgs e)
        {
            var data = NeuralEngine.GetImages();
            if (data.Count == 0)
            {
                MessageBox.Show("Add some art first!");
                _parentForm.Show();
                Close();
            }
            else
            {
                for (int i = 0; i < data.Count(); i++)
                {
                    ListViewGroup viewGroup;

                    var category = data.Keys.ElementAt(i);

                    if (listView1.Groups[category] == null)
                    {
                        viewGroup = new ListViewGroup(category, category);

                        listView1.Groups.Add(viewGroup);
                    }
                    else
                    {
                        viewGroup = listView1.Groups[category];
                    }

                    var images = GetImage();

                    MessageBox.Show(images.Count().ToString());

                    foreach (var image in images)
                    {
                        imageList1.Images.Add(image);
                    }

                    ListViewItem item = new ListViewItem(category, i, viewGroup);
                    listView1.Items.Add(item);
                }
            }
        }

        public List<Bitmap> GetImage()
        {
            var image = new List<Bitmap>();
            var paths = Directory.GetDirectories("pictures");
            foreach (var path in paths)
            {
                var imagePaths = Directory.GetFiles(path);
                var images = imagePaths.Select(imagePath => new Bitmap(imagePath));
                image.AddRange(images);
            }
            return image;
        }
    }
}