using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Following;
using System.IO;

namespace BlogNews
{
    class Program
    {
        static void Main(string[] args)
        {
            MyNews mn = new MyNews("BlogNews","russian");
            mn.DailyNews += ShowNews;
            mn.BroadcastNews();

        }
        public static void ShowNews(object sender, EventArgs e)
        {
            var agency = (MyNews)sender;
            if (agency != null)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"/BlogNews.txt";
                using (StreamWriter r = new StreamWriter(path,true))
                {
                    r.WriteLine(e.ToString());
                }
            }
        }
    }
}
