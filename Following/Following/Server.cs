using System;
using System.IO;
using System.Net;
using System.Text;

namespace Following
{
    public static class Server

    {
        public static string GetSourceCode(string url)
        {
            if (url == null)
                throw new ArgumentNullException();
            WebClient wc = new WebClient();
            var uri = new Uri(url);
            return wc.DownloadString(uri);
        }
        public static string SendGetRequest(string url)
        {
            try
            {
                ServicePointManager.DefaultConnectionLimit = 10;
                ServicePointManager.Expect100Continue = false;
                ServicePointManager.DnsRefreshTimeout = 1000;
                ServicePointManager.UseNagleAlgorithm = false;

                string response = "";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.57 Safari/537.36";
                request.Timeout = 300000;
                using (var stream = request.GetResponse().GetResponseStream())
                {
                    stream.ReadTimeout = 300000;
                    using (var streamReader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                    {
                        response = streamReader.ReadToEnd();
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return "";

        }
    }
}
