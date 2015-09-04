using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;

using My2Cents.HTC.PilotScoreSvc;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer;
using My2Cents.HTC.PilotScoreSvc.Utilities;


namespace My2Cents.HTC.AHPilotStats
{
    public partial class PilotDataLoaderForm : Form
    {
        #region Nested Classes

        class LoaderError
        {
            public int tourId;
            public string pilotName = "";
            public string errorMessage = "";
        }

        class DataLoaderThreadParams
        {
            public DataLoaderThreadParams() 
            { 
            }

            public List<TourNode> toursToLoad = new List<TourNode>();
            public UpdateProgressCallBackDelegate updateProgressCallBack;
            public LoadCompletedCallBackDelegate loadCompletedCallBack;
            public ProxySettingsDTO proxySettings;
            public List<string> pilotIdList = new List<string>();
            public List<LoaderError> statsErrorList = new List<LoaderError>();
            public List<LoaderError> scoreErrorList = new List<LoaderError>();
            public List<AcesHighPilotScore> scoreList = new List<AcesHighPilotScore>();
            public List<AcesHighPilotStats> statsList = new List<AcesHighPilotStats>();
        }

        class CompleteTourData
        {
            public int tourId;
            public string pilotId;
            public AcesHighPilotStats stats;
            public AcesHighPilotScore score;
        }

        #endregion

        #region Private Members

        // Thread callback delegates.
        private delegate void UpdateProgressCallBackDelegate();
        private delegate void LoadCompletedCallBackDelegate();
        private delegate void TourDefLoadCompletedCallBackDelegate();

        // Threads state
        private ParameterizedThreadStart _LoaderThreadStart;
        private Thread _LoaderThread;
        private int _ThreadsCompleted = 0;
        private int _UnitsLoaded = 0;
        private int _TotalStarted = 0;
        private UpdateProgressCallBackDelegate _UpdateProgressCallBack;
        private LoadCompletedCallBackDelegate _LoadCompletedCallBack;
        private DataLoaderThreadParams _threadParam;

        private readonly object lockObj = new object();

        private ProxySettingsDTO _proxySettings;
        private Form _parent;

        // validation
        private ErrorProvider _StartTourErrorProvider;
        private ErrorProvider _EndTourErrorProvider;
        private ErrorProvider _PilotNameErrorProvider;
        private ErrorProvider _TourTypeSelectorErrorProvider;

        private const string _notSelectedText = "<Load Single Pilot>";
        private const string _selectTourTypeText = "<Select a Tour Type>";

        /// <summary>
        /// HTC seem to only allow a scores or stats lookup from the same session once every 
        /// four seconds. To be on the safe-side, lets enforce a 5 second wait between calls
        /// to meet their assumed rule.
        /// </summary>
        private const int _waitTimeMillsecondsBetweenHttpCallsToHtc = 5000;

        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="parent">The MDI parent.</param>
        public PilotDataLoaderForm(Form parent)
        {
            _parent = parent;

            InitializeComponent();

            _proxySettings = ProxySettingsDTO.GetProxySettings();
            
            LoadTourDefs();

            _StartTourErrorProvider = new System.Windows.Forms.ErrorProvider();
            _StartTourErrorProvider.SetIconAlignment(this.txtbxStartTour, ErrorIconAlignment.MiddleRight);
            _StartTourErrorProvider.SetIconPadding(this.txtbxStartTour, 2);
            _StartTourErrorProvider.BlinkRate = 700;
            _StartTourErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;

            _EndTourErrorProvider = new System.Windows.Forms.ErrorProvider();
            _EndTourErrorProvider.SetIconAlignment(this.txtbxEndTour, ErrorIconAlignment.MiddleRight);
            _EndTourErrorProvider.SetIconPadding(this.txtbxEndTour, 2);
            _EndTourErrorProvider.BlinkRate = 700;
            _EndTourErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;

            _PilotNameErrorProvider = new System.Windows.Forms.ErrorProvider();
            _PilotNameErrorProvider.SetIconAlignment(this.txtbxPilotToLoad, ErrorIconAlignment.MiddleRight);
            _PilotNameErrorProvider.SetIconPadding(this.txtbxPilotToLoad, 2);
            _PilotNameErrorProvider.BlinkRate = 700;
            _PilotNameErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;

            _TourTypeSelectorErrorProvider = new System.Windows.Forms.ErrorProvider();
            _TourTypeSelectorErrorProvider.SetIconAlignment(this.txtbxPilotToLoad, ErrorIconAlignment.MiddleRight);
            _TourTypeSelectorErrorProvider.SetIconPadding(this.txtbxPilotToLoad, 2);
            _TourTypeSelectorErrorProvider.BlinkRate = 700;
            _TourTypeSelectorErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
        }


