using System;
using System.Globalization;
using System.Xml;

namespace My2Cents.HTC.PilotScoreSvc.Types
{
    public class TourNode
    {
        public TourNode(XmlNode node)
        {
            if(node == null)
                throw new ArgumentException("node cannot be null");

            TourId = int.Parse(node.SelectSingleNode("TourNumber").InnerXml);
            TourStartDate = ParseDate(node.SelectSingleNode("TourStartDate").InnerXml);
            TourEndDate = ParseDate(node.SelectSingleNode("TourEndDate").InnerXml);
            TourType = node.SelectSingleNode("TourType").InnerXml;
            TourSelectArg = node.SelectSingleNode("TourSelectArg").InnerXml;
        }

        public int TourId { get; private set; }

        public string TourType { get; private set; }

        public DateTime TourStartDate { get; private set; }

        public DateTime TourEndDate { get; private set; }

        public string TourSelectArg { get; private set; }


        private static DateTime ParseDate(string dateToParse)
        {
            DateTime result;
            var success = false;
            success = DateTime.TryParseExact(dateToParse, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                DateTimeStyles.AllowWhiteSpaces, out result);

            if (!success)
                success = DateTime.TryParseExact(dateToParse, "MM-dd-yy", CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out result);

            if (!success)
                success = DateTime.TryParseExact(dateToParse, "M-dd-yy", CultureInfo.InvariantCulture,
                    DateTimeStyles.AllowWhiteSpaces, out result);

            if (!success)
                throw new FormatException(string.Format("Failed to convert date {0}", dateToParse));

            return result;
        }
    }
}