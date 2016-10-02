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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void buttonAddArt_Click(object sender, EventArgs e)
        {
            var form = new AddArtForm();
            form.Show();
            Hide();
        }

        private void buttonCheckCategory_Click(object sender, EventArgs e)
        {
            var form = new CheckCategoryForm();
            form.Show();
            Hide();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var form = new BrowseForm(this);
            form.Show();
            Hide();
        }

        private void linkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           Application.Exit();
        }
    }
}
