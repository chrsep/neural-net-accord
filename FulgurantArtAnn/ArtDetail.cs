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
    public partial class ArtDetail : Form
    {
        BrowseForm form;
        public ArtDetail()
        {
            InitializeComponent();

            var data = form.picture();

            pictureBox1.Image = Image.FromFile(@"..\");
        }
    }
}
