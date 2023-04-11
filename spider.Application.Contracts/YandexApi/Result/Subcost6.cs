namespace spider.YandexApi.Result
{

    public class Subcost6
    {
        public string name { get; set; }
        public double value { get; set; }
        public List<RawMetric> raw_metrics { get; set; }
        public List<Subcost> subcosts { get; set; }
    }
}