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
    public partial class ProjectForm : Form
    {

        Project instance;
        public ProjectForm(Project p)
        {
            InitializeComponent();

            tbProjectName.Text = p.ProjectName;
            tbProjectNoOfParticipants.Text = p.ProjectNoOfParticipants.ToString();

            instance = p;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (!ValidateChildren())
            {
                MessageBox.Show("error");
                return;
            }
            else
            {
                instance.ProjectName = tbProjectName.Text;
                instance.ProjectNoOfParticipants = int.Parse(tbProjectNoOfParticipants.Text);

                tbProjectName.Clear();
                tbProjectNoOfParticipants.Clear();

                this.Close();
            }
        }

        private void tbProjectName_Validating(object sender, CancelEventArgs e)
        {
            if (tbProjectName.Text.Length < 3)
            {
                errorProvider.SetError((Control)sender, "Set a real name!");
                e.Cancel = true;
            }
        }

        private void tbProjectName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);

        }

        private void tbProjectNoOfParticipants_Validating(object sender, CancelEventArgs e)
        {

            int nr = 0;

            try {
                nr = int.Parse(tbProjectNoOfParticipants.Text);
                if (int.Parse(tbProjectNoOfParticipants.Text) < 0) //.text
                {
                    errorProvider.SetError((Control)sender, "Set a strictly positive number of participants");
                    e.Cancel = true;
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void tbProjectNoOfParticipants_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
