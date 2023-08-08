using System;
using System.Collections.Generic;
using System.Text;

namespace spider.AdvantageModels
{
    public class Car
    {
            public string imei { get; set; }
            public string number { get; set; }
            public string model { get; set; }
            public double maxWeight { get; set; }
            public double maxPalletCount { get; set; }
            public double maxPackageCount { get; set; }
            public int minStops {get;set;}
            public int maxStops {get;set;}

    }
}
