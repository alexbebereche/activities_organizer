using Activities_organizer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activities_organizer
{
    public partial class CategoryForm : Form
    {

        Category instance;

        public CategoryForm(Category c)
        {
            InitializeComponent();

            cbDomain.Text = c.Domain;
            tbCategory.Text = c.CategoryName;


            instance = c;


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (!ValidateChildren())
            {
                MessageBox.Show("error");
                return;
            }
            else
            {
                instance.Domain = cbDomain.Text;
                instance.CategoryName = tbCategory.Text;

                

                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbDomain_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbDomain.Text))
            {
                errorProvider.SetError((Control)sender, "Set a Domain!");
                e.Cancel = true;
            }
        }

        private void cbDomain_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
        }

        private void tbCategory_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbCategory.Text))
            {
                errorProvider.SetError((Control)sender, "Set a Category!");
                e.Cancel = true;
            }
        }

        private void tbCategory_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
        }
    }
}
