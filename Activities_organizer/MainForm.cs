using Activities_organizer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activities_organizer
{
    public partial class MainForm : Form
    {

        List<Activity> activities;
        List<Category> categories;
        List<Project> projects;

        public MainForm()
        {
            activities = new List<Activity>();
            categories = new List<Category>();
            projects = new List<Project>();

            InitializeComponent();
        }

        private void DisplayListView()
        {
            lvActivities.Items.Clear();

            foreach (Activity a in activities)
            {
                //Category c = new Category();

                ListViewItem item = new ListViewItem(a.Category.Domain);

                item.SubItems.Add(a.Category.CategoryName);
                item.SubItems.Add(a.ActivityName);
                item.SubItems.Add(a.ActivityEndTime.ToShortDateString());


                item.SubItems.Add(a.Project.ProjectName);
                item.SubItems.Add(a.Project.ProjectNoOfParticipants.ToString());

                //item.SubItems.Add(a)


                item.Tag = a;
                lvActivities.Items.Add(item);
            }

            lbCount.Text = activities.Count.ToString();
        }


        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            //Category c = new Category();

            //CategoryForm categoryForm = new CategoryForm(c);
            //categoryForm.ShowDialog();


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (!ValidateChildren())
            {
                MessageBox.Show("Error");
                return;
            }
            else
            {


                string domain = cbDomain.Text.Trim();
                string category = tbCategory.Text.Trim();
                string name = tbName.Text.Trim();
                
                DateTime date = dtpEndTime.Value;
             

                //string projectName = 


                Category c = new Category(category, domain);
                categories.Add(c);


                Project p = new Project();



                //Activity a = new Activity(name, date, domain, category, p);
                Activity a = new Activity(name, date, c, p);
                //a.Category.CategoryName = name;
                //a.Category.Domain = domain;


                activities.Add(a);



                cbDomain.ResetText();
                tbCategory.Clear();
                tbName.Clear();
                dtpEndTime.ResetText();




                DisplayListView();

            }
        }





        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Activity a = lvActivities.SelectedItems[0].Tag as Activity;
                ProjectForm form = new ProjectForm(a.Project);
                form.ShowDialog();

                DisplayListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvActivities_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Activity a = lvActivities.SelectedItems[0].Tag as Activity;

            Activity newActivity = (Activity)a.Clone();
            activities.Add(newActivity);

            DisplayListView();
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

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            if (tbName.Text.Length < 3)
            {
                errorProvider.SetError((Control)sender, "Set a real name!");
                e.Cancel = true;
            }
        }

        private void tbName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);

        }

        private void dtpEndTime_Validating(object sender, CancelEventArgs e)
        {
            if (dtpEndTime.Value > DateTime.Now)
            {
                errorProvider.SetError((Control)sender, "Set a date to not be in the future!");
                e.Cancel = true;
            }
        }

        private void dtpEndTime_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
        }

        private void serializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.Create("serialized.bin"))
            {
                formatter.Serialize(stream, activities);
            }
        }

        private void deserializeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = File.OpenRead("serialized.bin"))
            {
                activities = (List<Activity>)formatter.Deserialize(stream);

                DisplayListView();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog(); //choose where to save the file
            saveFileDialog.Filter = "Text File | *.txt";
            saveFileDialog.Title = "Save as text file";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //going with the recommended approach
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.WriteLine("Domain, Category, Name, End Time ,Project Name, No. Of Participants");

                    foreach (var a in activities)
                    {
                        try
                        {
                            sw.WriteLine("\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\""
                                  , a.Category.Domain.Replace("\"", "\"\"")
                                  , a.Category.CategoryName.Replace("\"", "\"\"")
                                  , a.ActivityName.Replace("\"", "\"\"")
                                  , a.ActivityEndTime.ToShortDateString().Replace("\"", "\"\"")
                                  , a.Project.ProjectName.Replace("\"", "\"\"")
                                  , a.Project.ProjectNoOfParticipants.ToString().Replace("\"", "\"\"")
                                  );
                        }
                        catch (Exception ex)
                        {
                            sw.WriteLine("\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\""
                                 , a.Category.Domain.Replace("\"", "\"\"")
                                 , a.Category.CategoryName.Replace("\"", "\"\"")
                                 , a.ActivityName.Replace("\"", "\"\"")
                                 , a.ActivityEndTime.ToShortDateString().Replace("\"", "\"\"")
                                 , "".Replace("\"", "\"\"")
                                 , a.Project.ProjectNoOfParticipants.ToString().Replace("\"", "\"\"")
                                 );
                        }

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            {
               
                activities.Sort();
                DisplayListView();
            }
        }

        private void editActivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Activity a = lvActivities.SelectedItems[0].Tag as Activity;
                activities.Remove(a);

                DisplayListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Activity a = lvActivities.SelectedItems[0].Tag as Activity;
                CategoryForm form = new CategoryForm(a.Category);


                form.ShowDialog();

                DisplayListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Activity a = lvActivities.SelectedItems[0].Tag as Activity;
                Category c = lvActivities.SelectedItems[0].Tag as Category;
                categories.Remove(c);

                a.Category.CategoryName = "";
                a.Category.Domain = "";

                
                DisplayListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void deleteProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Activity a = lvActivities.SelectedItems[0].Tag as Activity;
                Project p = lvActivities.SelectedItems[0].Tag as Project;


                projects.Remove(p);

                a.Project.ProjectName = "";
                a.Project.ProjectNoOfParticipants = 0;


                DisplayListView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
