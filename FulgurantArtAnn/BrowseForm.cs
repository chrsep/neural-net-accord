using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FulgurantArtAnn
{
    public partial class BrowseForm : Form
    {
        private readonly Form _parentForm;
        public string pic;

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
                var imageIndex = 0;
                for (var i = 0; i < data.Count(); i++)
                {
                    ListViewGroup viewGroup;
                    var category = data.Keys.ElementAt(i);
                    if (listView1.Groups[category] == null)
                    {
                        viewGroup = new ListViewGroup(category, category);
                        listView1.Groups.Add(viewGroup);
                    }
                    else viewGroup = listView1.Groups[category];


                    var images = data.Values.ElementAt(i);
                    var filenames = Directory.GetFiles("pictures/" + category);
                    for (var j = 0; j < images.Count; j++)
                    {
                        var image = images[j];
                        imageList1.Images.Add(image);
                        var item = new ListViewItem(new DirectoryInfo(filenames[j]).Name, imageIndex, viewGroup);
                        listView1.Items.Add(item);
                        imageIndex++;
                    }
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string pic = listView1.SelectedItems[0].Text.ToString();
            MessageBox.Show(pic);

            this.Hide();
            Form artDetail = new ArtDetail();
            artDetail.Show();

        }

        public string picture() {
            string pic = listView1.SelectedItems[0].Text.ToString();
            return pic;
        }
    }
}