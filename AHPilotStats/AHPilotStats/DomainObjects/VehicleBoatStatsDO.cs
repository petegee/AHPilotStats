using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class VehicleBoatStatsDO : StatsDomainObject
    {
        AcesHighPilotScore _Score;

        public VehicleBoatStatsDO(AcesHighPilotScore score)
        {
            _Score = score;
        }

        public override string TourIdentfier { get { return _Score.TourDetails; } }
        public override string TourType { get { return _Score.TourType; } }
        public override string TourNumber { get { return _Score.TourId; } }

        public override int OverAllKills { get { return _Score.Overall.VehicleBoat.Kills; } }
        public override int OverAllAssists { get { return _Score.Overall.VehicleBoat.Assists; } }
        public override int OverAllSorties { get { return _Score.Overall.VehicleBoat.Sorties; } }
        public override int OverAllLanded { get { return _Score.Overall.VehicleBoat.Landed; } }

        public override int OverAllBailed { get { return _Score.Overall.VehicleBoat.Bailed; } }
        public override int OverAllDitched { get { return _Score.Overall.VehicleBoat.Ditched; } }
        public override int OverAllCaptured { get { return _Score.Overall.VehicleBoat.Captured; } }
        public override int OverAllDeath { get { return _Score.Overall.VehicleBoat.Death; } }
        public override int OverAllDeathPlus1 { get { return _Score.Overall.VehicleBoat.Death + 1; } }
        public override int OverAllDisco { get { return _Score.Overall.VehicleBoat.Disco; } }
        public override string OverAllTime { get { return Utility.GetFormattedTime(_Score.Overall.VehicleBoat.Time); } }
        public override int OverAllTimeInSeconds { get { return _Score.Overall.VehicleBoat.Time; } }
        public override double OverAllTimeInHours { get { return (double)_Score.Overall.VehicleBoat.Time / (double)3600; } }

        [GridHelper(true)]
        public int OverAllTotalTimeInSeconds { get { return _Score.Overall.Total.Time; } }

        [GridHelper(true)]
        public double OverAllTotalTimeInHours { get { return (double)_Score.Overall.Total.Time / (double)3600; } }

        public override double HTCKillsPerDeath { get { return (double)_Score.VsEnemy.VehicleBoat.KillToDeathPlus1.Score; } }
    }
}
