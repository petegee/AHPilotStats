using System;

namespace My2Cents.HTC.AHPilotStats
{
    [AttributeUsage(AttributeTargets.Property)]
    public class GridHelperAttribute : Attribute
    {
        public GridHelperAttribute(string friendlyName)
        {
            FriendlyName = friendlyName;
            HideColumn = false;
        }

        public GridHelperAttribute(bool hideColumn)
        {
            FriendlyName = "x";
            HideColumn = hideColumn;
        }

        public bool HideColumn { get; set; }

        public string FriendlyName { get; set; }
    }
}
