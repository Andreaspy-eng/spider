namespace spider.YandexApi.Result

{
    public class RouteResult
    {
        public List<DetailedCostMetric> detailed_cost_metrics { get; set; }
        public List<object> dropped_locations { get; set; }
        public Metrics metrics { get; set; }
        public Options options { get; set; }
        public List<Routes> routes { get; set; }
        public string solver_status { get; set; }
        public List<Vehicle> vehicles { get; set; }

    }
}