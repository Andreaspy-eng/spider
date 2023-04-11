using System.Collections.Generic;

namespace spider.YandexApi.Result
{

    public class DetailedCostMetric
    {
        public string name { get; set; }
        public List<Subcost> subcosts { get; set; }
        public double value { get; set; }
    }

}