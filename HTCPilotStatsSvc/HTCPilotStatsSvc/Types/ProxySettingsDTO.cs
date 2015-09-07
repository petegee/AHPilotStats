using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;


namespace My2Cents.HTC.PilotScoreSvc.Types
{
    [Serializable()]
    public class ProxySettingsDTO
    {
        public enum ProxyOption {Direct, Custom };

        public ProxySettingsDTO() 
        { 
        }

        private ProxyOption _option = ProxyOption.Direct;
        private string _proxyName = "";
        private int _proxyPort = 8080;

        public ProxyOption Option
        {
            get { return _option;  }
            set { _option = value; }
        }

        public string ProxyHost
        {
            get { return _proxyName; }
            set { _proxyName = value; }
        }

        public int ProxyPort
        {
            get { return _proxyPort; }
            set { _proxyPort = value; }
        }


        public static ProxySettingsDTO GetProxySettings()
        {
            ProxySettingsDTO proxySettings = new ProxySettingsDTO();
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(".\\netconxsettings.xml");
                XmlSerializer xSerializer = new XmlSerializer(typeof(ProxySettingsDTO));
                proxySettings = (ProxySettingsDTO)xSerializer.Deserialize(reader);
                reader.Close();
            }
            catch
            {
                if(reader != null)
                    reader.Close();

                TextWriter writer = new StreamWriter(".\\netconxsettings.xml");
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ProxySettingsDTO));
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
