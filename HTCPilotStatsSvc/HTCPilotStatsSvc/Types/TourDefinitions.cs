using System;
using System.Collections.Generic;
using System.Text;


namespace My2Cents.HTC.PilotScoreSvc.Types
{
    public class TourDefinitions
    {
        public TourDefinitions()
        { 
        }

        private Dictionary<string, Dictionary<int, TourNode>> tours = new Dictionary<string, Dictionary<int, TourNode>>();

        public Dictionary<string, Dictionary<int, TourNode>> Tours
        {
            get { return tours; }
        }

        public bool IsTourDefinitionsComplete()
        {
            return tours.Count > 1;
        }

        public TourNode FindTour(string tourType, int tourid)
        {
            Dictionary<int, TourNode> tourDictionary = tours[tourType];
            return tourDictionary[tourid];
        }

        public void AddTourToMap(TourNode tour)
        {
            if (tours.ContainsKey(tour.TourType))
            {
                tours[tour.TourType].Add(tour.TourId, tour);
            }
            else
            { 
                Dictionary<int,TourNode> newTour = new Dictionary<int,TourNode>();
                newTour.Add(tour.TourId, tour);
                tours.Add(tour.TourType, newTour);
            }
        }
    }
}
