using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class DefineSquadronForm : Form
    {
        public DefineSquadronForm()
        {
            InitializeComponent();
        }

        private Squad _squad;

        private void DefineSquadronForm_Load(object sender, EventArgs e)
        {
            this.grpBoxSquadDetails.Enabled = false;
            this.squad_SquadMemberDataGridView.Visible = false;
            this.cmbBoxSquadPicker.DataSource = Registry.Instance.SquadNamesSet;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            _squad.Serialise();
            ((MainMDI)this.MdiParent).RefreshSquadMenuList();
            this.Close();
        }


        private void btnNewSquad_Click(object sender, EventArgs e)
        {
            _squad = new Squad();
            this.grpBoxSquadDetails.Enabled = true;
            this.squad_SquadMemberDataGridView.Visible = true;
            this.squad_SquadMemberDataGridView.DataSource = _squad.Members;
            this.txtBoxHomePage.Text = "";
            this.textBox1.Text = "";
        }


        private void cmbBoxSquadPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxSquadPicker.SelectedItem != null)
            {
                try
                {
                    _squad = Registry.Instance.GetSquad(cmbBoxSquadPicker.SelectedItem.ToString());
                }
                catch (SquadDoesNotExistInRegistryException ex)
                {
                    MessageBox.Show(ex.Message, "Unexpect Squad load error!");
                    return;
                }

                this.grpBoxSquadDetails.Enabled = true;
                this.squad_SquadMemberDataGridView.Visible = true;
                this.squad_SquadMemberDataGridView.DataSource = _squad.Members;
                this.txtBoxHomePage.Text = _squad.HomePage;
                this.textBox1.Text = _squad.SquadName;
            }
        }


        private void txtBoxHomePage_TextChanged(object sender, EventArgs e)
        {
            _squad.HomePage = txtBoxHomePage.Text;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _squad.SquadName = textBox1.Text;
        }

        private void squad_SquadMemberDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (squad_SquadMemberDataGridView.Columns[e.ColumnIndex].Name == "endTourDataGridViewTextBoxColumn")
            {
                if (Utility.IsInteger(e.FormattedValue.ToString()))
                {
                    int startTour = (int)squad_SquadMemberDataGridView[e.ColumnIndex-1, e.RowIndex].Value;
                    int endTour = Convert.ToInt32(e.FormattedValue.ToString());
                    if (endTour < startTour)
                    {
                        squad_SquadMemberDataGridView.Rows[e.RowIndex].ErrorText = "End tour cant be before start tour";
                        e.Cancel = true;
                    }
                }
                else
                {
                    squad_SquadMemberDataGridView.Rows[e.RowIndex].ErrorText = "Tours must be whole numbers";
                    e.Cancel = true;
                }
            }
            if (squad_SquadMemberDataGridView.Columns[e.ColumnIndex].Name == "startTourDataGridViewTextBoxColumn")
            {
                if (Utility.IsInteger(e.FormattedValue.ToString()))
                {
                    int startTour = Convert.ToInt32(e.FormattedValue.ToString());
                    int endTour = (int)squad_SquadMemberDataGridView[e.ColumnIndex + 1, e.RowIndex].Value;
                    if (startTour > endTour)
                    {
                        squad_SquadMemberDataGridView.Rows[e.RowIndex].ErrorText = "Start tour cant be after end tour";
                        e.Cancel = true;
                    }
                }
                else
                {
                    squad_SquadMemberDataGridView.Rows[e.RowIndex].ErrorText = "Tours must be whole numbers";
                    e.Cancel = true;
                }
            }
        }

        private void squad_SquadMemberDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Clear the row error in case the user presses ESC.   
            squad_SquadMemberDataGridView.Rows[e.RowIndex].ErrorText = String.Empty;
        }


    }
}