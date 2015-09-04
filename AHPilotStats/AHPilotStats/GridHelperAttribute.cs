using System;
using System.Collections.Generic;
using System.Text;

namespace My2Cents.HTC.AHPilotStats
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GridHelperAttribute : System.Attribute
    {
        private string friendlyName = "x";
        private bool hideColumn = false;

        public GridHelperAttribute(string friendlyName)
        {
            this.friendlyName = friendlyName;
        }

        public GridHelperAttribute(bool hideColumn)
        {
            this.hideColumn = hideColumn;
        }

        public bool HideColumn
        {
            get { return hideColumn; }
            set { hideColumn = value; }                
        }

        public string FriendlyName
        {
            get { return friendlyName; }
            set { friendlyName = value; }
        }
    }
}
