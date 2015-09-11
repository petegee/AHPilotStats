using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces
{
    public interface IHTCPilotStatsSvc
    {
        AcesHighPilotStats GetPilotStats(string pilotId, TourNode tour, ProxySettingsDTO proxySettings,string statsUrl);
    }
}
