using System;
using System.Collections.Generic;
using System.Text;
using Sgml;
using System.Xml;
using System.Net;
using System.IO;
using My2Cents.HTC.PilotScoreSvc.Types;

namespace My2Cents.HTC.PilotScoreSvc.Utilities
{
    class HttpToXMLLoader
    {
        ProxySettingsDTO _proxySettings = null;
        public HttpToXMLLoader(ProxySettingsDTO proxySettings) 
        {
            _proxySettings = proxySettings;
        }

        public XmlDocument LoadHtmlPageAsXMLByGet(string uri)
        {
            return LoadHtmlPageAsXMLInternal(String.Empty, uri, "GET");
        }

        public XmlDocument LoadHtmlPageAsXMLByPost(string uri, string postData)
        {
            return LoadHtmlPageAsXMLInternal(postData, uri, "POST");
        }

        private XmlDocument LoadHtmlPageAsXMLInternal(string postData, string uri, string httpMethod)
        {
            // Prepare web request...
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);

            // Deal with proxy details if any.
            WebProxy proxy = null;
            if (_proxySettings.Option == ProxySettingsDTO.ProxyOption.UseIESettings)
            {
                throw new NotSupportedException("IE proxy settings are not supported by this module!");
            }
            if (_proxySettings.Option == ProxySettingsDTO.ProxyOption.Custom)
            {
                proxy = new WebProxy(_proxySettings.ProxyHost, _proxySettings.ProxyPort);
                webrequest.Proxy = proxy;
            }
            webrequest.Method = httpMethod;

            if(String.Equals(httpMethod, "POST", StringComparison.OrdinalIgnoreCase))
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] data = encoding.GetBytes(postData);

                webrequest.ContentType = "application/x-www-form-urlencoded";
                webrequest.ContentLength = data.Length;

                using (Stream newStream = webrequest.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                }
            }


            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding(1252);
            StreamReader loResponseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string Buffer = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            webresponse.Close();

            StringReader stringReader = new StringReader(Buffer);

            // Use the cool sgml reader to 'interpret' the HTML as XML :) very nice!
            SgmlReader sgmlReader = new SgmlReader();
            sgmlReader.DocType = "HTML";
            sgmlReader.InputStream = stringReader;
            XmlDocument doc = new XmlDocument();
            doc.Load(sgmlReader);

            return doc;
        }
    }
}