        /// <summary>
        /// Load button click handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PilotDataLoaderForm_Load(object sender, EventArgs e)
        {
            this.cmboxTourType.Items.Add(_selectTourTypeText);
            foreach (string tour in Registry.Instance.TourDefinitions.Tours.Keys)
            {
                this.cmboxTourType.Items.Add(tour);  
            }

            this.cmboxTourType.SelectedIndex = 0;

            this.cmbBoxSquadSelect.Items.Add(_notSelectedText);
            foreach(string squadName in Registry.Instance.SquadNamesSet)
            {
                this.cmbBoxSquadSelect.Items.Add(squadName);
            }
            this.cmbBoxSquadSelect.SelectedItem = _notSelectedText;
            
            EnableLoadButton(false);

            _UpdateProgressCallBack = new UpdateProgressCallBackDelegate(UpdateProgressCallBack);
            _LoadCompletedCallBack = new LoadCompletedCallBackDelegate(LoadCompletedCallBack);

            _LoaderThreadStart = new ParameterizedThreadStart(this.LoaderThreadEntryPoint);

            this.txtbxStartTour.Enabled = false;
            this.txtbxEndTour.Enabled = false;
        }

        
        /// <summary>
        /// Call back for the loader threads to update their progress on the progress bar
        /// </summary>
        private void UpdateProgressCallBack()
        {
            lock (lockObj)
            { 
                this.progressBarLoading.Increment(1);
                this.lblUnitsLoaded.Text = (++_UnitsLoaded).ToString();
            }
        }

        /// <summary>
        /// Call back for the loader threads to notify they have completed.
        /// </summary>
        private void LoadCompletedCallBack()
        {
            lock (lockObj)
            {
                _ThreadsCompleted++;
                if (_ThreadsCompleted == _TotalStarted)
                {
                    ProcessLoadCompleted();
                }
            }
        }


        /// <summary>
        /// Handles the post load complete processing.
        /// </summary>
        private void ProcessLoadCompleted()
        {
            List<CompleteTourData> results = new List<CompleteTourData>();

            // Compile stats and scores into one logical unit.
            foreach (string pilotId in _threadParam.pilotIdList)
            {
                foreach (TourNode tour in _threadParam.toursToLoad)
                {
                    CompleteTourData compTourData = new CompleteTourData();

                    compTourData.pilotId = pilotId;
                    compTourData.tourId = tour.TourId;
                    compTourData.score = FindPilotTourScore(pilotId, tour.TourId); 
                    compTourData.stats = FindPilotTourStats(pilotId, tour.TourId); 
                    
                    results.Add(compTourData);
                }              
            }

            // Write each complete tour data results out to disk.
            foreach(CompleteTourData tourData in results)
            {
                if (!TourIsInError(tourData))
                {
                    // write out as one logical whole.
                    new XMLObjectSerialiser<AcesHighPilotScore>().WriteXMLFile(tourData.score, string.Format(".\\Data\\{0}_{1}_Score_{2}.xml", tourData.pilotId, tourData.score.TourType, tourData.score.TourId));
                    new XMLObjectSerialiser<AcesHighPilotStats>().WriteXMLFile(tourData.stats, string.Format(".\\Data\\{0}_{1}_Stats_{2}.xml", tourData.pilotId, tourData.stats.TourType, tourData.stats.TourId));
                }
            }

            // Display any errors that the loader threads caught to the user.
            BuildAndDisplayAnyErrorMessage();

            // Update the Registry and refresh the form.
            if (cmbBoxSquadSelect.SelectedItem.ToString() == _notSelectedText)
                ((MainMDI)MdiParent).RefreshPilotLists(this.txtbxPilotToLoad.Text);
            else
                ((MainMDI)MdiParent).RefreshSquadMemberPilotLists(this.cmbBoxSquadSelect.SelectedItem.ToString());

            RestoreForm();
            this.progressBarLoading.Value = 0;

        }


