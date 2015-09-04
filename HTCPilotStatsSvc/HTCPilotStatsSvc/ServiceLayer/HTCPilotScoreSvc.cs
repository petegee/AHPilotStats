using System;
using System.Xml;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;


namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    

    /// <summary>
    /// Implements a service which retrieves the scores for a given pilot, tour, and 
    /// tour-type combination via a formatted HTTP request to http://www.hitechcreations.com/cgi-bin/105score/105stats.pl
    /// </summary>
    internal class HTCPilotScoreSvc
    {
        internal HTCPilotScoreSvc()
        {
        }


        /// <summary>
        /// Load a single AcesHighPilotScore objects from HTC web server for a give pilot, tour, and tour type.
        /// </summary>
        /// <param name="pilotId">Pilot in-game handle to retrieve score of.</param>
        /// <param name="tourType">The tour type (Main Arena, or AvA/CT tour)</param>
        /// <param name="tourId">The tour number</param>
        /// <param name="proxySettings">DTO object detailing how we should connect to the internet.</param>
        /// <returns>The score for the nominated pilot/tour/tour-type combination.</returns>
        internal AcesHighPilotScore GetPilotScore(string pilotId, TourNode tour, ProxySettingsDTO proxySettings, string scoresURL)
        {
            if (tour == null)
                throw new ArgumentException("tour of type TourNode must be set!");
            if (pilotId == null)
                throw new ArgumentException("pilotId of type string must be set!");
            if (proxySettings == null)
                throw new ArgumentException("proxySettings of type ProxySettingsDTO must be set!");

            // Grab the web page and turn it into true XML.
            HttpToXMLLoader loader = new HttpToXMLLoader(proxySettings);
            string postData = "playername=" + pilotId + "&selectTour=" + tour.TourSelectArg + "&action=1&Submit=Get+Scores";
            XmlDocument doc = loader.LoadHtmlPageAsXMLByPost(scoresURL, postData);

            // XSLT 2.0 parse the Xml score page and transform to our internal format.
            XmlTextReader xsltDocReader = new XmlTextReader("PilotScoreTransform.xslt");
            XSLT2Transformer xformer = new XSLT2Transformer(doc, xsltDocReader);
            XmlDocument result = xformer.DoTransform(); // may throw Saxon.Api.DynamicError when cant convert 

            // Deserialise the XmlDocument to a in-memory object.
            AcesHighPilotScore score = (AcesHighPilotScore)new CommonUtils().DeserialiseFromXmlDoc(typeof(AcesHighPilotScore), result);

            // And fill in the rest of the details ourselves.
            score.GameId = CommonUtils.ToUpperFirstChar(pilotId);
            score.TourId = tour.TourId.ToString();
            score.TourDetails = CommonUtils.BuildTourDetailsTag(tour);
            score.TourType = tour.TourType.ToString();

            // Yah! all done.
            return score;
        }
    }
}