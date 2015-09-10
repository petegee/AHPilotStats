using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class PilotDataLoaderForm : Form
    {
        #region Nested Classes

        class LoaderError
        {
            public int TourId;
            public string PilotName = "";
            public string ErrorMessage = "";
        }

        class DataLoaderThreadParams
        {
            public DataLoaderThreadParams()
            {
                ToursToLoad = new List<TourNode>();
                PilotIdList = new List<string>();
                StatsErrorList = new List<LoaderError>();
                ScoreErrorList = new List<LoaderError>();
                ScoreList = new List<AcesHighPilotScore>();
                StatsList = new List<AcesHighPilotStats>();
            }

            public List<TourNode> ToursToLoad { get; private set; }
            public UpdateProgressCallBackDelegate ProgressCallBack { get; set; }
            public LoadCompletedCallBackDelegate CompletedCallBack { get; set; }
            public ProxySettingsDTO ProxySettings { get; set; }
            public List<string> PilotIdList { get; private set; }
            public List<LoaderError> StatsErrorList { get; private set; }
            public List<LoaderError> ScoreErrorList { get; private set; }
            public List<AcesHighPilotScore> ScoreList { get; private set; }
            public List<AcesHighPilotStats> StatsList { get; private set; }
        }

        class CompleteTourData
        {
            public int TourId { get; set; }
            public string PilotId { get; set; }
            public AcesHighPilotStats Stats { get; set; }
            public AcesHighPilotScore Score { get; set; }
        }

        #endregion

        #region Private Members

        // Thread callback delegates.
        private delegate void UpdateProgressCallBackDelegate();
        private delegate void LoadCompletedCallBackDelegate();

        // Threads state
        private ParameterizedThreadStart _loaderThreadStart;
        private Thread _loaderThread;
        private int _threadsCompleted;
        private int _unitsLoaded;
        private int _totalStarted;
        private UpdateProgressCallBackDelegate _updateProgressCallBack;
        private LoadCompletedCallBackDelegate _loadCompletedCallBack;
        private DataLoaderThreadParams _threadParam;

        private readonly object _lockObj = new object();

        private readonly ProxySettingsDTO _proxySettings;
        private readonly Form _parent;

        // validation
        private readonly ErrorProvider _startTourErrorProvider;
        private readonly ErrorProvider _endTourErrorProvider;
        private readonly ErrorProvider _pilotNameErrorProvider;
        private readonly ErrorProvider _tourTypeSelectorErrorProvider;

        private const string NotSelectedText = "<Load Single Pilot>";
        private const string SelectTourTypeText = "<Select a Tour Type>";

        /// <summary>
        /// HTC seem to only allow a scores or stats lookup from the same session once every 
        /// four seconds. To be on the safe-side, lets enforce a 5 second wait between calls
        /// to meet their assumed rule.
        /// </summary>
        private const int WaitTimeMillsecondsBetweenHttpCallsToHtc = 5000;

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

            _startTourErrorProvider = new ErrorProvider();
            _startTourErrorProvider.SetIconAlignment(txtbxStartTour, ErrorIconAlignment.MiddleRight);
            _startTourErrorProvider.SetIconPadding(txtbxStartTour, 2);
            _startTourErrorProvider.BlinkRate = 700;
            _startTourErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            _endTourErrorProvider = new ErrorProvider();
            _endTourErrorProvider.SetIconAlignment(txtbxEndTour, ErrorIconAlignment.MiddleRight);
            _endTourErrorProvider.SetIconPadding(txtbxEndTour, 2);
            _endTourErrorProvider.BlinkRate = 700;
            _endTourErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            _pilotNameErrorProvider = new ErrorProvider();
            _pilotNameErrorProvider.SetIconAlignment(txtbxPilotToLoad, ErrorIconAlignment.MiddleRight);
            _pilotNameErrorProvider.SetIconPadding(txtbxPilotToLoad, 2);
            _pilotNameErrorProvider.BlinkRate = 700;
            _pilotNameErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;

            _tourTypeSelectorErrorProvider = new ErrorProvider();
            _tourTypeSelectorErrorProvider.SetIconAlignment(txtbxPilotToLoad, ErrorIconAlignment.MiddleRight);
            _tourTypeSelectorErrorProvider.SetIconPadding(txtbxPilotToLoad, 2);
            _tourTypeSelectorErrorProvider.BlinkRate = 700;
            _tourTypeSelectorErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink;
        }


        /// <summary>
        /// Load button click handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PilotDataLoaderForm_Load(object sender, EventArgs e)
        {
            cmboxTourType.Items.Add(SelectTourTypeText);

            foreach (var tour in Registry.Instance.TourDefinitions.Tours.Keys)
                cmboxTourType.Items.Add(tour);  

            cmboxTourType.SelectedIndex = 0;

            cmbBoxSquadSelect.Items.Add(NotSelectedText);
            foreach(var squadName in Registry.Instance.SquadNamesSet)
                cmbBoxSquadSelect.Items.Add(squadName);

            cmbBoxSquadSelect.SelectedItem = NotSelectedText;
            
            EnableLoadButton(false);

            _updateProgressCallBack = UpdateProgressCallBack;
            _loadCompletedCallBack = LoadCompletedCallBack;
            _loaderThreadStart = LoaderThreadEntryPoint;

            txtbxStartTour.Enabled = false;
            txtbxEndTour.Enabled = false;
        }

        
        /// <summary>
        /// Call back for the loader threads to update their progress on the progress bar
        /// </summary>
        private void UpdateProgressCallBack()
        {
            lock (_lockObj)
            { 
                progressBarLoading.Increment(1);
                lblUnitsLoaded.Text = (++_unitsLoaded).ToString();
            }
        }

        /// <summary>
        /// Call back for the loader threads to notify they have completed.
        /// </summary>
        private void LoadCompletedCallBack()
        {
            lock (_lockObj)
            {
                _threadsCompleted++;
                if (_threadsCompleted == _totalStarted)
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
            var results = new List<CompleteTourData>();

            // Compile stats and scores into one logical unit.
            foreach (var pilotId in _threadParam.PilotIdList)
            {
                results.AddRange(_threadParam.ToursToLoad.Select(tour => new CompleteTourData
                {
                    PilotId = pilotId,
                    TourId = tour.TourId,
                    Score = FindPilotTourScore(pilotId, tour.TourId),
                    Stats = FindPilotTourStats(pilotId, tour.TourId)
                }));
            }

            // Write each complete tour data results out to disk.
            foreach (var tourData in results.Where(tourData => !TourIsInError(tourData)))
            {
                // write out as one logical whole.
                new XMLObjectSerialiser<AcesHighPilotScore>().WriteXmlFile(tourData.Score, string.Format(".\\Data\\{0}_{1}_Score_{2}.xml", tourData.PilotId, tourData.Score.TourType, tourData.Score.TourId));
                new XMLObjectSerialiser<AcesHighPilotStats>().WriteXmlFile(tourData.Stats, string.Format(".\\Data\\{0}_{1}_Stats_{2}.xml", tourData.PilotId, tourData.Stats.TourType, tourData.Stats.TourId));
            }

            // Display any errors that the loader threads caught to the user.
            BuildAndDisplayAnyErrorMessage();

            // Update the Registry and refresh the form.
            if (cmbBoxSquadSelect.SelectedItem.ToString() == NotSelectedText)
                ((MainMDI)MdiParent).RefreshPilotLists(txtbxPilotToLoad.Text);
            else
                ((MainMDI)MdiParent).RefreshSquadMemberPilotLists(cmbBoxSquadSelect.SelectedItem.ToString());

            RestoreForm();
            progressBarLoading.Value = 0;

        }


        /// <summary>
        /// For a given pilot/tour combination, return the relevant score object
        /// </summary>
        /// <param name="pilotId">pilot to find</param>
        /// <param name="tourId">tour to find</param>
        /// <returns>the relevant score object</returns>
        private AcesHighPilotScore FindPilotTourScore(string pilotId, int tourId)
        {
            return _threadParam.ScoreList
                .FirstOrDefault(score => score.GameId == pilotId && score.TourId == tourId.ToString());
        }


        /// <summary>
        /// For a given pilot/tour combination, return the relevant stats object
        /// </summary>
        /// <param name="pilotId">pilot to find</param>
        /// <param name="tourId">tour to find</param>
        /// <returns>the relevant stats object</returns>
        private AcesHighPilotStats FindPilotTourStats(string pilotId, int tourId)
        {
            return _threadParam.StatsList
                .FirstOrDefault(stats => stats.GameId == pilotId && stats.TourId == tourId.ToString());
        }


        /// <summary>
        /// Search through the errors list and display any errors found during 
        /// the load in a dialog box for the user.
        /// </summary>
        private void BuildAndDisplayAnyErrorMessage()
        {
            var inError = false;

            var errorMessageToDisplay = "Loading did not complete sucessfully:\n\n";

            foreach (var error in _threadParam.ScoreErrorList)
            {
                errorMessageToDisplay += error.ErrorMessage + "\n";
                inError = true;
            }

            foreach (var error in _threadParam.StatsErrorList)
            {
                errorMessageToDisplay += error.ErrorMessage + "\n";
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
            if (tourData.Score == null || tourData.Stats == null)
                return true;

            // See if we can find a match for this tour/person combo in the errors lists
            return  _threadParam.ScoreErrorList.Any(error => error.TourId == tourData.TourId && error.PilotName == tourData.PilotId) ||
                    _threadParam.StatsErrorList.Any(error => error.TourId == tourData.TourId && error.PilotName == tourData.PilotId);
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
                btnCancel.Enabled = false;

                var tourStart = Convert.ToInt32(txtbxStartTour.Text);
                var tourEnd = Convert.ToInt32(txtbxEndTour.Text);

                _threadParam = new DataLoaderThreadParams();

                // if a single pilot, add single pilot name to the list.
                if (cmbBoxSquadSelect.SelectedItem.ToString() == NotSelectedText)
                {
                    _threadParam.PilotIdList.Add(CommonUtils.ToUpperFirstChar(txtbxPilotToLoad.Text));
                }
                else // its a squad - so add each member of the sqaud to the list.
                {
                    var squad = Registry.Instance.GetSquad(cmbBoxSquadSelect.SelectedItem.ToString());
                    foreach (var squadMember in squad.Members.Where(squadMember => squadMember.StartTour <= tourStart && squadMember.EndTour >= tourStart))
                    {
                        _threadParam.PilotIdList.Add(CommonUtils.ToUpperFirstChar(squadMember.PilotName));
                    }
                }

                var tourType = cmboxTourType.SelectedItem.ToString();
               
                // build the list of tours to load.
                for (var tourId = tourStart; tourId <= tourEnd; tourId++)
                {
                    _threadParam.ToursToLoad.Add(Registry.Instance.TourDefinitions.FindTour(tourType, tourId));
                }

                // Set the thread callbacks.
                _threadParam.CompletedCallBack = _loadCompletedCallBack;
                _threadParam.ProgressCallBack  = _updateProgressCallBack;

                // Make sure they actually selected something sane. Shouldnt happen due to form validation.
                if (cmbBoxSquadSelect.SelectedItem.ToString() == NotSelectedText && string.IsNullOrEmpty(txtbxPilotToLoad.Text))
                {
                    throw new Exception();
                }
              
                _threadParam.ProxySettings = _proxySettings;

                // Kick off the threads to do the dirty work.
                _loaderThread = new Thread(_loaderThreadStart);
                _loaderThread.Start(_threadParam);

                _totalStarted = 1;

                if (cmbBoxSquadSelect.SelectedItem.ToString() == NotSelectedText)
                    progressBarLoading.Maximum = (tourEnd - tourStart + 1) * 2;
                else
                {
                    //var squad = Registry.Instance.GetSquad(cmbBoxSquadSelect.SelectedItem.ToString());
                    progressBarLoading.Maximum = _threadParam.PilotIdList.Count * 2;
                }
                    

                lblUnitsToLoad.Text = progressBarLoading.Maximum.ToString();
                lblLoading.Visible = true;
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
            lblUnitsToLoad.Text = "0";
            btnCancel.Enabled = true;
            lblLoading.Visible = false;
            _unitsLoaded = 0;
            _threadsCompleted = 0;
        }


        /// <summary>
        /// Set all the child controls enabled property.
        /// </summary>
        /// <param name="enable">true to enable, false to disable.</param>
        private void EnableControls(bool enable)
        {
            label1.Enabled = enable;
            label2.Enabled = enable;
            label3.Enabled = enable;
            label4.Enabled = enable;
            label5.Enabled = enable;
            txtbxPilotToLoad.Enabled = enable;
            txtbxPilotToLoad.Enabled = enable;
            txtbxStartTour.Enabled = enable;
            cmboxTourType.Enabled = enable;

            txtbxEndTour.Enabled = cmbBoxSquadSelect.SelectedItem.ToString() == NotSelectedText && enable;

            cmbBoxSquadSelect.Enabled = enable;

            EnableLoadButton(enable);  
        }


        /// <summary>
        /// Thread start point for the stats loader thread.
        /// </summary>
        /// <param name="obj">DataLoaderThreadParams</param>
        public void LoaderThreadEntryPoint(object obj)
        {
            var param = (DataLoaderThreadParams)obj;

            try
            {
                var onlyOneToLoad = param.ToursToLoad.Count * param.PilotIdList.Count == 1;

                foreach (var tour in param.ToursToLoad)
                {
                    foreach (var pilotId in param.PilotIdList)
                    {
                        //
                        // Load the Stats objects.
                        //
                        var statsUrl = ConfigurationManager.AppSettings["statsURL"];
                        var statsSvc = new HTCPilotStatsSvc();
                        try
                        {
                            param.StatsList.Add(statsSvc.GetPilotStats(pilotId, tour, param.ProxySettings, statsUrl));
                        }
                        catch (Exception e)
                        {
                            var loaderError = new LoaderError
                            {
                                TourId = tour.TourId,
                                PilotName = pilotId,
                                ErrorMessage =
                                    string.Format(" - could not load stats objects for pilot {0} for {1} tour {2}.",
                                        pilotId, tour.TourType, tour.TourId)
                            };
                            param.StatsErrorList.Add(loaderError);
                            Utility.WriteDebugTraceFile(e);
                        }

                        // Wait between calls.
                        Thread.Sleep(WaitTimeMillsecondsBetweenHttpCallsToHtc);
                        progressBarLoading.Invoke(param.ProgressCallBack);

                        //
                        // Load the scores objects.
                        //
                        var scoreSvc = new HTCPilotScoreSvc();
                        var scoresUrl = ConfigurationManager.AppSettings["scoresURL"];
                        try
                        {
                            param.ScoreList.Add(scoreSvc.GetPilotScore(pilotId, tour, param.ProxySettings, scoresUrl));
                        }
                        catch (Exception e)
                        {
                            var error = new LoaderError
                            {
                                TourId = tour.TourId,
                                PilotName = pilotId,
                                ErrorMessage =
                                    string.Format(" - could not load scores objects for pilot {0} for {1} tour {2}.",
                                        pilotId, tour.TourType, tour.TourId)
                            };
                            param.ScoreErrorList.Add(error);
                            Utility.WriteDebugTraceFile(e);
                        }

                        progressBarLoading.Invoke(param.ProgressCallBack);

                        if (!onlyOneToLoad)
                            Thread.Sleep(WaitTimeMillsecondsBetweenHttpCallsToHtc);
                    }
                }

            }
            finally
            {
                btnLoad.Invoke(param.CompletedCallBack);
            }
        }


        /// <summary>
        /// Calls the HTCPilotStatsSvc to load all the current tour definitions.
        /// </summary>
        private void LoadTourDefs()
        {
            var waitDlg = new WaitDialog
            {
                MdiParent = _parent,
                UseWaitCursor = true
            };
            waitDlg.Show();
            waitDlg.Update();

            var scoresUrl = ConfigurationManager.AppSettings["scoresURL"];
            var statsUrl = ConfigurationManager.AppSettings["statsURL"];

            if (Registry.Instance.AreTourDefinitionsInitialised() == false)
            {
                var tourDefsSvc = new HTCTourDefinitionsSvc();
                Registry.Instance.TourDefinitions = tourDefsSvc.GetTourDefinitions(ProxySettingsDTO.GetProxySettings(), scoresUrl, statsUrl);
            }

            waitDlg.Hide();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
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
            ValidateTourTextBoxEntry(txtbxEndTour, _endTourErrorProvider);
            ValidateTourTextBoxEntry(txtbxStartTour, _startTourErrorProvider);
        }

        private void ValidateTourTextBoxEntry(TextBox txtBox, ErrorProvider errorProvider)
        {
            if (cmboxTourType.SelectedItem.ToString() == SelectTourTypeText)
                return;

            var isValid = true;
            var selectedTour = 0;
            var minLegalTourSelection = GetMinTourForSelectedTourType();
            var maxLegalTourSelection = GetMaxTourForSelectedTourType();

            try
            {
                if (txtBox.Text != string.Empty)
                    selectedTour = Convert.ToInt32(txtBox.Text);
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
                var errorMsg = string.Format("There is no tour {0} in {1}. Please enter a number between {2} and {3}", 
                                                    selectedTour, 
                                                    cmboxTourType.SelectedItem, 
                                                    minLegalTourSelection, 
                                                    maxLegalTourSelection);

                errorProvider.SetError(txtBox, errorMsg);
                EnableLoadButton(false);
            }
        }

        private bool WithinTourBoundaries(int selectedTour, int minTour, int maxTour)
        {
            return ( selectedTour >= minTour
                     &&
                     selectedTour <= maxTour
                    );
        }

        private void ValidateTourTypeComboBox() 
        {
            if (cmboxTourType.Text.Length > 0)
            {
                EnableLoadButton(true);
                _tourTypeSelectorErrorProvider.SetError(cmboxTourType,"");
            }
            else
            {
                EnableLoadButton(false);
                _tourTypeSelectorErrorProvider.SetError(cmboxTourType, "Must select a tour type");
            }
        }

        private void ValidatePilotToLoadTextBox()
        {
            if (txtbxPilotToLoad.Text.Length > 0 || cmbBoxSquadSelect.SelectedItem.ToString() != NotSelectedText)
            {
                EnableLoadButton(true);
                _pilotNameErrorProvider.SetError(txtbxPilotToLoad, "");
            }
            else
            {
                EnableLoadButton(false);
                _pilotNameErrorProvider.SetError(txtbxPilotToLoad, "Must select a pilot game id");
            }
        }

        private void EnableLoadButton(bool enable)
        {
            if (enable)
            {
                if (_pilotNameErrorProvider.GetError(txtbxPilotToLoad) == "" &&
                    _tourTypeSelectorErrorProvider.GetError(cmboxTourType) == "" &&
                    _startTourErrorProvider.GetError(txtbxStartTour) == "" &&
                    _endTourErrorProvider.GetError(txtbxEndTour) == "")
                {
                    btnLoad.Enabled = true;
                }
            }
            else
            {
                btnLoad.Enabled = false;
            }
        }

        private void cmbBoxSquadSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbxPilotToLoad.Text = "";
            if (cmbBoxSquadSelect.SelectedItem.ToString() != NotSelectedText)
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
            cmbBoxSquadSelect.SelectedItem = NotSelectedText;
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
            var selectedTour = cmboxTourType.SelectedItem.ToString();
            return GetMinTourForTourType(Registry.Instance.TourDefinitions.Tours[selectedTour]);
        }


        private int GetMinTourForTourType(Dictionary<int, TourNode> tourDef)
        {
            return tourDef.Min(td => td.Value.TourId);
        }


        private int GetMaxTourForSelectedTourType()
        {
            var selectedTour = cmboxTourType.SelectedItem.ToString();
            return GetMaxTourForTourType(Registry.Instance.TourDefinitions.Tours[selectedTour]);
        }


        private int GetMaxTourForTourType(Dictionary<int, TourNode> tourDef)
        {
            return tourDef.Max(td => td.Value.TourId);
        }



        private void UpdateStartAndEndTourValidations()
        {
            if (cmboxTourType.SelectedItem.ToString() != SelectTourTypeText)
            {
                txtbxStartTour.Enabled = true;
                txtbxEndTour.Enabled = cmbBoxSquadSelect.SelectedItem.ToString() == NotSelectedText;
                txtbxStartTour.Text = GetMinTourForSelectedTourType().ToString();
                ValidateForm();
            }
            else
            {
                txtbxStartTour.Enabled = false;
                txtbxEndTour.Enabled = false;
                EnableLoadButton(false);
            }
        }
    }
}