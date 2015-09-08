using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class VehicleBoatStatsDO : StatsDomainObject
    {
        readonly AcesHighPilotScore _score;

        public VehicleBoatStatsDO(AcesHighPilotScore score)
        {
            _score = score;
        }

        public override string TourIdentfier { get { return _score.TourDetails; } }
        public override string TourType { get { return _score.TourType; } }
        public override string TourNumber { get { return _score.TourId; } }

        public override int OverAllKills { get { return _score.Overall.VehicleBoat.Kills; } }
        public override int OverAllAssists { get { return _score.Overall.VehicleBoat.Assists; } }
        public override int OverAllSorties { get { return _score.Overall.VehicleBoat.Sorties; } }
        public override int OverAllLanded { get { return _score.Overall.VehicleBoat.Landed; } }

        public override int OverAllBailed { get { return _score.Overall.VehicleBoat.Bailed; } }
        public override int OverAllDitched { get { return _score.Overall.VehicleBoat.Ditched; } }
        public override int OverAllCaptured { get { return _score.Overall.VehicleBoat.Captured; } }
        public override int OverAllDeath { get { return _score.Overall.VehicleBoat.Death; } }
        public override int OverAllDeathPlus1 { get { return _score.Overall.VehicleBoat.Death + 1; } }
        public override int OverAllDisco { get { return _score.Overall.VehicleBoat.Disco; } }
        public override string OverAllTime { get { return Utility.GetFormattedTime(_score.Overall.VehicleBoat.Time); } }
        public override int OverAllTimeInSeconds { get { return _score.Overall.VehicleBoat.Time; } }
        public override double OverAllTimeInHours { get { return (double)_score.Overall.VehicleBoat.Time / (double)3600; } }

        [GridHelper(true)]
        public int OverAllTotalTimeInSeconds { get { return _score.Overall.Total.Time; } }

        [GridHelper(true)]
        public double OverAllTotalTimeInHours { get { return (double)_score.Overall.Total.Time / (double)3600; } }

        public override double HTCKillsPerDeath { get { return (double)_score.VsEnemy.VehicleBoat.KillToDeathPlus1.Score; } }
    }
}
