using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class FighterStatsDO : StatsDomainObject
    {
        AcesHighPilotScore _Score;

        public FighterStatsDO(AcesHighPilotScore score)
        {
            _Score = score;
        }

        public override string TourNumber { get { return _Score.TourId; } }
        public override string TourIdentfier { get { return _Score.TourDetails; } }
        public override string TourType { get { return _Score.TourType; } }
        public override int OverAllKills { get { return _Score.Overall.Fighter.Kills; } }
        public override int OverAllAssists { get { return _Score.Overall.Fighter.Assists; } }
        public override int OverAllSorties { get { return _Score.Overall.Fighter.Sorties; } }
        public override int OverAllLanded { get { return _Score.Overall.Fighter.Landed; } }
        public override int OverAllBailed { get { return _Score.Overall.Fighter.Bailed; } }
        public override int OverAllDitched { get { return _Score.Overall.Fighter.Ditched; } }
        public override int OverAllCaptured { get { return _Score.Overall.Fighter.Captured; } }
        public override int OverAllDeath { get { return _Score.Overall.Fighter.Death; } }
        public override int OverAllDeathPlus1 { get { return _Score.Overall.Fighter.Death + 1; } }
        public override int OverAllDisco { get { return _Score.Overall.Fighter.Disco; } }
        public override string OverAllTime { get { return Utility.GetFormattedTime(_Score.Overall.Fighter.Time); } }
        public override int OverAllTimeInSeconds { get { return _Score.Overall.Fighter.Time; } }
        public override double OverAllTimeInHours { get { return (double)_Score.Overall.Fighter.Time / (double)3600; } }

        [GridHelper(true)]
        public int OverAllTotalTimeInSeconds { get { return _Score.Overall.Total.Time; } }

        [GridHelper(true)]
        public double OverAllTotalTimeInHours { get { return (double)_Score.Overall.Total.Time / (double)3600; } }

        public override double HTCKillsPerDeath { get { return (double)_Score.VsEnemy.Fighter.KillToDeathPlus1.Score; } }
    }
}
