using My2Cents.HTC.PilotScoreSvc.Types;
using System.Xml;

namespace My2Cents.HTC.PilotScoreSvc.Utilities
{
    public interface IHtmlToXMLLoader
    {
        XmlDocument LoadHtmlPageAsXmlByGet(string uri, ProxySettingsDTO proxySettings);

        XmlDocument LoadHtmlPageAsXmlByPost(string uri, string postData, ProxySettingsDTO proxySettings);
    }
}