        /// <summary>
        /// For a given pilot/tour combination, return the relevant score object
        /// </summary>
        /// <param name="pilotId">pilot to find</param>
        /// <param name="tourId">tour to find</param>
        /// <returns>the relevant score object</returns>
        private AcesHighPilotScore FindPilotTourScore(string pilotId, int tourId)
        {
            foreach (AcesHighPilotScore score in _threadParam.scoreList)
            {
                if (score.GameId == pilotId && score.TourId == tourId.ToString())
                {
                    return score;
                }
            }

            return null;
        }


        /// <summary>
        /// For a given pilot/tour combination, return the relevant stats object
        /// </summary>
        /// <param name="pilotId">pilot to find</param>
        /// <param name="tourId">tour to find</param>
        /// <returns>the relevant stats object</returns>
        private AcesHighPilotStats FindPilotTourStats(string pilotId, int tourId)
        {
            foreach (AcesHighPilotStats stats in _threadParam.statsList)
            {
                if (stats.GameId == pilotId && stats.TourId == tourId.ToString())
                {
                    return stats;
                }
            }

            return null;
        }


        /// <summary>
        /// Search through the errors list and display any errors found during 
        /// the load in a dialog box for the user.
        /// </summary>
        private void BuildAndDisplayAnyErrorMessage()
        {
            bool inError = false;

            string errorMessageToDisplay = "Loading did not complete sucessfully:\n\n";

            foreach (LoaderError error in _threadParam.scoreErrorList)
            {
                errorMessageToDisplay += error.errorMessage + "\n";
                inError = true;
            }

            foreach (LoaderError error in _threadParam.statsErrorList)
            {
                errorMessageToDisplay += error.errorMessage + "\n";
                inError = true;
            }

            if (inError)
            {
                MessageBox.Show(this, errorMessageToDisplay, "Loader Failure");
            }
        }



        /// <summary>
        /// For a given tour, it determines if there were any errors loading
        /// that tour. 
        /// </summary>
        /// <param name="tourData">Tour data to load - contains both pilot and tour ids.</param>
        /// <returns>true if loader errors found, false otherwise.</returns>
        private bool TourIsInError(CompleteTourData tourData)
        {
            if (tourData.score == null || tourData.stats == null)
                return true;

            bool inError = false;

            // See if we can find a match for this tour/person combo in the errors lists.
            foreach (LoaderError error in _threadParam.scoreErrorList)
            {
                if (error.tourId == tourData.tourId && error.pilotName == tourData.pilotId)
                {
                    inError = true;
                }
            }
            foreach (LoaderError error in _threadParam.statsErrorList)
            {
                if (error.tourId == tourData.tourId && error.pilotName == tourData.pilotId)
                {
                    inError = true;
                }
            }

            return inError;
        }


        /// <summary>
        /// Called when the user clicks the "Load" button. Spawns the two loader threads to
        /// do the dirty work and free up the UI thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                EnableControls(false);
                this.btnCancel.Enabled = false;

                int tourStart = System.Convert.ToInt32(this.txtbxStartTour.Text);
                int tourEnd = System.Convert.ToInt32(this.txtbxEndTour.Text);

                _threadParam = new DataLoaderThreadParams();

                // if a single pilot, add single pilot name to the list.
                if (cmbBoxSquadSelect.SelectedItem.ToString() == _notSelectedText)
                {
                    _threadParam.pilotIdList.Add(CommonUtils.ToUpperFirstChar(this.txtbxPilotToLoad.Text));
                }
                else // its a squad - so add each member of the sqaud to the list.
                { 
                    Squad squad = Registry.Instance.GetSquad(cmbBoxSquadSelect.SelectedItem.ToString());
                    foreach (Squad.SquadMember squadMember in squad.Members)
                    {
                        //only load this member if they were actually a member for that tour.
                        if (squadMember.StartTour <= tourStart && squadMember.EndTour >= tourStart)
                        {
                            _threadParam.pilotIdList.Add(CommonUtils.ToUpperFirstChar(squadMember.PilotName));
                        }
                    }
                }

                string tourType = this.cmboxTourType.SelectedItem.ToString();
               
