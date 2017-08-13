using System;
using System.Linq;
using System.Xml;
using FluentAssertions;
using Moq;
using My2Cents.HTC.PilotScoreSvc.ServiceLayer;
using My2Cents.HTC.PilotScoreSvc.Types;
using My2Cents.HTC.PilotScoreSvc.Utilities;
using NUnit.Framework;

namespace AHPilotStatsUnitTests.ServiceTests
{
    [TestFixture]
    public class HTCPilotScoreSvcTests
    {
        [TestFixture]
        public class GetPilotScore
        {
            [TestFixtureSetUp]
            public void PerFixtureSetUp()
            {
                _xmlDocumentDoc = new XmlDocument();
                _xmlDocumentDoc.Load("../../Data/POST-pilot-php20150915-144024.xml");

                _tour = new TourNode
                {
                    TourId = 111,
                    TourType = "TourType",
                    TourStartDate = DateTime.Now,
                    TourEndDate = DateTime.Now.AddDays(1),
                    TourSelectArg = "TourSelectArg"
                };
            }

            private HTCPilotScoreSvc _classUnderTest;
            private Mock<IHtmlToXMLLoader> _mockHtmlToXmlLoader;
            private TourNode _tour;
            private XmlDocument _xmlDocumentDoc;
            private const string PilotId = "Spatula";

            [SetUp]
            public void PerTestSetup()
            {             
                _mockHtmlToXmlLoader = new Mock<IHtmlToXMLLoader>();
                _mockHtmlToXmlLoader
                    .Setup(l => l.LoadHtmlPageAsXmlByPost(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ProxySettingsDTO>()))
                    .Returns(_xmlDocumentDoc);

                _classUnderTest = new HTCPilotScoreSvc(_mockHtmlToXmlLoader.Object);
            }

            [Test]
            public void ScoreThrowWhenPilotParameterIsNull()
            {
                Action methodUnderTest = () => _classUnderTest.GetPilotScore(null, _tour, "pilot.php", new ProxySettingsDTO());

                methodUnderTest.ShouldThrow<ArgumentNullException>()
                   .Where(ex => ex.Message.Contains("pilotId"));
            }

            [Test]
            public void ScoreThrowWhenTourNodeParameterIsNull()
            {
                Action methodUnderTest = () => _classUnderTest.GetPilotScore(PilotId, null, "pilot.php", new ProxySettingsDTO());

                methodUnderTest.ShouldThrow<ArgumentNullException>()
                   .Where(ex => ex.Message.Contains("TourNode"));
            }

            [Test]
            public void ScoreThrowWhenUrlParameterIsNull()
            {
                Action methodUnderTest = () => _classUnderTest.GetPilotScore(PilotId, _tour, null, new ProxySettingsDTO());

                methodUnderTest.ShouldThrow<ArgumentNullException>()
                   .Where(ex => ex.Message.Contains("scoresUrl"));
            }

            [Test]
            public void ScoreThrowWhenProxyParameterIsNull()
            {
                Action methodUnderTest = () => _classUnderTest.GetPilotScore(PilotId, _tour, "pilot.php", null);

                methodUnderTest.ShouldThrow<ArgumentNullException>()
                   .Where(ex => ex.Message.Contains("ProxySettingsDTO"));
            }

            [Test]
            public void ScoreShouldHaveTourInformationCorrectlyMapped()
            {
                var scores = _classUnderTest.GetPilotScore(PilotId, _tour, "pilot.php", new ProxySettingsDTO());

                scores.TourId.Should().Be(_tour.TourId.ToString());
                scores.GameId.Should().Be(PilotId);
                scores.TourType.Should().Be(_tour.TourType);
                scores.TourDetails.Should().Be(_tour.BuildTourDetailsTag());
            }

            [Test] 
            public void ShouldCorrectlyFormatPostParameters()
            {
                var expectedPostData = "playername=" + PilotId + "&selectTour=" + _tour.TourSelectArg + "&action=1&Submit=Get+Scores";

                _classUnderTest.GetPilotScore(PilotId, _tour, "pilot.php", new ProxySettingsDTO());

                _mockHtmlToXmlLoader
                    .Verify(l => l.LoadHtmlPageAsXmlByPost("pilot.php", expectedPostData, It.IsAny<ProxySettingsDTO>()));
            }

            [Test]
            /*From the Html/Xml file.
             * 
                Kills per Death+1	6.33	84
                Kills per Sortie	1.73	228
                Kills per Hour of Flight	6.59	589
                Kills Hit Percentage	4.66	1683
                Kill Points	3348.28	2176y
             */
            public void FighterScores()
            {
                var score = _classUnderTest.GetPilotScore(PilotId, _tour, "pilot.php", new ProxySettingsDTO());

                score.VsEnemy.Fighter.KillToDeathPlus1.Score.Should().Be((decimal)6.33);
                score.VsEnemy.Fighter.KillToDeathPlus1.Rank.Should().Be(84);

                score.VsEnemy.Fighter.KillPerSortie.Score.Should().Be((decimal)1.73);
                score.VsEnemy.Fighter.KillPerSortie.Rank.Should().Be(228);

                score.VsEnemy.Fighter.KillPerHour.Score.Should().Be((decimal)6.59);
                score.VsEnemy.Fighter.KillPerHour.Rank.Should().Be(589);

                score.VsEnemy.Fighter.HitPercentage.Score.Should().Be((decimal)4.66);
                score.VsEnemy.Fighter.HitPercentage.Rank.Should().Be(1683);

                score.VsEnemy.Fighter.Points.Score.Should().Be((decimal)3348.28);
                score.VsEnemy.Fighter.Points.Rank.Should().Be(2176);
            }

            [Test]
            /*  From the Html/Xml file.
             * 
                Damage per Death	0	0
                Damage per Sortie	0	0
                Damage Hit Percentage	0	0
                Damage Points	0	0
                Field Captures	0	0
             */
            public void BomberScores()
            {
                var score = _classUnderTest.GetPilotScore(PilotId, _tour, "pilot.php", new ProxySettingsDTO());

                score.VsObjects.Bomber.DamagePerDeathPlus1.Score.Should().Be((decimal)0);
                score.VsObjects.Bomber.DamagePerDeathPlus1.Rank.Should().Be(0);

                score.VsObjects.Bomber.DamagePerSortie.Score.Should().Be((decimal)0);
                score.VsObjects.Bomber.DamagePerSortie.Rank.Should().Be(0);

                score.VsObjects.Bomber.HitPercentage.Score.Should().Be((decimal)0);
                score.VsObjects.Bomber.HitPercentage.Rank.Should().Be(0);

                score.VsObjects.Bomber.FieldCaptures.Score.Should().Be((decimal)0);
                score.VsObjects.Bomber.FieldCaptures.Rank.Should().Be(0);

                score.VsObjects.Bomber.Points.Score.Should().Be((decimal)0);
                score.VsObjects.Bomber.Points.Rank.Should().Be(0);
            }

        }
    }
}
