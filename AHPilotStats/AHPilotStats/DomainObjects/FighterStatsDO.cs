using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class FighterStatsDO : StatsDomainObject
    {
        readonly AcesHighPilotScore _score;

        public FighterStatsDO(AcesHighPilotScore score)
        {
            _score = score;
        }

        public override string TourNumber { get { return _score.TourId; } }
        public override string TourIdentfier { get { return _score.TourDetails; } }
        public override string TourType { get { return _score.TourType; } }
        public override int OverAllKills { get { return _score.Overall.Fighter.Kills; } }
        public override int OverAllAssists { get { return _score.Overall.Fighter.Assists; } }
        public override int OverAllSorties { get { return _score.Overall.Fighter.Sorties; } }
        public override int OverAllLanded { get { return _score.Overall.Fighter.Landed; } }
        public override int OverAllBailed { get { return _score.Overall.Fighter.Bailed; } }
        public override int OverAllDitched { get { return _score.Overall.Fighter.Ditched; } }
        public override int OverAllCaptured { get { return _score.Overall.Fighter.Captured; } }
        public override int OverAllDeath { get { return _score.Overall.Fighter.Death; } }
        public override int OverAllDeathPlus1 { get { return _score.Overall.Fighter.Death + 1; } }
        public override int OverAllDisco { get { return _score.Overall.Fighter.Disco; } }
        public override string OverAllTime { get { return Utility.GetFormattedTime(_score.Overall.Fighter.Time); } }
        public override int OverAllTimeInSeconds { get { return _score.Overall.Fighter.Time; } }
        public override double OverAllTimeInHours { get { return (double)_score.Overall.Fighter.Time / 3600; } }

        [GridHelper(true)]
        public int OverAllTotalTimeInSeconds { get { return _score.Overall.Total.Time; } }

        [GridHelper(true)]
        public double OverAllTotalTimeInHours { get { return (double)_score.Overall.Total.Time / 3600; } }

        public override double HTCKillsPerDeath { get { return (double)_score.VsEnemy.Fighter.KillToDeathPlus1.Score; } }
    }
}
