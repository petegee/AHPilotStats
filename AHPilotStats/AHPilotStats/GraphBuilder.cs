using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Drawing;
using NPlot;
using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace My2Cents.HTC.AHPilotStats
{
    class GraphBuilder
    {
        private List<XYData> _plots = new List<XYData>();
        private string _pilotName ;
        public GraphBuilder(string pilotName)
        {
            _pilotName = pilotName;
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
            XYData xy = new XYData(pilotName, plotName, mode, tourTypeFilter, null, null, Color.DarkOrange);
            _plots.Add(xy);

            // hmmm, im not 100% convinced this will add the data in the correct order?? 
            foreach (FighterScoresDO score in Registry.Instance.GetPilotStats(pilotName).FighterScoresList)
            {
                if(score.TourType == tourTypeFilter)
                    xy.yData.Add((double)score.VsEnemyHitPercentageScore);
            }
        }


        public void AddRatioTrendPlot(string pilotName, string plotName, string statNameDividend, string statNameDivisor, Color lineColour, string mode, string tourTypeFilter)
        {
            XYData xy = new XYData(pilotName, plotName, mode, tourTypeFilter, statNameDividend, statNameDivisor, lineColour);
            _plots.Add(xy);
        }



        public void AddSimpleTrendPlot(string pilotName, string plotName, string statName, Color lineColour, string mode, string tourTypeFilter)
        {
            XYData xy = new XYData(pilotName, plotName, mode, tourTypeFilter, statName, null, lineColour);
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


            foreach (XYData plot in _plots)
            { 
                LinePlot lp = new LinePlot();
                lp.Color = plot.PlotColour;
                lp.AbscissaData = plot.xData;
                lp.OrdinateData = plot.yData;
                lp.Label = plot.PlotName;
                graphSurface.Add(lp);               
            }

            graphSurface.Title = "Pilot Trends";
            Grid grid = new Grid();
            grid.VerticalGridType = Grid.GridType.Fine;
            grid.HorizontalGridType = Grid.GridType.Fine;
            grid.MajorGridPen = new Pen(Color.LightGray, 0.5f);
            graphSurface.Add(grid);

            graphSurface.Refresh();


            Legend leg = new Legend();
            leg.AttachTo(PlotSurface2D.XAxisPosition.Top, PlotSurface2D.YAxisPosition.Right);
            leg.HorizontalEdgePlacement = Legend.Placement.Inside;
            leg.VerticalEdgePlacement = Legend.Placement.Outside;
            leg.XOffset = 10;
            leg.YOffset = 10;
            graphSurface.Legend = leg;
            graphSurface.LegendZOrder = 10;

            graphSurface.YAxis1.WorldMin = 0;
            graphSurface.XAxis1.Label = "Tour Number";
            graphSurface.XAxis1.WorldMin -= 1;
            graphSurface.XAxis1.WorldMax += 1;

            graphSurface.Show();
            graphSurface.Refresh();
        }



        class XYData // ***** NESTED CLASS ***** //
        {
            public ArrayList xData = new ArrayList();
            public ArrayList yData = new ArrayList();
            public string PlotName;
            public string PilotName;
            public Color PlotColour;

            public XYData(string pilotName, string name, string mode, string tourTypeFilter, string PropertyA, string PropertyB, Color plotColour)
            {
                PlotName = name;
                PlotColour = plotColour;
                PilotName = pilotName;

                foreach (StatsDomainObject stats in GetStats(mode, tourTypeFilter))
                {
                    xData.Add(stats.TourNumber);

                    if (PropertyA != null && PropertyB == null)
                    {
                        yData.Add(GetMemberAsDouble(PropertyA, stats));
                    }

                    if (PropertyA != null && PropertyB != null)
                    {
                        yData.Add(GetRatio(GetMemberAsDouble(PropertyA, stats), GetMemberAsDouble(PropertyB, stats)));
                    }
                }
            }


            private double GetMemberAsDouble(string propName, StatsDomainObject stats)
            {
                double value=0;
        
                try
                {
                    int intVal;
                    intVal = (int)typeof(StatsDomainObject).InvokeMember(propName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty,
                                    null,
                                    stats,
                                    null);
                    value = (double)intVal;
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
                SortableList<StatsDomainObject> list = new SortableList<StatsDomainObject>();

                switch (mode)
                {
                    case "Fighter":
                        list = Registry.Instance.GetPilotStats(PilotName).FighterStatsList;
                        break;
                    case "Attack":
                        list = Registry.Instance.GetPilotStats(PilotName).AttackStatsList;
                        break;
                    case "Bomber":
                        list = Registry.Instance.GetPilotStats(PilotName).BomberStatsList;
                        break;
                    case "Vehicle/Boat":
                        list = Registry.Instance.GetPilotStats(PilotName).VehicleBoatStatsList;
                        break;
                }

                SortableList<StatsDomainObject> filteredList = new SortableList<StatsDomainObject>();

                foreach (StatsDomainObject statsObj in list)
                {
                    if (statsObj.TourType == filterTourType)
                        filteredList.Add(statsObj);
                }

                if (filteredList.Count > 0)
                {
                    filteredList.SortList("TourNumber", ListSortDirection.Ascending);
                }

                return filteredList;
            }

        }
    }
}
