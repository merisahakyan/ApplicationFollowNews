using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Following
{
    public class NewsProp:EventArgs
    {   
        public string Title { get; set; }
        public string Date { get; set; }
        public int Views { get; set; }
        public string Link { get; set; }
        public override string ToString()
        {
            return $"--{Title}\n--{Date}\n--{Views}\n--{Link}";
        }

    }
}
