using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace spider.YandexApi
{
    public class Shipment
    {
        public int weight_kg { get; set; }
        public object units { get; set; }
        /*public Volume volume { get; set; }*/
    }

    public class Volume
    {
        public object width_m { get; set; }
        public object depth_m { get; set; }
        public object height_m { get; set; }
    }
}
