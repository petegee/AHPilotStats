using System;
using System.Xml;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;


namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
	/// <summary>
	/// Implements a service which retrieves the Obj Vs Obj statistics for a given pilot, tour, and 
    /// tour-type combination via a formatted HTTP request to http://www.hitechcreations.com/cgi-bin/105score/105stats.pl
	/// </summary>
    internal class HTCPilotStatsSvc
	{  
        internal HTCPilotStatsSvc()
		{
		}

        internal AcesHighPilotStats GetPilotStats(string pilotId, TourNode tour, ProxySettingsDTO proxySettings,  string statsURL)
        {
            if (tour == null)
                throw new ArgumentException("tour of type TourNode must be set!");
            if (pilotId == null)
                throw new ArgumentException("pilotId of type string must be set!");
            if (proxySettings == null)
                throw new ArgumentException("proxySettings of type ProxySettingsDTO must be set!");

            string postData = string.Format("playername={1}&selectTour={0}&action=1&Submit=Get+Stats", tour.TourSelectArg, pilotId);

            HttpToXMLLoader loader = new HttpToXMLLoader(proxySettings);
            XmlDocument statsPageDoc = loader.LoadHtmlPageAsXMLByPost(statsURL, postData);

            XmlTextReader xsltDocReader = new XmlTextReader("PilotStatsTransform.xslt");
            XSLT2Transformer transformer = new XSLT2Transformer(statsPageDoc, xsltDocReader);
            XmlDocument result = transformer.DoTransform();

            // Deserialise the XmlDocument to a in-memory object.
            AcesHighPilotStats stats = (AcesHighPilotStats)new CommonUtils().DeserialiseFromXmlDoc(typeof(AcesHighPilotStats), result);

            // And fill in the rest of the details ourselves.
            stats.GameId = CommonUtils.ToUpperFirstChar(pilotId);
            stats.TourId = tour.TourId.ToString();
            stats.TourType = tour.TourType.ToString();
            stats.TourDetails = CommonUtils.BuildTourDetailsTag(tour);

            return stats;
        }
	}
}


