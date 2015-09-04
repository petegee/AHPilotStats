using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using My2Cents.HTC.PilotScoreSvc;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Utilities;


namespace My2Cents.HTC.AHPilotStats
{ 
    public partial class MainMDI : Form
    {
        private PilotDataLoaderForm _LoaderForm;

        public MainMDI()
        {
            InitializeComponent();
            PopulatePilotDropDownMenuItems();
            PopulateSquadDropDownMenuItems();
        }

        public void RefreshPilotLists(string reloadedPilotName)
        {
            Registry.Instance.PopulatePilotList();
            PopulatePilotDropDownMenuItems();
            Registry.Instance.ReloadPilot(reloadedPilotName);
        }

        public void RefreshSquadMemberPilotLists(string squadName)
        {
            try
            {
                Squad squad = Registry.Instance.GetSquad(squadName);
                foreach (Squad.SquadMember squadMember in squad.Members)
                {
                    RefreshPilotLists(squadMember.PilotName);
                }
            }
            catch (SquadDoesNotExistInRegistryException ex)
            {
                Utility.WriteDebugTraceFile(ex);
                MessageBox.Show(ex.Message, "Unexpect Squad load error!");
            }
        }

        

        public void PopulatePilotDropDownMenuItems()
        {
            // delete all items to start with
            ((ToolStripMenuItem)this.menuStrip1.Items[1]).DropDownItems.Clear();

            // now reconstruct the lot.

            ToolStripMenuItem deletePilotsItem = new ToolStripMenuItem("&Delete Pilot");
            deletePilotsItem.Click += new EventHandler(OnDeletePilotClicked);
            ((ToolStripMenuItem)this.menuStrip1.Items[1]).DropDownItems.Add(deletePilotsItem);

            ToolStripSeparator seperator = new ToolStripSeparator();
            ((ToolStripMenuItem)this.menuStrip1.Items[1]).DropDownItems.Add(seperator);

            foreach (string pilot in Registry.Instance.PilotNamesSet)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(pilot);
                item.Click += new EventHandler(OnPilotClicked);
                ((ToolStripMenuItem)this.menuStrip1.Items[1]).DropDownItems.Add(item);
            }
        }

