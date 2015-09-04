using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class FighterScoresDO
    {
        AcesHighPilotScore _Score;

        public FighterScoresDO(AcesHighPilotScore score)
        { 
            _Score = score;
        }

        [GridHelper("Tour")]
        public int TourNumber { get { return System.Convert.ToInt32(_Score.TourId); } }

        [GridHelper("Details")]
        public string TourIdentfier { get { return _Score.TourDetails;  } }

        [GridHelper("Type")]
        public string TourType { get { return _Score.TourType; } }

        [GridHelper("K/D")]
        public decimal VsEnemyKillsToDeathScore     { get { return _Score.VsEnemy.Fighter.KillToDeathPlus1.Score;   } }

        [GridHelper("K/S")]
        public decimal VsEnemyKillsToSortieScore    { get { return _Score.VsEnemy.Fighter.KillPerSortie.Score;      } }

        [GridHelper("K/H")]
        public decimal VsEnemyKillsPerHourScore     { get { return _Score.VsEnemy.Fighter.KillPerHour.Score;        } }

        [GridHelper("Hit%")]
        public decimal VsEnemyHitPercentageScore    { get { return _Score.VsEnemy.Fighter.HitPercentage.Score;      } }
    }


}
