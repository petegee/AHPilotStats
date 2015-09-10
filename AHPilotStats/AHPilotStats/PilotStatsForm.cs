using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DgvFilterPopup;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.AHPilotStats.ExtensionMethods;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class PilotStatsForm : Form
    {
        private bool _compositeObjVsObjDataIncomplete;
        private readonly bool _isCompositeData;

        private readonly GraphBuilder _grapher;
        private readonly DgvFilterManager _attackScoreFilterManager = new DgvFilterManager();
        private readonly DgvFilterManager _attackStatsFilterManager = new DgvFilterManager();
        private readonly DgvFilterManager _bomberScoreFilterManager = new DgvFilterManager();
        private readonly DgvFilterManager _bomberStatsFilterManager = new DgvFilterManager();
        private readonly DgvFilterManager _fighterScoreFilterManager = new DgvFilterManager();
        private readonly DgvFilterManager _fighterStatsFilterManager = new DgvFilterManager();
        private readonly DgvFilterManager _vehicleScoreFilterManager = new DgvFilterManager();
        private readonly DgvFilterManager _vehicleStatsFilterManager = new DgvFilterManager();

        public string PilotName { get; private set; }

        public bool CompositeObjVsObjDataIncomplete
        {
            get { return _compositeObjVsObjDataIncomplete; }
            set {
                _compositeObjVsObjDataIncomplete = _isCompositeData && value;
            }
        }


        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var control = (TabControl) sender;

            if (control.SelectedTab.Text != "Obj Vs Obj" || !_compositeObjVsObjDataIncomplete) 
                return;

            const string message = "Some pilots in your squad need reloading to get the new 'Died In' data.\n" +
                                   "Identify the pilots/tours without 'Died In' data, and reload those.\n" +
                                   "Until then there will some gaps in your sqauds data.";

            MessageBox.Show(message, "Warning");
        }

        private void fighterStatsDODataGridView_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            fighterScoresDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void fighterStatsDODataGridView1_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            fighterStatsDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void attackScoresDODataGridView_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            attackScoresDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void attackStatsDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            attackStatsDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void bomberScoresDODataGridView_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            bomberScoresDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void bomberStatsDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            bomberStatsDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void vehicleBoatScoresDODataGridView_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            vehicleBoatScoresDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void vehicleBoatStatsDODataGridView_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            vehicleBoatStatsDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        public class NameValuePair
        {
            public string Name;
            public string Value;

            public NameValuePair(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }

        #region Construction

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Construction
        //
        //////////////////////////////////////////////////////////////////////////////////
        public PilotStatsForm(string pilotName, bool isCompositeData)
        {
            InitializeComponent();

            BindDataToGrids(pilotName);

            Text = pilotName;
            _isCompositeData = isCompositeData;
            PilotName = pilotName;

            _grapher = new GraphBuilder(pilotName);

            PopulateTourTypeFilterComboBox();

            foreach (var model in Registry.Instance.ModelSet)
                cmboxModelSelector.Items.Add(model);

            PopulateObjVsObjTourDropDownList();
            cmboxModelSelector.Enabled = false;
            cmbBoxObjVObjTourList.Enabled = true;

            cmbBoxMode.SelectedItem = "Fighter";
            plotSurface2D.Hide();
        }


        private void BindDataToGrids(string pilotName)
        {
            fighterScoresDODataGridView.DataSource =
                Utility.CreateDataTableFromList(Registry.Instance.GetPilotStats(pilotName).FighterScoresList);
            _fighterScoreFilterManager.ColumnFilterAdding += ColumnFilterAdding;
            _fighterScoreFilterManager.DataGridView = fighterScoresDODataGridView;

            fighterStatsDODataGridView.DataSource =
                Utility.CreateDataTableFromList(Registry.Instance.GetPilotStats(pilotName).FighterStatsList);
            _fighterStatsFilterManager.ColumnFilterAdding += ColumnFilterAdding;
            _fighterStatsFilterManager.DataGridView = fighterStatsDODataGridView;

            attackScoresDODataGridView.DataSource =
                Utility.CreateDataTableFromList(Registry.Instance.GetPilotStats(pilotName).AttackScoresList);
            _attackScoreFilterManager.ColumnFilterAdding += ColumnFilterAdding;
            _attackScoreFilterManager.DataGridView = attackScoresDODataGridView;

            attackStatsDODataGridView.DataSource =
                Utility.CreateDataTableFromList(Registry.Instance.GetPilotStats(pilotName).AttackStatsList);
            _attackStatsFilterManager.ColumnFilterAdding += ColumnFilterAdding;
            _attackStatsFilterManager.DataGridView = attackStatsDODataGridView;

            bomberScoresDODataGridView.DataSource =
                Utility.CreateDataTableFromList(Registry.Instance.GetPilotStats(pilotName).BomberScoresList);
            _bomberScoreFilterManager.ColumnFilterAdding += ColumnFilterAdding;
            _bomberScoreFilterManager.DataGridView = bomberScoresDODataGridView;

            bomberStatsDODataGridView.DataSource =
                Utility.CreateDataTableFromList(Registry.Instance.GetPilotStats(pilotName).BomberStatsList);
            _bomberStatsFilterManager.ColumnFilterAdding += ColumnFilterAdding;
            _bomberStatsFilterManager.DataGridView = bomberStatsDODataGridView;

            vehicleBoatScoresDODataGridView.DataSource =
                Utility.CreateDataTableFromList(Registry.Instance.GetPilotStats(pilotName).VehicleBoatScoresList);
            _vehicleScoreFilterManager.ColumnFilterAdding += ColumnFilterAdding;
            _vehicleScoreFilterManager.DataGridView = vehicleBoatScoresDODataGridView;

            vehicleBoatStatsDODataGridView.DataSource =
                Utility.CreateDataTableFromList(Registry.Instance.GetPilotStats(pilotName).VehicleBoatStatsList);
            _vehicleStatsFilterManager.ColumnFilterAdding += ColumnFilterAdding;
            _vehicleStatsFilterManager.DataGridView = vehicleBoatStatsDODataGridView;
        }


        private void ColumnFilterAdding(object sender, ColumnFilterEventArgs e)
        {
            if (e.Column.Name == "Type")
                e.ColumnFilter = new DgvComboBoxColumnFilter();
        }


        private void PopulateTourTypeFilterComboBox()
        {
            var tourTypesList = new List<string>();

            SortableList<StatsDomainObject> pilotStats = null;
            try
            {
                pilotStats = Registry.Instance.GetPilotStats(PilotName).FighterStatsList;
            }
            catch (PilotDoesNotExistInRegistryException)
            {
                //ignore.
            }

            if (pilotStats == null)
                return;

            foreach (var fs in pilotStats.Where(fs => fs.TourType != "[UNKNOWN]")
                .Where(fs => !tourTypesList.Contains(fs.TourType)))
            {
                tourTypesList.Add(fs.TourType);
            }

            foreach (var tourType in tourTypesList)
                cmbxTourTypeFilter.Items.Add(tourType);

            cmbxTourTypeFilter.SelectedIndex = 0;
        }

        #endregion

        #region Graphs Tab

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Graphs Tab
        //
        //////////////////////////////////////////////////////////////////////////////////

        private void chkLstBoxSelectGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGraph();
        }


        private void RefreshGraph()
        {
            _grapher.ResetGraph(plotSurface2D);

            foreach (int i in chkLstBoxSelectGraph.CheckedIndices)
            {
                _grapher.AddPlot(plotSurface2D, chkLstBoxSelectGraph.Items[i].ToString(), cmbBoxMode.Text,
                    cmbxTourTypeFilter.Text);
            }
            _grapher.RefreshPlots(plotSurface2D);
        }


        private void cmbBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGraph();
        }


        private void chkLstBoxSelectGraph_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            RefreshGraph();
        }

        private void cmbxTourTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGraph();
        }

        #endregion

        #region Object Vs Object Tab

        //////////////////////////////////////////////////////////////////////////////////
        //
        // Object Vs Object Tab
        //
        //////////////////////////////////////////////////////////////////////////////////


        private void PopulateObjVsObjTourDropDownList()
        {
            var pilotsFighterStats = Registry.Instance.GetPilotStats(PilotName)
                .FighterStatsList
                .Where(stats => stats.TourType != "[UNKNOWN]")
                .DistinctBy(stats => stats.TourIdentfier)
                .OrderBy(stats => stats.TourIdentfier)
                .Select(stats => new NameValuePair(stats.TourNumber, string.Format("{1} ({0})", stats.TourType, stats.TourNumber)))
                .Cast<object>()
                .ToArray();

            cmbBoxObjVObjTourList.Items.AddRange(pilotsFighterStats);
            cmbBoxObjVObjTourList.Items.Add(new NameValuePair("ALL", "All Tours"));
        }


        private void cmboxModelSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Registry.Instance.GetPilotStats(PilotName).ObjVsObjVisibleList.Clear();
            var selectedModel = (string) cmboxModelSelector.SelectedItem;
            foreach (var objScore in Registry.Instance.GetPilotStats(PilotName).ObjVsObjCompleteList.Where(objScore => objScore.Model == selectedModel))
            {
                Registry.Instance.GetPilotStats(PilotName).ObjVsObjVisibleList.Add(objScore);
            }

            objectVsObjectDOBindingSource.DataSource = Registry.Instance.GetPilotStats(PilotName).ObjVsObjVisibleList;
            PopulateObjVObjTotals(Registry.Instance.GetPilotStats(PilotName).ObjVsObjVisibleList);
            objectVsObjectDOBindingSource.ResetBindings(false);
        }



        private void radBtnByModel_CheckedChanged(object sender, EventArgs e)
        {
            cmboxModelSelector.Enabled = true;
            cmbBoxObjVObjTourList.Enabled = false;

            cmboxModelSelector_SelectedIndexChanged(this, new EventArgs());
        }


        private void radBtnByTour_CheckedChanged(object sender, EventArgs e)
        {
            cmboxModelSelector.Enabled = false;
            cmbBoxObjVObjTourList.Enabled = true;

            cmbBoxObjVObjTourList_SelectedIndexChanged(this, new EventArgs());
        }


        private void cmbBoxObjVObjTourList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedPair = (NameValuePair) cmbBoxObjVObjTourList.SelectedItem;
            if (selectedPair == null)
            {
                objectVsObjectDOBindingSource.Clear();
                return;
            }

            Registry.Instance.GetPilotStats(PilotName).ObjVsObjVisibleList.Clear();

            if (selectedPair.Name == "ALL")
            {
                // Collate all the kills, killed by, kills in for each model. Build a temorary list 
                // for this one.
                var distinctModelList = Registry.Instance.GetPilotStats(PilotName).ObjVsObjCompleteList
                    .DistinctBy(ovo => ovo.Model)
                    .Select(ovo => new ObjectVsObjectDO(ovo.ObjScore, ovo.TourIdentfier, ovo.TourType, ovo.TourNumber)
                        {
                            TourIdentfier = "All Tours", // change the tour id tag.
                            // delete their stats otherwise they get counted twice for the first tour.
                            KilledBy = 0,
                            KillsIn = 0,
                            KillsOf = 0,
                            DiedIn = 0
                        })
                    .ToList();

                // now sum up for each model.
                foreach (var compositeObjScore in distinctModelList)
                {
                    var score = compositeObjScore;
                    foreach (var objScore in 
                        Registry.Instance.GetPilotStats(PilotName).ObjVsObjCompleteList.Where(objScore => score.Model == objScore.Model))
                    {
                        compositeObjScore.KilledBy += objScore.KilledBy;
                        compositeObjScore.KillsIn += objScore.KillsIn;
                        compositeObjScore.KillsOf += objScore.KillsOf;
                        compositeObjScore.DiedIn += objScore.DiedIn;
                    }
                }

                // all done rebind to grid and we off singing!
                objectVsObjectDOBindingSource.DataSource = new SortableBindingList<ObjectVsObjectDO>(distinctModelList);

                // Hide the Tour Number column, as it makes no sense in this context.
                objectVsObjectDODataGridView.Columns[0].Visible = false;

                PopulateObjVObjTotals(distinctModelList);
            }
            else
            {
                // ensure we re-enable the Tour Number column
                objectVsObjectDODataGridView.Columns[0].Visible = true;

                // Else we are just listing from actual stats objects in the registry.

                var selectedTour = Convert.ToInt32(selectedPair.Name);
                foreach (var objScore in Registry.Instance.GetPilotStats(PilotName).ObjVsObjCompleteList
                    .Where(objScore => objScore.TourNumber == selectedTour))
                {
                    Registry.Instance.GetPilotStats(PilotName).ObjVsObjVisibleList.Add(objScore);
                }

                objectVsObjectDOBindingSource.DataSource =
                    Registry.Instance.GetPilotStats(PilotName).ObjVsObjVisibleList;

                PopulateObjVObjTotals(Registry.Instance.GetPilotStats(PilotName).ObjVsObjVisibleList);
            }

            // Reset bindings
            objectVsObjectDOBindingSource.ResetBindings(false);

            objectVsObjectDOBindingSource.Sort = "Model";
        }


        private void PopulateObjVObjTotals(IList<ObjectVsObjectDO> filteredStatsList)
        {
            var totalKills = 0;
            int? totalDeaths = 0;
            foreach (var objStat in filteredStatsList)
            {
                totalKills += objStat.KillsIn;
                totalDeaths += objStat.DiedIn;
            }

            txtBoxTotalKills.Text = totalKills.ToString();

            if (totalDeaths != null)
            {
                var averageKillsDeath = totalKills/((decimal) totalDeaths + 1);
                averageKillsDeath = decimal.Round(averageKillsDeath, 2);
                txtBoxTotalDeaths.Text = totalDeaths.ToString();
                txtBoxAvergageKillsDeath.Text = averageKillsDeath.ToString();
            }
            else
            {
                txtBoxTotalDeaths.Text = "No Data";
                txtBoxAvergageKillsDeath.Text = "No Data";
            }
        }

        #endregion
    }
}