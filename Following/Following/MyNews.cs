using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


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
    public class MyNews
    {
        public string AgencyName { get; set; }
        List<NewsProp> newslist = new List<NewsProp>();
        public string language { get; set; }


        public MyNews(string name, string language = "arm")
        {
            AgencyName = name;
            this.language = language;
        }

        void NewsListCreator()
        {
            string s1 = "</span> &nbsp; <small>Դիտվել է (.*?) անգամ</small><br>",
             s2 = "align=\"absmiddle\"></a>.*?<span class=\"time\">(.*?)  </span> &nbsp; <small>Դիտվել է .*? անգամ</small><br>",
             s3 = "</span> &nbsp; <small>Դիտվել է.*?<a href=\"(.*?)\" >(.*?)</a>";

            string s = Server.SendGetRequest("http://blognews.am/" + $"{this.language.Substring(0, 3).ToLower()}");
            
            if (this.language.Substring(0, 3).ToLower() == "rus")
            {
                s1 = "</span> &nbsp; <small>Просмотрен (.*?)  раз</small><br>";
                s2 = "align=\"absmiddle\"></a>.*?<span class=\"time\">(.*?)  </span> &nbsp; <small>Просмотрен .*? раз</small><br>";
                s3 = "</span> &nbsp; <small>Просмотрен.*?<a href=\"(.*?)\" >(.*?)</a>";
            }

            MatchCollection viewes = Regex.Matches(s, s1, RegexOptions.Singleline);
            foreach (Match x in viewes)
            {
                GroupCollection Group = x.Groups;
                var m = new NewsProp { Views = int.Parse(Group[1].Value.Trim()) };
                newslist.Add(m);
            }

            string[] strarr;
            int hour, minute;
            MatchCollection times = Regex.Matches(s, s2, RegexOptions.Singleline);
            int i = 0;
            foreach (Match x in times)
            {
                GroupCollection Group = x.Groups;
                strarr = Group[1].Value.Split(':');
                hour = int.Parse(strarr[0]);
                minute = int.Parse(strarr[1]);
                if ((hour < DateTime.Now.Hour) || (hour == DateTime.Now.Hour && minute < DateTime.Now.Minute))
                    newslist[i].Date = Group[1].Value.Trim() + "  " + DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year;
                else
                    newslist[i].Date = Group[1].Value.Trim() + "  " + DateTime.Now.AddDays(-1).Day + "." + DateTime.Now.AddDays(-1).Month + "." + DateTime.Now.AddDays(-1).Year;
                i++;
            }
            i = 0;
            MatchCollection titleslinks = Regex.Matches(s, s3, RegexOptions.Singleline);
            foreach (Match x in titleslinks)
            {
                if (i < newslist.Count)
                {
                    GroupCollection Group = x.Groups;
                    newslist[i].Link = "http://blognews.am" + Group[1].Value.Trim();
                    newslist[i].Title = Group[2].Value.Trim();
                    i++;
                }
                else
                    break;
            }


        }


        public event EventHandler<NewsProp> DailyNews;

        public void BroadcastNews()
        {
            NewsListCreator();
            foreach (var item in newslist)
            {
                DailyNews?.Invoke(this, item);
            }

        }

    }
}
