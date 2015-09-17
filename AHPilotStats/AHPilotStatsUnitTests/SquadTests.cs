using NUnit.Framework;
using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace AHPilotStatsUnitTests
{
    [TestFixture]
    public class SquadTests
    {
        [Test]
        public void SafeFileName_ShouldStripIllegalFilenameCharacters()
        {
            Assert.AreEqual("filename", Squad.SafeFileName("\\f/i:l*e?n\"a<m>e|"));
            Assert.AreEqual("filename", Squad.SafeFileName("filename"));
            Assert.AreEqual("", Squad.SafeFileName(""));
            Assert.AreEqual("", Squad.SafeFileName("\\/:*?\"<>|"));
        }
    }
}
