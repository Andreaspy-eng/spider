namespace spider.YandexApi.Result
{
    public class Routes
    {
        public Metrics metrics { get; set; }
        public List<Route> route { get; set; }
        public int run_number { get; set; }
        public Shift? shift { get; set; }
        public string vehicle_id { get; set; }
    }
}
