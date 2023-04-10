using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace spider.YandexApi
{
    public class QueryCreateRoteList
    {
        public Depot depot { get; set; }
        public List<Vehicles> vehicles { get; set; }
        public List<Locations> locations { get; set; }
        public List<Options> options { get; set; }

        public static QueryCreateRoteList test() { return default; }
    }
}
