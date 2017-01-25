using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Following;

namespace BlogNews
{
    class Program
    {
        static void Main(string[] args)
        {
            MyNews mn = new MyNews("BlogNews");
            mn.DailyNews += ShowNews;
            mn.BroadcastNews();

        }
        public static void ShowNews(object sender,EventArgs e)
        {
            var agency = (MyNews)sender;
            Console.WriteLine(agency.AgencyName);
            if (agency != null)
                Console.WriteLine($":   {e.ToString()}");
        }
    }
}
