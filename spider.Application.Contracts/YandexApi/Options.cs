using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace spider.YandexApi
{
    public class Options
    {
        public int time_zone { get; set; }
        public string quality { get; set; }
        [DefaultValue("truck")]
        public string routing_mode { get; set; }
        public string date { get; set; }
    }
}
