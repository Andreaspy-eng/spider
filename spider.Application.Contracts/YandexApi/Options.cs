using System;
using System.Collections.Generic;
using System.Text;

namespace spider.YandexApi
{
    public class Options
    {
        public bool absolute_time { get; set; }
        public string minimize { get; set; }
        public bool merge_multuorders { get; set; }
        public bool minimize_lateness_risk { get; set; }
        public bool avoid_tolls { get; set; }
        public List<Groups> balanced_groups { get; set; }
        public int time_zone { get; set; }
        public string quality { get; set; }
        public string data { get; set; }
        public string routing_mode { get; set; }
    }

    public class Groups
    {
        public string id { get; set; }
        public PenaltyGroups penalty { get; set; } 

    }

    public class PenaltyGroups
    {
        public int hour { get; set; }
        public int stop { get; set; }
    }
}
