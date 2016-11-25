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
    public partial class CheckCategoryForm : Form
    {
        private readonly Form _parentForm;
        private readonly NeuralEngine _engine;

        public CheckCategoryForm(Form parent)
        {
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
            _parentForm = parent;
            _engine = NeuralEngine.Instance;
        }

        private void linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _parentForm.Show();
            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            var image = Bitmap.FromFile(openFileDialog1.FileName);
            pictureBoxArt.Image = image;
            button1.Visible = true;
            labelCategory.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var processedImage = _engine.PreprocessImage(new Bitmap(pictureBoxArt.Image));
            pictureBoxArt.Image = processedImage;
            string category = _engine.Classify(processedImage);
            labelCategory.Text = "Category: " + category;
        }
    }
}
