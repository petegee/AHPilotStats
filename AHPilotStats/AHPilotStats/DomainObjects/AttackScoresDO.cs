using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class AttackScoresDO
    {
        AcesHighPilotScore _Score;

        public AttackScoresDO(AcesHighPilotScore score)
        {
            _Score = score;
        }

        [GridHelper("Tour")]
        public int TourNumber { get { return System.Convert.ToInt32(_Score.TourId); } }

        [GridHelper("Details")]
        public string TourIdentfier { get { return _Score.TourDetails; } }

        [GridHelper("Type")]
        public string TourType { get { return _Score.TourType; } }

        [GridHelper("K/D v NME")]
        public decimal VsEnemyKillsToDeathScore { get { return _Score.VsEnemy.Attack.KillToDeathPlus1.Score; } }

        [GridHelper("K/S v NME")]
        public decimal VsEnemyKillsToSortieScore { get { return _Score.VsEnemy.Attack.KillPerSortie.Score; } }

        [GridHelper("K/H v NME")]
        public decimal VsEnemyKillsPerHourScore { get { return _Score.VsEnemy.Attack.KillPerHour.Score; } }

        [GridHelper("Hit% v NME")]
        public decimal VsEnemyHitPercentageScore { get { return _Score.VsEnemy.Attack.HitPercentage.Score; } }

        [GridHelper("Dmg/D v Obj")]
        public decimal VsObjectsDamagePerDeathScore { get { return _Score.VsObjects.Attack.DamagePerDeathPlus1.Score; } }

        [GridHelper("Dmg/S v Obj")]
        public decimal VsObjectsDamagePerSortieScore { get { return _Score.VsObjects.Attack.DamagePerSortie.Score; } }

        [GridHelper("Hit% v Obj")]
        public decimal VsObjectsHitPercentageScore { get { return _Score.VsObjects.Attack.HitPercentage.Score; } }

        [GridHelper("Captures")]
        public decimal VsObjectsFieldCapturesScore { get { return _Score.VsObjects.Attack.FieldCaptures.Score; } }

    }
}
