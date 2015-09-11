using System;
using System.Xml;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    /// <summary>
    ///     Implements a service which retrieves the Obj Vs Obj statistics for a given pilot, tour, and
    ///     tour-type combination via a formatted HTTP request to http://www.hitechcreations.com/cgi-bin/105score/105stats.pl
    /// </summary>
    public class HTCPilotStatsSvc : IHTCPilotStatsSvc
    {
        public AcesHighPilotStats GetPilotStats(string pilotId, TourNode tour, ProxySettingsDTO proxySettings,
            string statsUrl)
        {
            if (tour == null)
                throw new ArgumentException("tour of type TourNode must be set!");
            if (pilotId == null)
                throw new ArgumentException("pilotId of type string must be set!");
            if (proxySettings == null)
                throw new ArgumentException("proxySettings of type ProxySettingsDTO must be set!");

            var postData = string.Format("playername={1}&selectTour={0}&action=1&Submit=Get+Stats", tour.TourSelectArg,
                pilotId);

            var loader = new HttpToXMLLoader(proxySettings);
            var statsPageDoc = loader.LoadHtmlPageAsXmlByPost(statsUrl, postData);

            var xsltDocReader = new XmlTextReader("PilotStatsTransform.xslt");
            var transformer = new XSLT2Transformer(statsPageDoc, xsltDocReader);
            var result = transformer.DoTransform();

            // Deserialise the XmlDocument to a in-memory object.
            var stats =
                (AcesHighPilotStats) new CommonUtils().DeserialiseFromXmlDoc(typeof (AcesHighPilotStats), result);

            // And fill in the rest of the details ourselves.
            stats.GameId = CommonUtils.ToUpperFirstChar(pilotId);
            stats.TourId = tour.TourId.ToString();
            stats.TourType = tour.TourType;
            stats.TourDetails = CommonUtils.BuildTourDetailsTag(tour);

            return stats;
        }
    }
}