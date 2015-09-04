namespace My2Cents.HTC.AHPilotStats
{
    partial class PilotStatsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFighterScore = new System.Windows.Forms.TabPage();
            this.fighterScoresDODataGridView = new System.Windows.Forms.DataGridView();
            this.tabFighterStats = new System.Windows.Forms.TabPage();
            this.fighterStatsDODataGridView = new System.Windows.Forms.DataGridView();
            this.tabAttackScore = new System.Windows.Forms.TabPage();
            this.attackScoresDODataGridView = new System.Windows.Forms.DataGridView();
            this.tabAttackStats = new System.Windows.Forms.TabPage();
            this.attackStatsDODataGridView = new System.Windows.Forms.DataGridView();
            this.tabBomberScore = new System.Windows.Forms.TabPage();
            this.bomberScoresDODataGridView = new System.Windows.Forms.DataGridView();
            this.tabBomberStats = new System.Windows.Forms.TabPage();
            this.bomberStatsDODataGridView = new System.Windows.Forms.DataGridView();
            this.tabVehicleBoatScore = new System.Windows.Forms.TabPage();
            this.vehicleBoatScoresDODataGridView = new System.Windows.Forms.DataGridView();
            this.tabVehicleBoatStats = new System.Windows.Forms.TabPage();
            this.vehicleBoatStatsDODataGridView = new System.Windows.Forms.DataGridView();
            this.tabObjVsObj = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtBoxTotalKills = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalKills = new System.Windows.Forms.Label();
            this.txtBoxAvergageKillsDeath = new System.Windows.Forms.TextBox();
            this.txtBoxTotalDeaths = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbBoxObjVObjTourList = new System.Windows.Forms.ComboBox();
            this.radBtnByTour = new System.Windows.Forms.RadioButton();
            this.cmboxModelSelector = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radBtnByModel = new System.Windows.Forms.RadioButton();
            this.objectVsObjectDODataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn85 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tourIdentfierDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tourTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.killsInDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.killsOfDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.killedByDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiedIn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KillsToDeath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objectVsObjectDOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabGraphs = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkLstBoxSelectGraph = new System.Windows.Forms.CheckedListBox();
            this.cmbxTourTypeFilter = new System.Windows.Forms.ComboBox();
            this.cmbBoxMode = new System.Windows.Forms.ComboBox();
            this.plotSurface2D = new NPlot.Windows.PlotSurface2D();
            this.Tour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TourIdentfier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TourType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl.SuspendLayout();
            this.tabFighterScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fighterScoresDODataGridView)).BeginInit();
            this.tabFighterStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fighterStatsDODataGridView)).BeginInit();
            this.tabAttackScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attackScoresDODataGridView)).BeginInit();
            this.tabAttackStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.attackStatsDODataGridView)).BeginInit();
            this.tabBomberScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bomberScoresDODataGridView)).BeginInit();
            this.tabBomberStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bomberStatsDODataGridView)).BeginInit();
            this.tabVehicleBoatScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleBoatScoresDODataGridView)).BeginInit();
            this.tabVehicleBoatStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vehicleBoatStatsDODataGridView)).BeginInit();
            this.tabObjVsObj.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectVsObjectDODataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectVsObjectDOBindingSource)).BeginInit();
            this.tabGraphs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabFighterScore);
            this.tabControl.Controls.Add(this.tabFighterStats);
            this.tabControl.Controls.Add(this.tabAttackScore);
            this.tabControl.Controls.Add(this.tabAttackStats);
            this.tabControl.Controls.Add(this.tabBomberScore);
            this.tabControl.Controls.Add(this.tabBomberStats);
            this.tabControl.Controls.Add(this.tabVehicleBoatScore);
            this.tabControl.Controls.Add(this.tabVehicleBoatStats);
            this.tabControl.Controls.Add(this.tabObjVsObj);
            this.tabControl.Controls.Add(this.tabGraphs);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1017, 562);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabFighterScore
            // 
            this.tabFighterScore.Controls.Add(this.fighterScoresDODataGridView);
            this.tabFighterScore.Location = new System.Drawing.Point(4, 22);
            this.tabFighterScore.Name = "tabFighterScore";
            this.tabFighterScore.Padding = new System.Windows.Forms.Padding(3);
            this.tabFighterScore.Size = new System.Drawing.Size(1009, 536);
            this.tabFighterScore.TabIndex = 0;
            this.tabFighterScore.Text = "Fighter Score";
            this.tabFighterScore.UseVisualStyleBackColor = true;
            // 
            // fighterStatsDODataGridView
            // 
            this.fighterScoresDODataGridView.AllowUserToAddRows = false;
            this.fighterScoresDODataGridView.AllowUserToDeleteRows = false;
            this.fighterScoresDODataGridView.AllowUserToOrderColumns = true;
            this.fighterScoresDODataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fighterScoresDODataGridView.Location = new System.Drawing.Point(3, 3);
            this.fighterScoresDODataGridView.Name = "fighterStatsDODataGridView";
            this.fighterScoresDODataGridView.ReadOnly = true;
            this.fighterScoresDODataGridView.Size = new System.Drawing.Size(1003, 530);
            this.fighterScoresDODataGridView.TabIndex = 0;
            this.fighterScoresDODataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.fighterStatsDODataGridView_DataBindingComplete);
            // 
            // tabFighterStats
            // 
            this.tabFighterStats.Controls.Add(this.fighterStatsDODataGridView);
            this.tabFighterStats.Location = new System.Drawing.Point(4, 22);
            this.tabFighterStats.Name = "tabFighterStats";
            this.tabFighterStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabFighterStats.Size = new System.Drawing.Size(1009, 536);
            this.tabFighterStats.TabIndex = 1;
            this.tabFighterStats.Text = "Fighter Stats";
            this.tabFighterStats.UseVisualStyleBackColor = true;
            // 
            // fighterStatsDODataGridView1
            // 
            this.fighterStatsDODataGridView.AllowUserToAddRows = false;
            this.fighterStatsDODataGridView.AllowUserToDeleteRows = false;
            this.fighterStatsDODataGridView.AllowUserToOrderColumns = true;
            this.fighterStatsDODataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fighterStatsDODataGridView.Location = new System.Drawing.Point(3, 3);
            this.fighterStatsDODataGridView.Name = "fighterStatsDODataGridView1";
            this.fighterStatsDODataGridView.ReadOnly = true;
            this.fighterStatsDODataGridView.Size = new System.Drawing.Size(1003, 530);
            this.fighterStatsDODataGridView.TabIndex = 0;
            this.fighterStatsDODataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.fighterStatsDODataGridView1_DataBindingComplete);
            // 
            // tabAttackScore
            // 
            this.tabAttackScore.Controls.Add(this.attackScoresDODataGridView);
            this.tabAttackScore.Location = new System.Drawing.Point(4, 22);
            this.tabAttackScore.Name = "tabAttackScore";
            this.tabAttackScore.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttackScore.Size = new System.Drawing.Size(1009, 536);
            this.tabAttackScore.TabIndex = 2;
            this.tabAttackScore.Text = "Attack Score";
            this.tabAttackScore.UseVisualStyleBackColor = true;
            // 
            // attackScoresDODataGridView
            // 
            this.attackScoresDODataGridView.AllowUserToAddRows = false;
            this.attackScoresDODataGridView.AllowUserToDeleteRows = false;
            this.attackScoresDODataGridView.AllowUserToOrderColumns = true;
            this.attackScoresDODataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attackScoresDODataGridView.Location = new System.Drawing.Point(3, 3);
            this.attackScoresDODataGridView.Name = "attackScoresDODataGridView";
            this.attackScoresDODataGridView.ReadOnly = true;
            this.attackScoresDODataGridView.Size = new System.Drawing.Size(1003, 530);
            this.attackScoresDODataGridView.TabIndex = 0;
            this.attackScoresDODataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.attackScoresDODataGridView_DataBindingComplete);
            // 
            // tabAttackStats
            // 
            this.tabAttackStats.Controls.Add(this.attackStatsDODataGridView);
            this.tabAttackStats.Location = new System.Drawing.Point(4, 22);
            this.tabAttackStats.Name = "tabAttackStats";
            this.tabAttackStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabAttackStats.Size = new System.Drawing.Size(1009, 536);
            this.tabAttackStats.TabIndex = 3;
            this.tabAttackStats.Text = "Attack Stats";
            this.tabAttackStats.UseVisualStyleBackColor = true;
            // 
            // attackStatsDODataGridView
            // 
            this.attackStatsDODataGridView.AllowUserToAddRows = false;
            this.attackStatsDODataGridView.AllowUserToDeleteRows = false;
            this.attackStatsDODataGridView.AllowUserToOrderColumns = true;
            this.attackStatsDODataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attackStatsDODataGridView.Location = new System.Drawing.Point(3, 3);
            this.attackStatsDODataGridView.Name = "attackStatsDODataGridView";
            this.attackStatsDODataGridView.ReadOnly = true;
            this.attackStatsDODataGridView.Size = new System.Drawing.Size(1003, 530);
            this.attackStatsDODataGridView.TabIndex = 0;
            this.attackStatsDODataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.attackStatsDODataGridView_DataBindingComplete);
            // 
            // tabBomberScore
            // 
            this.tabBomberScore.Controls.Add(this.bomberScoresDODataGridView);
            this.tabBomberScore.Location = new System.Drawing.Point(4, 22);
            this.tabBomberScore.Name = "tabBomberScore";
            this.tabBomberScore.Padding = new System.Windows.Forms.Padding(3);
            this.tabBomberScore.Size = new System.Drawing.Size(1009, 536);
            this.tabBomberScore.TabIndex = 4;
            this.tabBomberScore.Text = "Bomber Score";
            this.tabBomberScore.UseVisualStyleBackColor = true;
            // 
            // bomberScoresDODataGridView
            // 
            this.bomberScoresDODataGridView.AllowUserToAddRows = false;
            this.bomberScoresDODataGridView.AllowUserToDeleteRows = false;
            this.bomberScoresDODataGridView.AllowUserToOrderColumns = true;
            this.bomberScoresDODataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bomberScoresDODataGridView.Location = new System.Drawing.Point(3, 3);
            this.bomberScoresDODataGridView.Name = "bomberScoresDODataGridView";
            this.bomberScoresDODataGridView.ReadOnly = true;
            this.bomberScoresDODataGridView.Size = new System.Drawing.Size(1003, 530);
            this.bomberScoresDODataGridView.TabIndex = 0;
            this.bomberScoresDODataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.bomberScoresDODataGridView_DataBindingComplete);
            // 
            // tabBomberStats
            // 
            this.tabBomberStats.Controls.Add(this.bomberStatsDODataGridView);
            this.tabBomberStats.Location = new System.Drawing.Point(4, 22);
            this.tabBomberStats.Name = "tabBomberStats";
            this.tabBomberStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabBomberStats.Size = new System.Drawing.Size(1009, 536);
            this.tabBomberStats.TabIndex = 5;
            this.tabBomberStats.Text = "Bomber Stats";
            this.tabBomberStats.UseVisualStyleBackColor = true;
            // 
            // bomberStatsDODataGridView
            // 
            this.bomberStatsDODataGridView.AllowUserToAddRows = false;
            this.bomberStatsDODataGridView.AllowUserToDeleteRows = false;
            this.bomberStatsDODataGridView.AllowUserToOrderColumns = true;
            this.bomberStatsDODataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bomberStatsDODataGridView.Location = new System.Drawing.Point(3, 3);
            this.bomberStatsDODataGridView.Name = "bomberStatsDODataGridView";
            this.bomberStatsDODataGridView.ReadOnly = true;
            this.bomberStatsDODataGridView.Size = new System.Drawing.Size(1003, 530);
            this.bomberStatsDODataGridView.TabIndex = 0;
            this.bomberStatsDODataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.bomberStatsDODataGridView_DataBindingComplete);
            // 
            // tabVehicleBoatScore
            // 
            this.tabVehicleBoatScore.Controls.Add(this.vehicleBoatScoresDODataGridView);
            this.tabVehicleBoatScore.Location = new System.Drawing.Point(4, 22);
            this.tabVehicleBoatScore.Name = "tabVehicleBoatScore";
            this.tabVehicleBoatScore.Padding = new System.Windows.Forms.Padding(3);
            this.tabVehicleBoatScore.Size = new System.Drawing.Size(1009, 536);
            this.tabVehicleBoatScore.TabIndex = 6;
            this.tabVehicleBoatScore.Text = "Vehicle/Boat Score";
            this.tabVehicleBoatScore.UseVisualStyleBackColor = true;
            // 
            // vehicleBoatScoresDODataGridView
            // 
            this.vehicleBoatScoresDODataGridView.AllowUserToAddRows = false;
            this.vehicleBoatScoresDODataGridView.AllowUserToDeleteRows = false;
            this.vehicleBoatScoresDODataGridView.AllowUserToOrderColumns = true;
            this.vehicleBoatScoresDODataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vehicleBoatScoresDODataGridView.Location = new System.Drawing.Point(3, 3);
            this.vehicleBoatScoresDODataGridView.Name = "vehicleBoatScoresDODataGridView";
            this.vehicleBoatScoresDODataGridView.ReadOnly = true;
            this.vehicleBoatScoresDODataGridView.Size = new System.Drawing.Size(1003, 530);
            this.vehicleBoatScoresDODataGridView.TabIndex = 0;
            this.vehicleBoatScoresDODataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.vehicleBoatScoresDODataGridView_DataBindingComplete);
            // 
            // tabVehicleBoatStats
            // 
            this.tabVehicleBoatStats.Controls.Add(this.vehicleBoatStatsDODataGridView);
            this.tabVehicleBoatStats.Location = new System.Drawing.Point(4, 22);
            this.tabVehicleBoatStats.Name = "tabVehicleBoatStats";
            this.tabVehicleBoatStats.Padding = new System.Windows.Forms.Padding(3);
            this.tabVehicleBoatStats.Size = new System.Drawing.Size(1009, 536);
            this.tabVehicleBoatStats.TabIndex = 7;
            this.tabVehicleBoatStats.Text = "Vehicle/Boat Stats";
            this.tabVehicleBoatStats.UseVisualStyleBackColor = true;
            // 
            // vehicleBoatStatsDODataGridView
            // 
            this.vehicleBoatStatsDODataGridView.AllowUserToAddRows = false;
            this.vehicleBoatStatsDODataGridView.AllowUserToDeleteRows = false;
            this.vehicleBoatStatsDODataGridView.AllowUserToOrderColumns = true;
            this.vehicleBoatStatsDODataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vehicleBoatStatsDODataGridView.Location = new System.Drawing.Point(3, 3);
            this.vehicleBoatStatsDODataGridView.Name = "vehicleBoatStatsDODataGridView";
            this.vehicleBoatStatsDODataGridView.ReadOnly = true;
            this.vehicleBoatStatsDODataGridView.Size = new System.Drawing.Size(1003, 530);
            this.vehicleBoatStatsDODataGridView.TabIndex = 0;
            this.vehicleBoatStatsDODataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.vehicleBoatStatsDODataGridView_DataBindingComplete);
            // 
            // tabObjVsObj
            // 
            this.tabObjVsObj.Controls.Add(this.groupBox2);
            this.tabObjVsObj.Controls.Add(this.groupBox1);
            this.tabObjVsObj.Controls.Add(this.objectVsObjectDODataGridView);
            this.tabObjVsObj.Location = new System.Drawing.Point(4, 22);
            this.tabObjVsObj.Name = "tabObjVsObj";
            this.tabObjVsObj.Padding = new System.Windows.Forms.Padding(3);
            this.tabObjVsObj.Size = new System.Drawing.Size(1009, 536);
            this.tabObjVsObj.TabIndex = 8;
            this.tabObjVsObj.Text = "Obj Vs Obj";
            this.tabObjVsObj.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtBoxTotalKills);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtTotalKills);
            this.groupBox2.Controls.Add(this.txtBoxAvergageKillsDeath);
            this.groupBox2.Controls.Add(this.txtBoxTotalDeaths);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(391, 488);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(552, 42);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Running Totals";
            // 
            // txtBoxTotalKills
            // 
            this.txtBoxTotalKills.Location = new System.Drawing.Point(130, 16);
            this.txtBoxTotalKills.Name = "txtBoxTotalKills";
            this.txtBoxTotalKills.Size = new System.Drawing.Size(63, 20);
            this.txtBoxTotalKills.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(370, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Total Kills/Death";
            // 
            // txtTotalKills
            // 
            this.txtTotalKills.AutoSize = true;
            this.txtTotalKills.Location = new System.Drawing.Point(76, 22);
            this.txtTotalKills.Name = "txtTotalKills";
            this.txtTotalKills.Size = new System.Drawing.Size(52, 13);
            this.txtTotalKills.TabIndex = 9;
            this.txtTotalKills.Text = "Total Kills";
            // 
            // txtBoxAvergageKillsDeath
            // 
            this.txtBoxAvergageKillsDeath.Location = new System.Drawing.Point(478, 14);
            this.txtBoxAvergageKillsDeath.Name = "txtBoxAvergageKillsDeath";
            this.txtBoxAvergageKillsDeath.Size = new System.Drawing.Size(63, 20);
            this.txtBoxAvergageKillsDeath.TabIndex = 12;
            // 
            // txtBoxTotalDeaths
            // 
            this.txtBoxTotalDeaths.Location = new System.Drawing.Point(290, 15);
            this.txtBoxTotalDeaths.Name = "txtBoxTotalDeaths";
            this.txtBoxTotalDeaths.Size = new System.Drawing.Size(63, 20);
            this.txtBoxTotalDeaths.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Total Deaths";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbBoxObjVObjTourList);
            this.groupBox1.Controls.Add(this.radBtnByTour);
            this.groupBox1.Controls.Add(this.cmboxModelSelector);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radBtnByModel);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 178);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Group By";
            // 
            // cmbBoxObjVObjTourList
            // 
            this.cmbBoxObjVObjTourList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxObjVObjTourList.Enabled = false;
            this.cmbBoxObjVObjTourList.FormattingEnabled = true;
            this.cmbBoxObjVObjTourList.Location = new System.Drawing.Point(17, 131);
            this.cmbBoxObjVObjTourList.MaxDropDownItems = 12;
            this.cmbBoxObjVObjTourList.Name = "cmbBoxObjVObjTourList";
            this.cmbBoxObjVObjTourList.Size = new System.Drawing.Size(128, 21);
            this.cmbBoxObjVObjTourList.TabIndex = 4;
            this.cmbBoxObjVObjTourList.SelectedIndexChanged += new System.EventHandler(this.cmbBoxObjVObjTourList_SelectedIndexChanged);
            // 
            // radBtnByTour
            // 
            this.radBtnByTour.AutoSize = true;
            this.radBtnByTour.Checked = true;
            this.radBtnByTour.Location = new System.Drawing.Point(7, 95);
            this.radBtnByTour.Name = "radBtnByTour";
            this.radBtnByTour.Size = new System.Drawing.Size(47, 17);
            this.radBtnByTour.TabIndex = 6;
            this.radBtnByTour.TabStop = true;
            this.radBtnByTour.Text = "Tour";
            this.radBtnByTour.UseVisualStyleBackColor = true;
            this.radBtnByTour.CheckedChanged += new System.EventHandler(this.radBtnByTour_CheckedChanged);
            // 
            // cmboxModelSelector
            // 
            this.cmboxModelSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmboxModelSelector.FormattingEnabled = true;
            this.cmboxModelSelector.Location = new System.Drawing.Point(17, 58);
            this.cmboxModelSelector.MaxDropDownItems = 12;
            this.cmboxModelSelector.Name = "cmboxModelSelector";
            this.cmboxModelSelector.Size = new System.Drawing.Size(128, 21);
            this.cmboxModelSelector.Sorted = true;
            this.cmboxModelSelector.TabIndex = 1;
            this.cmboxModelSelector.SelectedIndexChanged += new System.EventHandler(this.cmboxModelSelector_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Model";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Tour";
            // 
            // radBtnByModel
            // 
            this.radBtnByModel.AutoSize = true;
            this.radBtnByModel.Location = new System.Drawing.Point(7, 22);
            this.radBtnByModel.Name = "radBtnByModel";
            this.radBtnByModel.Size = new System.Drawing.Size(54, 17);
            this.radBtnByModel.TabIndex = 3;
            this.radBtnByModel.Text = "Model";
            this.radBtnByModel.UseVisualStyleBackColor = true;
            this.radBtnByModel.CheckedChanged += new System.EventHandler(this.radBtnByModel_CheckedChanged);
            // 
            // objectVsObjectDODataGridView
            // 
            this.objectVsObjectDODataGridView.AllowUserToAddRows = false;
            this.objectVsObjectDODataGridView.AllowUserToDeleteRows = false;
            this.objectVsObjectDODataGridView.AllowUserToOrderColumns = true;
            this.objectVsObjectDODataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objectVsObjectDODataGridView.AutoGenerateColumns = false;
            this.objectVsObjectDODataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn85,
            this.tourIdentfierDataGridViewTextBoxColumn,
            this.tourTypeDataGridViewTextBoxColumn,
            this.modelDataGridViewTextBoxColumn,
            this.killsInDataGridViewTextBoxColumn,
            this.killsOfDataGridViewTextBoxColumn,
            this.killedByDataGridViewTextBoxColumn,
            this.DiedIn,
            this.KillsToDeath});
            this.objectVsObjectDODataGridView.DataSource = this.objectVsObjectDOBindingSource;
            this.objectVsObjectDODataGridView.Location = new System.Drawing.Point(175, 0);
            this.objectVsObjectDODataGridView.Name = "objectVsObjectDODataGridView";
            this.objectVsObjectDODataGridView.ReadOnly = true;
            this.objectVsObjectDODataGridView.Size = new System.Drawing.Size(771, 483);
            this.objectVsObjectDODataGridView.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn85
            // 
            this.dataGridViewTextBoxColumn85.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn85.DataPropertyName = "TourNumber";
            this.dataGridViewTextBoxColumn85.HeaderText = "Tour";
            this.dataGridViewTextBoxColumn85.Name = "dataGridViewTextBoxColumn85";
            this.dataGridViewTextBoxColumn85.ReadOnly = true;
            this.dataGridViewTextBoxColumn85.Width = 54;
            // 
            // tourIdentfierDataGridViewTextBoxColumn
            // 
            this.tourIdentfierDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tourIdentfierDataGridViewTextBoxColumn.DataPropertyName = "TourIdentfier";
            this.tourIdentfierDataGridViewTextBoxColumn.HeaderText = "Tour Details";
            this.tourIdentfierDataGridViewTextBoxColumn.Name = "tourIdentfierDataGridViewTextBoxColumn";
            this.tourIdentfierDataGridViewTextBoxColumn.ReadOnly = true;
            this.tourIdentfierDataGridViewTextBoxColumn.Width = 89;
            // 
            // tourTypeDataGridViewTextBoxColumn
            // 
            this.tourTypeDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.tourTypeDataGridViewTextBoxColumn.DataPropertyName = "TourType";
            this.tourTypeDataGridViewTextBoxColumn.HeaderText = "Tour Type";
            this.tourTypeDataGridViewTextBoxColumn.Name = "tourTypeDataGridViewTextBoxColumn";
            this.tourTypeDataGridViewTextBoxColumn.ReadOnly = true;
            this.tourTypeDataGridViewTextBoxColumn.Width = 81;
            // 
            // modelDataGridViewTextBoxColumn
            // 
            this.modelDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.modelDataGridViewTextBoxColumn.DataPropertyName = "Model";
            this.modelDataGridViewTextBoxColumn.HeaderText = "Model";
            this.modelDataGridViewTextBoxColumn.Name = "modelDataGridViewTextBoxColumn";
            this.modelDataGridViewTextBoxColumn.ReadOnly = true;
            this.modelDataGridViewTextBoxColumn.Width = 61;
            // 
            // killsInDataGridViewTextBoxColumn
            // 
            this.killsInDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.killsInDataGridViewTextBoxColumn.DataPropertyName = "KillsIn";
            this.killsInDataGridViewTextBoxColumn.HeaderText = "Kills In";
            this.killsInDataGridViewTextBoxColumn.Name = "killsInDataGridViewTextBoxColumn";
            this.killsInDataGridViewTextBoxColumn.ReadOnly = true;
            this.killsInDataGridViewTextBoxColumn.Width = 62;
            // 
            // killsOfDataGridViewTextBoxColumn
            // 
            this.killsOfDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.killsOfDataGridViewTextBoxColumn.DataPropertyName = "KillsOf";
            this.killsOfDataGridViewTextBoxColumn.HeaderText = "Kills Of";
            this.killsOfDataGridViewTextBoxColumn.Name = "killsOfDataGridViewTextBoxColumn";
            this.killsOfDataGridViewTextBoxColumn.ReadOnly = true;
            this.killsOfDataGridViewTextBoxColumn.Width = 64;
            // 
            // killedByDataGridViewTextBoxColumn
            // 
            this.killedByDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.killedByDataGridViewTextBoxColumn.DataPropertyName = "KilledBy";
            this.killedByDataGridViewTextBoxColumn.HeaderText = "Killed By";
            this.killedByDataGridViewTextBoxColumn.Name = "killedByDataGridViewTextBoxColumn";
            this.killedByDataGridViewTextBoxColumn.ReadOnly = true;
            this.killedByDataGridViewTextBoxColumn.Width = 72;
            // 
            // DiedIn
            // 
            this.DiedIn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DiedIn.DataPropertyName = "DiedIn";
            this.DiedIn.HeaderText = "Died In";
            this.DiedIn.Name = "DiedIn";
            this.DiedIn.ReadOnly = true;
            this.DiedIn.Width = 66;
            // 
            // KillsToDeath
            // 
            this.KillsToDeath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.KillsToDeath.DataPropertyName = "KillsToDeath";
            this.KillsToDeath.HeaderText = "Kills/Death";
            this.KillsToDeath.Name = "KillsToDeath";
            this.KillsToDeath.ReadOnly = true;
            this.KillsToDeath.Width = 84;
            // 
            // objectVsObjectDOBindingSource
            // 
            this.objectVsObjectDOBindingSource.DataSource = typeof(My2Cents.HTC.AHPilotStats.DomainObjects.ObjectVsObjectDO);
            // 
            // tabGraphs
            // 
            this.tabGraphs.Controls.Add(this.label6);
            this.tabGraphs.Controls.Add(this.label5);
            this.tabGraphs.Controls.Add(this.chkLstBoxSelectGraph);
            this.tabGraphs.Controls.Add(this.cmbxTourTypeFilter);
            this.tabGraphs.Controls.Add(this.cmbBoxMode);
            this.tabGraphs.Controls.Add(this.plotSurface2D);
            this.tabGraphs.Location = new System.Drawing.Point(4, 22);
            this.tabGraphs.Name = "tabGraphs";
            this.tabGraphs.Padding = new System.Windows.Forms.Padding(3);
            this.tabGraphs.Size = new System.Drawing.Size(1009, 536);
            this.tabGraphs.TabIndex = 9;
            this.tabGraphs.Text = "Graphs";
            this.tabGraphs.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-3, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Filter by Score Mode";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-3, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Filter by Tour Type";
            // 
            // chkLstBoxSelectGraph
            // 
            this.chkLstBoxSelectGraph.CheckOnClick = true;
            this.chkLstBoxSelectGraph.FormattingEnabled = true;
            this.chkLstBoxSelectGraph.Items.AddRange(new object[] {
            "Kill/Death Trend",
            "HTC Kill/Death Trend",
            "Kill/Landed Trend",
            "Kill/Sortie Trend",
            "Kill/Hour Trend",
            "Kill/Assist Trend",
            "Hit % Trend",
            "Sorties/Death Trend",
            "Sorties/Landed Trend",
            "Total Kills Trend",
            "Total Assists Trend",
            "Total Sorties Trend",
            "Total Landed Trend",
            "Total Bailed Trend",
            "Total Captured Trend",
            "Total Death Trend",
            "Total Time Trend"});
            this.chkLstBoxSelectGraph.Location = new System.Drawing.Point(0, 108);
            this.chkLstBoxSelectGraph.Name = "chkLstBoxSelectGraph";
            this.chkLstBoxSelectGraph.Size = new System.Drawing.Size(132, 259);
            this.chkLstBoxSelectGraph.TabIndex = 3;
            this.chkLstBoxSelectGraph.SelectedIndexChanged += new System.EventHandler(this.chkLstBoxSelectGraph_SelectedIndexChanged);
            this.chkLstBoxSelectGraph.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkLstBoxSelectGraph_ItemCheck);
            // 
            // cmbxTourTypeFilter
            // 
            this.cmbxTourTypeFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbxTourTypeFilter.FormattingEnabled = true;
            this.cmbxTourTypeFilter.Location = new System.Drawing.Point(0, 71);
            this.cmbxTourTypeFilter.Name = "cmbxTourTypeFilter";
            this.cmbxTourTypeFilter.Size = new System.Drawing.Size(132, 21);
            this.cmbxTourTypeFilter.TabIndex = 1;
            this.cmbxTourTypeFilter.SelectedIndexChanged += new System.EventHandler(this.cmbxTourTypeFilter_SelectedIndexChanged);
            // 
            // cmbBoxMode
            // 
            this.cmbBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxMode.FormattingEnabled = true;
            this.cmbBoxMode.Items.AddRange(new object[] {
            "Fighter",
            "Attack",
            "Bomber",
            "Vehicle/Boat"});
            this.cmbBoxMode.Location = new System.Drawing.Point(0, 22);
            this.cmbBoxMode.MaxDropDownItems = 4;
            this.cmbBoxMode.Name = "cmbBoxMode";
            this.cmbBoxMode.Size = new System.Drawing.Size(132, 21);
            this.cmbBoxMode.TabIndex = 2;
            this.cmbBoxMode.SelectedIndexChanged += new System.EventHandler(this.cmbBoxMode_SelectedIndexChanged);
            // 
            // plotSurface2D
            // 
            this.plotSurface2D.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.plotSurface2D.AutoScaleAutoGeneratedAxes = false;
            this.plotSurface2D.AutoScaleTitle = false;
            this.plotSurface2D.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotSurface2D.DateTimeToolTip = false;
            this.plotSurface2D.Legend = null;
            this.plotSurface2D.LegendZOrder = -1;
            this.plotSurface2D.Location = new System.Drawing.Point(138, 0);
            this.plotSurface2D.Name = "plotSurface2D";
            this.plotSurface2D.RightMenu = null;
            this.plotSurface2D.ShowCoordinates = true;
            this.plotSurface2D.Size = new System.Drawing.Size(744, 510);
            this.plotSurface2D.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            this.plotSurface2D.TabIndex = 0;
            this.plotSurface2D.Text = "plotSurface2D";
            this.plotSurface2D.Title = "";
            this.plotSurface2D.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.plotSurface2D.XAxis1 = null;
            this.plotSurface2D.XAxis2 = null;
            this.plotSurface2D.YAxis1 = null;
            this.plotSurface2D.YAxis2 = null;
            // 
            // Tour
            // 
            this.Tour.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Tour.DataPropertyName = "TourNumber";
            this.Tour.HeaderText = "Tour";
            this.Tour.Name = "Tour";
            this.Tour.ReadOnly = true;
            // 
            // TourIdentfier
            // 
            this.TourIdentfier.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.TourIdentfier.DataPropertyName = "TourIdentfier";
            this.TourIdentfier.HeaderText = "Tour Details";
            this.TourIdentfier.Name = "TourIdentfier";
            this.TourIdentfier.ReadOnly = true;
            // 
            // TourType
            // 
            this.TourType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.TourType.DataPropertyName = "TourType";
            this.TourType.HeaderText = "Tour Type";
            this.TourType.Name = "TourType";
            this.TourType.ReadOnly = true;
            // 
            // PilotStatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 562);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.Name = "PilotStatsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PilotStats";
            this.tabControl.ResumeLayout(false);
            this.tabFighterScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fighterScoresDODataGridView)).EndInit();
            this.tabFighterStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fighterStatsDODataGridView)).EndInit();
            this.tabAttackScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.attackScoresDODataGridView)).EndInit();
            this.tabAttackStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.attackStatsDODataGridView)).EndInit();
            this.tabBomberScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bomberScoresDODataGridView)).EndInit();
            this.tabBomberStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bomberStatsDODataGridView)).EndInit();
            this.tabVehicleBoatScore.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vehicleBoatScoresDODataGridView)).EndInit();
            this.tabVehicleBoatStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vehicleBoatStatsDODataGridView)).EndInit();
            this.tabObjVsObj.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectVsObjectDODataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectVsObjectDOBindingSource)).EndInit();
            this.tabGraphs.ResumeLayout(false);
            this.tabGraphs.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabFighterScore;
        private System.Windows.Forms.TabPage tabFighterStats;
        private System.Windows.Forms.TabPage tabAttackScore;
        private System.Windows.Forms.TabPage tabAttackStats;
        private System.Windows.Forms.TabPage tabBomberScore;
        private System.Windows.Forms.TabPage tabBomberStats;
        //private System.Windows.Forms.BindingSource fighterScoresDOBindingSource;
        private System.Windows.Forms.TabPage tabVehicleBoatScore;
        private System.Windows.Forms.TabPage tabVehicleBoatStats;
        private System.Windows.Forms.TabPage tabObjVsObj;
        private System.Windows.Forms.TabPage tabGraphs;
        private System.Windows.Forms.DataGridView fighterStatsDODataGridView;
        //private System.Windows.Forms.BindingSource fighterStatsDOBindingSource;
        private System.Windows.Forms.DataGridView attackScoresDODataGridView;
        //private System.Windows.Forms.BindingSource attackScoresDOBindingSource;
        private System.Windows.Forms.DataGridView attackStatsDODataGridView;
        //private System.Windows.Forms.BindingSource attackStatsDOBindingSource;
        private System.Windows.Forms.DataGridView bomberScoresDODataGridView;
        //private System.Windows.Forms.BindingSource bomberScoresDOBindingSource;
        private System.Windows.Forms.DataGridView bomberStatsDODataGridView;
        //private System.Windows.Forms.BindingSource bomberStatsDOBindingSource;
        private System.Windows.Forms.DataGridView vehicleBoatScoresDODataGridView;
        //private System.Windows.Forms.BindingSource vehicleBoatScoresDOBindingSource;
        private System.Windows.Forms.DataGridView vehicleBoatStatsDODataGridView;
        //private System.Windows.Forms.BindingSource vehicleBoatStatsDOBindingSource;
        private System.Windows.Forms.DataGridView objectVsObjectDODataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmboxModelSelector;
        private NPlot.Windows.PlotSurface2D plotSurface2D;
        private System.Windows.Forms.ComboBox cmbBoxMode;
        private System.Windows.Forms.CheckedListBox chkLstBoxSelectGraph;
        private System.Windows.Forms.BindingSource objectVsObjectDOBindingSource;
        private System.Windows.Forms.RadioButton radBtnByTour;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbBoxObjVObjTourList;
        private System.Windows.Forms.RadioButton radBtnByModel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label txtTotalKills;
        private System.Windows.Forms.TextBox txtBoxTotalKills;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBoxAvergageKillsDeath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxTotalDeaths;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbxTourTypeFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        //private System.Windows.Forms.DataGridViewTextBoxColumn TourNumber;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tour;
        private System.Windows.Forms.DataGridViewTextBoxColumn TourIdentfier;
        private System.Windows.Forms.DataGridViewTextBoxColumn TourType;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn20;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn19;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn21;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn23;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn79;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn27;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn36;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn33;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn30;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn34;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn38;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn32;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn28;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn29;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn31;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn35;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn37;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn80;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn39;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn43;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn40;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn41;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn42;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn44;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn81;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn45;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn54;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn51;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn48;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn52;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn56;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn50;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn46;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn47;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn49;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn53;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn55;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn82;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn57;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn64;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn60;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn66;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn65;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn59;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn62;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn61;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn63;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn58;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn83;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn67;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn76;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn73;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn70;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn74;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn78;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn72;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn68;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn69;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn71;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn75;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn77;
        //private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn84;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn85;
        private System.Windows.Forms.DataGridViewTextBoxColumn tourIdentfierDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tourTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn killsInDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn killsOfDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn killedByDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiedIn;
        private System.Windows.Forms.DataGridViewTextBoxColumn KillsToDeath;
        private System.Windows.Forms.DataGridView fighterScoresDODataGridView;
    }
}