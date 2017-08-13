using System;
using System.Linq;
using AHPilotStatsUnitTests.Fakes;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer;
using My2Cents.HTC.PilotScoreSvc.Types;
using NUnit.Framework;

namespace AHPilotStatsUnitTests.ServiceTests
{
    [TestFixture]
    public class HTCTourDefinitionsSvcTests
    {
        [TestFixture]
        public class GetTourDefinitions
        {
            public HTCTourDefinitionsSvc ClassUnderTest { get; set; }
            public FakeHtmlToXmlLoader FakeHtmlToXmlLoader { get; set; }


            [SetUp]
            public void Setup()
            {
                FakeHtmlToXmlLoader = new FakeHtmlToXmlLoader();
                ClassUnderTest = new HTCTourDefinitionsSvc(FakeHtmlToXmlLoader);
            }

            [Test]
            public void TourDefinitionKeysShouldBeCorrectlyTransformedFromXml()
            {
                var tourDefs = ClassUnderTest.GetTourDefinitions("pilot.php", new ProxySettingsDTO());

                Assert.AreEqual(6, tourDefs.Tours.Count, "There should only be six tour types. Five known, all the others should be transformed as Unknown.");
                Assert.IsTrue(tourDefs.Tours.ContainsKey("WW1Tour"), "Tour dictionary should contain key WW1Tour");
                Assert.IsTrue(tourDefs.Tours.ContainsKey("AvATour"), "Tour dictionary should contain key AvATour");
                Assert.IsTrue(tourDefs.Tours.ContainsKey("MainArenaTour"), "Tour dictionary should contain key MainArenaTour");
                Assert.IsTrue(tourDefs.Tours.ContainsKey("MWMainArenaTour"), "Tour dictionary should contain key MWMainArenaTour");
                Assert.IsTrue(tourDefs.Tours.ContainsKey("EWMainArenaTour"), "Tour dictionary should contain key EWMainArenaTour");
            }

            [Test]
            public void TourDefinitionsShouldMapBetaToursToUnknown()
            {
                var tourDefs = ClassUnderTest.GetTourDefinitions("pilot.php", new ProxySettingsDTO());
                Assert.IsTrue(tourDefs.Tours.ContainsKey("Unknown"), "Tour dictionary should contain key Unknown");
                Assert.IsFalse(tourDefs.Tours.Keys.Any(k => k.Contains("BetaTour")), "Tour dictionary should not contain Beta Tours");
            }

            [Test]
            public void ValidateMainArenaTourObjectsAreCorrectlyTransformed()
            {
                var tourDefs = ClassUnderTest.GetTourDefinitions("pilot.php", new ProxySettingsDTO());

                var tours = tourDefs.Tours["MainArenaTour"];

                Assert.AreEqual(188, tours.Count, "There should be 188 main arena/LW main arena tours");

                var firstTour = tours.OrderBy(t => t.Value.TourStartDate).First().Value;
                Assert.AreEqual(1, firstTour.TourId, "Main arena tour 1, should have an id of 1");
                Assert.AreEqual("MainArenaTour", firstTour.TourType, "TourType was not correctly transformed");
                Assert.AreEqual("Tour1", firstTour.TourSelectArg, "TourSelectArg was not correctly transformed");
                Assert.AreEqual(new DateTime(2000, 1, 24).Date, firstTour.TourStartDate.Date, "TourStartDate was not correctly transformed");
                Assert.AreEqual(new DateTime(2000, 2, 29).Date, firstTour.TourEndDate.Date, "TourEndDate was not correctly transformed");
            }

            [Test]
            public void ValidateEWMainArenaTourObjectsAreCorrectlyTransformed()
            {
                var tourDefs = ClassUnderTest.GetTourDefinitions("pilot.php", new ProxySettingsDTO());

                var tours = tourDefs.Tours["EWMainArenaTour"];

                Assert.AreEqual(96, tours.Count, "There should be 96 EW main arena tours");

                var firstTour = tours.OrderBy(t => t.Value.TourStartDate).First().Value;
                Assert.AreEqual(93, firstTour.TourId, "EW main arena tour 93 (the first), should have an id of 93");
                Assert.AreEqual("EWMainArenaTour", firstTour.TourType, "TourType was not correctly transformed");
                Assert.AreEqual("EWTour93", firstTour.TourSelectArg, "TourSelectArg was not correctly transformed");
                Assert.AreEqual(new DateTime(2007, 10, 1).Date, firstTour.TourStartDate.Date, "TourStartDate was not correctly transformed");
                Assert.AreEqual(new DateTime(2007, 10, 31).Date, firstTour.TourEndDate.Date, "TourEndDate was not correctly transformed");
            }

            [Test]
            public void ValidateMWMainArenaTourObjectsAreCorrectlyTransformed()
            {
                var tourDefs = ClassUnderTest.GetTourDefinitions("pilot.php", new ProxySettingsDTO());

                var tours = tourDefs.Tours["MWMainArenaTour"];

                Assert.AreEqual(96, tours.Count, "There should be 96 MW main arena tours");

                var firstTour = tours.OrderBy(t => t.Value.TourStartDate).First().Value;
                Assert.AreEqual(93, firstTour.TourId, "MW main arena tour 93 (the first), should have an id of 93");
                Assert.AreEqual("MWMainArenaTour", firstTour.TourType, "TourType was not correctly transformed");
                Assert.AreEqual("MWTour93", firstTour.TourSelectArg, "TourSelectArg was not correctly transformed");
                Assert.AreEqual(new DateTime(2007, 10, 1).Date, firstTour.TourStartDate.Date, "TourStartDate was not correctly transformed");
                Assert.AreEqual(new DateTime(2007, 10, 31).Date, firstTour.TourEndDate.Date, "TourEndDate was not correctly transformed");
            }

            [Test]
            public void ValidateAvATourObjectsAreCorrectlyTransformed()
            {
                var tourDefs = ClassUnderTest.GetTourDefinitions("pilot.php", new ProxySettingsDTO());

                var tours = tourDefs.Tours["AvATour"];

                Assert.AreEqual(161, tours.Count, "There should be 161 AvA tours");

                var firstTour = tours.OrderBy(t => t.Value.TourStartDate).First().Value;
                Assert.AreEqual(1, firstTour.TourId, "AvA tour 1, should have an id of 1");
                Assert.AreEqual("AvATour", firstTour.TourType, "TourType was not correctly transformed");
                Assert.AreEqual("CtTour1", firstTour.TourSelectArg, "TourSelectArg was not correctly transformed");
                Assert.AreEqual(new DateTime(2001, 12, 1).Date, firstTour.TourStartDate.Date, "TourStartDate was not correctly transformed");
                Assert.AreEqual(new DateTime(2002, 1, 31).Date, firstTour.TourEndDate.Date, "TourEndDate was not correctly transformed");
            }

        }
    }
}
