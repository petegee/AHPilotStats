using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using My2Cents.HTC.AHPilotStats.Collections;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;

namespace My2Cents.HTC.AHPilotStats.DataRepository
{
    public class Registry : IRegistry
    {
        public Registry()
        {
            PilotNamesSet = new HashSet<string>();
            SquadNamesSet = new HashSet<string>();
            ModelSet = new HashSet<string>();
            TourDefinitions = new TourDefinitions();

            _pilotDictionary = new CaseInsensitiveDictionary<PilotStats>();
            _pilotScoreObjMap = new CaseInsensitiveDictionary<SortableList<AcesHighPilotScore>>();
            _pilotStatsObjMap = new CaseInsensitiveDictionary<SortableList<AcesHighPilotStats>>();
            _squadList = new List<Squad>();

            PopulatePilotList();
            PopulateSquadList();
            BuildStatsRegistry();
        }

        public HashSet<string> PilotNamesSet { get; set; }
        public HashSet<string> SquadNamesSet { get; set; }
        public HashSet<string> ModelSet { get; set; }
        public TourDefinitions TourDefinitions { get; set; }

        public bool AreTourDefinitionsInitialised()
        {
            return TourDefinitions != null && TourDefinitions.IsTourDefinitionsComplete();
        }

        public PilotStats GetPilotStats(string pilotName)
        {
            if (_pilotDictionary.Count == 0)
                BuildStatsRegistry();

            if (_pilotDictionary.Count == 0)
                throw new ApplicationException("Failed to build registry or no XML data files found!!!");

            if (_pilotDictionary.ContainsKey(pilotName))
                return _pilotDictionary[pilotName];

            throw new PilotDoesNotExistInRegistryException(
                string.Format("\nCannot find any data for pilot \"{0}\"!\nPlease download pilot data.", pilotName));
        }


        public bool PilotStatsContains(string pilotName)
        {
            if (_pilotDictionary.Count == 0)
                BuildStatsRegistry();

            if (_pilotDictionary.Count == 0)
                throw new ApplicationException("Failed to build registry or no XML data files found!!!");

            return _pilotDictionary.ContainsKey(pilotName);
        }

        public void ReloadPilot(string reloadedPilotName)
        {
            RemovePilot(reloadedPilotName);
            BuildStatsRegistry();
        }

        public void RemovePilot(string pilotName)
        {
            if (_pilotScoreObjMap.ContainsKey(pilotName))
                _pilotScoreObjMap.Remove(pilotName);

            if (_pilotStatsObjMap.ContainsKey(pilotName))
                _pilotStatsObjMap.Remove(pilotName);

            _pilotDictionary.Remove(pilotName);
        }

        public void AddPilotStatsToRegistry(string pilotName, PilotStats stats)
        {
            if (!_pilotDictionary.ContainsKey(pilotName))
                _pilotDictionary.Add(pilotName, stats);
        }

