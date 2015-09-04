using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    internal class HTCTourDefinitionsSvc
    {
        internal HTCTourDefinitionsSvc()
        {
        }

        internal TourDefinitions GetTourDefinitions(ProxySettingsDTO proxySettings, string scoresURL, string statsURL)
        {
            TourDefinitionLoader loader = new TourDefinitionLoader();
            return loader.LoadTourDefinitions(proxySettings, scoresURL, statsURL);
        }
    }
}
