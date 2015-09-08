using System.IO;
using System.Xml.Serialization;

namespace My2Cents.HTC.AHPilotStats
{
    public class XMLObjectSerialiser<T>
    {
        public void WriteXmlFile(T obj, string fileName)
        {
            TextWriter writer = new StreamWriter(fileName);
            try
            {
                var serializer = new XmlSerializer(typeof (T));
                serializer.Serialize(writer, obj);
            }
            finally
            {
                writer.Close();
            }
        }
    }
}