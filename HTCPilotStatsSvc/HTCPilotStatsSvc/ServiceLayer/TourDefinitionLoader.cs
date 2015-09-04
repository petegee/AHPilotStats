using System;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Sgml;
using System.Web;
using System.Net;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;


namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    internal class TourDefinitionLoader
    {
        private TourDefinitions definitions = null;
                
        internal TourDefinitionLoader()
        {
            definitions = new TourDefinitions();
        }

        internal TourDefinitions LoadTourDefinitions(ProxySettingsDTO proxySettings, string scoresURL, string statsURL)
        {
            BuildTourMap(proxySettings, scoresURL);
            return definitions;
        }


        private void BuildTourMap(ProxySettingsDTO proxySettings, string scoresURL)
        {
            HttpToXMLLoader loader = new HttpToXMLLoader(proxySettings);
            XmlDocument xDoc = loader.LoadHtmlPageAsXMLByGet(scoresURL);

            XSLT2Transformer xformer = new XSLT2Transformer(xDoc, new XmlTextReader(@"TourListTransform.xslt"));
            XmlDocument transformedTourListDoc = xformer.DoTransform();

            foreach (XmlNode xNode in transformedTourListDoc.SelectNodes("/AHTourList/AHTourNode"))
            {
                TourNode node = new TourNode(xNode);

                definitions.AddTourToMap(new TourNode(xNode));
            }

            if (!definitions.IsTourDefinitionsComplete())
                throw new ApplicationException("Failed to build Tour Map!");
        }
    }
}
