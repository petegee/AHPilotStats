using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;


namespace My2Cents.HTC.AHPilotStats
{
    sealed class Registry // SINGLETON
    {
        public class CaseInsensitiveDictionary<TValue>
        {
            public CaseInsensitiveDictionary()
            { 
            }

            private Dictionary<string, TValue> internalDictionary = new Dictionary<string, TValue>();

            public void Add(string key, TValue value)
            {
                internalDictionary.Add(key.ToLower(), value);
            }

            public bool ContainsKey(string key)
            {
                return internalDictionary.ContainsKey(key.ToLower());
            }

            public bool Remove(string key)
            {
                return internalDictionary.Remove(key.ToLower());
            }

            public int Count
            {
                get { return internalDictionary.Count; }
            }

            public TValue this [string key]
            {
                get { return internalDictionary[key.ToLower()]; }
                set { internalDictionary[key.ToLower()] = value; }
            }
        }


        #region Fields
        private static readonly object obj = new object();
        private static Registry _instance;
        private CaseInsensitiveDictionary<PilotStatsRegistry> pilotDictionary = new CaseInsensitiveDictionary<PilotStatsRegistry>();

        private CaseInsensitiveDictionary<SortableList<AcesHighPilotScore>> _PilotScoreObjMap = new CaseInsensitiveDictionary<SortableList<AcesHighPilotScore>>();
        private CaseInsensitiveDictionary<SortableList<AcesHighPilotStats>> _PilotStatsObjMap = new CaseInsensitiveDictionary<SortableList<AcesHighPilotStats>>();
        
        private List<string> _pilotNamesSet  = new List<string>();
        private List<string> _squadNamesSet  = new List<string>();
        private List<string> _modelList = new List<string>();
        private List<Squad> _squadList = new List<Squad>();

        private TourDefinitions _tourDefinitions = null;

        #endregion

        #region Attributes
        public List<string> PilotNamesSet
        {
            get { return _pilotNamesSet; }
            set { _pilotNamesSet = value; }
        }
        public List<string> SquadNamesSet
        {
            get { return _squadNamesSet; }
            set { _squadNamesSet = value; }
        }
        public List<string> ModelList
        {
            get { return _modelList; }
            set { _modelList = value; }
        }
        public TourDefinitions TourDefinitions
        {
            get { return _tourDefinitions; }
            set { _tourDefinitions = value; }
        }

        #endregion

        private Registry() 
        {
            PopulatePilotList();
            PopulateSquadList();
            BuildStatsRegistry();
        }

        public static Registry Instance
        {
            get
            {
                lock (obj)
                {
                    if (_instance == null)
                    {
                        _instance = new Registry();
                    }
                    return _instance;
                }
            }
        }

        public PilotStatsRegistry GetPilotStats(string pilotName)
        {
            if (pilotDictionary.Count == 0)
                BuildStatsRegistry();

            if (pilotDictionary.Count == 0)
                throw new ApplicationException("Failed to build registry or no XML data files found!!!");

            if (pilotDictionary.ContainsKey(pilotName))
                return pilotDictionary[pilotName];
            else
            {
                throw new PilotDoesNotExistInRegistryException(string.Format("\nCannot find any data for pilot \"{0}\"!\nPlease download pilot data.", pilotName));
            }
        }


        public bool PilotStatsContains(string pilotName)
        {
            if (pilotDictionary.Count == 0)
                BuildStatsRegistry();

            if (pilotDictionary.Count == 0)
                throw new ApplicationException("Failed to build registry or no XML data files found!!!");

            return pilotDictionary.ContainsKey(pilotName);
        }

        public void AddPilotStatsToRegistry(string pilotName, PilotStatsRegistry stats)
        {
            if (!pilotDictionary.ContainsKey(pilotName))
                pilotDictionary.Add(pilotName, stats);
        }

        public void ReloadPilot(string reloadedPilotName)
        {
            RemovePilot(reloadedPilotName);
            BuildStatsRegistry();
        }

        public void RemovePilot(string pilotName)
        {
            if (_PilotScoreObjMap.ContainsKey(pilotName))
                _PilotScoreObjMap.Remove(pilotName);

            if (_PilotStatsObjMap.ContainsKey(pilotName))
                _PilotStatsObjMap.Remove(pilotName);

            pilotDictionary.Remove(pilotName);
        }

