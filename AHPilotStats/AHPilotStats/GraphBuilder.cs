using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using System.Linq;
using NPlot;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.AHPilotStats.Collections;
using My2Cents.HTC.AHPilotStats.DataRepository;
using Microsoft.Practices.Unity;

namespace My2Cents.HTC.AHPilotStats
{
    public class GraphBuilder
    {
        private readonly List<XyData> _plots;
        private readonly string _pilotName;

        public GraphBuilder(string pilotName)
        {
            _pilotName = pilotName;
            _plots = new List<XyData>();
        }

        [Dependency]
        public IRegistry Registry { get; set; }

        public void ResetGraph(NPlot.Windows.PlotSurface2D graphSurface)
        {
            _plots.Clear();
            RefreshPlots(graphSurface);
        }


        public void AddPlot(NPlot.Windows.PlotSurface2D graphSurface, string chosenGraphName, string mode, string tourTypeFilter)
        {
            var statsList = GetStats(mode, tourTypeFilter);

            switch (chosenGraphName)
            {
                case "Kill/Death Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllDeathPlus1", Color.Blue, statsList);
                    break;
                case "HTC Kill/Death Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "HTCKillsPerDeath", Color.CadetBlue, statsList);
                    break;
                case "Kill/Landed Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllLanded", Color.Crimson, statsList);
                    break;
                case "Kill/Sortie Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllSorties", Color.Chartreuse, statsList);
                    break;
                case "Kill/Hour Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllTimeInHours", Color.Chocolate, statsList);
                    break;
                case "Kill/Assist Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllAssists", Color.DarkOliveGreen, statsList);
                    break;
                case "Hit % Trend":
                    AddHitPercentageTrendPlot(_pilotName, chosenGraphName, tourTypeFilter, statsList);
                    break;
                case "Sorties/Landed Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllSorties", "OverAllLanded", Color.IndianRed, statsList);
                    break;
                case "Sorties/Death Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllSorties", "OverAllDeath", Color.DarkRed, statsList);
                    break;
                case "Total Kills Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllKills", Color.Teal, statsList);
                    break;
                case "Total Assists Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllAssists", Color.YellowGreen, statsList);
                    break;
                case "Total Sorties Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllSorties", Color.DarkMagenta, statsList);
                    break;
                case "Total Landed Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllLanded", Color.Turquoise, statsList);
                    break;
                case "Total Bailed Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllBailed", Color.Salmon, statsList);
                    break;
                case "Total Captured Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllCaptured", Color.MidnightBlue, statsList);
                    break;
                case "Total Death Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllDeath", Color.HotPink, statsList);
                    break;
                case "Total Time Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllTimeInHours", Color.DarkOrchid, statsList);
                    break;
            }
        }

        public void AddHitPercentageTrendPlot(string pilotName, string plotName, string tourTypeFilter, SortableList<StatsDomainObject> statsList)
        {
            Registry.GetPilotStats(pilotName).FighterScoresList.SortList("TourNumber", ListSortDirection.Ascending);
            var xy = new XyData(pilotName, plotName, null, null, Color.DarkOrange, statsList);
            _plots.Add(xy);

            // hmmm, im not 100% convinced this will add the data in the correct order?? 
            foreach (var score in 
                Registry.GetPilotStats(pilotName)
                    .FighterScoresList
                    .Where(score => score.TourType == tourTypeFilter))
            {
                xy.YData.Add((double)score.VsEnemyHitPercentageScore);
            }
        }

        public void AddRatioTrendPlot(string pilotName, string plotName, string statNameDividend, string statNameDivisor, Color lineColour, SortableList<StatsDomainObject> statsList)
        {
            var xy = new XyData(pilotName, plotName, statNameDividend, statNameDivisor, lineColour, statsList);
            _plots.Add(xy);
        }

        public void AddSimpleTrendPlot(string pilotName, string plotName, string statName, Color lineColour, SortableList<StatsDomainObject> statsList)
        {
            var xy = new XyData(pilotName, plotName, statName, null, lineColour, statsList);
            _plots.Add(xy);
        }

        public void RefreshPlots(NPlot.Windows.PlotSurface2D graphSurface)
        {
            graphSurface.Clear();

            if (_plots.Count == 0)
            {
                graphSurface.Hide();
                return;
            }


            foreach (var plot in _plots)
            {
                var lp = new LinePlot
                {
                    Color = plot.PlotColour,
                    AbscissaData = plot.XData,
                    OrdinateData = plot.YData,
                    Label = plot.PlotName
                };
                graphSurface.Add(lp);               
            }

            graphSurface.Title = "Pilot Trends";
            var grid = new Grid
            {
                VerticalGridType = Grid.GridType.Fine,
                HorizontalGridType = Grid.GridType.Fine,
                MajorGridPen = new Pen(Color.LightGray, 0.5f)
            };
            graphSurface.Add(grid);

            graphSurface.Refresh();


            var leg = new Legend
            {
                HorizontalEdgePlacement = Legend.Placement.Inside,
                VerticalEdgePlacement = Legend.Placement.Outside,
                XOffset = 10,
                YOffset = 10
            };
            leg.AttachTo(PlotSurface2D.XAxisPosition.Top, PlotSurface2D.YAxisPosition.Right);

            graphSurface.Legend = leg;
            graphSurface.LegendZOrder = 10;

            graphSurface.YAxis1.WorldMin = 0;
            graphSurface.XAxis1.Label = "Tour Number";
            graphSurface.XAxis1.WorldMin -= 1;
            graphSurface.XAxis1.WorldMax += 1;

            graphSurface.Show();
            graphSurface.Refresh();
        }

        private SortableList<StatsDomainObject> GetStats(string mode, string filterTourType)
        {
            var list = new SortableList<StatsDomainObject>();

            switch (mode)
            {
                case "Fighter":
                    list = Registry.GetPilotStats(_pilotName).FighterStatsList;
                    break;
                case "Attack":
                    list = Registry.GetPilotStats(_pilotName).AttackStatsList;
                    break;
                case "Bomber":
                    list = Registry.GetPilotStats(_pilotName).BomberStatsList;
                    break;
                case "Vehicle/Boat":
                    list = Registry.GetPilotStats(_pilotName).VehicleBoatStatsList;
                    break;
            }

            var filteredList = new SortableList<StatsDomainObject>();

            filteredList.AddRange(list.Where(statsObj => statsObj.TourType == filterTourType));

            if (filteredList.Count > 0)
            {
                filteredList.SortList("TourNumber", ListSortDirection.Ascending);
            }

            return filteredList;
        }

        class XyData // ***** NESTED CLASS ***** //
        {
            private readonly string _pilotName;

            public ArrayList XData { get; private set; }
            public ArrayList YData { get; private set; }
            public string PlotName { get; private set; }
            public Color PlotColour { get; private set; }

            public XyData(string pilotName, string name, string propertyA, string propertyB, Color plotColour, SortableList<StatsDomainObject> statsList)
            {
                PlotName = name;
                PlotColour = plotColour;
                _pilotName = pilotName;
                XData = new ArrayList();
                YData = new ArrayList();
                //foreach (var stats in GetStats(mode, tourTypeFilter))
                foreach (var stats in statsList)
                {
                    XData.Add(stats.TourNumber);

                    if (propertyA != null && propertyB == null)
                    {
                        YData.Add(GetMemberAsDouble(propertyA, stats));
                    }

                    if (propertyA != null && propertyB != null)
                    {
                        YData.Add(GetRatio(GetMemberAsDouble(propertyA, stats), GetMemberAsDouble(propertyB, stats)));
                    }
                }
            }

            private double GetMemberAsDouble(string propName, StatsDomainObject stats)
            {
                double value;
        
                try
                {
                    var intVal = (int)typeof(StatsDomainObject).InvokeMember(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty,
                        null,
                        stats,
                        null);
                    value = intVal;
                }
                catch (InvalidCastException)
                {
                    value = (double)typeof(StatsDomainObject).InvokeMember(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty,
                                                   null,
                                                   stats,
                                                   null);
                }
                return value;
            }

            private double GetRatio(double a, double b)
            {
                double ratio = 0;
                if (a > 0 && b > 0)
                    ratio = a / b;

                return ratio;
            }
        }
    }
}
