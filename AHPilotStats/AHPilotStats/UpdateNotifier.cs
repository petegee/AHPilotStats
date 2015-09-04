using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using My2Cents.HTC.AHPilotStats.DomainObjects;
using My2Cents.HTC.PilotScoreSvc.Types;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;

namespace My2Cents.HTC.AHPilotStats
{
    internal class UpdateNotifier
    {
        internal static bool CheckForUpdates()
        {
            try
            {
                // Prepare web request...
                HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["updateNotificationURL"]);

                ProxySettingsDTO proxySettings = ProxySettingsDTO.GetProxySettings();

                // Deal with proxy details if any.
                WebProxy proxy = null;
                if (proxySettings.Option == ProxySettingsDTO.ProxyOption.UseIESettings)
                {
                    throw new NotSupportedException("IE proxy settings are not supported by this module!");
                }
                if (proxySettings.Option == ProxySettingsDTO.ProxyOption.Custom)
                {
                    proxy = new WebProxy(proxySettings.ProxyHost, proxySettings.ProxyPort);
                    webrequest.Proxy = proxy;
                }

                webrequest.Method = "GET";
                webrequest.ContentType = "text/xml";

                HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
                Encoding enc = System.Text.Encoding.GetEncoding(1252);
                StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
                string Buffer = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                webresponse.Close();

                UpdateNotification updateNotification;
                using (TextReader reader = new StringReader(Buffer))
                {
                    XmlSerializer xSerializer = new XmlSerializer(typeof(UpdateNotification));
                    updateNotification = (UpdateNotification)xSerializer.Deserialize(reader);
                    reader.Close();
                }

                if (IsVersionNewer(updateNotification.UpdatedVersion) && DateTime.Now > updateNotification.ReleaseDate)
                {
                    UpdateNotificationForm form = new UpdateNotificationForm(updateNotification);
                    form.ShowDialog();
                    if (form.UserWantsToClose)
                        return true;
                }
            }
            catch (Exception ex)
            {
                Utility.WriteDebugTraceFile(ex);
            }

            return false;
        }


        private static bool IsVersionNewer(string updatedVersion)
        {
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
            string runningVersion = string.Format("{0}.{1}.{2}", fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart);
            return String.Compare(runningVersion, updatedVersion, true) != 0;
        }
    }
}