        public void PopulatePilotList()
        {
            if(!System.IO.Directory.Exists(@"data"))
            {
                System.IO.Directory.CreateDirectory(@"data");
                return;
            }

            string[] xmlFileNames = System.IO.Directory.GetFiles(@"data", "*.xml");

            foreach (string xmlFileName in xmlFileNames)
            {
                string pilotName = xmlFileName.Substring(0, xmlFileName.IndexOf('_', 0)).Substring(xmlFileName.IndexOf(@"\") + 1);
                pilotName = CommonUtils.ToUpperFirstChar(pilotName);
                if (!_pilotNamesSet.Contains(pilotName))
                {
                    _pilotNamesSet.Add(pilotName);
                }
            }
        }

        public void PopulateSquadList()
        {
            if (!System.IO.Directory.Exists(@"squads"))
            {
                System.IO.Directory.CreateDirectory(@"squads");
                return;
            }

            string[] xmlFileNames = System.IO.Directory.GetFiles(@"squads", "*.xml");

            foreach (string xmlFileName in xmlFileNames)
            {
                string squadFileName = xmlFileName.Substring(0, xmlFileName.IndexOf('_', 0)).Substring(xmlFileName.IndexOf(@"\") + 1);

                Squad squad = Squad.LoadSquad(squadFileName);
                _squadList.Add(squad);

                if (!_squadNamesSet.Contains(squad.SquadName))
                {
                    _squadNamesSet.Add(squad.SquadName);
                }
            }
        }

        public void ConstructScoresForPilot(AcesHighPilotScore score, string pilotName)
        {
            FighterScoresDO fighterScoreDO = new FighterScoresDO(score);
            GetPilotStats(pilotName).FighterScoresList.Add(fighterScoreDO);

            FighterStatsDO fighterStatsDO = new FighterStatsDO(score);
            GetPilotStats(pilotName).FighterStatsList.Add(fighterStatsDO);

            AttackScoresDO attackScoresDO = new AttackScoresDO(score);
            GetPilotStats(pilotName).AttackScoresList.Add(attackScoresDO);

            AttackStatsDO attackStatsDO = new AttackStatsDO(score);
            GetPilotStats(pilotName).AttackStatsList.Add(attackStatsDO);

            BomberScoresDO bomberScoresDO = new BomberScoresDO(score);
            GetPilotStats(pilotName).BomberScoresList.Add(bomberScoresDO);

            BomberStatsDO bomberStatsDO = new BomberStatsDO(score);
            GetPilotStats(pilotName).BomberStatsList.Add(bomberStatsDO);

            VehicleBoatScoresDO vehicleBoatScoresDO = new VehicleBoatScoresDO(score);
            GetPilotStats(pilotName).VehicleBoatScoresList.Add(vehicleBoatScoresDO);

            VehicleBoatStatsDO vehicleBoatStatsDO = new VehicleBoatStatsDO(score);
            GetPilotStats(pilotName).VehicleBoatStatsList.Add(vehicleBoatStatsDO);
        }

        public void ConstructStatsForPilot(AcesHighPilotStats stats, string pilotName)
        {
            if (stats.VsObjects.ObjectScore == null)
                throw new ApplicationException(string.Format("No stats objects can be found for pilot {0} in tour {1}", pilotName, stats.TourDetails));


            // for each aircraft listed for that tour.
            foreach (ObjectScore objScore in stats.VsObjects.ObjectScore)
            {
                // build mathmatical set of models (eg no duplicate entries).
                AddToModelList(objScore.Model);

                // add objVsObj score to our complete list.
                ObjectVsObjectDO objVsObjDO = new ObjectVsObjectDO(objScore, stats.TourDetails, stats.TourType, Int32.Parse(stats.TourId));
                GetPilotStats(pilotName).ObjVsObjCompleteList.Add(objVsObjDO);
            }
        }


        public Squad GetSquad(string squadName)
        {
            foreach (Squad sq in _squadList)
            {
                if (sq.SquadName == squadName)
                    return sq;
            }

            throw new SquadDoesNotExistInRegistryException(string.Format("Cant find squad", squadName));
        }


        private void BuildStatsRegistry()
        {
            List<string> errors = new List<string>();
            foreach (string pilotName in _pilotNamesSet)
            {
                if (!_PilotScoreObjMap.ContainsKey(pilotName))
                {
                    AddPilotStatsToRegistry(pilotName, new Registry.PilotStatsRegistry());
                    LoadScoreObjects(pilotName);
                    LoadStatsObjects(pilotName);

                    SortableList<AcesHighPilotScore> scoresList = _PilotScoreObjMap[pilotName];
                    SortableList<AcesHighPilotStats> statsList = _PilotStatsObjMap[pilotName];

                    foreach (AcesHighPilotScore score in scoresList)
                    {
                        try
                        {
                            ConstructScoresForPilot(score, pilotName);
                        }
                        catch(Exception ex)
                        {
                            errors.Add(ex.Message);
                        }
                    }

                    // For each tour.
                    foreach (AcesHighPilotStats stats in statsList)
                    {                   
                        try
                        {
                            ConstructStatsForPilot(stats, pilotName);
                        }
                        catch (Exception ex)
                        {
                            errors.Add(ex.Message);
                        }
                    }
                }
            }


            if (errors.Count > 0)
            {
                StringBuilder displayMsg = new StringBuilder();
                foreach (string errorMsg in errors)
                {
                    displayMsg.AppendFormat("{0}\n", errorMsg);
                }
                System.Windows.Forms.MessageBox.Show(string.Format("Some errors were encountered building pilot data:\n{0}", displayMsg.ToString()), "Aces High Pilot Stats");
            }
        }

        private void LoadScoreObjects(string selectedPilot)
        {
            SortableList<AcesHighPilotScore> scores = new SortableList<AcesHighPilotScore>();
            string[] xmlFileNames = System.IO.Directory.GetFiles(@"data", string.Format("{0}*_Score*.xml", selectedPilot));
            foreach (string xmlFileName in xmlFileNames)
            {
                XmlSerializer xSerializer = new XmlSerializer(typeof(AcesHighPilotScore));
                AcesHighPilotScore score = (AcesHighPilotScore)xSerializer.Deserialize(new StreamReader(xmlFileName));
                scores.Add(score);
            }

            if (!_PilotScoreObjMap.ContainsKey(selectedPilot))
                _PilotScoreObjMap.Add(selectedPilot, scores);
        }

        private void LoadStatsObjects(string selectedPilot)
        {
            SortableList<AcesHighPilotStats> stats = new SortableList<AcesHighPilotStats>();
            string[] xmlFileNames = System.IO.Directory.GetFiles(@"data", string.Format("{0}*_Stats*.xml", selectedPilot));
            foreach (string xmlFileName in xmlFileNames)
            {
                XmlSerializer xSerializer = new XmlSerializer(typeof(AcesHighPilotStats));
                AcesHighPilotStats stat = (AcesHighPilotStats)xSerializer.Deserialize(new StreamReader(xmlFileName));
                stats.Add(stat);
            }

            if (!_PilotStatsObjMap.ContainsKey(selectedPilot))
                _PilotStatsObjMap.Add(selectedPilot, stats);
        }

        private void AddToModelList(string model)
        {
            if (model == null)
                //return;// ignore - dont try add // 
            throw new ArgumentNullException("Cant add a null model to model list!!!");

            if (!_modelList.Contains(model))
                _modelList.Add(model);
        }

        public class PilotStatsRegistry
        {
            private SortableList<FighterScoresDO> _FighterScoresList = new SortableList<FighterScoresDO>();
            private SortableList<StatsDomainObject> _FighterStatsList = new SortableList<StatsDomainObject>();
            private SortableList<AttackScoresDO> _AttackScoresList = new SortableList<AttackScoresDO>();
            private SortableList<StatsDomainObject> _AttackStatsList = new SortableList<StatsDomainObject>();
            private SortableList<BomberScoresDO> _BomberScoresList = new SortableList<BomberScoresDO>();
            private SortableList<StatsDomainObject> _BomberStatsList = new SortableList<StatsDomainObject>();
            private SortableList<VehicleBoatScoresDO> _VehicleBoatScoresList = new SortableList<VehicleBoatScoresDO>();
            private SortableList<StatsDomainObject> _VehicleBoatStatsList = new SortableList<StatsDomainObject>();
            private SortableList<ObjectVsObjectDO> _ObjVsObjCompleteList = new SortableList<ObjectVsObjectDO>();
            private SortableBindingList<ObjectVsObjectDO> _ObjVsObjVisibleList = new SortableBindingList<ObjectVsObjectDO>();

            public SortableList<FighterScoresDO> FighterScoresList
            {
                get { return _FighterScoresList; }
                set { _FighterScoresList = value; }
            }

            public SortableList<StatsDomainObject> FighterStatsList
            {
                get { return _FighterStatsList; }
                set { _FighterStatsList = value; }
            }

            public SortableList<AttackScoresDO> AttackScoresList
            {
                get { return _AttackScoresList; }
                set { _AttackScoresList = value; }
            }

            public SortableList<StatsDomainObject> AttackStatsList
            {
                get { return _AttackStatsList; }
                set { _AttackStatsList = value; }
            }

            public SortableList<BomberScoresDO> BomberScoresList
            {
                get { return _BomberScoresList; }
                set { _BomberScoresList = value; }
            }

            public SortableList<StatsDomainObject> BomberStatsList
            {
                get { return _BomberStatsList; }
                set { _BomberStatsList = value; }
            }

            public SortableList<VehicleBoatScoresDO> VehicleBoatScoresList
            {
                get { return _VehicleBoatScoresList; }
                set { _VehicleBoatScoresList = value; }
            }

            public SortableList<StatsDomainObject> VehicleBoatStatsList
            {
                get { return _VehicleBoatStatsList; }
                set { _VehicleBoatStatsList = value; }
            }

            public SortableList<ObjectVsObjectDO> ObjVsObjCompleteList
            {
                get { return _ObjVsObjCompleteList; }
                set { _ObjVsObjCompleteList = value; }
            }

            public SortableBindingList<ObjectVsObjectDO> ObjVsObjVisibleList
            {
                get { return _ObjVsObjVisibleList; }
                set { _ObjVsObjVisibleList = value; }
            }      
        }

    }
}
