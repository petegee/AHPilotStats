using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;

namespace My2Cents.HTC.PilotScoreSvc.Types
{
    public class TourNode
    {
        private int _tourId;
        private string _tourStartDate;
        private string _tourEndDate;
        private string _tourType;
        private string _tourSelectArg;

        public TourNode(XmlNode node)
        {
            _tourId = int.Parse(node.SelectSingleNode("TourNumber").InnerXml);
            _tourStartDate = node.SelectSingleNode("TourStartDate").InnerXml;
            _tourEndDate = node.SelectSingleNode("TourEndDate").InnerXml;
            _tourType = node.SelectSingleNode("TourType").InnerXml;
            _tourSelectArg = node.SelectSingleNode("TourSelectArg").InnerXml;
        }

        public int TourId
        {
            get { return _tourId; }
        }

        public string TourType
        {
            get { return _tourType; }
        }

        public DateTime TourStartDate
        {
            get 
            { 
                return ParseDate(_tourStartDate); 
            }
        }

        public DateTime TourEndDate
        {
            get 
            {
                return ParseDate(_tourEndDate); 
            }
        }

        public string TourSelectArg
        {
            get
            {
                return _tourSelectArg; 
            }
        }


        private static DateTime ParseDate(string dateToParse)
        {
            DateTime result;
            bool success = false;
            success = DateTime.TryParseExact(dateToParse, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result);

            if (!success)
                success = DateTime.TryParseExact(dateToParse, "MM-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result);

            if (!success)
                success = DateTime.TryParseExact(dateToParse, "M-dd-yy", CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out result);

            if (!success)
                throw new System.FormatException(string.Format("Failed to convert date {0}", dateToParse));

            return result;
        }
    }
}
