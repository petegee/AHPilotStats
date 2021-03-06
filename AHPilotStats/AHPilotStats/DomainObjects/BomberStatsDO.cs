using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class BomberStatsDO : StatsDomainObject
    {
        readonly AcesHighPilotScore _score;

        public BomberStatsDO(AcesHighPilotScore score)
        {
            _score = score;
        }

        public override string TourIdentfier { get { return _score.TourDetails; } }
        public override string TourType { get { return _score.TourType; } }
        public override string TourNumber { get { return _score.TourId; } }

        public override int OverAllKills { get { return _score.Overall.Bomber.Kills; } }
        public override int OverAllAssists { get { return _score.Overall.Bomber.Assists; } }
        public override int OverAllSorties { get { return _score.Overall.Bomber.Sorties; } }
        public override int OverAllLanded { get { return _score.Overall.Bomber.Landed; } }

        public override int OverAllBailed { get { return _score.Overall.Bomber.Bailed; } }
        public override int OverAllDitched { get { return _score.Overall.Bomber.Ditched; } }
        public override int OverAllCaptured { get { return _score.Overall.Bomber.Captured; } }
        public override int OverAllDeath { get { return _score.Overall.Bomber.Death; } }
        public override int OverAllDeathPlus1 { get { return _score.Overall.Fighter.Death + 1; } }
        public override int OverAllDisco { get { return _score.Overall.Bomber.Disco; } }
        public override string OverAllTime { get { return Utility.GetFormattedTime(_score.Overall.Bomber.Time); } }
        public override int OverAllTimeInSeconds { get { return _score.Overall.Bomber.Time; } }
        public override double OverAllTimeInHours { get { return (double)_score.Overall.Bomber.Time / 3600; } }

        [GridHelper(true)]
        public int OverAllTotalTimeInSeconds { get { return _score.Overall.Total.Time; } }
        
        [GridHelper(true)]
        public double OverAllTotalTimeInHours { get { return  (double)_score.Overall.Total.Time /3600; } }

        public override double HTCKillsPerDeath { get { return 0; } }

    }
}
