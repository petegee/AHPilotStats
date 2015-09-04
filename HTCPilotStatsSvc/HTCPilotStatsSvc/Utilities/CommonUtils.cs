using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.Utilities
{
    class CommonUtils
    {    
        internal static string BuildTourDetailsTag(TourNode tour)
        {
            string shortStartDate = string.Format("{1}-{0}-{2}", tour.TourStartDate.Day, tour.TourStartDate.Month, tour.TourStartDate.Year);
            string shortEndDate = string.Format("{1}-{0}-{2}", tour.TourEndDate.Day, tour.TourEndDate.Month, tour.TourEndDate.Year);
            return string.Format("Tour {0}   {1} to {2}", tour.TourId, shortStartDate, shortEndDate);
        }


        internal object DeserialiseFromXmlDoc(Type type, XmlDocument doc)
        {
            XmlSerializer serilizer = new XmlSerializer(type);
            UTF8Encoding enc = new UTF8Encoding();
            byte[] byteArray = enc.GetBytes(doc.InnerXml);
            MemoryStream memStream = new MemoryStream(byteArray);
            return serilizer.Deserialize(memStream);
        }

        internal static string ToUpperFirstChar(string str)
        {
            string capd1stLetter = str.Length > 0 ?
            (
                char.ToUpper(str[0]) +
                (
                    str.Length > 1 ? str.Substring(1)
                    : ""
                )
            )
            : str;

            return capd1stLetter;
        }
    }


}
