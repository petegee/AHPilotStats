using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;

namespace My2Cents.HTC.AHPilotStats
{
    public class XMLObjectSerialiser<T>
    {
        public XMLObjectSerialiser() { }

        public void WriteXMLFile(T obj, string fileName)
        {
            TextWriter writer = new StreamWriter(fileName);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, obj);
            }
            finally
            {
                writer.Close();
            }
        }
    }
}
