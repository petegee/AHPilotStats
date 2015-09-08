using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Utilities;
using System;
using System.Linq;
using System.Windows.Forms;


namespace My2Cents.HTC.AHPilotStats
{ 
    public partial class MainMDI : Form
    {
        private PilotDataLoaderForm _loaderForm;

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
                var squad = Registry.Instance.GetSquad(squadName);
                foreach (var squadMember in squad.Members)
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
            ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems.Clear();

            // now reconstruct the lot.
            var deletePilotsItem = new ToolStripMenuItem("&Delete Pilot");
            deletePilotsItem.Click += OnDeletePilotClicked;
            ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems.Add(deletePilotsItem);

            var seperator = new ToolStripSeparator();
            ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems.Add(seperator);

            foreach (var item in Registry.Instance.PilotNamesSet.Select(pilot => new ToolStripMenuItem(pilot)))
            {
                item.Click += OnPilotClicked;
                ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems.Add(item);
            }
        }

        public void PopulateSquadDropDownMenuItems()
        {
            foreach (var item in from squad in Registry.Instance.SquadNamesSet where !IsInDropDownList(squad, 2) select new ToolStripMenuItem(squad))
            {
                item.Click += OnSquadClicked;
                ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems.Add(item);
            }
        }

        public void RefreshSquadMenuList()
        {
            Registry.Instance.PopulateSquadList();
            PopulateSquadDropDownMenuItems();
        }


        private bool IsInDropDownList(string name, int menuIndex)
        {
            return ((ToolStripMenuItem) menuStrip1.Items[menuIndex]).DropDownItems
                .Cast<ToolStripItem>()
                .Any(item => item.Text == name);
        }


        private void OnDeletePilotClicked(object sender, EventArgs args)
        {
            if (FindAndWarnOnOpenDeletePilotWindowDialog())
                return;

            if (FindAndWarnOnOpenLoaderDialog())
                return;

            if (FindAndWarnOnOpenStatsWindowDialog())
                return;

            var deleteForm = new DeleteForm {MdiParent = this};

            deleteForm.Show();
        }


        private void OnPilotClicked(object sender, EventArgs args)
        {
            foreach (var item in ((ToolStripMenuItem)menuStrip1.Items[1]).DropDownItems.Cast<ToolStripItem>()
                .Where(item => item.Pressed))
            {
                if (FindAndWarnOnOpenLoaderDialog())
                    return;

                if (FindAndWarnOnOpenDeletePilotWindowDialog())
                    return;

                OpenPilotStats(CommonUtils.ToUpperFirstChar(item.ToString()));
            }
        }

        private void OnSquadClicked(object sender, EventArgs args)
        {
            var selectedSquad = "";
            try
            {
                foreach (var item in ((ToolStripMenuItem)menuStrip1.Items[2]).DropDownItems.Cast<ToolStripMenuItem>()
                    .Where(item => item.Pressed))
                {
                    if (FindAndWarnOnOpenLoaderDialog())
                        return;
                    if (FindAndWarnOnOpenDeletePilotWindowDialog())
                        return;

                    BuildAndDisplaySquadForm(selectedSquad, Registry.Instance.GetSquad(item.ToString()));
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
            if (MdiChildren.All(f => f.GetType() != formType)) 
                return false;

            MessageBox.Show(this, message, "Error");
            return true;
        }

        private void OpenPilotStats(string selectedPilot)
        {
            if (FindAndDisplayStatsWindow(selectedPilot)) 
                return;

            var statsForm = new PilotStatsForm(selectedPilot, false) {MdiParent = this};
            statsForm.Show();
            statsForm.Focus();
        }

        private bool FindAndDisplayStatsWindow(string selectedPilot)
        {
            foreach (var childForm in MdiChildren.OfType<PilotStatsForm>()
                .Where(childForm => childForm.PilotName == selectedPilot))
            {
                childForm.Show();
                childForm.Focus();
                return true;
            }
            return false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog(this);
        }

        private void editConnectionTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectorForm = new NetConnectionSelectorForm {MdiParent = this};
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

            _loaderForm = new PilotDataLoaderForm(this) {MdiParent = this};
            _loaderForm.Show();            
        }

        private void BuildAndDisplaySquadForm(string squadName, Squad squad)
        {
            Registry.Instance.RemovePilot(squadName);

            var startTour = squad.GetMinTour(squad);
            var endTour = squad.GetMaxTour(squad);
            var inException = false;
            var reloadPilotsRequired = false;

            try
            {
                Registry.Instance.GetSquad(squadName).CheckSquadInSync(startTour, endTour);

                var squadBuilder = new SquadScoreStatsBuilder();
                for (var tour = startTour; tour <= endTour; tour++)
                {
                    var score = squadBuilder.BuildSquadScoreObject(squad, tour);
                    if (score.TourDetails.Contains("[NO DATA]"))
                        continue;

                    var stats = squadBuilder.BuildSquadStatsObject(squad, tour, ref reloadPilotsRequired);
                    var squadStatsReg = new Registry.PilotStatsRegistry();
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


            if (FindAndDisplayStatsWindow(squadName) || inException) 
                return;

            var form = new PilotStatsForm(squadName, true)
            {
                MdiParent = this,
                CompositeObjVsObjDataIncomplete = reloadPilotsRequired
            };
            form.Show();
        }

        private void defineSquadronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DefineSquadronForm { MdiParent = this }.Show();
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.paypal.com");
        }

        private void MainMDI_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ShowTipsAtStart)
            {
                new StartupTips().ShowDialog(this);
            }
        }

        private void tipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new StartupTips().ShowDialog(this);
        }
    }
}