using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using My2Cents.HTC.AHPilotStats.DataRepository;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Squad
    {
        public Squad()
        {
            Members = new BindingList<SquadMember>();
        }

        #region Nested Members Class

        [Serializable]
        public class SquadMember
        {
            public SquadMember()
            {
                EndTour = 999;
            }

            private string _pilotName = "";
            public string PilotName
            {
                get { return _pilotName; }
                set { _pilotName = value.TrimEnd().TrimStart(); }
            }

            public int StartTour { get; set; }

            public int EndTour { get; set; }
        }

        #endregion

        #region Fields

        private const string SquadFilePrototype = @".\squads\{0}_SquadDef.xml";

        #endregion

        #region Properties

        public BindingList<SquadMember> Members { get; set; }

        public string SquadName { get; set; }

        public string HomePage { get; set; }

        #endregion

        public void AddMember(string pilot)
        {
            Members.Add(new SquadMember {PilotName = pilot});
        }


        public int GetMinTour(Squad squad)
        {
            return Members
                .Select(squadMember => squadMember.StartTour)
                .Concat(new[] {999})
                .Min();
        }


        public int GetMaxTour(Squad squad)
        {
            var endTourByData = 0;
            foreach (var squadMember in Members)
            {
                if (!Registry.PilotStatsContains(squadMember.PilotName)) 
                    continue;

                endTourByData = 
                    Registry.GetPilotStats(squadMember.PilotName)
                        .FighterStatsList.Select(obj => Convert.ToInt32(obj.TourNumber))
                        .Concat(new[] {endTourByData})
                        .Max();
            }

            var endTourBySqaudDefinition = Members.Select(squadMember => squadMember.EndTour)
                .Concat(new[] {0})
                .Max();

            // if pilot has data which continues past their membership in a sqaud definition
            // ensure we return their sqaud defined end tour.
            return endTourBySqaudDefinition < endTourByData ? endTourBySqaudDefinition : endTourByData;
        }

        /// <summary>
        /// Determine if the pilot was in the sqaud for the nominated tour
        /// </summary>
        /// <param name="tourNumber">the tour in question</param>
        /// <param name="squadMember">the sqaud member details.</param>
        /// <returns>true if pilot was in squad for that tour, false otherwise.</returns>
        public bool WasPilotInSquadForThisTour(int tourNumber, SquadMember squadMember)
        {
            return (tourNumber >= squadMember.StartTour && tourNumber <= squadMember.EndTour);
        }

        public int CountMembersForTour(int tour)
        {
            // count the pilots for this tour.
            return Members.Count(squadMember => WasPilotInSquadForThisTour(tour, squadMember));
        }

        public void Serialise()
        {
            new XMLObjectSerialiser<Squad>().WriteXmlFile(this, string.Format(SquadFilePrototype, SafeFileName(SquadName)));
        }

        public void CheckSquadInSync(int startTour, int endTour)
        {
            var errorMessage = "";

            for (var tour = startTour; tour <= endTour; tour++)
            {
                foreach (var member in Members.Where(member => WasPilotInSquadForThisTour(tour, member)))
                {
                    PilotStats pilotReg;
                    try
                    {
                        pilotReg = Registry.GetPilotStats(member.PilotName);
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
                    var scoresFound = pilotReg.FighterScoresList.Any(score => score.TourNumber == tour);

                    // do we have the required stats data for this pilot for this tour?
                    var statsFound = pilotReg.FighterStatsList.Any(stats => stats.TourNumber == tour.ToString());

                    // if we have one type and not the other type, then something is wrong.
                    if ( (scoresFound && !statsFound)  ||
                         (statsFound  && !scoresFound))
                    {
                        errorMessage += string.Format("\nCannot find all the data for squad member {0} for tour {1}", member.PilotName, tour);
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
            var squad = new Squad();

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(string.Format(SquadFilePrototype, SafeFileName(squadFileName)));
                var xSerializer = new XmlSerializer(typeof(Squad));
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
            char[] unsafeFileNameChars = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };

            // strip out the above characters if in the string.
            return unsafeFileNameChars.Aggregate(unsafeFileName, (str, c) => str.Replace(c.ToString(), ""));
        }
        
        #endregion
    }
}
