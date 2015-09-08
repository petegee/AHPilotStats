using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class FighterScoresDO
    {
        readonly AcesHighPilotScore _score;

        public FighterScoresDO(AcesHighPilotScore score)
        { 
            _score = score;
        }

        [GridHelper("Tour")]
        public int TourNumber { get { return System.Convert.ToInt32(_score.TourId); } }

        [GridHelper("Details")]
        public string TourIdentfier { get { return _score.TourDetails;  } }

        [GridHelper("Type")]
        public string TourType { get { return _score.TourType; } }

        [GridHelper("K/D")]
        public decimal VsEnemyKillsToDeathScore     { get { return _score.VsEnemy.Fighter.KillToDeathPlus1.Score;   } }

        [GridHelper("K/S")]
        public decimal VsEnemyKillsToSortieScore    { get { return _score.VsEnemy.Fighter.KillPerSortie.Score;      } }

        [GridHelper("K/H")]
        public decimal VsEnemyKillsPerHourScore     { get { return _score.VsEnemy.Fighter.KillPerHour.Score;        } }

        [GridHelper("Hit%")]
        public decimal VsEnemyHitPercentageScore    { get { return _score.VsEnemy.Fighter.HitPercentage.Score;      } }
    }


}
