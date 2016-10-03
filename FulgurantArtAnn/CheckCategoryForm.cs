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
    }
}
