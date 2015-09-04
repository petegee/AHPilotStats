using System;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Serialization;

namespace My2Cents.HTC.AHPilotStats.DomainObjects
{
    [Serializable()]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class UpdateNotification
    {
        public string UpdatedVersion { get; set; }
        
        public DateTime ReleaseDate { get; set; }

        public string NewVersionURL { get; set; }
    }
}
