using System;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    public class AttackScoresDO
    {
        private readonly AcesHighPilotScore _score;

        public AttackScoresDO(AcesHighPilotScore score)
        {
            _score = score;
        }

        [GridHelper("Tour")]
        public int TourNumber
        {
            get { return Convert.ToInt32(_score.TourId); }
        }

        [GridHelper("Details")]
        public string TourIdentfier
        {
            get { return _score.TourDetails; }
        }

        [GridHelper("Type")]
        public string TourType
        {
            get { return _score.TourType; }
        }

        [GridHelper("K/D v NME")]
        public decimal VsEnemyKillsToDeathScore
        {
            get { return _score.VsEnemy.Attack.KillToDeathPlus1.Score; }
        }

        [GridHelper("K/S v NME")]
        public decimal VsEnemyKillsToSortieScore
        {
            get { return _score.VsEnemy.Attack.KillPerSortie.Score; }
        }

        [GridHelper("K/H v NME")]
        public decimal VsEnemyKillsPerHourScore
        {
            get { return _score.VsEnemy.Attack.KillPerHour.Score; }
        }

        [GridHelper("Hit% v NME")]
        public decimal VsEnemyHitPercentageScore
        {
            get { return _score.VsEnemy.Attack.HitPercentage.Score; }
        }

        [GridHelper("Dmg/D v Obj")]
        public decimal VsObjectsDamagePerDeathScore
        {
            get { return _score.VsObjects.Attack.DamagePerDeathPlus1.Score; }
        }

        [GridHelper("Dmg/S v Obj")]
        public decimal VsObjectsDamagePerSortieScore
        {
            get { return _score.VsObjects.Attack.DamagePerSortie.Score; }
        }

        [GridHelper("Hit% v Obj")]
        public decimal VsObjectsHitPercentageScore
        {
            get { return _score.VsObjects.Attack.HitPercentage.Score; }
        }

        [GridHelper("Captures")]
        public decimal VsObjectsFieldCapturesScore
        {
            get { return _score.VsObjects.Attack.FieldCaptures.Score; }
        }
    }
}