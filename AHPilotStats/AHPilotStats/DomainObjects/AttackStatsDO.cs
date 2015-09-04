using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class AttackStatsDO : StatsDomainObject
    {
        AcesHighPilotScore _Score;

        public AttackStatsDO(AcesHighPilotScore score)
        {
            _Score = score;
        }

        public override string TourIdentfier { get { return _Score.TourDetails; } }
        public override string TourType { get { return _Score.TourType; } }
        public override string TourNumber { get { return _Score.TourId; } }

        public override int OverAllKills { get { return _Score.Overall.Attack.Kills; } }
        public override int OverAllAssists { get { return _Score.Overall.Attack.Assists; } }
        public override int OverAllSorties { get { return _Score.Overall.Attack.Sorties; } }
        public override int OverAllLanded { get { return _Score.Overall.Attack.Landed; } }

        public override int OverAllBailed { get { return _Score.Overall.Attack.Bailed; } }
        public override int OverAllDitched { get { return _Score.Overall.Attack.Ditched; } }
        public override int OverAllCaptured { get { return _Score.Overall.Attack.Captured; } }
        public override int OverAllDeath { get { return _Score.Overall.Attack.Death; } }
        public override int OverAllDeathPlus1 { get { return _Score.Overall.Attack.Death + 1; } }
        public override int OverAllDisco { get { return _Score.Overall.Attack.Disco; } }
        public override string OverAllTime { get { return Utility.GetFormattedTime(_Score.Overall.Attack.Time); } }
        public override int OverAllTimeInSeconds { get { return _Score.Overall.Attack.Time; } }
        public override double OverAllTimeInHours { get { return (double)_Score.Overall.Attack.Time / (double)3600; } }

        [GridHelper(true)]
        public int OverAllTotalTimeInSeconds { get { return _Score.Overall.Total.Time; } }
        
        [GridHelper(true)]
        public double OverAllTotalTimeInHours { get { return (double)_Score.Overall.Total.Time / (double)3600; } }

        public override double HTCKillsPerDeath { get { return (double)_Score.VsEnemy.Attack.KillToDeathPlus1.Score; } }
    
    }


}
