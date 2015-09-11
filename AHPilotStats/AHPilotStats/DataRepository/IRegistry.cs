using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DataRepository
{
    public interface IRegistry
    {
        PilotStats GetPilotStats(string pilotName);

        bool AreTourDefinitionsInitialised();

        bool PilotStatsContains(string pilotName);

        void PopulatePilotList();

        void ConstructScoresForPilot(AcesHighPilotScore score, string pilotName);

        void ConstructStatsForPilot(AcesHighPilotStats stats, string pilotName);

        Squad GetSquad(string squadName);

    }
}
