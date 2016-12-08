using System;
using System.Drawing;
using System.Windows.Forms;

namespace FulgurantArtAnn
{
    public partial class CheckCategoryForm : Form
    {
        private readonly Form _parentForm;

        public CheckCategoryForm(Form parent)
        {
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            _parentForm = parent;
            
        }

        private void linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _parentForm.Show();
            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var image = new Bitmap(openFileDialog1.FileName);
            pictureBoxArt.Image = image;
            button1.Visible = true;
            labelCategory.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var processedImage = NeuralEngine.PreprocessImage(new Bitmap(pictureBoxArt.Image));
            pictureBoxArt.Image = processedImage;
            var category = NeuralEngine.Instance.Classify(processedImage);
            labelCategory.Text = "Category: " + category;
        }

        private void CheckCategoryForm_Load(object sender, EventArgs e)
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
                NeuralEngine.Instance.TrainClasificationNetwork();
            }
        }
    }
}