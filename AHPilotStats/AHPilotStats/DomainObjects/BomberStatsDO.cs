using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class BomberStatsDO : StatsDomainObject
    {
        AcesHighPilotScore _Score;

        public BomberStatsDO(AcesHighPilotScore score)
        {
            _Score = score;
        }

        public override string TourIdentfier { get { return _Score.TourDetails; } }
        public override string TourType { get { return _Score.TourType; } }
        public override string TourNumber { get { return _Score.TourId; } }

        public override int OverAllKills { get { return _Score.Overall.Bomber.Kills; } }
        public override int OverAllAssists { get { return _Score.Overall.Bomber.Assists; } }
        public override int OverAllSorties { get { return _Score.Overall.Bomber.Sorties; } }
        public override int OverAllLanded { get { return _Score.Overall.Bomber.Landed; } }

        public override int OverAllBailed { get { return _Score.Overall.Bomber.Bailed; } }
        public override int OverAllDitched { get { return _Score.Overall.Bomber.Ditched; } }
        public override int OverAllCaptured { get { return _Score.Overall.Bomber.Captured; } }
        public override int OverAllDeath { get { return _Score.Overall.Bomber.Death; } }
        public override int OverAllDeathPlus1 { get { return _Score.Overall.Fighter.Death + 1; } }
        public override int OverAllDisco { get { return _Score.Overall.Bomber.Disco; } }
        public override string OverAllTime { get { return Utility.GetFormattedTime(_Score.Overall.Bomber.Time); } }
        public override int OverAllTimeInSeconds { get { return _Score.Overall.Bomber.Time; } }
        public override double OverAllTimeInHours { get { return (double)_Score.Overall.Bomber.Time / (double)3600; } }

        [GridHelper(true)]
        public int OverAllTotalTimeInSeconds { get { return _Score.Overall.Total.Time; } }
        [GridHelper(true)]
        public double OverAllTotalTimeInHours { get { return (double)_Score.Overall.Total.Time / (double)3600; } }

        public override double HTCKillsPerDeath { get { return (double)0; } }

    }
}
