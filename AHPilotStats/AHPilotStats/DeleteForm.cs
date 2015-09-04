using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class DeleteForm : Form
    {
        public DeleteForm()
        {
            InitializeComponent();
            PopulateDropList();

        }

        public void PopulateDropList()
        {
            this.pilotListCmbBox.DataSource = Registry.Instance.PilotNamesSet;
            this.pilotListCmbBox.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deletePilotButton_Click(object sender, EventArgs e)
        {
            if (this.pilotListCmbBox.SelectedItem == null)
                return;

            string pilotToDelete = this.pilotListCmbBox.SelectedItem.ToString();
            if (pilotToDelete == "<Select pilot to delete>")
                return;

            DialogResult result = MessageBox.Show(this, string.Format("Are you sure you want to delete {0}?", pilotToDelete), "Confirm Delete", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
                return;

            Registry.Instance.RemovePilot(pilotToDelete);
            Registry.Instance.PilotNamesSet.Remove(pilotToDelete);

            bool found = false;
            try
            {
                DirectoryInfo dinf = new DirectoryInfo("data");
                foreach (FileInfo finf in dinf.GetFiles())
                {
                    if (finf.Name.Contains(pilotToDelete))
                    {
                        File.Delete(finf.FullName);
                        found = true;
                    }
                }

                if (!found)
                    throw new FileNotFoundException();

            }
            catch (FileNotFoundException)
            {
                this.statusLabel.Visible = true;
                this.statusLabel.Text = string.Format("Failed to delete {0} or no data files exist.", pilotToDelete);
                return;
            }

            this.statusLabel.Visible = true;
            this.statusLabel.Text = string.Format("Pilot {0} has been sucessfully deleted.", pilotToDelete);
            this.pilotListCmbBox.Refresh();
            
            ((MainMDI)this.MdiParent).RefreshPilotLists(pilotToDelete);

        }
    }
}