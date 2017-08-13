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
        private readonly IHtmlToXMLLoader _loader;

        public HTCPilotScoreSvc(IHtmlToXMLLoader loader)
        {
            _loader = loader;
        }

        /// <summary>
        ///     Load a single AcesHighPilotScore objects from HTC web server for a give pilot, tour, and tour type.
        /// </summary>
        /// <param name="pilotId">Pilot in-game handle to retrieve score of.</param>
        /// <param name="tour">The tour</param>
        /// <param name="proxySettings">DTO object detailing how we should connect to the internet.</param>
        /// <param name="scoresUrl"></param>
        /// <returns>The score for the nominated pilot/tour/tour-type combination.</returns>
        public AcesHighPilotScore GetPilotScore(string pilotId, TourNode tour, string scoresUrl, ProxySettingsDTO proxySettings)
        {
            if (tour == null)
                throw new ArgumentNullException("tour of type TourNode must be set!");
            if (pilotId == null)
                throw new ArgumentNullException("pilotId of type string must be set!");
            if (scoresUrl == null)
                throw new ArgumentNullException("scoresUrl of type string must be set!");
            if (proxySettings == null)
                throw new ArgumentNullException("proxySettings of type ProxySettingsDTO must be set!");

            // Grab the web page and turn it into true XML.
            var postData = "playername=" + pilotId + "&selectTour=" + tour.TourSelectArg + "&action=1&Submit=Get+Scores";
            var doc = _loader.LoadHtmlPageAsXmlByPost(scoresUrl, postData, proxySettings);

            // XSLT 2.0 parse the Xml score page and transform to our public format.
            var xsltDocReader = new XmlTextReader("PilotScoreTransform.xslt");
            var xformer = new XSLT2Transformer(doc, xsltDocReader);
            var result = xformer.DoTransform(); // may throw Saxon.Api.DynamicError when cant convert 

            // Deserialise the XmlDocument to a in-memory object.
            var score = result.DeserialiseFromXmlDoc<AcesHighPilotScore>();

            // And fill in the rest of the details ourselves.
            score.GameId = pilotId.ToUpperFirstChar();
            score.TourId = tour.TourId.ToString();
            score.TourDetails = tour.BuildTourDetailsTag();
            score.TourType = tour.TourType;

            // Yah! all done.
            return score;
        }
    }
}