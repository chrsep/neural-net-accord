using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FulgurantArtAnn
{
    public partial class ArtDetail : Form
    {
        private readonly Form _parentform;

        public ArtDetail(Form form, ListViewItem chosenImage)
        {
            InitializeComponent();
            var category = chosenImage.Group.Name;
            var filename = chosenImage.Text;
            var fullPath = Directory.GetFiles("pictures/" + category).First(path => path.Contains(filename));
            _parentform = form;
            pictureBox1.Image = new Bitmap(fullPath);
            label2.Text = Path.GetFileName(fullPath);
            var similarImages = NeuralEngine.Instance.FindSimilar(fullPath);
            UpdateList(similarImages);
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void ArtDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentform.Show();
        }

        private void listView1_DoubleClick(object sender, System.EventArgs e)
        {
            var similarImages = NeuralEngine.Instance.FindSimilar(listView1.SelectedItems[0].Tag as string);
            pictureBox1.Image = new Bitmap(listView1.SelectedItems[0].Tag as string);
            label2.Text = Path.GetFileName(listView1.SelectedItems[0].Tag as string);
            UpdateList(similarImages);
        }

        private void UpdateList(List<KeyValuePair<string, Bitmap>> similarImages)
        {
            listView1.Clear();
            var view = new ListViewGroup("Similiar Image");
            listView1.Groups.Add(view);
            imageList1.Images.Clear();
            for (var index = 0; index < similarImages.Count; index++)
            {
                var image = similarImages[index];
                imageList1.Images.Add(image.Value);
                var imageName = Path.GetFileName(image.Key);
                var item = new ListViewItem(imageName, index, view) { Tag = image.Key };
                listView1.Items.Add(item);
            }
        }
    }
}