        public void PopulatePilotList()
        {
            if (!Directory.Exists(@"data"))
            {
                Directory.CreateDirectory(@"data");
                return;
            }

            foreach (var pilotName in Directory.GetFiles(@"data", "*.xml").Select(xmlFileName => xmlFileName.Substring(0, xmlFileName.IndexOf('_', 0)).Substring(xmlFileName.IndexOf(@"\") + 1)))
            {
                PilotNamesSet.Add(pilotName.ToUpperFirstChar());
            }
        }

        public void ConstructScoresForPilot(AcesHighPilotScore score, string pilotName)
        {
            var fighterScoreDo = new FighterScoresDO(score);
            GetPilotStats(pilotName).FighterScoresList.Add(fighterScoreDo);

            var fighterStatsDo = new FighterStatsDO(score);
            GetPilotStats(pilotName).FighterStatsList.Add(fighterStatsDo);

            var attackScoresDo = new AttackScoresDO(score);
            GetPilotStats(pilotName).AttackScoresList.Add(attackScoresDo);

            var attackStatsDo = new AttackStatsDO(score);
            GetPilotStats(pilotName).AttackStatsList.Add(attackStatsDo);

            var bomberScoresDo = new BomberScoresDO(score);
            GetPilotStats(pilotName).BomberScoresList.Add(bomberScoresDo);

            var bomberStatsDo = new BomberStatsDO(score);
            GetPilotStats(pilotName).BomberStatsList.Add(bomberStatsDo);

            var vehicleBoatScoresDo = new VehicleBoatScoresDO(score);
            GetPilotStats(pilotName).VehicleBoatScoresList.Add(vehicleBoatScoresDo);

            var vehicleBoatStatsDo = new VehicleBoatStatsDO(score);
            GetPilotStats(pilotName).VehicleBoatStatsList.Add(vehicleBoatStatsDo);
        }

        public void ConstructStatsForPilot(AcesHighPilotStats stats, string pilotName)
        {
            if (stats.VsObjects.ObjectScore == null)
                throw new ApplicationException(string.Format("No stats objects can be found for pilot {0} in tour {1}",
                    pilotName, stats.TourDetails));

            // for each aircraft listed for that tour.
            foreach (var objScore in stats.VsObjects.ObjectScore)
            {
                // build mathmatical set of models (eg no duplicate entries).
                ModelSet.Add(objScore.Model);

                // add objVsObj score to our complete list.
                var objVsObjDo = new ObjectVsObjectDO(objScore, stats.TourDetails, stats.TourType,
                    int.Parse(stats.TourId));
                GetPilotStats(pilotName).ObjVsObjCompleteList.Add(objVsObjDo);
            }
        }

        public Squad GetSquad(string squadName)
        {
            var sqaud = _squadList.SingleOrDefault(s => s.SquadName == squadName);
            if (sqaud == null)
                throw new SquadDoesNotExistInRegistryException(string.Format("Cant find squad {0}", squadName));

            return sqaud;
        }

        public void PopulateSquadList()
        {
            if (!Directory.Exists(@"squads"))
            {
                Directory.CreateDirectory(@"squads");
                return;
            }

            foreach (var xmlFileName in Directory.GetFiles(@"squads", "*.xml"))
            {
                var squadFileName =
                    xmlFileName.Substring(0, xmlFileName.IndexOf('_', 0)).Substring(xmlFileName.IndexOf(@"\") + 1);

                var squad = Squad.LoadSquad(squadFileName);
                _squadList.Add(squad);
                SquadNamesSet.Add(squad.SquadName);
            }
        }

        private void BuildStatsRegistry()
        {
            var errors = new List<string>();
            foreach (var pilotName in PilotNamesSet.Where(pilotName => !_pilotScoreObjMap.ContainsKey(pilotName)))
            {
                AddPilotStatsToRegistry(pilotName, new PilotStats());
                LoadScoreObjects(pilotName);
                LoadStatsObjects(pilotName);

                var scoresList = _pilotScoreObjMap[pilotName];
                var statsList = _pilotStatsObjMap[pilotName];

                foreach (var score in scoresList)
                {
                    try
                    {
                        ConstructScoresForPilot(score, pilotName);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(ex.Message);
                    }
                }

                // For each tour.
                foreach (var stats in statsList)
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

            if (errors.Count <= 0)
                return;

            MessageBox.Show(
                string.Format("Some errors were encountered building pilot data:\n{0}", string.Join("\n", errors)),
                "Aces High Pilot Stats");
        }

        private void LoadScoreObjects(string selectedPilot)
        {
            var scores = new SortableList<AcesHighPilotScore>();
            var xmlFileNames = Directory.GetFiles(@"data", string.Format("{0}*_Score*.xml", selectedPilot));
            scores.AddRange(from xmlFileName in xmlFileNames
                let xSerializer = new XmlSerializer(typeof (AcesHighPilotScore))
                select (AcesHighPilotScore) xSerializer.Deserialize(new StreamReader(xmlFileName)));

            if (!_pilotScoreObjMap.ContainsKey(selectedPilot))
                _pilotScoreObjMap.Add(selectedPilot, scores);
        }

        private void LoadStatsObjects(string selectedPilot)
        {
            var stats = new SortableList<AcesHighPilotStats>();
            var xmlFileNames = Directory.GetFiles(@"data", string.Format("{0}*_Stats*.xml", selectedPilot));
            stats.AddRange(from xmlFileName in xmlFileNames
                let xSerializer = new XmlSerializer(typeof (AcesHighPilotStats))
                select (AcesHighPilotStats) xSerializer.Deserialize(new StreamReader(xmlFileName)));

            if (!_pilotStatsObjMap.ContainsKey(selectedPilot))
                _pilotStatsObjMap.Add(selectedPilot, stats);
        }

        #region Fields

        private readonly CaseInsensitiveDictionary<PilotStats> _pilotDictionary;
        private readonly CaseInsensitiveDictionary<SortableList<AcesHighPilotScore>> _pilotScoreObjMap;
        private readonly CaseInsensitiveDictionary<SortableList<AcesHighPilotStats>> _pilotStatsObjMap;
        private readonly List<Squad> _squadList;

        #endregion
    }
}