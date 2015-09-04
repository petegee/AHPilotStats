using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using My2Cents.HTC.PilotScoreSvc;
using System.Text.RegularExpressions;
using My2Cents.HTC.PilotScoreSvc.Types;
using System.Data;
using System.Reflection;
using My2Cents.HTC.AHPilotStats.DomainObjects;

namespace My2Cents.HTC.AHPilotStats
{
    class Utility
    {
        internal static readonly object syncRoot = new object();

        static public DataTable CreateDataTableFromList<T>(List<T> list)
        {
            DataTable table = new DataTable();

            Type t = typeof(T);
            PropertyInfo[] pis = t.GetProperties();
            for (int i = 0; i < pis.Length; i++)
            {
                PropertyInfo pi = (PropertyInfo)pis.GetValue(i);
                object[] customAttrs = pi.GetCustomAttributes(true);
                if (!((GridHelperAttribute)customAttrs[0]).HideColumn)
                {
                    string friendlyName = ((GridHelperAttribute)customAttrs[0]).FriendlyName;
                    table.Columns.Add(friendlyName, pi.PropertyType);

                }
            }

            object[] tableParams = new object[table.Columns.Count];
            foreach (T obj in list)
            {
                int itr = 0;
                for (int i = 0; i < pis.Length; i++)
                {
                    PropertyInfo pi = (PropertyInfo)pis.GetValue(i);
                    object[] customAttrs = pi.GetCustomAttributes(true);
                    if (!((GridHelperAttribute)customAttrs[0]).HideColumn)
                    {
                        tableParams[itr++] = t.GetProperty(pi.Name, pi.PropertyType).GetValue(obj, new object[] { });
                    }
                }
                table.Rows.Add(tableParams);
            }
            table.AcceptChanges();
            return table;
        }

