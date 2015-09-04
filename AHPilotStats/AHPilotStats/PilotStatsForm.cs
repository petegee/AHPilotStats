using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using My2Cents.HTC.PilotScoreSvc; 
using My2Cents.HTC.AHPilotStats.DomainObjects;
using DgvFilterPopup;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class PilotStatsForm : Form
    {
        public class NVPair
        {
            public string Name;
            public string Value;
            public NVPair(string name, string value)
            {
                Name = name;
                Value = value;
            }
            public override string ToString()
            {
                return Value;
            }
        }

        private DgvFilterManager fighterScoreFilterManager = new DgvFilterManager();
        private DgvFilterManager fighterStatsFilterManager = new DgvFilterManager();
        private DgvFilterManager attackScoreFilterManager = new DgvFilterManager();
        private DgvFilterManager attackStatsFilterManager = new DgvFilterManager();
        private DgvFilterManager bomberScoreFilterManager = new DgvFilterManager();
        private DgvFilterManager bomberStatsFilterManager = new DgvFilterManager();
        private DgvFilterManager vehicleScoreFilterManager = new DgvFilterManager();
        private DgvFilterManager vehicleStatsFilterManager = new DgvFilterManager();

        private string _PilotName;
        private GraphBuilder grapher;
        private bool _isCompositeData = false;
        private bool _compositeObjVsObjDataIncomplete = false;

        public string PilotName
        {
            get { return _PilotName;  }
        }

        public bool CompositeObjVsObjDataIncomplete
        {
            get { return _compositeObjVsObjDataIncomplete; }
            set 
            {
                if (_isCompositeData)
                    _compositeObjVsObjDataIncomplete = value;
                else
                    _compositeObjVsObjDataIncomplete = false;
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

            this.Text = pilotName;
            _isCompositeData = isCompositeData;
            _PilotName = pilotName;

            grapher = new GraphBuilder(pilotName);

            PopulateTourTypeFilterComboBox();
            //BindPilotStatsToGrids(pilotName);

            foreach (string model in Registry.Instance.ModelList)
                this.cmboxModelSelector.Items.Add(model);

            PopulateObjVsObjTourDropDownList();
            //radBtnByTour.Checked = true;
            //radBtnByModel.Checked = false;
            cmboxModelSelector.Enabled = false;
            cmbBoxObjVObjTourList.Enabled = true;

            cmbBoxMode.SelectedItem = "Fighter";
            plotSurface2D.Hide();

            if (_PilotName == "AKUAG")
            {
                TabPage akuagTabPage = new TabPage("AKUAG");
                AKUAGStatsPanel panel = new AKUAGStatsPanel();
                akuagTabPage.Controls.Add(panel);
                this.tabControl.TabPages.Add(akuagTabPage);
            }

           
        }


        private void BindDataToGrids(string pilotName)
        {
            this.fighterScoresDODataGridView.DataSource = Utility.CreateDataTableFromList<FighterScoresDO>(Registry.Instance.GetPilotStats(pilotName).FighterScoresList);
            fighterScoreFilterManager.ColumnFilterAdding += new ColumnFilterEventHandler(ColumnFilterAdding);
            fighterScoreFilterManager.DataGridView = this.fighterScoresDODataGridView;

            this.fighterStatsDODataGridView.DataSource = Utility.CreateDataTableFromList<StatsDomainObject>(Registry.Instance.GetPilotStats(pilotName).FighterStatsList);
            fighterStatsFilterManager.ColumnFilterAdding += new ColumnFilterEventHandler(ColumnFilterAdding);
            fighterStatsFilterManager.DataGridView = this.fighterStatsDODataGridView;

            this.attackScoresDODataGridView.DataSource = Utility.CreateDataTableFromList<AttackScoresDO>(Registry.Instance.GetPilotStats(pilotName).AttackScoresList);
            this.attackScoreFilterManager.ColumnFilterAdding += new ColumnFilterEventHandler(ColumnFilterAdding);
            this.attackScoreFilterManager.DataGridView = this.attackScoresDODataGridView;

            this.attackStatsDODataGridView.DataSource = Utility.CreateDataTableFromList<StatsDomainObject>(Registry.Instance.GetPilotStats(pilotName).AttackStatsList );
            this.attackStatsFilterManager.ColumnFilterAdding += new ColumnFilterEventHandler(ColumnFilterAdding);
            this.attackStatsFilterManager.DataGridView = this.attackStatsDODataGridView;

            this.bomberScoresDODataGridView.DataSource = Utility.CreateDataTableFromList<BomberScoresDO>(Registry.Instance.GetPilotStats(pilotName).BomberScoresList);
            this.bomberScoreFilterManager.ColumnFilterAdding += new ColumnFilterEventHandler(ColumnFilterAdding);
            this.bomberScoreFilterManager.DataGridView = this.bomberScoresDODataGridView;

            this.bomberStatsDODataGridView.DataSource = Utility.CreateDataTableFromList<StatsDomainObject>(Registry.Instance.GetPilotStats(pilotName).BomberStatsList);
            this.bomberStatsFilterManager.ColumnFilterAdding += new ColumnFilterEventHandler(ColumnFilterAdding);
            this.bomberStatsFilterManager.DataGridView = this.bomberStatsDODataGridView;

            this.vehicleBoatScoresDODataGridView.DataSource = Utility.CreateDataTableFromList<VehicleBoatScoresDO>(Registry.Instance.GetPilotStats(pilotName).VehicleBoatScoresList);
            this.vehicleScoreFilterManager.ColumnFilterAdding += new ColumnFilterEventHandler(ColumnFilterAdding);
            this.vehicleScoreFilterManager.DataGridView = this.vehicleBoatScoresDODataGridView;

            this.vehicleBoatStatsDODataGridView.DataSource = Utility.CreateDataTableFromList<StatsDomainObject>(Registry.Instance.GetPilotStats(pilotName).VehicleBoatStatsList);
            this.vehicleStatsFilterManager.ColumnFilterAdding += new ColumnFilterEventHandler(ColumnFilterAdding);
            this.vehicleStatsFilterManager.DataGridView = this.vehicleBoatStatsDODataGridView;

        }


        void ColumnFilterAdding(object sender, ColumnFilterEventArgs e)
        {
            switch (e.Column.Name)
            {
                case "Type":
                    e.ColumnFilter = new DgvComboBoxColumnFilter();
                    break;
            }
        }


        private void PopulateTourTypeFilterComboBox()
        {
            List<string> tourTypesList = new List<string>();

            SortableList<StatsDomainObject> pilotStats = null;
            try
            {
                pilotStats = Registry.Instance.GetPilotStats(_PilotName).FighterStatsList ;
            }
            catch(PilotDoesNotExistInRegistryException)
            {
                //ignore.
            }

            if (pilotStats == null)
                return;

            foreach (FighterStatsDO fs in pilotStats)
            {
                // if we try to process an empty (due to missing data?) then silently ignore and dont add to drop down.
                if (fs.TourType == "[UNKNOWN]")
                    continue;

                if (!tourTypesList.Contains(fs.TourType))
                    tourTypesList.Add(fs.TourType);
            }

            foreach (string tourType in tourTypesList)
                cmbxTourTypeFilter.Items.Add(tourType);

            cmbxTourTypeFilter.SelectedIndex = 0;
        }

        //private void BindPilotStatsToGrids(string pilotName)
        //{
            // Bind to Grids
            //this.fighterScoresDOBindingSource.DataSource = Registry.Instance.GetPilotStats(pilotName).FighterScoresList;

            //this.fighterStatsDOBindingSource.DataSource = Registry.Instance.GetPilotStats(pilotName).FighterStatsList;
            //this.attackScoresDOBindingSource.DataSource = Registry.Instance.GetPilotStats(pilotName).AttackScoresList;
            //this.attackStatsDOBindingSource.DataSource = Registry.Instance.GetPilotStats(pilotName).AttackStatsList;
            //this.bomberScoresDOBindingSource.DataSource = Registry.Instance.GetPilotStats(pilotName).BomberScoresList;
            //this.bomberStatsDOBindingSource.DataSource = Registry.Instance.GetPilotStats(pilotName).BomberStatsList;
            //this.vehicleBoatScoresDOBindingSource.DataSource = Registry.Instance.GetPilotStats(pilotName).VehicleBoatScoresList;
            //this.vehicleBoatStatsDOBindingSource.DataSource = Registry.Instance.GetPilotStats(pilotName).VehicleBoatStatsList;
        //}

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
            grapher.ResetGraph(plotSurface2D);

            foreach (int i in chkLstBoxSelectGraph.CheckedIndices)
            {
                grapher.AddPlot(plotSurface2D, chkLstBoxSelectGraph.Items[i].ToString(), cmbBoxMode.Text, cmbxTourTypeFilter.Text);
            }
            grapher.RefreshPlots(plotSurface2D);
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
            Registry.Instance.GetPilotStats(_PilotName).FighterStatsList.SortList("TourNumber",ListSortDirection.Descending);


            foreach (FighterStatsDO fs in Registry.Instance.GetPilotStats(_PilotName).FighterStatsList)
            {
                // if we try to process an empty (due to missing data?) then silently ignore and dont add to drop down.
                if (fs.TourType == "[UNKNOWN]")
                    continue;

                NVPair pair = new NVPair(fs.TourNumber.ToString(), string.Format("{1} ({0})", fs.TourType.ToString(), fs.TourNumber));
                cmbBoxObjVObjTourList.Items.Add(pair);
            }

            NVPair allPair = new NVPair("ALL", "All Tours");
            cmbBoxObjVObjTourList.Items.Add(allPair);
        }


        private void cmboxModelSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList.Clear();
            string selectedModel = (string)cmboxModelSelector.SelectedItem;
            foreach (ObjectVsObjectDO objScore in Registry.Instance.GetPilotStats(_PilotName).ObjVsObjCompleteList)
            {
                if (objScore.Model == selectedModel)
                {
                    Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList.Add(objScore);
                }
            }

            this.objectVsObjectDOBindingSource.DataSource = Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList;
            PopulateObjVObjTotals(Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList);
            this.objectVsObjectDOBindingSource.ResetBindings(false);
        }


        private bool IsModelInObjVsObjList(SortableBindingList<ObjectVsObjectDO> list, string model)
        {
            foreach (ObjectVsObjectDO objScore in list)
            {
                if (objScore.Model == model)
                    return true;
            }
            return false;
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
            NVPair selectedPair = (NVPair)cmbBoxObjVObjTourList.SelectedItem;
            if (selectedPair == null)
            {
                this.objectVsObjectDOBindingSource.Clear();
                return;
            }

            Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList.Clear();

            if (selectedPair.Name == "ALL")
            {
                // Collate all the kills, killed by, kills in for each model. Build a temorary list 
                // for this one.

                SortableBindingList<ObjectVsObjectDO> compositeList = new SortableBindingList<ObjectVsObjectDO>();
                Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList.Clear();

                // build a temp place holder object for each model.
                foreach (ObjectVsObjectDO objScore in Registry.Instance.GetPilotStats(_PilotName).ObjVsObjCompleteList)
                {
                    if (!IsModelInObjVsObjList(compositeList, objScore.Model))
                    {
                        ObjectVsObjectDO compositeObject = new ObjectVsObjectDO(objScore.ObjScore, objScore.TourIdentfier, objScore.TourType, objScore.TourNumber);
                        compositeObject.TourIdentfier = "All Tours"; // change the tour id tag.

                        // delete their stats otherwise they get counted twice for the first tour.
                        compositeObject.KilledBy = 0;
                        compositeObject.KillsIn = 0;
                        compositeObject.KillsOf = 0;
                        compositeObject.DiedIn = 0;

                        compositeList.Add(compositeObject);
                    }
                }

                // now sum up for each model.
                foreach (ObjectVsObjectDO compositeObjScore in compositeList)
                {
                    foreach (ObjectVsObjectDO objScore in Registry.Instance.GetPilotStats(_PilotName).ObjVsObjCompleteList)
                    {
                        if (compositeObjScore.Model == objScore.Model)
                        {
                            compositeObjScore.KilledBy += objScore.KilledBy;
                            compositeObjScore.KillsIn += objScore.KillsIn;
                            compositeObjScore.KillsOf += objScore.KillsOf;
                            compositeObjScore.DiedIn += objScore.DiedIn;
                        }
                    }
                }


                // all done rebind to grid and we off singing!
                this.objectVsObjectDOBindingSource.DataSource = compositeList;

                // Hide the Tour Number column, as it makes no sense in this context.
                this.objectVsObjectDODataGridView.Columns[0].Visible = false;

                PopulateObjVObjTotals(compositeList);
            }
            else
            {
                // ensure we re-enable the Tour Number column
                this.objectVsObjectDODataGridView.Columns[0].Visible = true;

                // Else we are just listing from actual stats objects in the registry.

                int selectedTour = System.Convert.ToInt32(selectedPair.Name);
                foreach (ObjectVsObjectDO objScore in Registry.Instance.GetPilotStats(_PilotName).ObjVsObjCompleteList)
                {
                    if (objScore.TourNumber == selectedTour)
                    {
                        Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList.Add(objScore);
                    }
                }

                this.objectVsObjectDOBindingSource.DataSource = Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList;
                PopulateObjVObjTotals(Registry.Instance.GetPilotStats(_PilotName).ObjVsObjVisibleList);
            }

            // Reset bindings
            this.objectVsObjectDOBindingSource.ResetBindings(false);

            this.objectVsObjectDOBindingSource.Sort = "Model";
        }


        private void PopulateObjVObjTotals(SortableBindingList<ObjectVsObjectDO> filteredStatsList)
        {
            int totalKills = 0;
            int? totalDeaths = 0;
            decimal averageKillsDeath = 0;
            foreach (DomainObjects.ObjectVsObjectDO objStat in filteredStatsList)
            {
                totalKills += objStat.KillsIn;
                totalDeaths += objStat.DiedIn;
            }

            this.txtBoxTotalKills.Text = totalKills.ToString();

            if (totalDeaths != null)
            {
                averageKillsDeath = (decimal)totalKills / ((decimal)totalDeaths + 1);
                averageKillsDeath = decimal.Round(averageKillsDeath, 2);
                this.txtBoxTotalDeaths.Text = totalDeaths.ToString();
                this.txtBoxAvergageKillsDeath.Text = averageKillsDeath.ToString();
            }
            else
            {
                this.txtBoxTotalDeaths.Text = "No Data";
                this.txtBoxAvergageKillsDeath.Text = "No Data";
            }
        }

        #endregion


        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabControl tabControl = (TabControl)sender;

            if (tabControl.SelectedTab.Text == "Obj Vs Obj" && this._compositeObjVsObjDataIncomplete)
            {
                string message =
                    "Some pilots in your squad need reloading to get the new 'Died In' data.\n" +
                    "Identify the pilots/tours without 'Died In' data, and reload those.\n" +
                    "Until then there will some gaps in your sqauds data.";
                MessageBox.Show(message, "Warning");
            }
        }





        //////////////////////////////////////////////////////////////////////////////////
        //
        // On Form being first shown Event.
        //
        // Used so we can set the sort order on the correct column.
        //
        //////////////////////////////////////////////////////////////////////////////////


        //private void PilotStatsForm_Shown(object sender, EventArgs e)
        //{
        //    SortByColumn0Desc(fighterStatsDODataGridView);
        //    SortByColumn0Desc(fighterStatsDODataGridView1);
        //    SortByColumn0Desc(attackScoresDODataGridView);
        //    SortByColumn0Desc(attackStatsDODataGridView);
        //    SortByColumn0Desc(bomberScoresDODataGridView);
        //    SortByColumn0Desc(bomberStatsDODataGridView);
        //    SortByColumn0Desc(vehicleBoatScoresDODataGridView);
        //    SortByColumn0Desc(vehicleBoatStatsDODataGridView);
        //}

        //private void SortByColumn0Desc(DataGridView gridToSetSortColumn)
        //{
        //    //TODO:
        //    //gridToSetSortColumn.Sort(gridToSetSortColumn.Columns[0], ListSortDirection.Descending);
        //}

        private void fighterStatsDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.fighterScoresDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void fighterStatsDODataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.fighterStatsDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void attackScoresDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.attackScoresDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void attackStatsDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.attackStatsDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void bomberScoresDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.bomberScoresDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void bomberStatsDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.bomberStatsDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void vehicleBoatScoresDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.vehicleBoatScoresDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void vehicleBoatStatsDODataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            this.vehicleBoatStatsDODataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }


    }
}