        public void PopulateSquadDropDownMenuItems()
        {
            foreach (string squad in Registry.Instance.SquadNamesSet)
            {
                if (!IsInDropDownList(squad, 2))
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(squad);
                    item.Click += new EventHandler(OnSquadClicked);
                    ((ToolStripMenuItem)this.menuStrip1.Items[2]).DropDownItems.Add(item);
                }
            }
        }

        public void RefreshSquadMenuList()
        {
            Registry.Instance.PopulateSquadList();
            PopulateSquadDropDownMenuItems();
        }


        private bool IsInDropDownList(string name, int menuIndex)
        {
            foreach (ToolStripItem item in ((ToolStripMenuItem)this.menuStrip1.Items[menuIndex]).DropDownItems)
            {
                if (item.Text == name)
                {
                    return true;
                }
            }
            return false;
        }


        private void OnDeletePilotClicked(object sender, EventArgs args)
        {
            if (FindAndWarnOnOpenDeletePilotWindowDialog())
                return;

            if (FindAndWarnOnOpenLoaderDialog())
                return;

            if (FindAndWarnOnOpenStatsWindowDialog())
                return;

            DeleteForm deleteForm = new DeleteForm();
            deleteForm.MdiParent = this; 
            deleteForm.Show();
        }


        private void OnPilotClicked(object sender, EventArgs args)
        {
            foreach (ToolStripItem item in ((ToolStripMenuItem)this.menuStrip1.Items[1]).DropDownItems)
            {
                if (item.Pressed)
                {
                    if (FindAndWarnOnOpenLoaderDialog())
                        return;
                    if (FindAndWarnOnOpenDeletePilotWindowDialog())
                        return;

                    string selectedPilot = item.ToString();
                    OpenPilotStats(CommonUtils.ToUpperFirstChar(selectedPilot));
                }
            }
        }

        private void OnSquadClicked(object sender, EventArgs args)
        {
            string selectedSquad = "";
            try
            {
                foreach (ToolStripMenuItem item in ((ToolStripMenuItem)this.menuStrip1.Items[2]).DropDownItems)
                {
                    if (item.Pressed)
                    {
                        if (FindAndWarnOnOpenLoaderDialog())
                            return;
                        if (FindAndWarnOnOpenDeletePilotWindowDialog())
                            return;

                        selectedSquad = item.ToString();
                        BuildAndDisplaySquadForm(selectedSquad, Registry.Instance.GetSquad(selectedSquad));
                    }
                }
            }
            catch (SquadDoesNotExistInRegistryException e)
            {
                Utility.WriteDebugTraceFile(e);
                MessageBox.Show(string.Format("Failed to compile squad \"{0}\" data.\nCheck that the pilots nominated in your squad have data.", selectedSquad ) );
            }
        }
  
        private bool FindAndWarnOnOpenLoaderDialog()
        {
            return InternalFindAndWarnOnDialog(typeof(PilotDataLoaderForm), "Please close the Data Loader form First");
        }


        private bool FindAndWarnOnOpenStatsWindowDialog()
        {
            return InternalFindAndWarnOnDialog(typeof(PilotStatsForm), "Please close any open Pilot Window(s) First");
        }

        private bool FindAndWarnOnOpenDeletePilotWindowDialog()
        {
            return InternalFindAndWarnOnDialog(typeof(DeleteForm), "Please close the Delete Pilot form First");
        }

        private bool InternalFindAndWarnOnDialog(Type formType, string message)
        {
            foreach (Form f in MdiChildren)
            {
                if (f.GetType() == formType)
                {
                    MessageBox.Show(this, message, "Error");
                    return true;
                }
            }
            return false;
        }

        private void OpenPilotStats(string selectedPilot)
        {
            if (!FindAndDisplayStatsWindow(selectedPilot))
            {
                PilotStatsForm statsForm = new PilotStatsForm(selectedPilot, false);
                statsForm.MdiParent = this;
                statsForm.Show();
                statsForm.Focus();            
            }
        }

        private bool FindAndDisplayStatsWindow(string selectedPilot)
        {
            PilotStatsForm childForm = null;
            foreach (Form f in MdiChildren)
            {
                if (f is PilotStatsForm)
                {
                    childForm = (PilotStatsForm)f;
                    if (childForm.PilotName == selectedPilot)
                    {
                        childForm.Show();
                        childForm.Focus();
                        return true;
                    }
                }
            }
            return false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog(this);
        }

        private void editConnectionTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NetConnectionSelectorForm selectorForm = new NetConnectionSelectorForm();
            selectorForm.MdiParent = this;
            selectorForm.Show();
        }

        private void loadNewPilotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindAndWarnOnOpenStatsWindowDialog())
                return;

            if (FindAndWarnOnOpenLoaderDialog())
                return;

            if (FindAndWarnOnOpenStatsWindowDialog())
                return;

            if (FindAndWarnOnOpenDeletePilotWindowDialog())
                return;

            _LoaderForm = new PilotDataLoaderForm(this);
            _LoaderForm.MdiParent = this;
            _LoaderForm.Show();            
        }

        private void BuildAndDisplaySquadForm(string squadName, Squad squad)
        {
            Registry.Instance.RemovePilot(squadName);

            int startTour = squad.GetMinTour(squad);
            int endTour = squad.GetMaxTour(squad);
            bool inException = false;
            bool reloadPilotsRequired = false;

            try
            {
                Registry.Instance.GetSquad(squadName).CheckSquadInSync(startTour, endTour);
                
                SquadScoreStatsBuilder squadBuilder = new SquadScoreStatsBuilder();
                for (int tour = startTour; tour <= endTour; tour++)
                {
                    AcesHighPilotScore score = squadBuilder.BuildSquadScoreObject(squad, tour);
                    if (score.TourDetails.Contains("[NO DATA]"))
                        continue;

                    AcesHighPilotStats stats = squadBuilder.BuildSquadStatsObject(squad, tour, ref reloadPilotsRequired);
                    Registry.PilotStatsRegistry squadStatsReg = new Registry.PilotStatsRegistry();
                    Registry.Instance.AddPilotStatsToRegistry(squadName, squadStatsReg);
                    Registry.Instance.ConstructScoresForPilot(score, squadName);
                    Registry.Instance.ConstructStatsForPilot(stats, squadName);
                }
            }
            catch (SquadOutOfSyncException ex)
            {
                MessageBox.Show(ex.Text, "Squad out of sync error");
                inException = true;
            }
            catch (SquadDoesNotExistInRegistryException squadEx)
            {
                MessageBox.Show(squadEx.Text, "Squad loading error");
                inException = true;
            }
            

            if (!FindAndDisplayStatsWindow(squadName) && !inException)
            {
                PilotStatsForm form = new PilotStatsForm(squadName, true);
                form.MdiParent = this;
                form.CompositeObjVsObjDataIncomplete = reloadPilotsRequired;
                form.Show();
            }
        }

        private void defineSquadronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DefineSquadronForm form = new DefineSquadronForm();
            form.MdiParent = this;
            form.Show();
        }

        private void newsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.major.geek.nz/AKUAG/Resources_Files/News.aspx");
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.paypal.com");
        }

        private void MainMDI_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ShowTipsAtStart)
            {
                StartupTips startupDlg = new StartupTips();
                startupDlg.ShowDialog(this);
            }
        }

        private void tipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartupTips startupDlg = new StartupTips();
            startupDlg.ShowDialog(this);
        }
    }
}