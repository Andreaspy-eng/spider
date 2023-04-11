using System.Collections.Generic;

namespace spider.YandexApi.Result
{
    public class RawMetric
    {
        public string name { get; set; }
        public double value { get; set; }
        public List<RawMetric> raw_metrics { get; set; }
    }
}