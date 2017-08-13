using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Types;
using System.Collections.Generic;

namespace My2Cents.HTC.AHPilotStats.DataRepository
{
    public interface IRegistry
    {
        HashSet<string> PilotNamesSet { get; set; }
        HashSet<string> SquadNamesSet { get; set; }
        HashSet<string> ModelSet { get; set; }
        TourDefinitions TourDefinitions { get; set; }

        PilotStats GetPilotStats(string pilotName);

        bool AreTourDefinitionsInitialised();

        bool PilotStatsContains(string pilotName);

        void ReloadPilot(string reloadedPilotName);

        void RemovePilot(string pilotName);

        void AddPilotStatsToRegistry(string pilotName, PilotStats stats);

        void PopulatePilotList();

        void ConstructScoresForPilot(AcesHighPilotScore score, string pilotName);

        void ConstructStatsForPilot(AcesHighPilotStats stats, string pilotName);

        Squad GetSquad(string squadName);

        void PopulateSquadList();
    }
}
