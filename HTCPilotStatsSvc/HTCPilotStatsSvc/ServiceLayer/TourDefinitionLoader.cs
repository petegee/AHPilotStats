using System;
using System.Xml;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    public class TourDefinitionLoader
    {
        private readonly TourDefinitions _definitions;

        public TourDefinitionLoader()
        {
            _definitions = new TourDefinitions();
        }

        public TourDefinitions LoadTourDefinitions(ProxySettingsDTO proxySettings, string scoresUrl, string statsUrl)
        {
            BuildTourMap(proxySettings, scoresUrl);
            return _definitions;
        }


        private void BuildTourMap(ProxySettingsDTO proxySettings, string scoresURL)
        {
            var loader = new HttpToXMLLoader(proxySettings);
            var xDoc = loader.LoadHtmlPageAsXmlByGet(scoresURL);

            var xformer = new XSLT2Transformer(xDoc, new XmlTextReader(@"TourListTransform.xslt"));
            var transformedTourListDoc = xformer.DoTransform();

            foreach (XmlNode xNode in transformedTourListDoc.SelectNodes("/AHTourList/AHTourNode"))
            {
                _definitions.AddTourToMap(new TourNode(xNode));
            }

            if (!_definitions.IsTourDefinitionsComplete())
                throw new ApplicationException("Failed to build Tour Map!");
        }
    }
}