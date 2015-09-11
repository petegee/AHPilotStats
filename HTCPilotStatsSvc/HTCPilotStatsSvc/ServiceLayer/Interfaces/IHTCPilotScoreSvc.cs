using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces
{
    public interface IHTCPilotScoreSvc
    {
        AcesHighPilotScore GetPilotScore(string pilotId, TourNode tour, ProxySettingsDTO proxySettings, string scoresURL);
    }
}
