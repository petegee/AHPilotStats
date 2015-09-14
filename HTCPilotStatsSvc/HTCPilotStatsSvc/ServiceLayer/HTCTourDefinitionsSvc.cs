using My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;
using System;
using System.Xml;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    public class HTCTourDefinitionsSvc : IHTCTourDefinitionsSvc
    {
        private readonly IHtmlToXMLLoader _loader;

        public HTCTourDefinitionsSvc(IHtmlToXMLLoader loader)
        {
            _loader = loader;
        }

        public TourDefinitions GetTourDefinitions(string scoresUrl, string statsUrl, ProxySettingsDTO proxySettings)
        {
            var definitions = new TourDefinitions();

            var xDoc = _loader.LoadHtmlPageAsXmlByGet(scoresUrl, proxySettings);

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