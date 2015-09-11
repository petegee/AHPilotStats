using My2Cents.HTC.PilotScoreSvc.Types;


namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    public class ObjectVsObjectDO
    {
        readonly ObjectScore _objScore;
        readonly int _tourNumber;
        readonly string _tourType;

        public ObjectVsObjectDO(ObjectScore objScore, string tourId, string tourType, int tourNumber)
        {
            _objScore = objScore;
            _tourType = tourType;
            TourIdentfier = tourId;
            _tourNumber = tourNumber;

            KillsOf = _objScore.KillsOf;
            KillsIn = _objScore.KillsIn;
            KilledBy = _objScore.KilledBy;
            DiedIn = _objScore.DiedIn;
        }

        public string TourIdentfier { get; set; }
        public int TourNumber { get { return _tourNumber;  } }
        public string TourType { get { return _tourType; } }

        public string Model { get { return _objScore.Model;    } }


        public int KillsOf { get; set; }

        public int KillsIn { get; set; }

        public int KilledBy { get; set; }

        public int? DiedIn { get; set; }

        public decimal? KillsToDeath
        {
            get 
            {
                if (DiedIn != null)
                {
                    return decimal.Round(KillsIn / ((decimal)DiedIn + 1),2); 
                }

                return null;
            }
        }

        public ObjectScore ObjScore
        {
            get { return _objScore;  }
        }
    }
}
