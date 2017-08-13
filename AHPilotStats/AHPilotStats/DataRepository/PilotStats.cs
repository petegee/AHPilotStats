using My2Cents.HTC.AHPilotStats.Collections;
using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace My2Cents.HTC.AHPilotStats.DataRepository
{
    public class PilotStats
    {
        public PilotStats()
        {
            FighterScoresList = new SortableList<FighterScoresDO>();
            FighterStatsList = new SortableList<StatsDomainObject>();
            AttackScoresList = new SortableList<AttackScoresDO>();
            AttackStatsList = new SortableList<StatsDomainObject>();
            BomberScoresList = new SortableList<BomberScoresDO>();
            BomberStatsList = new SortableList<StatsDomainObject>();
            VehicleBoatScoresList = new SortableList<VehicleBoatScoresDO>();
            VehicleBoatStatsList = new SortableList<StatsDomainObject>();
            ObjVsObjCompleteList = new SortableList<ObjectVsObjectDO>();
            ObjVsObjVisibleList = new SortableBindingList<ObjectVsObjectDO>();
        }

        public SortableList<FighterScoresDO> FighterScoresList { get; set; }

        public SortableList<StatsDomainObject> FighterStatsList { get; set; }

        public SortableList<AttackScoresDO> AttackScoresList { get; set; }

        public SortableList<StatsDomainObject> AttackStatsList { get; set; }

        public SortableList<BomberScoresDO> BomberScoresList { get; set; }

        public SortableList<StatsDomainObject> BomberStatsList { get; set; }

        public SortableList<VehicleBoatScoresDO> VehicleBoatScoresList { get; set; }

        public SortableList<StatsDomainObject> VehicleBoatStatsList { get; set; }

        public SortableList<ObjectVsObjectDO> ObjVsObjCompleteList { get; set; }

        public SortableBindingList<ObjectVsObjectDO> ObjVsObjVisibleList { get; set; }
    }
}
