using Microsoft.Practices.Unity;
using My2Cents.HTC.AHPilotStats.DataRepository;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class DeleteForm : Form
    {
        public DeleteForm()
        {
            InitializeComponent();
            PopulateDropList();
        }

        [Dependency]
        public IRegistry Registry { get; set; }

        public void PopulateDropList()
        {
            pilotListCmbBox.DataSource = Registry.PilotNamesSet;
            pilotListCmbBox.Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void deletePilotButton_Click(object sender, EventArgs e)
        {
            if (pilotListCmbBox.SelectedItem == null)
                return;

            var pilotToDelete = pilotListCmbBox.SelectedItem.ToString();
            if (pilotToDelete == "<Select pilot to delete>")
                return;

            var result = MessageBox.Show(this, string.Format("Are you sure you want to delete {0}?", pilotToDelete), "Confirm Delete", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes)
                return;

            Registry.RemovePilot(pilotToDelete);
            Registry.PilotNamesSet.Remove(pilotToDelete);

            var found = false;
            try
            {
                var dinf = new DirectoryInfo("data");
                foreach (var finf in dinf.GetFiles().Where(finf => finf.Name.Contains(pilotToDelete)))
                {
                    File.Delete(finf.FullName);
                    found = true;
                }

                if (!found)
                    throw new FileNotFoundException();

            }
            catch (FileNotFoundException)
            {
                statusLabel.Visible = true;
                statusLabel.Text = string.Format("Failed to delete {0} or no data files exist.", pilotToDelete);
                return;
            }

            statusLabel.Visible = true;
            statusLabel.Text = string.Format("Pilot {0} has been sucessfully deleted.", pilotToDelete);
            pilotListCmbBox.Refresh();
            
            ((MainMDI)MdiParent).RefreshPilotLists(pilotToDelete);

        }
    }
}