                // build the list of tours to load.
                for (int tourId = tourStart; tourId <= tourEnd; tourId++)
                {
                    _threadParam.toursToLoad.Add(Registry.Instance.TourDefinitions.FindTour(tourType, tourId));
                }

                // Set the thread callbacks.
                _threadParam.loadCompletedCallBack = _LoadCompletedCallBack;
                _threadParam.updateProgressCallBack = _UpdateProgressCallBack;

                // Make sure they actually selected something sane. Shouldnt happen due to form validation.
                if (cmbBoxSquadSelect.SelectedItem.ToString() == _notSelectedText && string.IsNullOrEmpty(txtbxPilotToLoad.Text))
                {
                    throw new Exception();
                }
              
                _threadParam.proxySettings = _proxySettings;

                // Kick off the threads to do the dirty work.
                _LoaderThread = new Thread(_LoaderThreadStart);
                _LoaderThread.Start(_threadParam);

                _TotalStarted = 1;

                if (cmbBoxSquadSelect.SelectedItem.ToString() == _notSelectedText)
                    this.progressBarLoading.Maximum = (tourEnd - tourStart + 1) * 2;
                else
                {
                    Squad squad = Registry.Instance.GetSquad(cmbBoxSquadSelect.SelectedItem.ToString());
                    this.progressBarLoading.Maximum = _threadParam.pilotIdList.Count * 2;
                }
                    

