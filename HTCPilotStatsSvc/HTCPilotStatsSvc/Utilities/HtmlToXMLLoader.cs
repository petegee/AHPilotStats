using My2Cents.HTC.PilotScoreSvc.Types;
using Sgml;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace My2Cents.HTC.PilotScoreSvc.Utilities
{
    public class HtmlToXMLLoader : IHtmlToXMLLoader
    {
        public XmlDocument LoadHtmlPageAsXmlByGet(string uri, ProxySettingsDTO proxySettings)
        {
            return LoadHtmlPageAsXmlInternal(string.Empty, uri, "GET", proxySettings);
        }

        public XmlDocument LoadHtmlPageAsXmlByPost(string uri, string postData, ProxySettingsDTO proxySettings)
        {
            return LoadHtmlPageAsXmlInternal(postData, uri, "POST", proxySettings);
        }

        private XmlDocument LoadHtmlPageAsXmlInternal(string postData, string uri, string httpMethod, ProxySettingsDTO proxySettings)
        {
            // Prepare web request...
            var webrequest = (HttpWebRequest) WebRequest.Create(uri);

            // Deal with proxy details if any.
            if (proxySettings.Option == ProxySettingsDTO.ProxyOption.Custom)
            {
                var proxy = new WebProxy(proxySettings.ProxyHost, proxySettings.ProxyPort);
                webrequest.Proxy = proxy;
            }
            webrequest.Method = httpMethod;

            if (string.Equals(httpMethod, "POST", StringComparison.OrdinalIgnoreCase))
            {
                var encoding = new ASCIIEncoding();
                var data = encoding.GetBytes(postData);

                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = data.Length;

                using (var newStream = webrequest.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                }
            }


            var webresponse = (HttpWebResponse) webrequest.GetResponse();
            var enc = Encoding.GetEncoding(1252);
            var loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            var buffer = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            webresponse.Close();

            var stringReader = new StringReader(buffer);

            // Use the cool sgml reader to 'interpret' the HTML as XML :) very nice!
            var sgmlReader = new SgmlReader
            {
                DocType = "HTML",
                InputStream = stringReader
            };
            var doc = new XmlDocument();
            doc.Load(sgmlReader);

            return doc;
        }
    }
}