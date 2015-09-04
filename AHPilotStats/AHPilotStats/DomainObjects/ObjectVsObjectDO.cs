using System;
using System.Collections.Generic;
using System.Text;
using My2Cents.HTC.PilotScoreSvc.Types;


namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    class ObjectVsObjectDO
    {
        ObjectScore _ObjScore;
        string _TourId;
        int _TourNumber;
        string _TourType;
        int _killsOf;
        int _killsIn;
        int _killedBy;
        int? _diedIn; //can be null

        public ObjectVsObjectDO(ObjectScore objScore, string tourId, string tourType, int tourNumber)
        {
            _ObjScore = objScore;
            _TourType = tourType;
            _TourId = tourId;
            _TourNumber = tourNumber;

            _killsOf = _ObjScore.KillsOf;
            _killsIn = _ObjScore.KillsIn;
            _killedBy = _ObjScore.KilledBy;
            _diedIn = _ObjScore.DiedIn;
        }

        public string TourIdentfier 
        { 
            get { return _TourId;   }
            set { _TourId = value;  }
        }

        public int TourNumber { get { return _TourNumber;  } }
        public string TourType      { get { return _TourType; } }

        public string Model { get { return _ObjScore.Model;    } }


        public int KillsOf  
        {
            get { return _killsOf; }
            set { _killsOf = value; }
        }
        
        public int KillsIn  
        {
            get { return _killsIn; }
            set { _killsIn = value; }
        }

        public int KilledBy
        {
            get { return _killedBy; }
            set { _killedBy = value; }
        }

        public int? DiedIn
        {
            get { return _diedIn; }
            set { _diedIn = value; }
        }

        public decimal? KillsToDeath
        {
            get 
            {
                if (_diedIn != null)
                {
                    return decimal.Round((decimal)_killsIn / ((decimal)_diedIn + 1),2); 
                }

                return null;
            }
        }

        public ObjectScore ObjScore
        {
            get { return _ObjScore;  }
        }
    }
}
