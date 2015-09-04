using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;


namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
/*
    internal class HTCPilotCombinedDataSvc
    {
        internal delegate void UpdateProgressCallBackDelegate();
        internal delegate void LoadCompletedCallBackDelegate();


        class DataLoaderThreadParams
        {
            public DataLoaderThreadParams()
            {
            }

            public List<HTCTour> toursToLoad = new List<HTCTour>();
            public UpdateProgressCallBackDelegate updateProgressCallBack;
            public LoadCompletedCallBackDelegate loadCompletedCallBack;
            public ProxySettingsDTO proxySettings;
            public List<string> pilotsToLoad = new List<string>();
            public List<LoaderError> statsErrorList = new List<LoaderError>();
            public List<LoaderError> scoreErrorList = new List<LoaderError>();
            public List<AcesHighPilotScore> scoreList = new List<AcesHighPilotScore>();
            public List<AcesHighPilotStats> statsList = new List<AcesHighPilotStats>();
        }

        class LoaderError
        {
            public int tourId;
            public string pilotName = "";
            public string errorMessage = "";
        }


        internal DataLoaderThreadParams _threadParams = new DataLoaderThreadParams();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pilotsToLoad"></param>
        /// <param name="toursToLoad"></param>
        /// <param name="proxySettings"></param>
        /// <returns></returns>
        internal List<CompleteTourData> LoadAllData(List<string> pilotsToLoad,
                                                    List<HTCTour> toursToLoad,
                                                    ProxySettingsDTO proxySettings)
        {
            LoadAllData(pilotsToLoad, toursToLoad, proxySettings, null, null);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pilotsToLoad"></param>
        /// <param name="toursToLoad"></param>
        /// <param name="proxySettings"></param>
        /// <param name="progressCallback"></param>
        /// <param name="completeCallback"></param>
        /// <returns></returns>
        internal List<CompleteTourData> LoadAllData(List<string> pilotsToLoad, 
                                                    List<HTCTour> toursToLoad, 
                                                    ProxySettingsDTO proxySettings, 
                                                    UpdateProgressCallBackDelegate progressCallback, 
                                                    LoadCompletedCallBackDelegate completeCallback )
        {

            _threadParams.toursToLoad = toursToLoad;
            _threadParams.pilotsToLoad = pilotsToLoad;
            _threadParams.proxySettings = proxySettings;
            _threadParams.loadCompletedCallBack = completeCallback;
            _threadParams.updateProgressCallBack = progressCallback;


            // Start the loader threads.
            _ScoreLoaderThread = new Thread(_ScoreThreadStart);
            _ScoreLoaderThread.Start(_threadParams);
            _StatsLoaderThread = new Thread(_StatsThreadStart);
            _StatsLoaderThread.Start(_threadParams);
        }

        #region Nested Classes
        //
        // Nested Classes
        //




        #endregion

        // Thread callback delegates.
        private delegate void UpdateProgressCallBackDelegate();
        private delegate void LoadCompletedCallBackDelegate();
        private delegate void TourDefLoadCompletedCallBackDelegate();

        // Threads state
        private ParameterizedThreadStart _StatsThreadStart;
        private ParameterizedThreadStart _ScoreThreadStart;
        private Thread _ScoreLoaderThread;
        private Thread _StatsLoaderThread;
        private int _ThreadsCompleted = 0;
        private int _UnitsLoaded = 0;
        private int _TotalStarted = 0;
        private UpdateProgressCallBackDelegate _UpdateProgressCallBack;
        private LoadCompletedCallBackDelegate _LoadCompletedCallBack;
        private DataLoaderThreadParams _threadParam;

        private readonly object lockObj = new object();



        internal HTCPilotCombinedDataSvc() 
        {
            _UpdateProgressCallBack = new UpdateProgressCallBackDelegate(UpdateProgressCallBack);
            _LoadCompletedCallBack = new LoadCompletedCallBackDelegate(LoadCompletedCallBack);

            _StatsThreadStart = new ParameterizedThreadStart(this.StatsThreadEntryPoint);
            _ScoreThreadStart = new ParameterizedThreadStart(this.ScoreThreadEntryPoint);
        }



        private void ProcessLoadCompleted()
        {
            List<CompleteTourData> results = new List<CompleteTourData>();

            // Compile stats and scores into one logical unit.
            foreach (string pilotId in _threadParam.pilotIdList)
            {
                foreach (HTCTour tour in _threadParam.toursToLoad)
                {
                    CompleteTourData compTourData = new CompleteTourData();

                    compTourData.pilotId = pilotId;
                    compTourData.tourId = tour.scoreTourDefinion.TourId;
                    compTourData.score = FindPilotTourScore(pilotId, tour.scoreTourDefinion.TourId);
                    compTourData.stats = FindPilotTourStats(pilotId, tour.scoreTourDefinion.TourId);

                    results.Add(compTourData);
                }
            }

            // Write each complete tour data results out to disk.
            foreach (CompleteTourData tourData in results)
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


        internal void UpdateProgressCallBack()
        {
            lock (lockObj)
            {
                this.progressBarLoading.Increment(1);
                this.lblUnitsLoaded.Text = (++_UnitsLoaded).ToString();
            }
        }


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

    }
 * */
}
