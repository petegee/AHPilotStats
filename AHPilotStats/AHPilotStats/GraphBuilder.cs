using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using System.Linq;
using NPlot;
using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace My2Cents.HTC.AHPilotStats
{
    class GraphBuilder
    {
        private readonly List<XyData> _plots;
        private readonly string _pilotName;

        public GraphBuilder(string pilotName)
        {
            _pilotName = pilotName;
            _plots = new List<XyData>();
        }


        public void ResetGraph(NPlot.Windows.PlotSurface2D graphSurface)
        {
            _plots.Clear();
            RefreshPlots(graphSurface);
        }


        public void AddPlot(NPlot.Windows.PlotSurface2D graphSurface, string chosenGraphName, string mode, string tourTypeFilter)
        {
            switch (chosenGraphName)
            {
                case "Kill/Death Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllDeathPlus1", Color.Blue, mode, tourTypeFilter);
                    break;
                case "HTC Kill/Death Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "HTCKillsPerDeath", Color.CadetBlue, mode, tourTypeFilter);
                    break;
                case "Kill/Landed Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllLanded", Color.Crimson, mode, tourTypeFilter);
                    break;
                case "Kill/Sortie Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllSorties", Color.Chartreuse, mode, tourTypeFilter);
                    break;
                case "Kill/Hour Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllTimeInHours", Color.Chocolate, mode, tourTypeFilter);
                    break;
                case "Kill/Assist Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllKills", "OverAllAssists", Color.DarkOliveGreen, mode, tourTypeFilter);
                    break;
                case "Hit % Trend":
                    AddHitPercentageTrendPlot(_pilotName, chosenGraphName, mode, tourTypeFilter);
                    break;
                case "Sorties/Landed Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllSorties", "OverAllLanded", Color.IndianRed, mode, tourTypeFilter);
                    break;
                case "Sorties/Death Trend":
                    AddRatioTrendPlot(_pilotName, chosenGraphName, "OverAllSorties", "OverAllDeath", Color.DarkRed, mode, tourTypeFilter);
                    break;
                case "Total Kills Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllKills", Color.Teal, mode, tourTypeFilter);
                    break;
                case "Total Assists Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllAssists", Color.YellowGreen, mode, tourTypeFilter);
                    break;
                case "Total Sorties Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllSorties", Color.DarkMagenta, mode, tourTypeFilter);
                    break;
                case "Total Landed Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllLanded", Color.Turquoise, mode, tourTypeFilter);
                    break;
                case "Total Bailed Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllBailed", Color.Salmon, mode, tourTypeFilter);
                    break;
                case "Total Captured Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllCaptured", Color.MidnightBlue, mode, tourTypeFilter);
                    break;
                case "Total Death Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllDeath", Color.HotPink, mode, tourTypeFilter);
                    break;
                case "Total Time Trend":
                    AddSimpleTrendPlot(_pilotName, chosenGraphName, "OverAllTimeInHours", Color.DarkOrchid, mode, tourTypeFilter);
                    break;
            }
        }



        public void AddHitPercentageTrendPlot(string pilotName, string plotName, string mode, string tourTypeFilter)
        {
            Registry.Instance.GetPilotStats(pilotName).FighterScoresList.SortList("TourNumber", ListSortDirection.Ascending);
            var xy = new XyData(pilotName, plotName, mode, tourTypeFilter, null, null, Color.DarkOrange);
            _plots.Add(xy);

            // hmmm, im not 100% convinced this will add the data in the correct order?? 
            foreach (var score in 
                Registry.Instance.GetPilotStats(pilotName)
                    .FighterScoresList
                    .Where(score => score.TourType == tourTypeFilter))
            {
                xy.YData.Add((double)score.VsEnemyHitPercentageScore);
            }
        }


        public void AddRatioTrendPlot(string pilotName, string plotName, string statNameDividend, string statNameDivisor, Color lineColour, string mode, string tourTypeFilter)
        {
            var xy = new XyData(pilotName, plotName, mode, tourTypeFilter, statNameDividend, statNameDivisor, lineColour);
            _plots.Add(xy);
        }



        public void AddSimpleTrendPlot(string pilotName, string plotName, string statName, Color lineColour, string mode, string tourTypeFilter)
        {
            var xy = new XyData(pilotName, plotName, mode, tourTypeFilter, statName, null, lineColour);
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



        class XyData // ***** NESTED CLASS ***** //
        {
            private readonly string _pilotName;

            public ArrayList XData { get; private set; }
            public ArrayList YData { get; private set; }
            public string PlotName { get; private set; }
            public Color PlotColour { get; private set; }

            public XyData(string pilotName, string name, string mode, string tourTypeFilter, string propertyA, string propertyB, Color plotColour)
            {
                PlotName = name;
                PlotColour = plotColour;
                _pilotName = pilotName;
                XData = new ArrayList();
                YData = new ArrayList();
                foreach (var stats in GetStats(mode, tourTypeFilter))
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


            private SortableList<StatsDomainObject> GetStats(string mode, string filterTourType)
            {
                var list = new SortableList<StatsDomainObject>();

                switch (mode)
                {
                    case "Fighter":
                        list = Registry.Instance.GetPilotStats(_pilotName).FighterStatsList;
                        break;
                    case "Attack":
                        list = Registry.Instance.GetPilotStats(_pilotName).AttackStatsList;
                        break;
                    case "Bomber":
                        list = Registry.Instance.GetPilotStats(_pilotName).BomberStatsList;
                        break;
                    case "Vehicle/Boat":
                        list = Registry.Instance.GetPilotStats(_pilotName).VehicleBoatStatsList;
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

        }
    }
}
