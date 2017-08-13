using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.AHPilotStats
{
    internal class Utility
    {
        public static readonly object SyncRoot = new object();

        public static DataTable CreateDataTableFromList<T>(List<T> list)
        {
            var table = new DataTable();

            var t = typeof (T);
            var pis = t.GetProperties();
            for (var i = 0; i < pis.Length; i++)
            {
                var pi = (PropertyInfo) pis.GetValue(i);
                var customAttrs = pi.GetCustomAttributes(true);
                if (((GridHelperAttribute) customAttrs[0]).HideColumn) 
                    continue;

                var friendlyName = ((GridHelperAttribute) customAttrs[0]).FriendlyName;
                table.Columns.Add(friendlyName, pi.PropertyType);
            }

            var tableParams = new object[table.Columns.Count];
            foreach (var obj in list)
            {
                var itr = 0;
                for (var i = 0; i < pis.Length; i++)
                {
                    var pi = (PropertyInfo) pis.GetValue(i);
                    var customAttrs = pi.GetCustomAttributes(true);
                    if (!((GridHelperAttribute) customAttrs[0]).HideColumn)
                    {
                        tableParams[itr++] = t.GetProperty(pi.Name, pi.PropertyType).GetValue(obj, new object[] {});
                    }
                }
                table.Rows.Add(tableParams);
            }
            table.AcceptChanges();
            return table;
        }

        public static string GetFormattedTime(int timeInSeconds)
        {
            var remainingSeconds = 0;

            var wholeHours = timeInSeconds/3600;
            remainingSeconds = timeInSeconds%3600;

            var wholeMins = remainingSeconds/60;
            remainingSeconds = remainingSeconds%60;

            return string.Format("{0}:{1}:{2}",
                (wholeHours.ToString().PadLeft(2, '0')),
                (wholeMins.ToString().PadLeft(2, '0')),
                (remainingSeconds.ToString().PadLeft(2, '0')));
        }


        public static void ConstructAcesHighPilotScoreInnerObjects(ref AcesHighPilotScore ps)
        {
            ////////////////////////////////////////////////////////////////////////////////////
            // Vs Enemies
            ////////////////////////////////////////////////////////////////////////////////////
            ps.VsEnemy = new AcesHighPilotScoreVsEnemy
            {
                Attack = new AcesHighPilotScoreVsEnemyAttack
                {
                    HitPercentage = new AcesHighPilotScoreVsEnemyAttackHitPercentage(),
                    KillPerHour = new AcesHighPilotScoreVsEnemyAttackKillPerHour(),
                    KillPerSortie = new AcesHighPilotScoreVsEnemyAttackKillPerSortie(),
                    KillToDeathPlus1 = new AcesHighPilotScoreVsEnemyAttackKillToDeathPlus1(),
                    Points = new AcesHighPilotScoreVsEnemyAttackPoints()
                },
                Fighter = new AcesHighPilotScoreVsEnemyFighter
                {
                    HitPercentage = new AcesHighPilotScoreVsEnemyFighterHitPercentage(),
                    KillPerHour = new AcesHighPilotScoreVsEnemyFighterKillPerHour(),
                    KillPerSortie = new AcesHighPilotScoreVsEnemyFighterKillPerSortie(),
                    KillToDeathPlus1 = new AcesHighPilotScoreVsEnemyFighterKillToDeathPlus1(),
                    Points = new AcesHighPilotScoreVsEnemyFighterPoints()
                },
                VehicleBoat = new AcesHighPilotScoreVsEnemyVehicleBoat
                {
                    HitPercentage = new AcesHighPilotScoreVsEnemyVehicleBoatHitPercentage(),
                    KillPerHour = new AcesHighPilotScoreVsEnemyVehicleBoatKillPerHour(),
                    KillPerSortie = new AcesHighPilotScoreVsEnemyVehicleBoatKillPerSortie(),
                    KillToDeathPlus1 = new AcesHighPilotScoreVsEnemyVehicleBoatKillToDeathPlus1(),
                    Points = new AcesHighPilotScoreVsEnemyVehicleBoatPoints()
                }
            };

            ////////////////////////////////////////////////////////////////////////////////////
            // Vs Objects
            ////////////////////////////////////////////////////////////////////////////////////
            ps.VsObjects = new AcesHighPilotScoreVsObjects
            {
                Attack = new AcesHighPilotScoreVsObjectsAttack
                {
                    DamagePerDeathPlus1 = new AcesHighPilotScoreVsObjectsAttackDamagePerDeathPlus1(),
                    DamagePerSortie = new AcesHighPilotScoreVsObjectsAttackDamagePerSortie(),
                    FieldCaptures = new AcesHighPilotScoreVsObjectsAttackFieldCaptures(),
                    HitPercentage = new AcesHighPilotScoreVsObjectsAttackHitPercentage(),
                    Points = new AcesHighPilotScoreVsObjectsAttackPoints()
                },
                Bomber = new AcesHighPilotScoreVsObjectsBomber
                {
                    DamagePerDeathPlus1 = new AcesHighPilotScoreVsObjectsBomberDamagePerDeathPlus1(),
                    DamagePerSortie = new AcesHighPilotScoreVsObjectsBomberDamagePerSortie(),
                    FieldCaptures = new AcesHighPilotScoreVsObjectsBomberFieldCaptures(),
                    HitPercentage = new AcesHighPilotScoreVsObjectsBomberHitPercentage(),
                    Points = new AcesHighPilotScoreVsObjectsBomberPoints()
                },
                VehicleBoat = new AcesHighPilotScoreVsObjectsVehicleBoat
                {
                    DamagePerDeathPlus1 = new AcesHighPilotScoreVsObjectsVehicleBoatDamagePerDeathPlus1(),
                    DamagePerSortie = new AcesHighPilotScoreVsObjectsVehicleBoatDamagePerSortie(),
                    FieldCaptures = new AcesHighPilotScoreVsObjectsVehicleBoatFieldCaptures(),
                    HitPercentage = new AcesHighPilotScoreVsObjectsVehicleBoatHitPercentage(),
                    Points = new AcesHighPilotScoreVsObjectsVehicleBoatPoints()
                }
            };


            ////////////////////////////////////////////////////////////////////////////////////
            // Overall / Raw Stats
            ////////////////////////////////////////////////////////////////////////////////////
            ps.Overall = new AcesHighPilotScoreOverall
            {
                Attack = new AcesHighPilotScoreOverallAttack(),
                Bomber = new AcesHighPilotScoreOverallBomber(),
                Fighter = new AcesHighPilotScoreOverallFighter(),
                VehicleBoat = new AcesHighPilotScoreOverallVehicleBoat(),
                Total = new AcesHighPilotScoreOverallTotal()
            };

        }


        public static void ConstructAcesHighPilotStatsInnerObjects(ref AcesHighPilotStats pilotStats, int objScoreLen)
        {
            pilotStats.VsCountry = new AcesHighPilotStatsVsCountry {CountryScore = new CountryScore[3]};
            pilotStats.VsObjects = new AcesHighPilotStatsVsObjects {ObjectScore = new ObjectScore[objScoreLen]};
        }


        public static void WriteDebugTraceFile(Exception e)
        {
            lock (SyncRoot)
            {
                var now = DateTime.Now;
                var filename = string.Format("AHPilotStatsError_{0}{1}{2}_{3}-{4}-{5}-{6}.dump",
                    now.Year,
                    now.Month,
                    now.Day,
                    now.Hour,
                    now.Minute,
                    now.Second,
                    now.Millisecond);
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(filename);
                    var errorString = string.Format("{0}\n\n{1} was thrown at {2}\n{3}\n\nStack Trace\n{4}", filename,
                        e.GetType(), e.Message, e.TargetSite, e.StackTrace);
                    writer.WriteLine(errorString);
                }
                finally
                {
                    if (writer != null) writer.Close();
                }
            }
        }

        public static bool IsInteger(string theValue)
        {
            var isNumber = new Regex(@"^\d+$");
            var m = isNumber.Match(theValue);
            return m.Success;
        }
    }
}