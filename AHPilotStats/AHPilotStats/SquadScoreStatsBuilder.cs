using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using My2Cents.HTC.PilotScoreSvc;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats
{
    class SquadScoreStatsBuilder
    {
        public SquadScoreStatsBuilder() { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pilots"></param>
        /// <param name="squadName"></param>
        /// <param name="tourNumber"></param>
        /// <returns></returns>
        public AcesHighPilotScore BuildSquadScoreObject(Squad squad, int tourNumber)
        {
            AcesHighPilotScore squadScore = new AcesHighPilotScore();
            Utility.ConstructAcesHighPilotScoreInnerObjects(ref squadScore);

            // count the pilots for this tour.
            int numPilots = squad.CountMembersForTour(tourNumber);


            foreach (Squad.SquadMember squadMember in squad.Members)
            {
                if (!Registry.Instance.PilotStatsContains(squadMember.PilotName))
                    continue;

                Registry.PilotStatsRegistry pilotReg = Registry.Instance.GetPilotStats(squadMember.PilotName);

                // If Pilot wasnt in this sqaud for this tour, dont count their data
                if (!squad.WasPilotInSquadForThisTour(tourNumber, squadMember))
                    continue;    

                foreach (AttackScoresDO attackScore in pilotReg.AttackScoresList)
                {
                    if (attackScore.TourNumber != tourNumber)
                        continue;

                    squadScore.GameId = squad.SquadName;
                    squadScore.TourDetails = attackScore.TourIdentfier;
                    squadScore.TourType = attackScore.TourType;
                    squadScore.TourId = attackScore.TourNumber.ToString();

                    squadScore.VsEnemy.Attack.HitPercentage.Score += attackScore.VsEnemyHitPercentageScore / numPilots;
                    squadScore.VsEnemy.Attack.KillPerHour.Score += attackScore.VsEnemyKillsPerHourScore / numPilots;
                    squadScore.VsEnemy.Attack.KillPerSortie.Score += attackScore.VsEnemyKillsToSortieScore / numPilots;
                    squadScore.VsEnemy.Attack.KillToDeathPlus1.Score += attackScore.VsEnemyKillsToDeathScore / numPilots;
                    squadScore.VsObjects.Attack.DamagePerDeathPlus1.Score += attackScore.VsObjectsDamagePerDeathScore / numPilots;
                    squadScore.VsObjects.Attack.DamagePerSortie.Score += attackScore.VsObjectsDamagePerSortieScore / numPilots;
                    squadScore.VsObjects.Attack.FieldCaptures.Score += attackScore.VsObjectsFieldCapturesScore / numPilots;
                    squadScore.VsObjects.Attack.HitPercentage.Score += attackScore.VsObjectsHitPercentageScore / numPilots;
                }

                foreach (BomberScoresDO bomberScores in pilotReg.BomberScoresList)
                {
                    if (bomberScores.TourNumber != tourNumber)
                        continue;

                    squadScore.VsObjects.Bomber.DamagePerDeathPlus1.Score += bomberScores.VsObjectsDamagePerDeathScore / numPilots;
                    squadScore.VsObjects.Bomber.DamagePerSortie.Score += bomberScores.VsObjectsDamagePerSortieScore / numPilots;
                    squadScore.VsObjects.Bomber.FieldCaptures.Score += bomberScores.VsObjectsFieldCapturesScore / numPilots;
                    squadScore.VsObjects.Bomber.HitPercentage.Score += bomberScores.VsObjectsHitPercentageScore / numPilots;
                }

                foreach (FighterScoresDO fighterScores in pilotReg.FighterScoresList)
                {
                    if (fighterScores.TourNumber != tourNumber)
                        continue;

                    squadScore.VsEnemy.Fighter.HitPercentage.Score += fighterScores.VsEnemyHitPercentageScore / numPilots;
                    squadScore.VsEnemy.Fighter.KillPerHour.Score += fighterScores.VsEnemyKillsPerHourScore / numPilots;
                    squadScore.VsEnemy.Fighter.KillPerSortie.Score += fighterScores.VsEnemyKillsToSortieScore / numPilots;
                    squadScore.VsEnemy.Fighter.KillToDeathPlus1.Score += fighterScores.VsEnemyKillsToDeathScore / numPilots;
                }

                foreach (VehicleBoatScoresDO vehicleBoatScores in pilotReg.VehicleBoatScoresList)
                {
                    if (vehicleBoatScores.TourNumber != tourNumber)
                        continue;

                    squadScore.VsEnemy.VehicleBoat.HitPercentage.Score += vehicleBoatScores.VsEnemyHitPercentageScore / numPilots;
                    squadScore.VsEnemy.VehicleBoat.KillPerHour.Score += vehicleBoatScores.VsEnemyKillsPerHourScore / numPilots;
                    squadScore.VsEnemy.VehicleBoat.KillPerSortie.Score += vehicleBoatScores.VsEnemyKillsToSortieScore / numPilots;
                    squadScore.VsEnemy.VehicleBoat.KillToDeathPlus1.Score += vehicleBoatScores.VsEnemyKillsToDeathScore / numPilots;
                    squadScore.VsObjects.VehicleBoat.DamagePerDeathPlus1.Score += vehicleBoatScores.VsObjectsDamagePerDeathScore / numPilots;
                    squadScore.VsObjects.VehicleBoat.DamagePerSortie.Score += vehicleBoatScores.VsObjectsDamagePerSortieScore / numPilots;
                    squadScore.VsObjects.VehicleBoat.FieldCaptures.Score += vehicleBoatScores.VsObjectsFieldCapturesScore / numPilots;
                    squadScore.VsObjects.VehicleBoat.HitPercentage.Score += vehicleBoatScores.VsObjectsHitPercentageScore / numPilots;
                }


                foreach (AttackStatsDO stats in pilotReg.AttackStatsList)
                {
                    if (stats.TourNumber != tourNumber.ToString())
                        continue;

                    squadScore.Overall.Attack.Assists += stats.OverAllAssists;
                    squadScore.Overall.Attack.Bailed += stats.OverAllBailed;
                    squadScore.Overall.Attack.Captured += stats.OverAllCaptured;
                    squadScore.Overall.Attack.Death += stats.OverAllDeath;
                    squadScore.Overall.Attack.Disco += stats.OverAllDisco;
                    squadScore.Overall.Attack.Ditched += stats.OverAllDitched;
                    squadScore.Overall.Attack.Kills += stats.OverAllKills;
                    squadScore.Overall.Attack.Landed += stats.OverAllLanded;
                    squadScore.Overall.Attack.Sorties += stats.OverAllSorties;
                    squadScore.Overall.Attack.Time += stats.OverAllTimeInSeconds;
                }

                foreach (BomberStatsDO stats in pilotReg.BomberStatsList)
                {
                    if (stats.TourNumber != tourNumber.ToString())
                        continue;

                    squadScore.Overall.Bomber.Assists += stats.OverAllAssists;
                    squadScore.Overall.Bomber.Bailed += stats.OverAllBailed;
                    squadScore.Overall.Bomber.Captured += stats.OverAllCaptured;
                    squadScore.Overall.Bomber.Death += stats.OverAllDeath;
                    squadScore.Overall.Bomber.Disco += stats.OverAllDisco;
                    squadScore.Overall.Bomber.Ditched += stats.OverAllDitched;
                    squadScore.Overall.Bomber.Kills += stats.OverAllKills;
                    squadScore.Overall.Bomber.Landed += stats.OverAllLanded;
                    squadScore.Overall.Bomber.Sorties += stats.OverAllSorties;
                    squadScore.Overall.Bomber.Time += stats.OverAllTimeInSeconds;
                }

                foreach (FighterStatsDO stats in pilotReg.FighterStatsList)
                {
                    if (stats.TourNumber != tourNumber.ToString())
                        continue;

                    squadScore.Overall.Fighter.Assists += stats.OverAllAssists;
                    squadScore.Overall.Fighter.Bailed += stats.OverAllBailed;
                    squadScore.Overall.Fighter.Captured += stats.OverAllCaptured;
                    squadScore.Overall.Fighter.Death += stats.OverAllDeath;
                    squadScore.Overall.Fighter.Disco += stats.OverAllDisco;
                    squadScore.Overall.Fighter.Ditched += stats.OverAllDitched;
                    squadScore.Overall.Fighter.Kills += stats.OverAllKills;
                    squadScore.Overall.Fighter.Landed += stats.OverAllLanded;
                    squadScore.Overall.Fighter.Sorties += stats.OverAllSorties;
                    squadScore.Overall.Fighter.Time += stats.OverAllTimeInSeconds;
                }

                foreach (VehicleBoatStatsDO stats in pilotReg.VehicleBoatStatsList)
                {
                    if (stats.TourNumber != tourNumber.ToString())
                        continue;

                    squadScore.Overall.VehicleBoat.Assists += stats.OverAllAssists;
                    squadScore.Overall.VehicleBoat.Bailed += stats.OverAllBailed;
                    squadScore.Overall.VehicleBoat.Captured += stats.OverAllCaptured;
                    squadScore.Overall.VehicleBoat.Death += stats.OverAllDeath;
                    squadScore.Overall.VehicleBoat.Disco += stats.OverAllDisco;
                    squadScore.Overall.VehicleBoat.Ditched += stats.OverAllDitched;
                    squadScore.Overall.VehicleBoat.Kills += stats.OverAllKills;
                    squadScore.Overall.VehicleBoat.Landed += stats.OverAllLanded;
                    squadScore.Overall.VehicleBoat.Sorties += stats.OverAllSorties;
                    squadScore.Overall.VehicleBoat.Time += stats.OverAllTimeInSeconds;
                }

            }

            squadScore.VsEnemy.Attack.HitPercentage.Score = decimal.Round(squadScore.VsEnemy.Attack.HitPercentage.Score, 2);
            squadScore.VsEnemy.Attack.KillPerHour.Score = decimal.Round(squadScore.VsEnemy.Attack.KillPerHour.Score, 2);
            squadScore.VsEnemy.Attack.KillPerSortie.Score = decimal.Round(squadScore.VsEnemy.Attack.KillPerSortie.Score, 2);
            squadScore.VsEnemy.Attack.KillToDeathPlus1.Score = decimal.Round(squadScore.VsEnemy.Attack.KillToDeathPlus1.Score, 2);
            squadScore.VsObjects.Attack.DamagePerDeathPlus1.Score = decimal.Round(squadScore.VsObjects.Attack.DamagePerDeathPlus1.Score, 2);
            squadScore.VsObjects.Attack.DamagePerSortie.Score = decimal.Round(squadScore.VsObjects.Attack.DamagePerSortie.Score, 2);
            squadScore.VsObjects.Attack.FieldCaptures.Score = decimal.Round(squadScore.VsObjects.Attack.FieldCaptures.Score, 2);
            squadScore.VsObjects.Attack.HitPercentage.Score = decimal.Round(squadScore.VsObjects.Attack.HitPercentage.Score, 2);
            squadScore.VsObjects.Bomber.DamagePerDeathPlus1.Score = decimal.Round(squadScore.VsObjects.Bomber.DamagePerDeathPlus1.Score, 2);
            squadScore.VsObjects.Bomber.DamagePerSortie.Score = decimal.Round(squadScore.VsObjects.Bomber.DamagePerSortie.Score, 2);
            squadScore.VsObjects.Bomber.FieldCaptures.Score = decimal.Round(squadScore.VsObjects.Bomber.FieldCaptures.Score, 2);
            squadScore.VsObjects.Bomber.HitPercentage.Score = decimal.Round(squadScore.VsObjects.Bomber.HitPercentage.Score, 2);
            squadScore.VsEnemy.Fighter.HitPercentage.Score = decimal.Round(squadScore.VsEnemy.Fighter.HitPercentage.Score, 2);
            squadScore.VsEnemy.Fighter.KillPerHour.Score = decimal.Round(squadScore.VsEnemy.Fighter.KillPerHour.Score, 2);
            squadScore.VsEnemy.Fighter.KillPerSortie.Score = decimal.Round(squadScore.VsEnemy.Fighter.KillPerSortie.Score, 2);
            squadScore.VsEnemy.Fighter.KillToDeathPlus1.Score = decimal.Round(squadScore.VsEnemy.Fighter.KillToDeathPlus1.Score, 2);
            squadScore.VsEnemy.VehicleBoat.HitPercentage.Score = decimal.Round(squadScore.VsEnemy.VehicleBoat.HitPercentage.Score, 2);
            squadScore.VsEnemy.VehicleBoat.KillPerHour.Score = decimal.Round(squadScore.VsEnemy.VehicleBoat.KillPerHour.Score, 2);
            squadScore.VsEnemy.VehicleBoat.KillPerSortie.Score = decimal.Round(squadScore.VsEnemy.VehicleBoat.KillPerSortie.Score, 2);
            squadScore.VsEnemy.VehicleBoat.KillToDeathPlus1.Score = decimal.Round(squadScore.VsEnemy.VehicleBoat.KillToDeathPlus1.Score, 2);
            squadScore.VsObjects.VehicleBoat.DamagePerDeathPlus1.Score = decimal.Round(squadScore.VsObjects.VehicleBoat.DamagePerDeathPlus1.Score, 2);
            squadScore.VsObjects.VehicleBoat.DamagePerSortie.Score = decimal.Round(squadScore.VsObjects.VehicleBoat.DamagePerSortie.Score, 2);
            squadScore.VsObjects.VehicleBoat.FieldCaptures.Score = decimal.Round(squadScore.VsObjects.VehicleBoat.FieldCaptures.Score, 2);
            squadScore.VsObjects.VehicleBoat.HitPercentage.Score = decimal.Round(squadScore.VsObjects.VehicleBoat.HitPercentage.Score, 2);

            if (squadScore.TourDetails == null)
                squadScore.TourDetails = string.Format("{0} - [NO DATA]", tourNumber);

            if (squadScore.TourId == null)
                squadScore.TourId = tourNumber.ToString();

            if (squadScore.TourType == null)
                squadScore.TourType = "[UNKNOWN]";


            return squadScore;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="pilots"></param>
        /// <param name="squadName"></param>
        /// <param name="tourNumber"></param>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public AcesHighPilotStats BuildSquadStatsObject(Squad squad, int tourNumber, ref bool reloadPilotsRequired)
        {
            string tourIdentifier = "";
            string tourType = "";

            Dictionary<string, ObjectScore> tourStats = new Dictionary<string, ObjectScore>();


            // FOR EACH PILOT IN SQUAD
            foreach (Squad.SquadMember squadMember in squad.Members)
            {
                if (!Registry.Instance.PilotStatsContains(squadMember.PilotName))
                    continue;

                // get pilot's registry.
                Registry.PilotStatsRegistry pilotReg = Registry.Instance.GetPilotStats(squadMember.PilotName);

                // If Pilot wasnt in this sqaud for this tour, dont count their data
                if (!squad.WasPilotInSquadForThisTour(tourNumber, squadMember))
                    continue;  

                // FOR EACH MODEL
                foreach (ObjectVsObjectDO objVsObj in pilotReg.ObjVsObjCompleteList)
                {
                    // only for the tour we're interested in
                    if (objVsObj.TourNumber == tourNumber)
                    {
                        if (tourIdentifier == "")
                            tourIdentifier = objVsObj.TourIdentfier;
                        
                        if(tourType == "")
                            tourType = objVsObj.TourType;

                        if (!tourStats.ContainsKey(objVsObj.Model))
                        {
                            // doesnt exist yet? create new one and add.
                            ObjectScore thisObjvObj = new ObjectScore();
                            thisObjvObj.Model = objVsObj.Model;
                            thisObjvObj.KilledBy = objVsObj.KilledBy;
                            thisObjvObj.KillsIn = objVsObj.KillsIn;
                            thisObjvObj.KillsOf = objVsObj.KillsOf;
                            thisObjvObj.DiedIn  = objVsObj.DiedIn;
                            if (objVsObj.DiedIn == null)
                                reloadPilotsRequired = true;

                            tourStats.Add(thisObjvObj.Model, thisObjvObj);
                        }
                        else
                        {
                            // get existing objects reference and add to it.
                            ObjectScore thisObjvObj = tourStats[objVsObj.Model];
                            thisObjvObj.KilledBy += objVsObj.KilledBy;
                            thisObjvObj.KillsIn  += objVsObj.KillsIn;
                            thisObjvObj.KillsOf  += objVsObj.KillsOf;
                            thisObjvObj.DiedIn   += objVsObj.DiedIn;
                            if (objVsObj.DiedIn == null)
                                reloadPilotsRequired = true;
                        }
                    }
                }
            }

            AcesHighPilotStats squadStatsObj = new AcesHighPilotStats();
            Utility.ConstructAcesHighPilotStatsInnerObjects(ref squadStatsObj, tourStats.Count);
            squadStatsObj.GameId = squad.SquadName;
            squadStatsObj.TourId = tourNumber.ToString();
            squadStatsObj.TourType = tourType;
            squadStatsObj.TourDetails = tourIdentifier;

            // copy the list content to bounded sized array.
            int i = 0;
            foreach(ObjectScore obj in tourStats.Values)
            {
                squadStatsObj.VsObjects.ObjectScore[i++] = obj;
            }

            return squadStatsObj;
        }
    }
}
