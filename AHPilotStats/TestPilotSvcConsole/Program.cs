using System;
using System.Globalization;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc;
using My2Cents.NetUtils.HTTPSender;
using Sgml;
using System.Web;


namespace TestPilotSvcConsole
{
    class Program
    {
        XmlDocument statsXmlDoc = new XmlDocument();

        Dictionary<int, TourNode> mainArenaTourDictionary = new Dictionary<int, TourNode>();
        Dictionary<int, TourNode> avaTourDictionary = new Dictionary<int, TourNode>();

        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.RunTests();

            Console.ReadLine();
        }


        public void RunTests()
        {
            HTCPilotStatsSvc statsSvc = new HTCPilotStatsSvc();
            AcesHighPilotStats stats = statsSvc.GetPilotStatsMultiThreaded("99MecInf", TourType.MainArenaTour, 76, new ProxySettingsDTO());

            HTCPilotScoreSvc scoreSvc = new HTCPilotScoreSvc();
            AcesHighPilotScore score = scoreSvc.GetPilotScore("99MecInf", TourType.MainArenaTour, 76, new ProxySettingsDTO());

            int i = 0;

            /*
            TourNode tour = TourDefinitions.Instance.GetTourDetails(76, TourType.MainArenaTour);

            Sender httpSender = new Sender(new ProxySettingsDTO());
            StreamReader prototype = File.OpenText("individual_scores.http.txt");
            httpSender.RequestString = "gameid=" + "99MecInf" + "&tour=" + tour.FullyQualifiedTourIdentifier;
            httpSender.URLEncodeRequestString();
            httpSender.Initialise(prototype, "http://www.hitechcreations.com/cgi-bin/105score/105score.pl");
            StreamReader response = httpSender.Send();


            string PilotVsBuffer = response.ReadToEnd();
            StringReader pilotVsStringReader = new StringReader(PilotVsBuffer);

            // Use the cool sgml reader to 'interpret' the HTML as XML :) very nice!
            SgmlReader pilotVsSgmlReader = new SgmlReader();
            pilotVsSgmlReader.DocType = "HTML";
            pilotVsSgmlReader.InputStream = pilotVsStringReader;
            statsXmlDoc.Load(pilotVsSgmlReader);

            statsXmlDoc.Save(Console.Out);
            statsXmlDoc.Save(@"C:\response.xml");
            */

        }

    }
}