        public static string GetFormattedTime(int timeInSeconds)
        {
            int remainingSeconds = 0;

            int wholeHours = timeInSeconds / 3600;
            remainingSeconds = timeInSeconds % 3600;

            int wholeMins = remainingSeconds / 60;
            remainingSeconds = remainingSeconds % 60;

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

            ps.VsEnemy = new AcesHighPilotScoreVsEnemy();

            // Attack
            ps.VsEnemy.Attack = new AcesHighPilotScoreVsEnemyAttack();
            ps.VsEnemy.Attack.HitPercentage = new AcesHighPilotScoreVsEnemyAttackHitPercentage();
            ps.VsEnemy.Attack.KillPerHour = new AcesHighPilotScoreVsEnemyAttackKillPerHour();
            ps.VsEnemy.Attack.KillPerSortie = new AcesHighPilotScoreVsEnemyAttackKillPerSortie();
            ps.VsEnemy.Attack.KillToDeathPlus1 = new AcesHighPilotScoreVsEnemyAttackKillToDeathPlus1();
            ps.VsEnemy.Attack.Points = new AcesHighPilotScoreVsEnemyAttackPoints();

            // Fighter
            ps.VsEnemy.Fighter = new AcesHighPilotScoreVsEnemyFighter();
            ps.VsEnemy.Fighter.HitPercentage = new AcesHighPilotScoreVsEnemyFighterHitPercentage();
            ps.VsEnemy.Fighter.KillPerHour = new AcesHighPilotScoreVsEnemyFighterKillPerHour();
            ps.VsEnemy.Fighter.KillPerSortie = new AcesHighPilotScoreVsEnemyFighterKillPerSortie();
            ps.VsEnemy.Fighter.KillToDeathPlus1 = new AcesHighPilotScoreVsEnemyFighterKillToDeathPlus1();
            ps.VsEnemy.Fighter.Points = new AcesHighPilotScoreVsEnemyFighterPoints();

            // Vehicles/Boat
            ps.VsEnemy.VehicleBoat = new AcesHighPilotScoreVsEnemyVehicleBoat();
            ps.VsEnemy.VehicleBoat.HitPercentage = new AcesHighPilotScoreVsEnemyVehicleBoatHitPercentage();
            ps.VsEnemy.VehicleBoat.KillPerHour = new AcesHighPilotScoreVsEnemyVehicleBoatKillPerHour();
            ps.VsEnemy.VehicleBoat.KillPerSortie = new AcesHighPilotScoreVsEnemyVehicleBoatKillPerSortie();
            ps.VsEnemy.VehicleBoat.KillToDeathPlus1 = new AcesHighPilotScoreVsEnemyVehicleBoatKillToDeathPlus1();
            ps.VsEnemy.VehicleBoat.Points = new AcesHighPilotScoreVsEnemyVehicleBoatPoints();



            ////////////////////////////////////////////////////////////////////////////////////
            // Vs Objects
            ////////////////////////////////////////////////////////////////////////////////////

            ps.VsObjects = new AcesHighPilotScoreVsObjects();

            // Attack
            ps.VsObjects.Attack = new AcesHighPilotScoreVsObjectsAttack();
            ps.VsObjects.Attack.DamagePerDeathPlus1 = new AcesHighPilotScoreVsObjectsAttackDamagePerDeathPlus1();
            ps.VsObjects.Attack.DamagePerSortie = new AcesHighPilotScoreVsObjectsAttackDamagePerSortie();
            ps.VsObjects.Attack.FieldCaptures = new AcesHighPilotScoreVsObjectsAttackFieldCaptures();
            ps.VsObjects.Attack.HitPercentage = new AcesHighPilotScoreVsObjectsAttackHitPercentage();
            ps.VsObjects.Attack.Points = new AcesHighPilotScoreVsObjectsAttackPoints();

            // Bomber
            ps.VsObjects.Bomber = new AcesHighPilotScoreVsObjectsBomber();
            ps.VsObjects.Bomber.DamagePerDeathPlus1 = new AcesHighPilotScoreVsObjectsBomberDamagePerDeathPlus1();
            ps.VsObjects.Bomber.DamagePerSortie = new AcesHighPilotScoreVsObjectsBomberDamagePerSortie();
            ps.VsObjects.Bomber.FieldCaptures = new AcesHighPilotScoreVsObjectsBomberFieldCaptures();
            ps.VsObjects.Bomber.HitPercentage = new AcesHighPilotScoreVsObjectsBomberHitPercentage();
            ps.VsObjects.Bomber.Points = new AcesHighPilotScoreVsObjectsBomberPoints();

            // Vehicle/Boat
            ps.VsObjects.VehicleBoat = new AcesHighPilotScoreVsObjectsVehicleBoat();
            ps.VsObjects.VehicleBoat.DamagePerDeathPlus1 = new AcesHighPilotScoreVsObjectsVehicleBoatDamagePerDeathPlus1();
            ps.VsObjects.VehicleBoat.DamagePerSortie = new AcesHighPilotScoreVsObjectsVehicleBoatDamagePerSortie();
            ps.VsObjects.VehicleBoat.FieldCaptures = new AcesHighPilotScoreVsObjectsVehicleBoatFieldCaptures();
            ps.VsObjects.VehicleBoat.HitPercentage = new AcesHighPilotScoreVsObjectsVehicleBoatHitPercentage();
            ps.VsObjects.VehicleBoat.Points = new AcesHighPilotScoreVsObjectsVehicleBoatPoints();




            ////////////////////////////////////////////////////////////////////////////////////
            // Overall / Raw Stats
            ////////////////////////////////////////////////////////////////////////////////////

            ps.Overall = new AcesHighPilotScoreOverall();

            ps.Overall.Attack = new AcesHighPilotScoreOverallAttack();
            ps.Overall.Bomber = new AcesHighPilotScoreOverallBomber();
            ps.Overall.Fighter = new AcesHighPilotScoreOverallFighter();
            ps.Overall.VehicleBoat = new AcesHighPilotScoreOverallVehicleBoat();
            ps.Overall.Total = new AcesHighPilotScoreOverallTotal();
        }


        internal static void ConstructAcesHighPilotStatsInnerObjects(ref AcesHighPilotStats pilotStats, int objScoreLen)
        {
            pilotStats.VsCountry = new AcesHighPilotStatsVsCountry();
            pilotStats.VsCountry.CountryScore = new CountryScore[3];

            pilotStats.VsObjects = new AcesHighPilotStatsVsObjects();
            pilotStats.VsObjects.ObjectScore = new ObjectScore[objScoreLen];
        }


        internal static void WriteDebugTraceFile(Exception e)
        {
            lock (Utility.syncRoot)
            {
                DateTime now = DateTime.Now;
                string filename = string.Format("AHPilotStatsError_{0}{1}{2}_{3}-{4}-{5}-{6}.dump",
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
                    string errorString = string.Format("{0}\n\n{1} was thrown at {2}\n{3}\n\nStack Trace\n{4}", filename, e.GetType().ToString(), e.Message, e.TargetSite, e.StackTrace);
                    writer.WriteLine(errorString);
                }
                finally
                {
                    writer.Close();
                }
            }
        }

        public static bool IsInteger(string theValue)
        {
            Regex _isNumber = new Regex(@"^\d+$");
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }
    }
}
