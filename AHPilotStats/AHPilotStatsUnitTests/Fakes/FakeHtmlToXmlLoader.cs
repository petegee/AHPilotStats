using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;

namespace AHPilotStatsUnitTests.Fakes
{
    public class FakeHtmlToXmlLoader : IHtmlToXMLLoader
    {
        public XmlDocument LoadHtmlPageAsXmlByGet(string uri, ProxySettingsDTO proxySettings)
        {
            if (!uri.Contains("pilot.php"))
                throw new NotSupportedException("Attempting to load HTML by GET from an unexpected URL");

            var xDoc = new XmlDocument();
            xDoc.Load("../../Data/GET-pilot-php20150915-144004.xml");
            return xDoc;
        }

        public XmlDocument LoadHtmlPageAsXmlByPost(string uri, string postData, ProxySettingsDTO proxySettings)
        {
            if (uri.Contains("pilot.php"))
            {
                var xDoc = new XmlDocument();
                xDoc.Load("../../Data/POST-pilot-php20150915-144024.xml");
                return xDoc;
            }
            if (uri.Contains("players.php"))
            {
                var xDoc = new XmlDocument();
                xDoc.Load("../../Data/POST-players-php20150915-144016.xml");
                return xDoc;
            }

            throw new NotSupportedException("Attempting to load HTML by GET from an unexpected URL");
        }
    }
}
