using System;
using System.Xml;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer.Interfaces;

namespace My2Cents.HTC.PilotScoreSvc.ServiceLayer
{
    /// <summary>
    ///     Implements a service which retrieves the scores for a given pilot, tour, and
    ///     tour-type combination via a formatted HTTP request to http://www.hitechcreations.com/
    /// </summary>
    public class HTCPilotScoreSvc : IHTCPilotScoreSvc
    {
        /// <summary>
        ///     Load a single AcesHighPilotScore objects from HTC web server for a give pilot, tour, and tour type.
        /// </summary>
        /// <param name="pilotId">Pilot in-game handle to retrieve score of.</param>
        /// <param name="tour">The tour</param>
        /// <param name="proxySettings">DTO object detailing how we should connect to the internet.</param>
        /// <param name="scoresURL"></param>
        /// <returns>The score for the nominated pilot/tour/tour-type combination.</returns>
        public AcesHighPilotScore GetPilotScore(string pilotId, TourNode tour, ProxySettingsDTO proxySettings,
            string scoresURL)
        {
            if (tour == null)
                throw new ArgumentException("tour of type TourNode must be set!");
            if (pilotId == null)
                throw new ArgumentException("pilotId of type string must be set!");
            if (proxySettings == null)
                throw new ArgumentException("proxySettings of type ProxySettingsDTO must be set!");

            // Grab the web page and turn it into true XML.
            var loader = new HttpToXMLLoader(proxySettings);
            var postData = "playername=" + pilotId + "&selectTour=" + tour.TourSelectArg + "&action=1&Submit=Get+Scores";
            var doc = loader.LoadHtmlPageAsXmlByPost(scoresURL, postData);

            // XSLT 2.0 parse the Xml score page and transform to our public format.
            var xsltDocReader = new XmlTextReader("PilotScoreTransform.xslt");
            var xformer = new XSLT2Transformer(doc, xsltDocReader);
            var result = xformer.DoTransform(); // may throw Saxon.Api.DynamicError when cant convert 

            // Deserialise the XmlDocument to a in-memory object.
            var score =
                (AcesHighPilotScore) new CommonUtils().DeserialiseFromXmlDoc(typeof (AcesHighPilotScore), result);

            // And fill in the rest of the details ourselves.
            score.GameId = CommonUtils.ToUpperFirstChar(pilotId);
            score.TourId = tour.TourId.ToString();
            score.TourDetails = CommonUtils.BuildTourDetailsTag(tour);
            score.TourType = tour.TourType;

            // Yah! all done.
            return score;
        }
    }
}