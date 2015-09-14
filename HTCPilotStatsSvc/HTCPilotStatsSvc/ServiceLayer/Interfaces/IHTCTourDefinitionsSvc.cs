using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces
{
    public interface IHTCTourDefinitionsSvc
    {
        TourDefinitions GetTourDefinitions(string scoresUrl, string statsUrl, ProxySettingsDTO proxySettings);
    }
}
