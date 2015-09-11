using Microsoft.Practices.Unity;
using My2Cents.HTC.AHPilotStats.DataRepository;
using My2Cents.HTC.AHPilotStats.DependencyManagement;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Utilities;
using System;
using System.Linq;
using System.Windows.Forms;

namespace My2Cents.HTC.AHPilotStats
{ 
    public partial class MainMDI : Form
    {
        public MainMDI()
        {
            InitializeComponent();
            PopulatePilotDropDownMenuItems();
            PopulateSquadDropDownMenuItems();
        }

        [Dependency]
        public IRegistry Registry { get; set; }

        public void RefreshPilotLists(string reloadedPilotName)
        {
            Registry.PopulatePilotList();
            PopulatePilotDropDownMenuItems();
            Registry.ReloadPilot(reloadedPilotName);
        }

        public void RefreshSquadMemberPilotLists(string squadName)
        {
            try
            {
                var squad = Registry.GetSquad(squadName);
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
            PilotsMenuItems.Clear();

            // now reconstruct the lot.
            var deletePilotsItem = new ToolStripMenuItem("&Delete Pilot");
            deletePilotsItem.Click += OnDeletePilotClicked;
            PilotsMenuItems.Add(deletePilotsItem);

            var seperator = new ToolStripSeparator();
            PilotsMenuItems.Add(seperator);

            foreach (var item in Registry.PilotNamesSet.Select(pilot => new ToolStripMenuItem(pilot)))
            {
                item.Click += OnPilotClicked;
                PilotsMenuItems.Add(item);
            }
        }

        public void PopulateSquadDropDownMenuItems()
        {
            foreach (var item in Registry.SquadNamesSet.Select(name => new ToolStripMenuItem(name)))
            {
                item.Click += OnSquadClicked;
                SquadsMenuItem.Add(item);
            }
        }

        public void RefreshSquadMenuList()
        {
            Registry.PopulateSquadList();
            PopulateSquadDropDownMenuItems();
        }

        private void OnDeletePilotClicked(object sender, EventArgs args)
        {
            if (FindAndWarnOnOpenDeletePilotWindowDialog() || FindAndWarnOnOpenLoaderDialog() || FindAndWarnOnOpenStatsWindowDialog())
                return;

            var form = ServiceLocator.Instance.Resolve<DeleteForm>();
            form.MdiParent = this;
            form.Show();
        }


        private void OnPilotClicked(object sender, EventArgs args)
        {
            if (FindAndWarnOnOpenLoaderDialog() || FindAndWarnOnOpenDeletePilotWindowDialog())
                return;

            var clicked = PilotsMenuItems.Cast<ToolStripItem>().SingleOrDefault(item => item.Pressed);
            OpenPilotStats(CommonUtils.ToUpperFirstChar(clicked.Text));
        }

        private void OnSquadClicked(object sender, EventArgs args)
        {
            var selectedSquad = "";
            try
            {
                if (FindAndWarnOnOpenLoaderDialog() || FindAndWarnOnOpenDeletePilotWindowDialog())
                    return;

                var clicked = SquadsMenuItem.Cast<ToolStripItem>().SingleOrDefault(item => item.Pressed);

                selectedSquad = clicked.Text;
                BuildAndDisplaySquadForm(selectedSquad, Registry.GetSquad(selectedSquad));
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

            var form = BuildPilotStatsForm(selectedPilot, false);
            form.Show();
            form.Focus();
        }

        private PilotStatsForm BuildPilotStatsForm(string selectedPilot, bool isCompositeData)
        {
            var form = ServiceLocator.Instance.Resolve<PilotStatsForm>(
                new ResolverOverride[]
                {
                    new ParameterOverride("pilotName", selectedPilot),
                    new ParameterOverride("isCompositeData", isCompositeData)
                });
            form.MdiParent = this;
            return form;
        }

        private bool FindAndDisplayStatsWindow(string selectedPilot)
        {
            var childForm = MdiChildren.OfType<PilotStatsForm>()
                .SingleOrDefault(form => form.PilotName == selectedPilot);

            if (childForm != null)
            {
                childForm.Show();
                childForm.Focus();
                return true;
            }

            return false;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = ServiceLocator.Instance.Resolve<About>();
            about.ShowDialog(this);
        }

        private void editConnectionTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectorForm = ServiceLocator.Instance.Resolve<NetConnectionSelectorForm>();
            selectorForm.Show(this);
        }

        private void loadNewPilotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindAndWarnOnOpenStatsWindowDialog() || FindAndWarnOnOpenLoaderDialog() || 
                FindAndWarnOnOpenStatsWindowDialog() || FindAndWarnOnOpenDeletePilotWindowDialog())
                return;

            var form = ServiceLocator.Instance.Resolve<PilotDataLoaderForm>();
            form.Show(this);         
        }

        private void BuildAndDisplaySquadForm(string squadName, Squad squad)
        {
            Registry.RemovePilot(squadName);

            var startTour = squad.GetMinTour(squad);
            var endTour = squad.GetMaxTour(squad);
            var inException = false;
            var reloadPilotsRequired = false;

            try
            {
                Registry.GetSquad(squadName).CheckSquadInSync(startTour, endTour);

                var squadBuilder = ServiceLocator.Instance.Resolve<SquadScoreStatsBuilder>();
                for (var tour = startTour; tour <= endTour; tour++)
                {
                    var score = squadBuilder.BuildSquadScoreObject(squad, tour);
                    if (score.TourDetails.Contains("[NO DATA]"))
                        continue;

                    var stats = squadBuilder.BuildSquadStatsObject(squad, tour, ref reloadPilotsRequired);
                    var squadStatsReg = new PilotStats();
                    Registry.AddPilotStatsToRegistry(squadName, squadStatsReg);
                    Registry.ConstructScoresForPilot(score, squadName);
                    Registry.ConstructStatsForPilot(stats, squadName);
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

            var form = BuildPilotStatsForm(squadName, true);
            form.CompositeObjVsObjDataIncomplete = reloadPilotsRequired;
            form.Show();
        }

        private void defineSquadronToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = ServiceLocator.Instance.Resolve<DefineSquadronForm>();
            form.Show(this);
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"http://www.paypal.com");
        }

        private void MainMDI_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ShowTipsAtStart)
            {
                ServiceLocator.Instance.Resolve<StartupTips>().ShowDialog(this);
            }
        }

        private void tipsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServiceLocator.Instance.Resolve<StartupTips>().ShowDialog(this);
        }

        private ToolStripItemCollection PilotsMenuItems
        {
            get { return ((ToolStripMenuItem)menuStrip1.Items[1]).DropDown.Items; }
        }

        private ToolStripItemCollection SquadsMenuItem
        {
            get { return ((ToolStripMenuItem)menuStrip1.Items[2]).DropDown.Items; }
        }
    }
}