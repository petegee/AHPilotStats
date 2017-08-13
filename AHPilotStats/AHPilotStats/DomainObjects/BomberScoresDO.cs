using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    public class BomberScoresDO
    {
        readonly AcesHighPilotScore _score;

        public BomberScoresDO(AcesHighPilotScore score)
        {
            _score = score;
        }


        [GridHelper("Tour")]
        public int TourNumber { get { return System.Convert.ToInt32(_score.TourId); } }

        [GridHelper("Details")]
        public string TourIdentfier { get { return _score.TourDetails; } }

        [GridHelper("Type")]
        public string TourType { get { return _score.TourType; } }

        [GridHelper("Dmg/D")]
        public decimal VsObjectsDamagePerDeathScore { get { return _score.VsObjects.Bomber.DamagePerDeathPlus1.Score; } }

        [GridHelper("Dmg/S")]
        public decimal VsObjectsDamagePerSortieScore { get { return _score.VsObjects.Bomber.DamagePerSortie.Score; } }

        [GridHelper("Hit%")]
        public decimal VsObjectsHitPercentageScore { get { return _score.VsObjects.Bomber.HitPercentage.Score; } }

        [GridHelper("Captures")]
        public decimal VsObjectsFieldCapturesScore { get { return _score.VsObjects.Bomber.FieldCaptures.Score; } }



    }
}
