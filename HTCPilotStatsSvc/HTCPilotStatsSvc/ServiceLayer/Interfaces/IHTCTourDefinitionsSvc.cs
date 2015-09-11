using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces
{
    public interface IHTCTourDefinitionsSvc
    {
        TourDefinitions GetTourDefinitions(ProxySettingsDTO proxySettings, string scoresUrl, string statsUrl);
    }
}
