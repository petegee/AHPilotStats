using System.Collections.Generic;

namespace My2Cents.HTC.PilotScoreSvc.Types
{
    public class TourDefinitions
    {
        public TourDefinitions()
        {
            Tours = new Dictionary<string, Dictionary<int, TourNode>>();
        }

        public Dictionary<string, Dictionary<int, TourNode>> Tours { get; private set; }

        public bool IsTourDefinitionsComplete()
        {
            return Tours.Count > 1;
        }

        public TourNode FindTour(string tourType, int tourid)
        {
            var tourDictionary = Tours[tourType];
            return tourDictionary[tourid];
        }

        public void AddTourToMap(TourNode tour)
        {
            if (Tours.ContainsKey(tour.TourType))
            {
                Tours[tour.TourType].Add(tour.TourId, tour);
            }
            else
            {
                var newTour = new Dictionary<int, TourNode> {{tour.TourId, tour}};
                Tours.Add(tour.TourType, newTour);
            }
        }
    }
}