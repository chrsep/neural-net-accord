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
            var similarImages = NeuralEngine.Instance.FindSimilar(fullPath);

            imageList1.Images.Clear();
            foreach (var image in similarImages)
                imageList1.Images.Add(image);

            listView1.Clear();
            for (var i = 0; i < imageList1.Images.Count; i++)
            {
                var item = new ListViewItem(i.ToString(), i);
                listView1.Items.Add(item);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void ArtDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            _parentform.Show();
        }
    }
}