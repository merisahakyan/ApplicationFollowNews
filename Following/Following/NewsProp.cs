using System;

namespace Following
{
    public class NewsProp
    {   
        public string Title { get; set; }
        public string Date { get; set; }
        public int Views { get; set; }
        public string Link { get; set; }
        public override string ToString()
        {
            return $"{Title}</br> {Environment.NewLine}{Date}</br>{Environment.NewLine}Viewed:{Views}</br>{Environment.NewLine}<a href=\"{Link}\">LinkTo</a></br>{Environment.NewLine}</br>";
        }

    }
}
