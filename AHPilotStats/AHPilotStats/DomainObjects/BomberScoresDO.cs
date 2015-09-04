using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class BomberScoresDO
    {
        AcesHighPilotScore _Score;

        public BomberScoresDO(AcesHighPilotScore score)
        {
            _Score = score;
        }


        [GridHelper("Tour")]
        public int TourNumber { get { return System.Convert.ToInt32(_Score.TourId); } }

        [GridHelper("Details")]
        public string TourIdentfier { get { return _Score.TourDetails; } }

        [GridHelper("Type")]
        public string TourType { get { return _Score.TourType; } }

        [GridHelper("Dmg/D")]
        public decimal VsObjectsDamagePerDeathScore { get { return _Score.VsObjects.Bomber.DamagePerDeathPlus1.Score; } }

        [GridHelper("Dmg/S")]
        public decimal VsObjectsDamagePerSortieScore { get { return _Score.VsObjects.Bomber.DamagePerSortie.Score; } }

        [GridHelper("Hit%")]
        public decimal VsObjectsHitPercentageScore { get { return _Score.VsObjects.Bomber.HitPercentage.Score; } }

        [GridHelper("Captures")]
        public decimal VsObjectsFieldCapturesScore { get { return _Score.VsObjects.Bomber.FieldCaptures.Score; } }



    }
}
