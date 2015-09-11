using My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;
using System;
using System.Xml;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    public class HTCTourDefinitionsSvc : IHTCTourDefinitionsSvc
    {
        public TourDefinitions GetTourDefinitions(ProxySettingsDTO proxySettings, string scoresUrl, string statsUrl)
        {
            var definitions = new TourDefinitions();

            var loader = new HttpToXMLLoader(proxySettings);
            var xDoc = loader.LoadHtmlPageAsXmlByGet(scoresUrl);

            var xformer = new XSLT2Transformer(xDoc, new XmlTextReader(@"TourListTransform.xslt"));
            var transformedTourListDoc = xformer.DoTransform();

            foreach (XmlNode xNode in transformedTourListDoc.SelectNodes("/AHTourList/AHTourNode"))
            {
                definitions.AddTourToMap(new TourNode(xNode));
            }

            if (!definitions.IsTourDefinitionsComplete())
                throw new ApplicationException("Failed to build Tour Map!");

            return definitions;
        }
    }
}