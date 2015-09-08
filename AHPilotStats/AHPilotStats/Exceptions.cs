
namespace My2Cents.HTC.AHPilotStats
{
    public class SquadOutOfSyncException : System.ApplicationException
    {
        public SquadOutOfSyncException(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }

    public class PilotDoesNotExistInRegistryException : System.ApplicationException
    { 
        public PilotDoesNotExistInRegistryException(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }

    public class SquadDoesNotExistInRegistryException : System.ApplicationException
    {
        public SquadDoesNotExistInRegistryException(string text)
        {
            Text = text;
        }

        public string Text { get; set; }
    }
}
