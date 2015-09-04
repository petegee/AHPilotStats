using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace My2Cents.HTC.AHPilotStats
{
    public partial class AKUAGStatsPanel : UserControl
    {
        public struct WoodenSpoonAward
        {
            public string WinnerName;
            public decimal KillsPerAssist;
        }

        public struct WarhawkAward
        {
            public string WinnerName;
            public int Kills;
        }

        public struct MostKillsAward
        {
            public string WinnerName;
            public int Kills;
        }

        public struct XOAward
        {
            public string WinnerName;
            public decimal Points;
        }


        public AKUAGStatsPanel()
        {
            InitializeComponent();
            CreateTourDebriefData();
        }

        private void CreateTourDebriefData()
        {
            StringBuilder debriefText = new StringBuilder();
            int tourNumber = GetLatestTourNumber();
            debriefText.Append(string.Format("Tour {0} debrief{1}{1}", tourNumber, Environment.NewLine));
            Squad squad = Registry.Instance.GetSquad("AKUAG");
            
            XOAward xoAward;
            xoAward.WinnerName = "";
            xoAward.Points = 0;
            decimal highestPoints = 0;

            foreach (Squad.SquadMember member in squad.Members)
            {
                if (!squad.WasPilotInSquadForThisTour(tourNumber, member))
                    continue;

                Registry.PilotStatsRegistry pilotStats = Registry.Instance.GetPilotStats(member.PilotName);
                FighterStatsDO stats = GetPilotStats(pilotStats, tourNumber);
                FighterScoresDO scores = GetPilotScores(pilotStats, tourNumber);
                if (scores != null && stats != null)
                {
                    debriefText.Append(string.Format("{0}{1}", member.PilotName, Environment.NewLine));

                    debriefText.Append(string.Format("Total Kills = {0}{1}", stats.OverAllKills, Environment.NewLine));
                    debriefText.Append(string.Format("Total Assists = {0}{1}", stats.OverAllAssists, Environment.NewLine));
                    debriefText.Append(string.Format("Total Time = {0}{1}", decimal.Round((decimal)stats.OverAllTimeInHours, 2), Environment.NewLine));
                    debriefText.Append(string.Format("Kills/Death = {0}{1}", decimal.Round((decimal)stats.OverAllKills / (decimal)stats.OverAllDeathPlus1, 2), Environment.NewLine));
                    debriefText.Append(string.Format("Kills/Assist = {0}{1}", stats.OverAllAssists == 0 ? (decimal)stats.OverAllKills : decimal.Round((decimal)stats.OverAllKills / (decimal)stats.OverAllAssists, 2), Environment.NewLine));
                    debriefText.Append(string.Format("Kills/Hour = {0}{1}", decimal.Round((decimal)stats.OverAllKills / (decimal)stats.OverAllTimeInHours, 2), Environment.NewLine));
                    debriefText.Append(string.Format("Kills/Sortie = {0}{1}", stats.OverAllSorties == 0 ? 0 : decimal.Round((decimal)stats.OverAllKills / (decimal)stats.OverAllSorties, 2), Environment.NewLine));
                    debriefText.Append(string.Format("Hit% = {0} {1}", scores.VsEnemyHitPercentageScore, Environment.NewLine));
                    debriefText.Append("AKUAG Performance Rating = ");

                    decimal points = GetPilotsPoints(stats, scores);
                    debriefText.Append(points);

                    if (points > highestPoints && member.PilotName != "Spatula")
                    {
                        highestPoints = points;
                        xoAward = new XOAward();
                        xoAward.Points = points;
                        xoAward.WinnerName = member.PilotName;
                    }

                    debriefText.Append(Environment.NewLine);
                    debriefText.Append(Environment.NewLine);
                }
            }

            debriefText.Append(string.Format("Awards{0}", Environment.NewLine));

            debriefText.Append(string.Format("The XO position goes to: {0} for {1} points", xoAward.WinnerName, decimal.Round(xoAward.Points, 2)));
            debriefText.Append(Environment.NewLine);
            
            // Wooden spoon award.
            WoodenSpoonAward spoon = GetWoodenSpoonAward(squad, tourNumber);
            debriefText.Append(string.Format("Wooden Spoon award goes to: {0} for {1} kills/assist.", spoon.WinnerName, decimal.Round(spoon.KillsPerAssist, 2)));
            
            // Warhawk award.
            debriefText.Append(Environment.NewLine);
            WarhawkAward warhawk = GetWarhawkAward(squad, tourNumber);
            if (warhawk.Kills != Int32.MinValue)
                debriefText.Append(string.Format("Warhawk award goes to: {0} for {1} kills in any P40 model.", warhawk.WinnerName, warhawk.Kills.ToString()));
            else
                debriefText.Append("Warhawk award not won this tour.");

            // most kills award.
            debriefText.Append(Environment.NewLine);
            MostKillsAward killer = GetMostKillsAward(squad, tourNumber);
            debriefText.Append(string.Format("King Killer award goes to: {0} for {1} kills.", killer.WinnerName, killer.Kills));
            

            this.textBox1.Text = debriefText.ToString();
        }

        private int GetLatestTourNumber()
        {
            int maxTour = 0;
            Registry.PilotStatsRegistry spatsStatsRegistry = Registry.Instance.GetPilotStats("Spatula");
            foreach (FighterScoresDO fighterScore in spatsStatsRegistry.FighterScoresList)
            { 
                if(fighterScore.TourNumber > maxTour)
                    maxTour = fighterScore.TourNumber;
            }

            return maxTour;
        }


        private MostKillsAward GetMostKillsAward(Squad squad, int tourNumber)
        {
            MostKillsAward award = new MostKillsAward();
            award.Kills = Int32.MinValue;

            foreach (Squad.SquadMember member in squad.Members)
            {
                MostKillsAward candidateAward = new MostKillsAward();
                Registry.PilotStatsRegistry pilotStats = Registry.Instance.GetPilotStats(member.PilotName);
                FighterStatsDO stats = GetPilotStats(pilotStats, tourNumber);
                if (stats == null)
                    continue;

                candidateAward.WinnerName = member.PilotName;
                candidateAward.Kills = stats.OverAllKills;

                if (candidateAward.Kills > award.Kills)
                    award = candidateAward;
            }

            return award;
        }


        private WarhawkAward GetWarhawkAward(Squad squad, int tourNumber)
        {
            WarhawkAward award = new WarhawkAward();
            award.Kills = Int32.MinValue;

            foreach (Squad.SquadMember member in squad.Members)
            {
                WarhawkAward candidateAward = new WarhawkAward();
                Registry.PilotStatsRegistry pilotStats = Registry.Instance.GetPilotStats(member.PilotName);
                if (pilotStats == null)
                    continue;

                int kills = 0;
                foreach (ObjectVsObjectDO objVObj in pilotStats.ObjVsObjCompleteList)
                {
                    if (objVObj.Model.Contains("P-40"))
                    {
                        kills = objVObj.KillsIn;
                    }
                }
                candidateAward.WinnerName = member.PilotName;
                candidateAward.Kills = kills;

                if (candidateAward.Kills < award.Kills)
                    award = candidateAward;
            }

            return award;
        }


        private WoodenSpoonAward GetWoodenSpoonAward(Squad squad, int tourNumber)
        {
            WoodenSpoonAward award = new WoodenSpoonAward();
            award.KillsPerAssist = Int32.MaxValue;

            foreach (Squad.SquadMember member in squad.Members)
            {
                WoodenSpoonAward candidateAward = new WoodenSpoonAward();
                Registry.PilotStatsRegistry pilotStats = Registry.Instance.GetPilotStats(member.PilotName);
                FighterStatsDO stats = GetPilotStats(pilotStats, tourNumber);
                if (stats == null)
                    continue;

                candidateAward.WinnerName = member.PilotName;
                candidateAward.KillsPerAssist = stats.OverAllAssists == 0 ? (decimal)stats.OverAllKills : (decimal)stats.OverAllKills / stats.OverAllAssists;

                if (candidateAward.KillsPerAssist < award.KillsPerAssist)
                    award = candidateAward;
            }

            return award;
        }


        private decimal GetPilotsPoints(FighterStatsDO stats, FighterScoresDO scores)
        {
            if (stats != null && scores != null)
            { 
                decimal factor = 1;

                // if pilot hasnt spent 10 hours online, they are get 50% reduction in points.
                if (stats.OverAllTotalTimeInSeconds < 36000)
                    factor = (decimal)0.5;

                decimal KDPoints = (((decimal)stats.HTCKillsPerDeath) / (decimal)10) * (decimal)30;
                decimal KHPoints = (((decimal)scores.VsEnemyKillsPerHourScore) / (decimal)6.5) * (decimal)20;
                decimal KSPoints = (((decimal)scores.VsEnemyKillsToSortieScore) / (decimal)2) * (decimal)25;
                decimal HPPoints = ((decimal)scores.VsEnemyHitPercentageScore / (decimal)10) * (decimal)20;
                decimal actualTimePoints = ((decimal)stats.OverAllTotalTimeInSeconds / (decimal)72000) * (decimal)5;

                // ensure pilot can only earn a maximum of 5 points from time.
                decimal TmPoints = actualTimePoints > 5 ? 5 : actualTimePoints;

                return decimal.Round(((KDPoints + KHPoints + KSPoints + HPPoints + TmPoints) * factor),2);           
            }

            return 0;
        }

        private FighterStatsDO GetPilotStats(Registry.PilotStatsRegistry pilotStats, int tourNumber)
        {
            FighterStatsDO stats = null;
            foreach (FighterStatsDO thisStats in pilotStats.FighterStatsList)
            {
                if (thisStats.TourNumber == tourNumber.ToString())
                    stats = thisStats;
            }
            return stats;
        }

        private FighterScoresDO GetPilotScores(Registry.PilotStatsRegistry pilotStats, int tourNumber)
        {
            FighterScoresDO scores = null;
            foreach (FighterScoresDO thisScores in pilotStats.FighterScoresList)
            {
                if (thisScores.TourNumber == tourNumber)
                    scores = thisScores;
            }

            return scores;
        }
    }
}
