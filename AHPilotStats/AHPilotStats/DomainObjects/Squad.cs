using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Squad
    {
        #region Nested Members Class

        [Serializable()]
        public class SquadMember
        {
            public SquadMember() { }

            private string _pilotName = "";
            private int _startTour;
            private int _endTour=999;

            public string PilotName
            {
                get { return _pilotName; }
                set { _pilotName = value.TrimEnd().TrimStart(); }
            }

            public int StartTour
            {
                get { return _startTour;  }
                set { _startTour = value; }
            }

            public int EndTour
            {
                get { return _endTour; }
                set { _endTour = value; }
            }
            
        }

        #endregion


        #region Fields
        private string _squadName;
        private string _homePage;
        public BindingList<SquadMember> Members = new BindingList<SquadMember>();
        private const string _squadFilePrototype = @".\squads\{0}_SquadDef.xml";


        #endregion

        #region Properties
        public string SquadName
        {
            get { return _squadName;  }
            set { _squadName = value; }
        }

        public string HomePage
        {
            get { return _homePage; }
            set { _homePage = value; }
        }
        #endregion

        public Squad() 
        { 
        }


        public void AddMember(string pilot)
        {
            SquadMember squadMember = new SquadMember();
            squadMember.PilotName = pilot;
            Members.Add(squadMember);
        }


        public int GetMinTour(Squad squad)
        {
            int startTour = 999;
            foreach (SquadMember squadMember in this.Members)
            {
                // capture the min
                if (squadMember.StartTour < startTour)
                    startTour = squadMember.StartTour;
            }
            return startTour;
        }


        public int GetMaxTour(Squad squad)
        {
            int endTourByData = 0;
            int endTourBySqaudDefinition = 0;
            foreach (SquadMember squadMember in this.Members)
            {
                if (Registry.Instance.PilotStatsContains(squadMember.PilotName))
                {
                    Registry.PilotStatsRegistry pilotStats = Registry.Instance.GetPilotStats(squadMember.PilotName);
                    foreach (StatsDomainObject obj in pilotStats.FighterStatsList)
                    {
                        int thisTourNumber = System.Convert.ToInt32(obj.TourNumber);
                        if (thisTourNumber > endTourByData)
                            endTourByData = thisTourNumber;
                    }
                }
            }

            foreach (SquadMember squadMember in this.Members)
            {
                // capture the min
                if (squadMember.EndTour > endTourBySqaudDefinition)
                    endTourBySqaudDefinition = squadMember.EndTour;
            }

            // if pilot has data which continues past their membership in a sqaud definition
            // ensure we return their sqaud defined end tour.
            if (endTourBySqaudDefinition < endTourByData)
                return endTourBySqaudDefinition;
            else
                return endTourByData;
        }

        /// <summary>
        /// Determine if the pilot was in the sqaud for the nominated tour
        /// </summary>
        /// <param name="tourNumber">the tour in question</param>
        /// <param name="squadMember">the sqaud member details.</param>
        /// <returns>true if pilot was in squad for that tour, false otherwise.</returns>
        public bool WasPilotInSquadForThisTour(int tourNumber, Squad.SquadMember squadMember)
        {
            return (tourNumber >= squadMember.StartTour && tourNumber <= squadMember.EndTour);
        }

        public int CountMembersForTour(int tour)
        {
            // count the pilots for this tour.
            int numPilots = 0;
            foreach (Squad.SquadMember squadMember in Members)
            {
                if (WasPilotInSquadForThisTour(tour, squadMember))
                    numPilots++;
            }

            return numPilots;
        }

        public void Serialise()
        {
            new XMLObjectSerialiser<Squad>().WriteXMLFile(this, string.Format(_squadFilePrototype, SafeFileName(this.SquadName)));
        }

        public void CheckSquadInSync(int startTour, int endTour)
        {
            string errorMessage = "";

            for (int tour = startTour; tour <= endTour; tour++)
            {
                foreach (Squad.SquadMember member in Members)
                {
                    if (WasPilotInSquadForThisTour(tour, member))
                    {
                        bool scoresFound = false;
                        bool statsFound = false;

                        Registry.PilotStatsRegistry pilotReg = null;
                        try
                        {
                            pilotReg = Registry.Instance.GetPilotStats(member.PilotName);
                        }
                        catch (PilotDoesNotExistInRegistryException notExistEx)
                        {
                            errorMessage += notExistEx.Text;
                            goto end;
                        }
                        catch (ApplicationException appEx)
                        {
                            errorMessage += appEx.Message;
                            continue;
                        }

                        // do we have the required scores data for this pilot for this tour?
                        foreach (FighterScoresDO score in pilotReg.FighterScoresList)
                        {
                            if (score.TourNumber == tour)
                            {
                                // found :)
                                scoresFound = true;
                                break;
                            }
                        }

                        // do we have the required stats data for this pilot for this tour?
                        foreach (StatsDomainObject stats in pilotReg.FighterStatsList)
                        {
                            if (stats.TourNumber == tour.ToString())
                            {
                                // found :)
                                statsFound = true;
                                break;
                            }
                        }

                        // if we have one type and not the other type, then something is wrong.
                        if ( (scoresFound && !statsFound)  ||
                             (statsFound  && !scoresFound)
                           )
                        {
                            errorMessage += string.Format("\nCannot find all the data for squad member {0} for tour {1}", member.PilotName, tour);
                        }
                    }
                }
            }

            end:

            if(errorMessage != "")
                throw new SquadOutOfSyncException(errorMessage);
        }


        #region Static Methods

        public static Squad LoadSquad(string squadFileName)
        {
            Squad squad = new Squad();

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(string.Format(_squadFilePrototype, SafeFileName(squadFileName)));
                XmlSerializer xSerializer = new XmlSerializer(typeof(Squad));
                squad = (Squad)xSerializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to Find Squad Definition File!");
            }
            finally
            {
                if(reader != null)
                    reader.Close();
            }

            return squad;
        }


        public static string SafeFileName(string unsafeFileName)
        {
            string safeName = "";
            char[] unsafeFileNameChars = {'\\','/',':','*','?','"','<','>','|',};
            foreach (char c in unsafeFileName)
            {
                bool isSafe = true;
                foreach(char unSafeChar in unsafeFileNameChars)
                {
                    if(c == unSafeChar)
                        isSafe = false;
                }

                if (isSafe)
                {
                    safeName = safeName + c;
                }
            }

            return safeName;
        }
        
        #endregion
    }
}
