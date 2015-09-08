using System;
using System.IO;
using System.Xml.Serialization;

namespace My2Cents.HTC.PilotScoreSvc.Types
{
    [Serializable]
    public class ProxySettingsDTO
    {
        public enum ProxyOption
        {
            Direct,
            Custom
        };

        public ProxySettingsDTO()
        {
            Option = ProxyOption.Direct;
            ProxyHost = "";
            ProxyPort = 8080;
        }

        public ProxyOption Option { get; set; }

        public string ProxyHost { get; set; }

        public int ProxyPort { get; set; }


        public static ProxySettingsDTO GetProxySettings()
        {
            var proxySettings = new ProxySettingsDTO();
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(".\\netconxsettings.xml");
                var xSerializer = new XmlSerializer(typeof (ProxySettingsDTO));
                proxySettings = (ProxySettingsDTO) xSerializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                if (reader != null)
                    reader.Close();

                var writer = new StreamWriter(".\\netconxsettings.xml");
                try
                {
                    var serializer = new XmlSerializer(typeof (ProxySettingsDTO));
                    serializer.Serialize(writer, proxySettings);
                }
                finally
                {
                    writer.Close();
                }
            }

            return proxySettings;
        }
    }
}