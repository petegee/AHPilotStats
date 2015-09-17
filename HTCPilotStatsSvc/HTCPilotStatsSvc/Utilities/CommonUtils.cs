using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.Utilities
{
    public static class CommonUtils
    {
        public static string BuildTourDetailsTag(this TourNode tour)
        {
            var shortStartDate = string.Format("{1}-{0}-{2}", tour.TourStartDate.Day, tour.TourStartDate.Month,
                tour.TourStartDate.Year);
            var shortEndDate = string.Format("{1}-{0}-{2}", tour.TourEndDate.Day, tour.TourEndDate.Month,
                tour.TourEndDate.Year);
            return string.Format("Tour {0}   {1} to {2}", tour.TourId, shortStartDate, shortEndDate);
        }


        public static T DeserialiseFromXmlDoc<T>(this XmlDocument doc)
        {
            var serilizer = new XmlSerializer(typeof(T));
            var enc = new UTF8Encoding();
            var byteArray = enc.GetBytes(doc.InnerXml);
            var memStream = new MemoryStream(byteArray);
            return (T)serilizer.Deserialize(memStream);
        }

        public static string ToUpperFirstChar(this string str)
        {
            var capatilisedFirstLetterString = str.Length > 0
                ? (
                    char.ToUpper(str[0]) +
                    (
                        str.Length > 1
                            ? str.Substring(1)
                            : ""
                        )
                    )
                : str;

            return capatilisedFirstLetterString;
        }
    }
}