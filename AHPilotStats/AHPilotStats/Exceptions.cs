using System;
using System.Collections.Generic;
using System.Text;

namespace My2Cents.HTC.AHPilotStats
{
    public class SquadOutOfSyncException : System.ApplicationException
    {
        private string _text;

        public SquadOutOfSyncException(string text)
        { 
            _text = text;
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }

    public class PilotDoesNotExistInRegistryException : System.ApplicationException
    { 
        private string _text;

        public PilotDoesNotExistInRegistryException(string text)
        { 
            _text = text;
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        } 
    }

    public class SquadDoesNotExistInRegistryException : System.ApplicationException
    {
        private string _text;

        public SquadDoesNotExistInRegistryException(string text)
        {
            _text = text;
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
