using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    public class HTCTourDefinitionsSvc
    {
        public TourDefinitions GetTourDefinitions(ProxySettingsDTO proxySettings, string scoresUrl, string statsUrl)
        {
            var loader = new TourDefinitionLoader();
            return loader.LoadTourDefinitions(proxySettings, scoresUrl, statsUrl);
        }
    }
}