                this.lblUnitsToLoad.Text = this.progressBarLoading.Maximum.ToString();
                this.lblLoading.Visible = true;
            }
            catch (Exception)
            {
                // Silently ignore any exceptions in this thread.
                RestoreForm();
            }
        }



        /// <summary>
        /// Reset the form for the next load request.
        /// </summary>
        private void RestoreForm()
        {
            EnableControls(true);
            this.lblUnitsToLoad.Text = "0";
            this.btnCancel.Enabled = true;
            this.lblLoading.Visible = false;
            _UnitsLoaded = 0;
            _ThreadsCompleted = 0;
        }


        /// <summary>
        /// Set all the child controls enabled property.
        /// </summary>
        /// <param name="enable">true to enable, false to disable.</param>
        private void EnableControls(bool enable)
        {
            this.label1.Enabled = enable;
            this.label2.Enabled = enable;
            this.label3.Enabled = enable;
            this.label4.Enabled = enable;
            this.label5.Enabled = enable;
            this.txtbxPilotToLoad.Enabled = enable;
            this.txtbxPilotToLoad.Enabled = enable;
            this.txtbxStartTour.Enabled = enable;
            this.cmboxTourType.Enabled = enable;

            if (cmbBoxSquadSelect.SelectedItem.ToString() != _notSelectedText)
                this.txtbxEndTour.Enabled = false;
            else
                this.txtbxEndTour.Enabled = enable;

            this.cmbBoxSquadSelect.Enabled = enable;

            EnableLoadButton(enable);  
        }


        /// <summary>
        /// Thread start point for the stats loader thread.
        /// </summary>
        /// <param name="obj">DataLoaderThreadParams</param>
        public void LoaderThreadEntryPoint(object obj)
        {
            DataLoaderThreadParams param = (DataLoaderThreadParams)obj;

            try
            {
                bool onlyOneToLoad = param.toursToLoad.Count * param.pilotIdList.Count == 1;

                foreach (TourNode tour in param.toursToLoad)
                {
                    foreach (string pilotId in param.pilotIdList)
                    {
                        //
                        // Load the Stats objects.
                        //
                        string statsURL = ConfigurationManager.AppSettings["statsURL"];
                        HTCPilotStatsSvc statsSvc = new HTCPilotStatsSvc();
                        try
                        {
                            param.statsList.Add(statsSvc.GetPilotStats(pilotId, tour, param.proxySettings, statsURL));
                        }
                        catch (Exception e)
                        {
                            LoaderError loaderError = new LoaderError();
                            loaderError.tourId = tour.TourId;
                            loaderError.pilotName = pilotId;
                            loaderError.errorMessage = string.Format(" - could not load stats objects for pilot {0} for {1} tour {2}.", pilotId, tour.TourType.ToString(), tour.TourId);
                            param.statsErrorList.Add(loaderError);
                            Utility.WriteDebugTraceFile(e);
                        }

                        // Wait between calls.
                        Thread.Sleep(_waitTimeMillsecondsBetweenHttpCallsToHtc);
                        progressBarLoading.Invoke(param.updateProgressCallBack);

                        //
                        // Load the scores objects.
                        //
                        HTCPilotScoreSvc scoreSvc = new HTCPilotScoreSvc();
                        string scoresURL = ConfigurationManager.AppSettings["scoresURL"];
                        try
                        {
                            param.scoreList.Add(scoreSvc.GetPilotScore(pilotId, tour, param.proxySettings, scoresURL));
                        }
                        catch (Exception e)
                        {
                            LoaderError error = new LoaderError();
                            error.tourId = tour.TourId;
                            error.pilotName = pilotId;
                            error.errorMessage = string.Format(" - could not load scores objects for pilot {0} for {1} tour {2}.", pilotId, tour.TourType.ToString(), tour.TourId);
                            param.scoreErrorList.Add(error);
                            Utility.WriteDebugTraceFile(e);
                        }

                        progressBarLoading.Invoke(param.updateProgressCallBack);

                        if (!onlyOneToLoad)
                            Thread.Sleep(_waitTimeMillsecondsBetweenHttpCallsToHtc);
                    }
                }

            }
            finally
            {
                btnLoad.Invoke(param.loadCompletedCallBack);
            }
        }


        /// <summary>
        /// Calls the HTCPilotStatsSvc to load all the current tour definitions.
        /// </summary>
        private void LoadTourDefs()
        {
            WaitDialog waitDlg = new WaitDialog();
            waitDlg.MdiParent = _parent;
            waitDlg.Show();
            
            waitDlg.UseWaitCursor = true;
            waitDlg.Update();

            string scoresURL = ConfigurationManager.AppSettings["scoresURL"];
            string statsURL = ConfigurationManager.AppSettings["statsURL"];

            if (Registry.Instance.TourDefinitions == null)
            {
                HTCTourDefinitionsSvc tourDefsSvc = new HTCTourDefinitionsSvc();
                Registry.Instance.TourDefinitions = tourDefsSvc.GetTourDefinitions(ProxySettingsDTO.GetProxySettings(), scoresURL, statsURL);
            }

            waitDlg.Hide();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtbxStartTour_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }


        private void txtbxEndTour_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }


        private void cmboxTourType_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }


        private void txtbxPilotToLoad_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }

        private void cmbBoxSquadSelect_Validating(object sender, CancelEventArgs e)
        {
            ValidateForm();
        }

        private void ValidateForm()
        {
            ValidateTourTypeComboBox();
            ValidatePilotToLoadTextBox();
            ValidateTourTextBoxEntry(this.txtbxEndTour, _EndTourErrorProvider);
            ValidateTourTextBoxEntry(this.txtbxStartTour, _StartTourErrorProvider);
        }

        private void ValidateTourTextBoxEntry(TextBox txtBox, ErrorProvider errorProvider)
        {
            if (cmboxTourType.SelectedItem.ToString() == _selectTourTypeText)
                return;

            bool isValid = true;
            int selectedTour = 0;
            int minLegalTourSelection = GetMinTourForSelectedTourType();
            int maxLegalTourSelection = GetMaxTourForSelectedTourType();

            try
            {
                if (txtBox.Text != string.Empty)
                    selectedTour = System.Convert.ToInt32(txtBox.Text);
            }
            catch
            {
                isValid = false;
            }
            if (isValid && WithinTourBoundaries(selectedTour, minLegalTourSelection, maxLegalTourSelection))
            {
                errorProvider.SetError(txtBox, "");
                EnableLoadButton(true);
            }
            else
            {
                string errorMsg = string.Format("There is no tour {0} in {1}. Please enter a number between {2} and {3}", 
                                                    selectedTour, 
                                                    cmboxTourType.SelectedItem.ToString(), 
                                                    minLegalTourSelection, 
                                                    maxLegalTourSelection);

                errorProvider.SetError(txtBox, errorMsg);
                EnableLoadButton(false);
            }
        }

        private bool WithinTourBoundaries(int selectedTour, int MinTour, int MaxTour)
        {
            return ( selectedTour >= MinTour
                     &&
                     selectedTour <= MaxTour
                    );
        }

        private void ValidateTourTypeComboBox() 
        {
            if (this.cmboxTourType.Text.Length > 0)
            {
                EnableLoadButton(true);
                _TourTypeSelectorErrorProvider.SetError(this.cmboxTourType,"");
            }
            else
            {
                EnableLoadButton(false);
                _TourTypeSelectorErrorProvider.SetError(this.cmboxTourType, "Must select a tour type");
            }
        }

        private void ValidatePilotToLoadTextBox()
        {
            if (this.txtbxPilotToLoad.Text.Length > 0 || cmbBoxSquadSelect.SelectedItem.ToString() != _notSelectedText)
            {
                EnableLoadButton(true);
                _PilotNameErrorProvider.SetError(this.txtbxPilotToLoad, "");
            }
            else
            {
                EnableLoadButton(false);
                _PilotNameErrorProvider.SetError(this.txtbxPilotToLoad, "Must select a pilot game id");
            }
        }

        private void EnableLoadButton(bool enable)
        {
            if (enable)
            {
                if (_PilotNameErrorProvider.GetError(txtbxPilotToLoad) == "" &&
                    _TourTypeSelectorErrorProvider.GetError(cmboxTourType) == "" &&
                    _StartTourErrorProvider.GetError(txtbxStartTour) == "" &&
                    _EndTourErrorProvider.GetError(txtbxEndTour) == "")
                {
                    this.btnLoad.Enabled = true;
                }
            }
            else
            {
                this.btnLoad.Enabled = false;
            }
        }

        private void cmbBoxSquadSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtbxPilotToLoad.Text = "";
            if (cmbBoxSquadSelect.SelectedItem.ToString() != _notSelectedText)
            {
                txtbxPilotToLoad.Enabled = false;
                txtbxEndTour.Text = txtbxStartTour.Text;
                txtbxEndTour.Enabled = false;
            }
            else
            {
                txtbxPilotToLoad.Enabled = true;
                txtbxEndTour.Text = "";
                txtbxEndTour.Enabled = true ;
            }

            ValidateForm();
        }

        private void txtbxPilotToLoad_TextChanged(object sender, EventArgs e)
        {
            cmbBoxSquadSelect.SelectedItem = _notSelectedText;
        }

        private void txtbxStartTour_TextChanged(object sender, EventArgs e)
        {
            txtbxEndTour.Text = txtbxStartTour.Text;
        }

        private void cmboxTourType_TextChanged(object sender, EventArgs e)
        {
            UpdateStartAndEndTourValidations();
        }

        private void cmboxTourType_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateStartAndEndTourValidations();
        }


        private int GetMinTourForSelectedTourType()
        {
            string selectedTour = cmboxTourType.SelectedItem.ToString();
            return GetMinTourForTourType(Registry.Instance.TourDefinitions.Tours[selectedTour]);
        }


        private int GetMinTourForTourType(Dictionary<int, TourNode> tourDef)
        {
            int mintour = 10000;
            foreach (TourNode tour in tourDef.Values)
            {
                if (tour.TourId < mintour)
                    mintour = tour.TourId;
            }

            return mintour;
        }


        private int GetMaxTourForSelectedTourType()
        {
            string selectedTour = cmboxTourType.SelectedItem.ToString();
            return GetMaxTourForTourType(Registry.Instance.TourDefinitions.Tours[selectedTour]);
        }


        private int GetMaxTourForTourType(Dictionary<int, TourNode> tourDef)
        {
            int maxTour = 0;
            foreach (TourNode tour in tourDef.Values)
            {
                if (tour.TourId > maxTour)
                    maxTour = tour.TourId;
            }

            return maxTour;
        }



        private void UpdateStartAndEndTourValidations()
        {
            if (cmboxTourType.SelectedItem.ToString() != _selectTourTypeText)
            {
                this.txtbxStartTour.Enabled = true;

                if(cmbBoxSquadSelect.SelectedItem.ToString() == _notSelectedText)
                    this.txtbxEndTour.Enabled = true;
                else
                    this.txtbxEndTour.Enabled = false;

                this.txtbxStartTour.Text = GetMinTourForSelectedTourType().ToString();
                ValidateForm();
            }
            else
            {
                this.txtbxStartTour.Enabled = false;
                this.txtbxEndTour.Enabled = false;
                EnableLoadButton(false);
            }
        }
    }
}